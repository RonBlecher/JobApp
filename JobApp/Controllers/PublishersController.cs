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

        [Authorize(Roles = "Publisher")]
        [HttpGet]
        public async Task<List<PublishedJobByCountry>> GetPublishedJobsByCountry()
        {
            Publisher publisher = (await GetPublisher());

            var jobCities = publisher.PostedJobs.SelectMany(job => job.JobCities).ToList();

            return jobCities.GroupBy(cityJob => cityJob.CityName).Select(key => new PublishedJobByCountry
            {
                City = key.Key,
                Count = key.Count()
            }).ToList();
        }

        private async Task<Publisher> GetPublisher()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim idClaim = claims.Where(claim => claim.Type == "Id").First();

            var publishers = await _context.Publisher.Include(p => p.PostedJobs).ThenInclude(i => i.JobCities).Where(pub => pub.ID.ToString().Equals(idClaim.Value)).ToListAsync();

            return publishers.First();
        }

        // GET: Publishers
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List()
        {
            var publishers = await _context.Publisher.Include(p => p.PostedJobs).ToListAsync();
            return View(publishers);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Search(string name, string email)
        {
            var publishers = await (
                from publisher in _context.Publisher
                where ((name != null) ? publisher.Name.ToLower().Contains(name.ToLower()) : true) &&
                      ((email != null) ? publisher.Email.ToLower().Contains(email.ToLower()) : true)
                select publisher
                ).Include(p => p.PostedJobs).ToListAsync();

            return PartialView(publishers);
        }

        public IActionResult Register()
        {
            var identity = (ClaimsIdentity)User.Identity;
            if (identity.IsAuthenticated)
            {
                Claim role = identity.Claims.Where(claim => claim.Type == ClaimTypes.Role).First();
                string controllerName = LayoutDecision.GetControllerName(role);
                return RedirectToAction("Index", controllerName);
            }
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

        public IActionResult Login()
        {
            var identity = (ClaimsIdentity)User.Identity;
            if (identity.IsAuthenticated)
            {
                Claim role = identity.Claims.Where(claim => claim.Type == ClaimTypes.Role).First();
                string controllerName = LayoutDecision.GetControllerName(role);
                return RedirectToAction("Index", controllerName);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            if (login == null)
            {
                return NotFound();
            }

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

            var publishers = _context.Publisher.Where(publisher => publisher.Email == login.Email && publisher.Password == login.Password).ToList();
            if (publishers != null && publishers.Count() == 1)
            {
                SignIn(publishers.First());
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("Email", "Wrong Email or Password");
            ModelState.AddModelError("Password", "Wrong Email or Password");
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

            var authProperites = new AuthenticationProperties { };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperites);
        }

        // GET: Publishers/Details/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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

            if ((role.Value == "Publisher" && idClaim.Value != id.ToString()) || role.Value == "Seeker")
            {
                return RedirectToAction("NoPermission", "Home");
            }

            var publisher = await _context.Publisher.Include(p => p.PostedJobs).FirstOrDefaultAsync(p => p.ID == id);
            if (publisher == null)
            {
                return RedirectToAction("Error", "Home");
            }
            PublisherEditModel publisherEdit = new PublisherEditModel
            {
                ID = publisher.ID,
                Name = publisher.Name,
                Email = publisher.Email,
                PhoneNum = publisher.PhoneNum
            };
            return View(publisherEdit);
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Email,PhoneNum,OldPassword,NewPassword")] PublisherEditModel publisherEdit)
        {
            if (id != publisherEdit.ID)
            {
                return NotFound();
            }

            if ((string.IsNullOrEmpty(publisherEdit.OldPassword) ^ string.IsNullOrEmpty(publisherEdit.NewPassword)) == true)
            {
                ModelState.AddModelError("OldPassword", "Error on changing password");
                ModelState.AddModelError("NewPassword", "Error on changing password");
                return View(publisherEdit);
            }

            if (_context.Publisher.Any(p => p.Email == publisherEdit.Email && p.ID != publisherEdit.ID))
            {
                ModelState.AddModelError("Email", "Email already exists in the system");
                return View(publisherEdit);
            }

            if (ModelState.IsValid)
            {
                Publisher publisher = await _context.Publisher.FindAsync(id);
                publisher.Name = publisherEdit.Name;
                publisher.Email = publisherEdit.Email;
                publisher.PhoneNum = publisherEdit.PhoneNum;
                if (publisher.Password == publisherEdit.OldPassword)
                {
                    publisher.Password = publisherEdit.NewPassword;
                }
                else if (!string.IsNullOrEmpty(publisherEdit.OldPassword))
                {
                    ModelState.AddModelError("OldPassword", "Incorrect password");
                    return View(publisherEdit);
                }

                var identity = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                Claim idClaim = claims.Where(claim => claim.Type == "Id").First();
                Claim role = claims.Where(claim => claim.Type == ClaimTypes.Role).First();

                try
                {
                    _context.Update(publisher);
                    await _context.SaveChangesAsync();

                    if (role.Value == "Publisher" && publisher.ID.ToString() == idClaim.Value)
                    {
                        UpdateIdentityClaim(publisher);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublisherExists(publisherEdit.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (role.Value == "Publisher")
                {
                    return RedirectToAction(nameof(Index));
                }
                if (role.Value == "Admin")
                {
                    return RedirectToAction(nameof(List));
                }
            }
            return View(publisherEdit);
        }

        private void UpdateIdentityClaim(Publisher publisher)
        {
            SignIn(publisher);
        }

        // GET: Publishers/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
