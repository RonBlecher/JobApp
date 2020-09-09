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
    public class SeekerSkillsController : Controller
    {
        private readonly JobAppContext _context;

        public SeekerSkillsController(JobAppContext context)
        {
            _context = context;
        }

        // GET: SeekerSkills
        public async Task<IActionResult> Index()
        {
            var jobAppContext = _context.SeekerSkill.Include(s => s.Seeker).Include(s => s.Skill);
            return View(await jobAppContext.ToListAsync());
        }

        // GET: SeekerSkills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seekerSkill = await _context.SeekerSkill
                .Include(s => s.Seeker)
                .Include(s => s.Skill)
                .FirstOrDefaultAsync(m => m.SeekerID == id);
            if (seekerSkill == null)
            {
                return NotFound();
            }

            return View(seekerSkill);
        }

        // GET: SeekerSkills/Create
        public IActionResult Create()
        {
            ViewData["SeekerID"] = new SelectList(_context.Seeker, "ID", "Email");
            ViewData["SkillName"] = new SelectList(_context.Skill, "Name", "Name");
            return View();
        }

        // POST: SeekerSkills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SeekerID,SkillName")] SeekerSkill seekerSkill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seekerSkill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SeekerID"] = new SelectList(_context.Seeker, "ID", "Email", seekerSkill.SeekerID);
            ViewData["SkillName"] = new SelectList(_context.Skill, "Name", "Name", seekerSkill.SkillName);
            return View(seekerSkill);
        }

        // GET: SeekerSkills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seekerSkill = await _context.SeekerSkill.FindAsync(id);
            if (seekerSkill == null)
            {
                return NotFound();
            }
            ViewData["SeekerID"] = new SelectList(_context.Seeker, "ID", "Email", seekerSkill.SeekerID);
            ViewData["SkillName"] = new SelectList(_context.Skill, "Name", "Name", seekerSkill.SkillName);
            return View(seekerSkill);
        }

        // POST: SeekerSkills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SeekerID,SkillName")] SeekerSkill seekerSkill)
        {
            if (id != seekerSkill.SeekerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seekerSkill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeekerSkillExists(seekerSkill.SeekerID))
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
            ViewData["SeekerID"] = new SelectList(_context.Seeker, "ID", "Email", seekerSkill.SeekerID);
            ViewData["SkillName"] = new SelectList(_context.Skill, "Name", "Name", seekerSkill.SkillName);
            return View(seekerSkill);
        }

        // GET: SeekerSkills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seekerSkill = await _context.SeekerSkill
                .Include(s => s.Seeker)
                .Include(s => s.Skill)
                .FirstOrDefaultAsync(m => m.SeekerID == id);
            if (seekerSkill == null)
            {
                return NotFound();
            }

            return View(seekerSkill);
        }

        // POST: SeekerSkills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seekerSkill = await _context.SeekerSkill.FindAsync(id);
            _context.SeekerSkill.Remove(seekerSkill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeekerSkillExists(int id)
        {
            return _context.SeekerSkill.Any(e => e.SeekerID == id);
        }
    }
}
