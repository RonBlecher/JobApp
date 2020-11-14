using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobApp.Data;
using JobApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using JobApp.Twitter;
using Job = JobApp.Models.Job;

namespace JobApp.Controllers
{
    public class JobsController : Controller
    {
        private readonly JobAppContext _context;
        private readonly JobViewModelToJobConverter jobViewModelToJobConverter;

        public JobsController(JobAppContext context)
        {
            _context = context;
            jobViewModelToJobConverter = new JobViewModelToJobConverter(context);
        }

        // GET: Jobs
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Index()
        {
            List<Job> jobs = await _context.Job
                .Include(j => j.Publisher)
                .Include(j => j.JobSeekers).ThenInclude(js => js.Seeker)
                .Include(j => j.JobSkills).ThenInclude(js => js.Skill)
                .Include(j => j.JobCities).ThenInclude(jc => jc.City)
                .ToListAsync();
           
            return View(jobs);
        }

        // GET: Jobs
        [Authorize(Roles = "Seeker")]
        public async Task<IActionResult> LookingForJobs()
        {
            Seeker seeker = await GetCurrentLoginSeeker();
            List<Job> appliedJobs = await GetAppliedJobs();

            ViewData["Seeker"] = seeker;
            ViewData["AppliedJobs"] = appliedJobs;

            List<string> seekSkills = new List<string>();
            await _context.SeekerSkill.Where(sk => sk.SeekerID == seeker.ID).ForEachAsync(sk => seekSkills.Add(sk.SkillName));

            List<string> seekRegions = new List<string>();
            await _context.SeekerRegion.Where(sr => sr.SeekerID == seeker.ID).ForEachAsync(sr => seekRegions.Add(sr.RegionName));

            List<Job> jobs = await _context.Job
                .Include(j => j.Publisher)
                .Include(j => j.JobSeekers).ThenInclude(js => js.Seeker)
                .Include(j => j.JobSkills).ThenInclude(js => js.Skill)
                .Include(j => j.JobCities).ThenInclude(jc => jc.City)
                .Where(j => j.JobSeekers.Any(js => js.SeekerID == seeker.ID && js.JobID == j.ID) == false)
                .Where(j => j.JobSkills.Any(js => seekSkills.Contains(js.SkillName)))
                .Where(j => j.JobCities.Any(jc => seekRegions.Contains(jc.City.RegionName)))
                .OrderByDescending(j => j.LastEdited)
                .ToListAsync();

            return View(jobs);
        }


        // GET: Jobs
        [Authorize(Roles = "Seeker")]
        public async Task<IActionResult> LookingForAllJobs()
        {
            Seeker seeker = await GetCurrentLoginSeeker();
            List<Job> appliedJobs = await GetAppliedJobs();

            ViewData["Seeker"] = seeker;
            ViewData["AppliedJobs"] = appliedJobs;
            ViewData["SearchSkill"] = "";
            ViewData["SearchCity"] = "";
            ViewData["FromDate"] = "";
            ViewData["ToDate"] = "";

            List<Job> jobs = await _context.Job
                .Include(j => j.Publisher)
                .Include(j => j.JobSeekers).ThenInclude(js => js.Seeker)
                .Include(j => j.JobSkills).ThenInclude(js => js.Skill)
                .Include(j => j.JobCities).ThenInclude(jc => jc.City)
                .OrderByDescending(j => j.LastEdited)
                .ToListAsync();

            return View(jobs);
        }

        [Authorize(Roles = "Seeker")]
        public async Task<IActionResult> Search(string skill, string city, string fromDate, string toDate)
        {
            Seeker seeker = await GetCurrentLoginSeeker();
            List<Job> appliedJobs = await GetAppliedJobs();

            ViewData["Seeker"] = seeker;
            ViewData["AppliedJobs"] = appliedJobs;
            ViewData["SearchSkill"] = skill;
            ViewData["SearchCity"] = city;
            ViewData["SearchFromDate"] = fromDate;
            ViewData["SearchToDate"] = toDate;

            List<Job> allJobs = await _context.Job
                .Include(j => j.Publisher)
                .Include(j => j.JobSeekers).ThenInclude(js => js.Seeker)
                .Include(j => j.JobSkills).ThenInclude(js => js.Skill)
                .Include(j => j.JobCities).ThenInclude(jc => jc.City)
                .OrderByDescending(j => j.LastEdited)
                .ToListAsync();

            var filteredJobs = 
                from job in allJobs
                where isJobContainsSkill(skill, job) && isJobContainsCity(city, job) && RangeOfDate(fromDate, toDate, job)
                select job;

            return View("LookingForAllJobs", filteredJobs);
        }

        private Boolean isJobContainsSkill(string skill, Job job)
        {
            return (skill != null) ? job.JobSkills.Where(jobSkill => jobSkill.Skill.Name.ToLower().Contains(skill.ToLower())).Count() > 0 : true;
        }

        private Boolean isJobContainsCity(string city, Job job)
        {
            return (city != null) ? job.JobCities.Where(jobCity => jobCity.CityName.ToLower().Contains(city.ToLower())).Count() > 0 : true;
        }

