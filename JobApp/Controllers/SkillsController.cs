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
    public class SkillsController : Controller
    {
        private readonly JobAppContext _context;

        public SkillsController(JobAppContext context)
        {
            _context = context;
        }

        // GET: Skills
        public async Task<IActionResult> Index()
        {
            return View(await _context.Skill.ToListAsync());
        }

        // GET: Skills/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _context.Skill
                .FirstOrDefaultAsync(m => m.Name == id);
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        // GET: Skills/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Skills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Skill skill)
        {
            if (string.IsNullOrEmpty(skill.Name))
            {
                ModelState.AddModelError("Name", "Skill name is empty");
                return View(skill);
            }

            if (ModelState.IsValid)
            {
                if (!SkillExists(skill.Name))
                {
                    _context.Add(skill);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Name", "Skill already exists");
            }
            return View(skill);
        }

        // GET: Skills/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _context.Skill.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }
            return View(skill);
        }

        // POST: Skills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name")] Skill skill)
        {
            if (id != skill.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkillExists(skill.Name))
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
            return View(skill);
        }

        // GET: Skills/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _context.Skill
                .FirstOrDefaultAsync(m => m.Name == id);
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        // POST: Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var skill = await _context.Skill.FindAsync(id);
            _context.Skill.Remove(skill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkillExists(string id)
        {
            return _context.Skill.Any(e => e.Name == id);
        }
    }
}
