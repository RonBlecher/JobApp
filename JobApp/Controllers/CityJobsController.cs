using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobApp.Data;
using JobApp.Models;

namespace JobApp.Controllers
{
    public class CityJobsController : Controller
    {
        private readonly JobAppContext _context;

        public CityJobsController(JobAppContext context)
        {
            _context = context;
        }

        /*
        // GET: CityJobs
        public async Task<IActionResult> Index()
        {
            var jobAppContext = _context.CityJob.Include(c => c.City).Include(c => c.Job);
            return View(await jobAppContext.ToListAsync());
        }

        // GET: CityJobs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cityJob = await _context.CityJob
                .Include(c => c.City)
                .Include(c => c.Job)
                .FirstOrDefaultAsync(m => m.CityName == id);
            if (cityJob == null)
            {
                return NotFound();
            }

            return View(cityJob);
        }

        // GET: CityJobs/Create
        public IActionResult Create()
        {
            ViewData["CityName"] = new SelectList(_context.City, "Name", "Name");
            ViewData["JobID"] = new SelectList(_context.Job, "ID", "Description");
            return View();
        }

        // POST: CityJobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CityName,JobID")] CityJob cityJob)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cityJob);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityName"] = new SelectList(_context.City, "Name", "Name", cityJob.CityName);
            ViewData["JobID"] = new SelectList(_context.Job, "ID", "Description", cityJob.JobID);
            return View(cityJob);
        }

        // GET: CityJobs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cityJob = await _context.CityJob.FindAsync(id);
            if (cityJob == null)
            {
                return NotFound();
            }
            ViewData["CityName"] = new SelectList(_context.City, "Name", "Name", cityJob.CityName);
            ViewData["JobID"] = new SelectList(_context.Job, "ID", "Description", cityJob.JobID);
            return View(cityJob);
        }

        // POST: CityJobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CityName,JobID")] CityJob cityJob)
        {
            if (id != cityJob.CityName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cityJob);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityJobExists(cityJob.CityName))
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
            ViewData["CityName"] = new SelectList(_context.City, "Name", "Name", cityJob.CityName);
            ViewData["JobID"] = new SelectList(_context.Job, "ID", "Description", cityJob.JobID);
            return View(cityJob);
        }

        // GET: CityJobs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cityJob = await _context.CityJob
                .Include(c => c.City)
                .Include(c => c.Job)
                .FirstOrDefaultAsync(m => m.CityName == id);
            if (cityJob == null)
            {
                return NotFound();
            }

            return View(cityJob);
        }

        // POST: CityJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var cityJob = await _context.CityJob.FindAsync(id);
            _context.CityJob.Remove(cityJob);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CityJobExists(string id)
        {
            return _context.CityJob.Any(e => e.CityName == id);
        }
        */
    }
}
