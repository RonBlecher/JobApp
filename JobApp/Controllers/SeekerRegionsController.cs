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
    public class SeekerRegionsController : Controller
    {
        private readonly JobAppContext _context;

        public SeekerRegionsController(JobAppContext context)
        {
            _context = context;
        }

        /*
        // GET: SeekerRegions
        public async Task<IActionResult> Index()
        {
            var jobAppContext = _context.SeekerRegion.Include(s => s.Region).Include(s => s.Seeker);
            return View(await jobAppContext.ToListAsync());
        }

        // GET: SeekerRegions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seekerRegion = await _context.SeekerRegion
                .Include(s => s.Region)
                .Include(s => s.Seeker)
                .FirstOrDefaultAsync(m => m.SeekerID == id);
            if (seekerRegion == null)
            {
                return NotFound();
            }

            return View(seekerRegion);
        }

        // GET: SeekerRegions/Create
        public IActionResult Create()
        {
            ViewData["RegionName"] = new SelectList(_context.Region, "Name", "Name");
            ViewData["SeekerID"] = new SelectList(_context.Seeker, "ID", "Email");
            return View();
        }

        // POST: SeekerRegions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SeekerID,RegionName")] SeekerRegion seekerRegion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seekerRegion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RegionName"] = new SelectList(_context.Region, "Name", "Name", seekerRegion.RegionName);
            ViewData["SeekerID"] = new SelectList(_context.Seeker, "ID", "Email", seekerRegion.SeekerID);
            return View(seekerRegion);
        }

        // GET: SeekerRegions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seekerRegion = await _context.SeekerRegion.FindAsync(id);
            if (seekerRegion == null)
            {
                return NotFound();
            }
            ViewData["RegionName"] = new SelectList(_context.Region, "Name", "Name", seekerRegion.RegionName);
            ViewData["SeekerID"] = new SelectList(_context.Seeker, "ID", "Email", seekerRegion.SeekerID);
            return View(seekerRegion);
        }

        // POST: SeekerRegions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SeekerID,RegionName")] SeekerRegion seekerRegion)
        {
            if (id != seekerRegion.SeekerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(seekerRegion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeekerRegionExists(seekerRegion.SeekerID))
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
            ViewData["RegionName"] = new SelectList(_context.Region, "Name", "Name", seekerRegion.RegionName);
            ViewData["SeekerID"] = new SelectList(_context.Seeker, "ID", "Email", seekerRegion.SeekerID);
            return View(seekerRegion);
        }

        // GET: SeekerRegions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seekerRegion = await _context.SeekerRegion
                .Include(s => s.Region)
                .Include(s => s.Seeker)
                .FirstOrDefaultAsync(m => m.SeekerID == id);
            if (seekerRegion == null)
            {
                return NotFound();
            }

            return View(seekerRegion);
        }

        // POST: SeekerRegions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seekerRegion = await _context.SeekerRegion.FindAsync(id);
            _context.SeekerRegion.Remove(seekerRegion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeekerRegionExists(int id)
        {
            return _context.SeekerRegion.Any(e => e.SeekerID == id);
        }
        */
    }
}
