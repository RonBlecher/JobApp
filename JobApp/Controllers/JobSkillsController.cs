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
    public class JobSkillsController : Controller
    {
        private readonly JobAppContext _context;

        public JobSkillsController(JobAppContext context)
        {
            _context = context;
        }

        /*
        // GET: JobSkills
        public async Task<IActionResult> Index()
        {
            var jobAppContext = _context.JobSkill.Include(j => j.Job).Include(j => j.Skill);
            return View(await jobAppContext.ToListAsync());
        }

        // GET: JobSkills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobSkill = await _context.JobSkill
                .Include(j => j.Job)
                .Include(j => j.Skill)
                .FirstOrDefaultAsync(m => m.JobID == id);
            if (jobSkill == null)
            {
                return NotFound();
            }

            return View(jobSkill);
        }

        // GET: JobSkills/Create
        public IActionResult Create()
        {
            ViewData["JobID"] = new SelectList(_context.Job, "ID", "Description");
            ViewData["SkillName"] = new SelectList(_context.Skill, "Name", "Name");
            return View();
        }

        // POST: JobSkills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobID,SkillName")] JobSkill jobSkill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobSkill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobID"] = new SelectList(_context.Job, "ID", "Description", jobSkill.JobID);
            ViewData["SkillName"] = new SelectList(_context.Skill, "Name", "Name", jobSkill.SkillName);
            return View(jobSkill);
        }

        // GET: JobSkills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobSkill = await _context.JobSkill.FindAsync(id);
            if (jobSkill == null)
            {
                return NotFound();
            }
            ViewData["JobID"] = new SelectList(_context.Job, "ID", "Description", jobSkill.JobID);
            ViewData["SkillName"] = new SelectList(_context.Skill, "Name", "Name", jobSkill.SkillName);
            return View(jobSkill);
        }

        // POST: JobSkills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobID,SkillName")] JobSkill jobSkill)
        {
            if (id != jobSkill.JobID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobSkill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobSkillExists(jobSkill.JobID))
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
            ViewData["JobID"] = new SelectList(_context.Job, "ID", "Description", jobSkill.JobID);
            ViewData["SkillName"] = new SelectList(_context.Skill, "Name", "Name", jobSkill.SkillName);
            return View(jobSkill);
        }

        // GET: JobSkills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobSkill = await _context.JobSkill
                .Include(j => j.Job)
                .Include(j => j.Skill)
                .FirstOrDefaultAsync(m => m.JobID == id);
            if (jobSkill == null)
            {
                return NotFound();
            }

            return View(jobSkill);
        }

        // POST: JobSkills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobSkill = await _context.JobSkill.FindAsync(id);
            _context.JobSkill.Remove(jobSkill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobSkillExists(int id)
        {
            return _context.JobSkill.Any(e => e.JobID == id);
        }
        */
    }
}
