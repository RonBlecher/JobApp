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
using System.IO;
using Microsoft.AspNetCore.Http;
using ServiceReference1;
using Job = JobApp.Models.Job;
using System.Threading;

namespace JobApp.Controllers
{
    public class SeekersController : Controller
    {
        private readonly JobAppContext _context;
        private readonly long _fileSizeLimit = 5 * 1024 * 1024; // 5MB

        public SeekersController(JobAppContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Seeker")]
        // GET: Publishers
        public async Task<IActionResult> Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim idClaim = claims.Where(claim => claim.Type == "Id").First();

            var seekers = await _context.Seeker
                .Where(seeker => seeker.ID.ToString() == idClaim.Value)
                .Include(s => s.SeekerJobs).ThenInclude(sj => sj.Job)
                .Include(s => s.SeekerSkills).ThenInclude(sk => sk.Skill)
                .ToListAsync();

            ViewData["New Jobs"] = await FindNewJobs(seekers.First());

            JobWSSoapClient jobWSSoapClient = new JobWSSoapClient(JobWSSoapClient.EndpointConfiguration.JobWSSoap);
            var jobCategories = await jobWSSoapClient.GetJobCategoriesAsync();
            ViewBag.jobCategiries = jobCategories.GetJobCategoriesResult.Take(5);
            return View(seekers.First());
        }

        private async Task<int> FindNewJobs(Seeker currentSeeker)
        {
            List<Job> matchedJobs = await GetNewJobs(currentSeeker);
            return matchedJobs.Count();
        }

        [HttpGet]
        public async Task<List<JobMonthCount>> GetCvAppliedPerMonth()
        {
            Seeker seeker = await GetSeeker();

            return seeker.SeekerJobs.GroupBy(seekerJob => seekerJob.SubmitDate.Month).Select(key => new JobMonthCount
            {
                Month = key.Key,
                Count = key.Count()
            }).ToList();
        }

        [HttpGet]
        public async Task<List<JobMonthCount>> GetSeekerNewJobs()
        {
            List<Job> newJobs = await GetNewJobs(await GetSeeker());

            return newJobs.GroupBy(job => job.LastEdited.Month).Select(key => new JobMonthCount
            {
                Month = key.Key,
                Count = key.Count()
            }).ToList();
        }

        private async Task<List<Job>> GetNewJobs(Seeker currentSeeker)
        {
            List<string> seekSkills = new List<string>();
            await _context.SeekerSkill.Where(sk => sk.SeekerID == currentSeeker.ID).ForEachAsync(sk => seekSkills.Add(sk.SkillName));

            List<string> seekRegions = new List<string>();
            await _context.SeekerRegion.Where(sr => sr.SeekerID == currentSeeker.ID).ForEachAsync(sr => seekRegions.Add(sr.RegionName));

            List<Job> newJobs = await _context.Job
                .Include(j => j.Publisher)
                .Include(j => j.JobSeekers).ThenInclude(js => js.Seeker)
                .Include(j => j.JobSkills).ThenInclude(js => js.Skill)
                .Include(j => j.JobCities).ThenInclude(jc => jc.City)
                .Where(j => j.JobSeekers.Any(js => js.SeekerID == currentSeeker.ID && js.JobID == j.ID) == false)
                .Where(j => j.JobSkills.Any(js => seekSkills.Contains(js.SkillName)))
                .Where(j => j.JobCities.Any(jc => seekRegions.Contains(jc.City.RegionName)))
                .OrderByDescending(j => j.LastEdited)
                .ToListAsync();

            return newJobs;
        }

        private async Task<Seeker> GetSeeker()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim idClaim = claims.Where(claim => claim.Type == "Id").First();

            var seekers = await _context.Seeker
                .Where(seeker => seeker.ID.ToString() == idClaim.Value)
                .Include(s => s.SeekerJobs).ThenInclude(sj => sj.Job)
                .Include(s => s.SeekerSkills).ThenInclude(sk => sk.Skill)
                .ToListAsync();

            return seekers.First();
        }