        private Boolean RangeOfDate(string fromDate, string toDate, Job job)
        {
            if (fromDate != null && toDate != null) {

                DateTime DateTimeFromDate = DateTime.Parse(fromDate);
                DateTime DateTimeToDate = DateTime.Parse(toDate);

                return job.LastEdited >= DateTimeFromDate && job.LastEdited <= DateTimeToDate;
            }
            else if (fromDate != null && toDate == null)
            {
                DateTime DateTimeFromDate = DateTime.Parse(fromDate);
                return job.LastEdited >= DateTimeFromDate;
            }
            else if (fromDate == null && toDate != null)
            {
                DateTime DateTimeToDate = DateTime.Parse(toDate);
                return job.LastEdited <= DateTimeToDate;
            }
            return true;
        }

        private async Task<List<Job>> GetAppliedJobs()
        {
            Seeker seeker = await GetCurrentLoginSeeker();
            List<Job> appliedJobs = await _context.SeekerJob
                .Where(js => js.SeekerID.ToString() == seeker.ID.ToString())
                .OrderByDescending(js => js.SubmitDate)
                .Select(js => js.Job)
                .ToListAsync();
            appliedJobs.ForEach(j =>
            {
                j.Publisher = _context.Publisher.First(p => p.ID == j.PublisherId);
                j.JobSeekers = _context.SeekerJob.Where(sj => sj.SeekerID == seeker.ID).Include(sj => sj.Seeker).ToList();
                j.JobSkills = _context.JobSkill.Where(js => js.JobID == j.ID).Include(js => js.Skill).ToList();
                j.JobCities = _context.CityJob.Where(cj => cj.JobID == j.ID).Include(cj => cj.City).ToList();
            });

            return appliedJobs;
        }

        private async Task<Seeker> GetCurrentLoginSeeker()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim idClaim = claims.Where(claim => claim.Type == "Id").First();

            return await _context.Seeker.FirstAsync(s => s.ID.ToString() == idClaim.Value);
        }

        [HttpGet]
        [Authorize(Roles = "Seeker, Admin")]
        public async Task<List<Job>> AllPublishedJobs()
        {
            List<Job> jobs = await _context.Job
                .Include(j => j.Publisher)
                .Include(j => j.JobSeekers).ThenInclude(js => js.Seeker)
                .Include(j => j.JobSkills)
                .Include(j => j.JobCities)
                .ToListAsync();

            return jobs;
        }

        [HttpGet]
        [Authorize(Roles = "Publisher")]
        public async Task<List<Job>> MyPublishedJobs()
        {
            return await GetPublisherJobs();
        }

        [HttpGet]
        [Authorize(Roles = "Publisher")]
        public async Task<IActionResult> MyPublishedJobsView()
        {
            return View(await GetPublisherJobs());
        }

        private async Task<List<Job>> GetPublisherJobs()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim idClaim = claims.Where(claim => claim.Type == "Id").First();

            int publisherID = Convert.ToInt32(idClaim.Value);
            List<Job> publisherJobs = await _context.Job
                .Where(j => j.PublisherId == publisherID)
                .Include(j => j.Publisher)
                .Include(j => j.JobSeekers).ThenInclude(js => js.Seeker)
                .Include(j => j.JobSkills).ThenInclude(js => js.Skill)
                .Include(j => j.JobCities).ThenInclude(jc => jc.City)
                .ToListAsync();
            return publisherJobs;
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .Where(j => j.ID == id)
                .Include(j => j.Publisher)
                .Include(j => j.JobSeekers).ThenInclude(js => js.Seeker)
                .Include(j => j.JobSkills).ThenInclude(js => js.Skill)
                .Include(j => j.JobCities).ThenInclude(jc => jc.City)
                .FirstOrDefaultAsync();
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        [Authorize(Roles = "Publisher")]
        public async Task<IActionResult> Create()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim idClaim = claims.Where(claim => claim.Type == "Id").First();
            int publisherID = Convert.ToInt32(idClaim.Value);

            Publisher publisher = await _context.Publisher.FirstAsync(p => p.ID == publisherID);
            List<Skill> skills = await _context.Skill.ToListAsync();
            List<City> cities = await _context.City.ToListAsync();

            ViewData["Publisher"] = publisher;
            ViewData["Skills"] = skills;
            ViewData["Cities"] = cities;

            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Publisher")]
        public async Task<IActionResult> Create([Bind("Title, Description, JobSkillsId, JobCities, Lon, Lat")] JobViewModel jobViewModel)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim idClaim = claims.Where(claim => claim.Type == "Id").First();

            jobViewModel.PublisherId = Convert.ToInt32(idClaim.Value);
            Publisher publisher = await _context.Publisher.FirstAsync(p => p.ID == jobViewModel.PublisherId);

            Job job = new Job();

