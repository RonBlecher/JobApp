using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobApp.Data;
using JobApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace JobApp.Controllers
{
    public class PublishersController : Controller
    {
        private readonly JobAppContext _context;

        public PublishersController(JobAppContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Publisher")]
        // GET: Publishers
        public async Task<IActionResult> Index()
        {
            var x = await _context.Publisher.Include(p => p.PostedJobs).ToListAsync();
            return View(x);
        }

        [Authorize(Roles = "Admin")]
        // GET: Publishers
        public async Task<IActionResult> List(string search)
        {
            var publishers = await _context.Publisher.Include(p => p.PostedJobs).ToListAsync();
            if (!string.IsNullOrEmpty(search))
            {
                publishers = publishers.Where(publisher => String.Compare(publisher.Name, search,
                    comparisonType: StringComparison.OrdinalIgnoreCase) == 0).ToList();
            }

            return View(publishers);
        }

        public async Task<IActionResult> Search(string name, string email)
        {
            var results = from publisher in _context.Publisher
                          where name != null ? publisher.Name.ToLower().Contains(name.ToLower()) : true ||
                              email != null ? publisher.Email.ToLower().Contains(email.ToLower()) : true
                          select publisher;
            var publishers = await results.Include(p => p.PostedJobs).ToListAsync();

            return View("List", publishers);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind("Name,Email,PhoneNum,Password")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                if (!EmailExists(publisher.Email))
                {
                    _context.Add(publisher);
                    await _context.SaveChangesAsync();
                    SignIn(publisher);
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Email", "Email already exists in the system");
            }
            return View(publisher);
        }
     
        public IActionResult Login(string email, string password)
        {
            var identity = (ClaimsIdentity)User.Identity;
            if (identity.IsAuthenticated)
            {
                IEnumerable<Claim> claims = identity.Claims;
                Claim role = claims.Where(claim => claim.Type == ClaimTypes.Role).First();
                if (role != null && role.Value == "Publisher")
                {
                    return RedirectToAction("Index");
                }
            }

            var publishers = _context.Publisher.Where(publisher => publisher.Email == email && publisher.Password == password).ToList();
            if (publishers != null && publishers.Count() == 1)
            {
                SignIn(publishers.First());
                return RedirectToAction("Index");
            }

            return View();
        }

        private async void SignIn(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.ID.ToString()),
                new Claim("Email", user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, "Publisher"),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperites = new AuthenticationProperties
            {
 
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperites);
        }

        // GET: Publishers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publisher
                .Include(p => p.PostedJobs)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // GET: Publishers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Email,PhoneNum,Password")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                if (!EmailExists(publisher.Email))
                {
                    _context.Add(publisher);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Email", "Email already exists in the system");
            }
            return View(publisher);
        }

        // GET: Publishers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim idClaim = claims.Where(claim => claim.Type == "Id").First();
            Claim role = claims.Where(claim => claim.Type == ClaimTypes.Role).First();

            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }

            if(idClaim.Value != id.ToString() && role.Value != "Admin")
            {
                return RedirectToAction("NoPermission", "Home");
            }

            var publisher = await _context.Publisher.Include(p => p.PostedJobs).FirstOrDefaultAsync(p => p.ID == id);
            if (publisher == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(publisher);
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Email,PhoneNum,Password")] Publisher publisher)
        {
            if (id != publisher.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publisher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublisherExists(publisher.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        // GET: Publishers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publisher = await _context.Publisher
                .FirstOrDefaultAsync(m => m.ID == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publisher = await _context.Publisher.FindAsync(id);
            _context.Publisher.Remove(publisher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublisherExists(int id)
        {
            return _context.Publisher.Any(p => p.ID == id);
        }

        private bool EmailExists(string email)
        {
            return _context.Publisher.Any(p => p.Email == email);
        }
    }
}
