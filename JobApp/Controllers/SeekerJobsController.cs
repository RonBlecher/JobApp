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
    public class SeekerJobsController : Controller
    {
        private readonly JobAppContext _context;

        public SeekerJobsController(JobAppContext context)
        {
            _context = context;
        }

        /*
        // GET: SeekerJobs
        public async Task<IActionResult> Index()
        {
            var jobAppContext = _context.SeekerJob.Include(s => s.Job).Include(s => s.Seeker);
            return View(await jobAppContext.ToListAsync());
        }

        // GET: SeekerJobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seekerJob = await _context.SeekerJob
                .Include(s => s.Job)
                .Include(s => s.Seeker)
                .FirstOrDefaultAsync(m => m.SeekerID == id);
            if (seekerJob == null)
            {
                return NotFound();
            }

            return View(seekerJob);
        }

        // GET: SeekerJobs/Create
        public IActionResult Create()
        {
            ViewData["JobID"] = new SelectList(_context.Job, "ID", "Description");
            ViewData["SeekerID"] = new SelectList(_context.Seeker, "ID", "Email");
            return View();
        }

        // POST: SeekerJobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SeekerID,JobID")] SeekerJob seekerJob)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seekerJob);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobID"] = new SelectList(_context.Job, "ID", "Description", seekerJob.JobID);
            ViewData["SeekerID"] = new SelectList(_context.Seeker, "ID", "Email", seekerJob.SeekerID);
            return View(seekerJob);
        }

        // GET: SeekerJobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seekerJob = await _context.SeekerJob.FindAsync(id);
            if (seekerJob == null)
            {
                return NotFound();
            }
            ViewData["JobID"] = new SelectList(_context.Job, "ID", "Description", seekerJob.JobID);
            ViewData["SeekerID"] = new SelectList(_context.Seeker, "ID", "Email", seekerJob.SeekerID);
            return View(seekerJob);
        }

        // POST: SeekerJobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SeekerID,JobID")] SeekerJob seekerJob)
        {
            if (id != seekerJob.SeekerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seekerJob);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeekerJobExists(seekerJob.SeekerID))
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
            ViewData["JobID"] = new SelectList(_context.Job, "ID", "Description", seekerJob.JobID);
            ViewData["SeekerID"] = new SelectList(_context.Seeker, "ID", "Email", seekerJob.SeekerID);
            return View(seekerJob);
        }

        // GET: SeekerJobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seekerJob = await _context.SeekerJob
                .Include(s => s.Job)
                .Include(s => s.Seeker)
                .FirstOrDefaultAsync(m => m.SeekerID == id);
            if (seekerJob == null)
            {
                return NotFound();
            }

            return View(seekerJob);
        }

        // POST: SeekerJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seekerJob = await _context.SeekerJob.FindAsync(id);
            _context.SeekerJob.Remove(seekerJob);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeekerJobExists(int id)
        {
            return _context.SeekerJob.Any(e => e.SeekerID == id);
        }
        */
    }
}
