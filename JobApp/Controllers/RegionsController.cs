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
    public class RegionsController : Controller
    {
        private readonly JobAppContext _context;

        public RegionsController(JobAppContext context)
        {
            _context = context;
        }

        // GET: Regions
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var regionsView = await (
                from r in _context.Region
                join cj in _context.CityJob.Include(jc => jc.City)
                on r.Name equals cj.City.RegionName into res
                from cj in res.DefaultIfEmpty()
                select new RegionListView
                {
                    Name = r.Name,
                    Cities = r.Cities.Select(c => c.Name).ToList(),
                    RegionJobsNum = _context.CityJob
                                     .Include(jc => jc.City)
                                     .Where(jc => r.Name == jc.City.RegionName)
                                     .Select(jc => new { jc.CityName })
                                     .Distinct()
                                     .Count()
                }).ToListAsync();
            return View(regionsView);
        }

        // GET: Regions/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _context.Region
                .Include(r => r.Cities)
                .FirstOrDefaultAsync(m => m.Name == id);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // GET: Regions/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Regions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Name")] Region region)
        {
            if (ModelState.IsValid)
            {
                if (!RegionExists(region.Name))
                {
                    _context.Add(region);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Name", "Region already exists");
            }
            return View(region);
        }

        /*
        // GET: Regions/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _context.Region.FindAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            return View(region);
        }

        // POST: Regions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id, [Bind("Name")] Region region)
        {
            if (id != region.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(region);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegionExists(region.Name))
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
            return View(region);
        }
        */

        // GET: Regions/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _context.Region
                .FirstOrDefaultAsync(m => m.Name == id);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // POST: Regions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var region = await _context.Region.FindAsync(id);
            _context.Region.Remove(region);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegionExists(string id)
        {
            return _context.Region.Any(e => e.Name == id);
        }

        public ActionResult Video()
        {
            return View();
        }
    }
}
