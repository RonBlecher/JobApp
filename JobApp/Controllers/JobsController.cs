using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobApp.Data;
using JobApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

        [Authorize(Roles="Admin")]
        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            String search = "";
            List<Job> jobs = await _context.Job.ToListAsync();
            if (!string.IsNullOrEmpty(search))
            {
                jobs = jobs.Where(job => String.Compare(job.Title, search,
                    comparisonType: StringComparison.OrdinalIgnoreCase) == 0).ToList();
            }

            EnrichJob(jobs);
            return View(jobs);
        }

        [Authorize(Roles = "Seeker")]
        // GET: Jobs
        public async Task<IActionResult> LookingForJobs()
        {
            String search = "";
            List<Job> jobs = await _context.Job.ToListAsync();
            if (!string.IsNullOrEmpty(search))
            {
                jobs = jobs.Where(job => String.Compare(job.Title, search,
                    comparisonType: StringComparison.OrdinalIgnoreCase) == 0).ToList();
            }

            EnrichJob(jobs);
            return View(jobs);
        }


        [HttpGet]
        [Authorize(Roles = "Publisher")]
        public async Task<List<Job>> MyPublishedJobs()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim idClaim = claims.Where(claim => claim.Type == "Id").First();

            Publisher pulisher = _context.Publisher.Where(publisher => publisher.ID.ToString() == idClaim.Value).ToList().First();
            // return publisher.PostedJobs.ToList();

            List<Job> jobs = await _context.Job.ToListAsync();
            return jobs.Where(job => job.PublisherId.ToString() == idClaim.Value).ToList();
            //EnrichJob(jobs);
            //List<Job> jobsOfPublisher = jobs.Where(job => job.Publisher.ID == Int32.Parse(idClaim.Value)).ToList();
            //return jobsOfPublisher;
        }

        public async Task<IActionResult> MyPublishedJobsView()
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim idClaim = claims.Where(claim => claim.Type == "Id").First();

            Publisher pulisher = _context.Publisher.Where(publisher => publisher.ID.ToString() == idClaim.Value).ToList().First();
            // return publisher.PostedJobs.ToList();

            List<Job> jobs = await _context.Job.ToListAsync();
            List<Job> jobsOfSpecificPublisherLogged = jobs.Where(job => job.PublisherId.ToString() == idClaim.Value).ToList();
            EnrichJob(jobsOfSpecificPublisherLogged);
            return View(jobsOfSpecificPublisherLogged);
        }

        private void EnrichJob(List<Job> jobs)
        {
            jobs.ForEach(job =>
            {
                List<JobSkill> jobSkills =  _context.JobSkill.Where(jobSkill => jobSkill.Job.ID == job.ID).ToList();
                job.JobSkills = jobSkills;

                List<SeekerJob> seekerJobs = _context.SeekerJob.Where(seekerJob => seekerJob.Job.ID == job.ID).ToList();
                job.JobSeekers = seekerJobs;

                Publisher pulisher = _context.Publisher.Where(publisher => publisher.ID == job.PublisherId).ToList().First();
                job.Publisher = pulisher;
            });
       
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(job);
        }

        // GET: Jobs/Create
        public async Task<IActionResult> Create()
        {
            List<Publisher> publishers = await _context.Publisher.ToListAsync();
            List<Skill> skills = await _context.Skill.ToListAsync();

            ViewData["Publishers"] = publishers; // Send this list to the view
            ViewData["Skills"] = skills;

            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title, Description, JobSkillsId, PublisherId, Lon, Lat")] JobViewModel jobViewModel)
        {
            Job job = new Job();

            if (ModelState.IsValid)
            {
                job = await jobViewModelToJobConverter.Convert(jobViewModel);
                job.LastEdited = DateTime.Now;

                _context.Add(job);
                await _context.SaveChangesAsync();

                return RedirectToAction("Create");
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

            var job = await _context.Job.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        [Authorize(Roles ="Seeker")]
        public async Task<IActionResult> ApplyCV(int? id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim idClaim = claims.Where(claim => claim.Type == "Id").First();

            Seeker seeker = _context.Seeker.Where(publisher => publisher.ID.ToString() == idClaim.Value).ToList().First();

            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job.FindAsync(id);
            var jobSeekersOfJob = await _context.SeekerJob.Where(seekerJob => seekerJob.JobID == job.ID).ToListAsync();

            if (job == null)
            {
                return NotFound();
            }

            SeekerJob seekerJob = new SeekerJob
            {
                JobID = job.ID,
                SeekerID = seeker.ID
            };

            if (jobSeekersOfJob != null)
            {
                job.JobSeekers = jobSeekersOfJob;
                if(job.JobSeekers.Where(jobSeekers => jobSeekers.SeekerID == seekerJob.SeekerID).ToList().Count() == 0)
                {
                    job.JobSeekers.Add(seekerJob);
                }
            }
            else
            {
                job.JobSeekers = new List<SeekerJob>
                {
                    seekerJob
                };
            }
       
            try
            {
                _context.Update(job);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(job.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(LookingForJobs));
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description")] Job job)
        {
            if (id != job.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    job.LastEdited = DateTime.Now;
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.ID))
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
            return View(job);
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

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.Job.FindAsync(id);
            _context.Job.Remove(job);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
            return _context.Job.Any(e => e.ID == id);
        }
    }
}