        // GET: Seekers
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List(string search)
        {
            var seekers = await _context.Seeker
                .Include(s => s.SeekerJobs)
                .Include(s => s.SeekerSkills)
                .ToListAsync();
            if (!string.IsNullOrEmpty(search))
            {
                seekers = seekers.Where(seeker => String.Compare(seeker.Name, search,
                    comparisonType: StringComparison.OrdinalIgnoreCase) == 0).ToList();
            }

            return View(seekers);
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind("Name,Email,PhoneNum,Password")] Seeker seeker)
        {
            if (ModelState.IsValid)
            {
                if (!EmailExists(seeker.Email))
                {
                    _context.Add(seeker);
                    await _context.SaveChangesAsync();
                    SignIn(seeker);
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Email", "Email already exists in the system");
            }
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
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
                if (role != null && role.Value == "Seeker")
                {
                    return RedirectToAction("Index");
                }
            }

            var seekers = _context.Seeker.Where(seeker => seeker.Email == login.Email && seeker.Password == login.Password).ToList();
            if (seekers != null && seekers.Count() == 1)
            {
                SignIn(seekers.First());
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
                new Claim(ClaimTypes.Role, "Seeker"),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperites = new AuthenticationProperties { };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperites);
        }

        public async Task<IActionResult> DownloadCV(int? id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim idClaim = claims.Where(claim => claim.Type == "Id").First();
            Claim role = claims.Where(claim => claim.Type == ClaimTypes.Role).First();

            if (id == null)
            {
                return NotFound();
            }

            if (role.Value == "Seeker" && idClaim.Value != id.ToString())
            {
                return RedirectToAction("NoPermission", "Home");
            }

            var seeker = await _context.Seeker.FirstOrDefaultAsync(m => m.ID == id);
            if (seeker == null)
            {
                return NotFound();
            }

            MemoryStream cvStream = new MemoryStream();
            cvStream.Write(seeker.CV, 0, seeker.CV.Length);
            cvStream.Position = 0;
            return File(cvStream, System.Net.Mime.MediaTypeNames.Application.Octet, seeker.CVFileName);
        }

        // GET: Seekers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim idClaim = claims.Where(claim => claim.Type == "Id").First();
            Claim role = claims.Where(claim => claim.Type == ClaimTypes.Role).First();

            if (id == null)
            {
                return NotFound();
            }

            if (role.Value == "Seeker" && idClaim.Value != id.ToString())
            {
                return RedirectToAction("NoPermission", "Home");
            }

            var seeker = await _context.Seeker
                .Include(s => s.SeekerJobs)
                .Include(s => s.SeekerSkills)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (seeker == null)
            {
                return NotFound();
            }

            return View(seeker);
        }

        // GET: Seekers/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Seekers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("CVObj,ID,Name,Email,PhoneNum,Password")] Seeker seeker)
        {
            if (ModelState.IsValid)
            {
                if (!EmailExists(seeker.Email))
                {
                    if (seeker.CVObj != null)
                    {
                        if (seeker.CVObj.Length > _fileSizeLimit)
                        {
                            ModelState.AddModelError("CVObj", "File size is over 5MB");
                            return View(seeker);
                        }

                        using (MemoryStream ms = new MemoryStream())
                        {
                            seeker.CVObj.CopyTo(ms);
                            seeker.CV = ms.ToArray();
                            seeker.CVFileName = seeker.CVObj.FileName;
                        }
                    }

                    _context.Add(seeker);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(List));
                }
                ModelState.AddModelError("Email", "Email already exists in the system");
            }
            return View(seeker);
        }

        // GET: Seekers/Edit/5
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

            if (idClaim.Value != id.ToString() && role.Value != "Admin")
            {
                return RedirectToAction("NoPermission", "Home");
            }

            var seeker = await _context.Seeker.FindAsync(id);
            if (seeker == null)
            {
                return RedirectToAction("Error", "Home");
            }
            SeekerEditModel seekerEdit = new SeekerEditModel
            {
                ID = seeker.ID,
                Name = seeker.Name,
                Email = seeker.Email,
                PhoneNum = seeker.PhoneNum,
                CV = seeker.CV,
                CVFileName = seeker.CVFileName
            };
            return View(seekerEdit);
        }

        // POST: Seekers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CVObj,ID,Name,Email,PhoneNum,OldPassword,NewPassword")] SeekerEditModel seekerEdit)
        {
            if (id != seekerEdit.ID)
            {
                return NotFound();
            }

            if ((string.IsNullOrEmpty(seekerEdit.OldPassword) ^ string.IsNullOrEmpty(seekerEdit.NewPassword)) == true)
            {
                ModelState.AddModelError("OldPassword", "Error on changing password");
                ModelState.AddModelError("NewPassword", "Error on changing password");
                return View(seekerEdit);
            }

            if (_context.Seeker.Any(s => s.Email == seekerEdit.Email && s.ID != seekerEdit.ID))
            {
                ModelState.AddModelError("Email", "Email already exists in the system");
                return View(seekerEdit);
            }

            if (ModelState.IsValid)
            {
                Seeker seeker = await _context.Seeker.FindAsync(id);
                seeker.Name = seekerEdit.Name;
                seeker.Email = seekerEdit.Email;
                seeker.PhoneNum = seekerEdit.PhoneNum;
                if (seeker.Password == seekerEdit.OldPassword)
                {
                    seeker.Password = seekerEdit.NewPassword;
                }
                else if (!string.IsNullOrEmpty(seekerEdit.OldPassword))
                {
                    ModelState.AddModelError("OldPassword", "Incorrect password");
                    return View(seekerEdit);
                }

                if (seekerEdit.CVObj != null)
                {
                    if (seekerEdit.CVObj.Length > _fileSizeLimit)
                    {
                        ModelState.AddModelError("CVObj", "File size is over 5MB");
                        return View(seekerEdit);
                    }

                    using (MemoryStream ms = new MemoryStream())
                    {
                        seekerEdit.CVObj.CopyTo(ms);
                        // update SEEKER cv file, overcomes clicking cancel button bug in OpenFileDialog 
                        seeker.CV = ms.ToArray();
                        seeker.CVFileName = seekerEdit.CVObj.FileName;
                    }
                }

                var identity = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                Claim idClaim = claims.Where(claim => claim.Type == "Id").First();
                Claim role = claims.Where(claim => claim.Type == ClaimTypes.Role).First();

                try
                {
                    _context.Update(seeker);
                    await _context.SaveChangesAsync();

                    if (seeker.ID.ToString() == idClaim.Value)
                    {
                        UpdateIdentityClaim(seeker);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeekerExists(seekerEdit.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                if (role.Value == "Seeker")
                {
                    return RedirectToAction(nameof(Index));
                }
                if (role.Value == "Admin")
                {
                    return RedirectToAction(nameof(List));
                }
            }
            return View(seekerEdit);
        }

        private void UpdateIdentityClaim(Seeker seeker)
        {
            SignIn(seeker);
        }

        // GET
        [Authorize(Roles = "Seeker")]
        public async Task<IActionResult> MyRegions()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim idClaim = claims.Where(claim => claim.Type == "Id").First();
            int seekerID = Convert.ToInt32(idClaim.Value);

            var allRegions = await _context.Region.Select(r => new SeekerRegionCheckBoxItem()
            {
                RegionName = r.Name,
                IsChecked = r.RegionSeekers.Any(rs => rs.RegionName == r.Name && rs.SeekerID == seekerID)
            }).OrderBy(r => r.RegionName).ToListAsync();

            return View(new SeekerRegionViewModel { RegionCheckBoxItems = allRegions });
        }

        [HttpPost]
        [Authorize(Roles = "Seeker")]
        public async Task<IActionResult> MyRegions(SeekerRegionViewModel srvm)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim idClaim = claims.Where(claim => claim.Type == "Id").First();
            int seekerID = Convert.ToInt32(idClaim.Value);

            foreach (var checkBoxItem in srvm.RegionCheckBoxItems)
            {
                if (checkBoxItem.IsChecked)
                {
                    if (!SeekerRegionExists(seekerID, checkBoxItem.RegionName))
                    {
                        _context.SeekerRegion.Add(new SeekerRegion { SeekerID = seekerID, RegionName = checkBoxItem.RegionName });
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    if (SeekerRegionExists(seekerID, checkBoxItem.RegionName))
                    {
                        SeekerRegion sr = _context.SeekerRegion.First(sr => sr.SeekerID == seekerID && sr.RegionName == checkBoxItem.RegionName);
                        _context.SeekerRegion.Remove(sr);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET
        [Authorize(Roles = "Seeker")]
        public async Task<IActionResult> MySkills()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim idClaim = claims.Where(claim => claim.Type == "Id").First();
            int seekerID = Convert.ToInt32(idClaim.Value);

            var allSkills = await _context.Skill.Select(s => new SeekerSkillCheckBoxItem()
            {
                SkillName = s.Name,
                IsChecked = s.SkillSeekers.Any(sk => sk.SkillName == s.Name && sk.SeekerID == seekerID)
            }).OrderBy(s => s.SkillName).ToListAsync();

            return View(new SeekerSkillViewModel { SkillCheckBoxItems = allSkills });
        }

        [HttpPost]
        [Authorize(Roles = "Seeker")]
        public async Task<IActionResult> MySkills(SeekerSkillViewModel ssvm)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim idClaim = claims.Where(claim => claim.Type == "Id").First();
            int seekerID = Convert.ToInt32(idClaim.Value);

            foreach (var checkBoxItem in ssvm.SkillCheckBoxItems)
            {
                if (checkBoxItem.IsChecked)
                {
                    if (!SeekerSkillExists(seekerID, checkBoxItem.SkillName))
                    {
                        _context.SeekerSkill.Add(new SeekerSkill { SeekerID = seekerID, SkillName = checkBoxItem.SkillName });
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    if (SeekerSkillExists(seekerID, checkBoxItem.SkillName))
                    {
                        SeekerSkill sk = _context.SeekerSkill.First(s => s.SeekerID == seekerID && s.SkillName == checkBoxItem.SkillName);
                        _context.SeekerSkill.Remove(sk);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Seekers/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seeker = await _context.Seeker
                .FirstOrDefaultAsync(m => m.ID == id);
            if (seeker == null)
            {
                return NotFound();
            }

            return View(seeker);
        }

        // POST: Seekers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seeker = await _context.Seeker.FindAsync(id);
            _context.Seeker.Remove(seeker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(List));
        }

        private bool SeekerExists(int id)
        {
            return _context.Seeker.Any(s => s.ID == id);
        }

        private bool EmailExists(string email)
        {
            return _context.Seeker.Any(s => s.Email == email);
        }

        private bool SeekerRegionExists(int seekerID, string regionName)
        {
            return _context.SeekerRegion.Any(sr => sr.SeekerID == seekerID && sr.RegionName == regionName);
        }

        private bool SeekerSkillExists(int seekerID, string skillName)
        {
            return _context.SeekerSkill.Any(sk => sk.SeekerID == seekerID && sk.SkillName == skillName);
        }

        public ActionResult Video()
        {
            return View();
        }
    }
}