            if (ModelState.IsValid)
            {
                var twitter = new TwitterApi();
                string messagePost = string.Format(
                    "Job: {0}\n" +
                    "Description: {1}\n" +
                    "Publisher: {2}\n" +
                    "Skills: {3}\n" +
                    "Cities: {4}",
                    jobViewModel.Title, jobViewModel.Description, publisher.Name,
                    String.Join(", ", jobViewModel.JobSkillsId.ToArray()), String.Join(", ", jobViewModel.JobCities.ToArray()));
                var response = await twitter.Tweet(messagePost);

                job = await jobViewModelToJobConverter.Convert(jobViewModel);
                job.LastEdited = DateTime.Now;

                _context.Add(job);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(MyPublishedJobsView));
            } 
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            }
            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .Where(j => j.ID == id)
                .Include(j => j.Publisher)
                .Include(j => j.JobSeekers).ThenInclude(js => js.Seeker)
                .Include(j => j.JobSkills).ThenInclude(js => js.Skill)
                .Include(j => j.JobCities).ThenInclude(jc => jc.City)
                .FirstOrDefaultAsync();
            if (job == null)
            {
                return NotFound();
            }

            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim idClaim = claims.Where(claim => claim.Type == "Id").First();
            Claim role = claims.Where(claim => claim.Type == ClaimTypes.Role).First();

            if (role.Value == "Admin" || (role.Value == "Publisher" && idClaim.Value == job.PublisherId.ToString()))
            {
                Publisher publisher = await _context.Publisher.FirstAsync(p => p.ID == job.PublisherId);
                List<Skill> skills = await _context.Skill.ToListAsync();
                List<City> cities = await _context.City.ToListAsync();
                ViewData["Publisher"] = publisher;
                ViewData["Skills"] = skills;
                ViewData["Cities"] = cities;

                return View(jobViewModelToJobConverter.Convert(job));
            }
            return RedirectToAction("NoPermission", "Home");
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID, Title, Description, JobSkillsId, PublisherId, JobCities, Lon, Lat")] JobViewModel jobViewModel)
        {
            if (id != jobViewModel.ID)
            {
                return NotFound();
            }

            Job job = await _context.Job.FirstAsync(j => j.ID == id);
            if (job == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Job changedJob = await jobViewModelToJobConverter.Convert(jobViewModel);
                job.Title = changedJob.Title;
                job.Description = changedJob.Description;
                job.LastEdited = DateTime.Now;
                job.Lon = changedJob.Lon;
                job.Lat = changedJob.Lat;
                job.JobSkills = changedJob.JobSkills;
                job.JobCities = changedJob.JobCities;

                try
                {
                    // remove M2M entries before update
                    _context.JobSkill.RemoveRange(_context.JobSkill.Where(js => js.JobID == job.ID));
                    _context.CityJob.RemoveRange(_context.CityJob.Where(cj => cj.JobID == job.ID));

                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(jobViewModel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                var identity = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                Claim role = claims.Where(claim => claim.Type == ClaimTypes.Role).First();
                if (role.Value == "Admin")
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(MyPublishedJobsView));
                }
            }
            return View(jobViewModel);
        }

        [Authorize(Roles = "Seeker")]
        public async Task<IActionResult> ApplyCV(int? id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim idClaim = claims.Where(claim => claim.Type == "Id").First();

            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .Where(j => j.ID == id)
                .Include(j => j.Publisher)
                .Include(j => j.JobSeekers).ThenInclude(js => js.Seeker)
                .Include(j => j.JobSkills).ThenInclude(js => js.Skill)
                .Include(j => j.JobCities).ThenInclude(jc => jc.City)
                .FirstOrDefaultAsync();

            if (job == null)
            {
                return NotFound();
            }

            Seeker seeker = _context.Seeker.FirstOrDefault(s => s.ID.ToString() == idClaim.Value);

            if (seeker == null)
            {
                return NotFound();
            }

            if (seeker.CV != null && !_context.SeekerJob.Any(sj => sj.SeekerID == seeker.ID && sj.JobID == job.ID))
            {
                SeekerJob seekerJob = new SeekerJob
                {
                    JobID = job.ID,
                    SeekerID = seeker.ID,
                    SubmitDate = DateTime.Now
                };
                _context.Add(seekerJob);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(LookingForJobs));
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .FirstOrDefaultAsync(m => m.ID == id);
            if (job == null)
            {
                return NotFound();
            }

            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim idClaim = claims.Where(claim => claim.Type == "Id").First();
            Claim role = claims.Where(claim => claim.Type == ClaimTypes.Role).First();

            if (role.Value == "Admin" || (role.Value == "Publisher" && idClaim.Value == job.PublisherId.ToString()))
            {
                return View(job);
            }
            return RedirectToAction("NoPermission", "Home");
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.Job.FindAsync(id);
            _context.Job.Remove(job);
            await _context.SaveChangesAsync();

            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim role = claims.Where(claim => claim.Type == ClaimTypes.Role).First();
            if (role.Value == "Admin")
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(MyPublishedJobsView));
        }

        private bool JobExists(int id)
        {
            return _context.Job.Any(e => e.ID == id);
        }
    }
}


