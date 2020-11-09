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
    public class AdminsController : Controller
    {
        private readonly JobAppContext _context;

        public AdminsController(JobAppContext context)
        {
            _context = context;
        }

        // GET: Admins
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            ViewData["SeekersCount"] = await GetSeekersCount();
            ViewData["PublishersCount"] = await GetPublishersCount();
            ViewData["PublishedJobsCount"] = await GetPublishedJobsCount();
            ViewData["AppliedCvCount"] = await GetAppliedCvsCount();
            ViewData["TopPublishers"] = await GetTopPublishers();
            ViewData["TopSeekers"] = await GetTopSeekers();

            return View();
        }

        private async Task<int> GetSeekersCount()
        {
            var seekers = await _context.Seeker.ToListAsync();

            return seekers.Count;
        }

        private async Task<int> GetPublishersCount()
        {
            var publishers = await _context.Publisher.ToListAsync();

            return publishers.Count;
        }

        private async Task<int> GetPublishedJobsCount()
        {
            var publishers = await _context.Publisher.Include(p => p.PostedJobs).ToListAsync();
            int publishedJobs = 0;

            foreach (var pub in publishers) {
                publishedJobs += pub.PostedJobs.Count;
            }
            return publishedJobs;
        }

        private async Task<int> GetAppliedCvsCount()
        {
            var seekers = await _context.Seeker
               .Include(s => s.SeekerJobs).ThenInclude(sj => sj.Job)
               .Include(s => s.SeekerSkills).ThenInclude(sk => sk.Skill)
               .ToListAsync();
            int appliedCvs = 0;

            foreach (var seeker in seekers)
            {
                appliedCvs += seeker.SeekerJobs.Count;
            }

            return appliedCvs;
        }

        private async Task<List<Publisher>> GetTopPublishers()
        {
            var publishers = await _context.Publisher.Include(p => p.PostedJobs).ToListAsync();

            return publishers.OrderByDescending(x => x.PostedJobs.Count).Take(5).ToList();
        }

        private async Task<List<Seeker>> GetTopSeekers()
        {
            var seekers = await _context.Seeker
               .Include(s => s.SeekerJobs).ThenInclude(sj => sj.Job)
               .Include(s => s.SeekerSkills).ThenInclude(sk => sk.Skill)
               .ToListAsync();

            return seekers.OrderByDescending(x => x.SeekerJobs.Count).Take(5).ToList();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult PublisherIndex()
        {
            return RedirectToAction("List", "publishers");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List()
        {
            var x = await _context.Admin.ToListAsync();
            return View(x);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "home");
        }

        // GET: /<controller>/ 
        public IActionResult Login(string email, string password)
        {
            var identity = (ClaimsIdentity)User.Identity;
            if (identity.IsAuthenticated)
            {
                IEnumerable<Claim> claims = identity.Claims;
                Claim role = claims.Where(claim => claim.Type == ClaimTypes.Role).First();
                if (role != null && role.Value == "Admin")
                {
                    return RedirectToAction("Index");
                }
            }

            var admins = _context.Admin.Where(admin => admin.Email == email && admin.Password == password).ToList();
            if (admins != null && admins.Count() == 1)
            {
                SignIn(admins.First());
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
                new Claim(ClaimTypes.Role, "Admin"),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperites = new AuthenticationProperties() { };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperites);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Search(string name, string email)
        {
            var results = from admin in _context.Admin
                          where name != null ? admin.Name.ToLower().Contains(name.ToLower()) : true ||
                            email != null ? admin.Email.ToLower().Contains(email.ToLower()) : true
                          select admin;
            return View("List", await results.ToListAsync());
        }

        // GET: Admins/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin
                .FirstOrDefaultAsync(a => a.ID == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: Admins/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ID,Name,Email,PhoneNum,Password")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                if (!EmailExists(admin.Email))
                {
                    _context.Add(admin);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(List));
                }
                ModelState.AddModelError("Email", "Email already exists in the system");
            }
            return View(admin);
        }

        // GET: Admins/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            AdminEditModel adminEdit = new AdminEditModel
            {
                ID = admin.ID,
                Name = admin.Name,
                Email = admin.Email,
                PhoneNum = admin.PhoneNum
            };
            return View(adminEdit);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Email,PhoneNum,OldPassword,NewPassword")] AdminEditModel adminEdit)
        {
            if (id != adminEdit.ID)
            {
                return NotFound();
            }

            if ((string.IsNullOrEmpty(adminEdit.OldPassword) ^ string.IsNullOrEmpty(adminEdit.NewPassword)) == true)
            {
                ModelState.AddModelError("OldPassword", "Error on changing password");
                ModelState.AddModelError("NewPassword", "Error on changing password");
                return View(adminEdit);
            }

            if (_context.Admin.Any(a => a.Email == adminEdit.Email && a.ID != adminEdit.ID))
            {
                ModelState.AddModelError("Email", "Email already exists in the system");
                return View(adminEdit);
            }

            if (ModelState.IsValid)
            {
                Admin admin = await _context.Admin.FindAsync(id);

                if (admin == null)
                {
                    return NotFound();
                }

                admin.Name = adminEdit.Name;
                admin.Email = adminEdit.Email;
                admin.PhoneNum = adminEdit.PhoneNum;
                if (admin.Password == adminEdit.OldPassword)
                {
                    admin.Password = adminEdit.NewPassword;
                } 
                else if (!string.IsNullOrEmpty(adminEdit.OldPassword))
                {
                    ModelState.AddModelError("OldPassword", "Incorrect password");
                    return View(adminEdit);
                }

                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                    UpdateIdentityClaim(admin);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(adminEdit.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(List));
            }
            return View(adminEdit);
        }

        private void UpdateIdentityClaim(Admin admin)
        {
            SignIn(admin);
        }

        // GET: Admins/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim idClaim = claims.Where(claim => claim.Type == "Id").First();

            if (id.ToString() == idClaim.Value)
            {
                return RedirectToAction(nameof(Index));
            }

            var admin = await _context.Admin
                .FirstOrDefaultAsync(m => m.ID == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = await _context.Admin.FindAsync(id);
            _context.Admin.Remove(admin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(int id)
        {
            return _context.Admin.Any(a => a.ID == id);
        }

        private bool EmailExists(string email)
        {
            return _context.Admin.Any(a => a.Email == email);
        }
    }
}
