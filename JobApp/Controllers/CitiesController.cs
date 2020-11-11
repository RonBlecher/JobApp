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

namespace JobApp.Controllers
{
    public class CitiesController : Controller
    {
        private readonly JobAppContext _context;

        public CitiesController(JobAppContext context)
        {
            _context = context;
        }

        // GET: Cities
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var citiesView = await (
                from c in _context.City
                join cj in _context.CityJob on c.Name equals cj.CityName into res
                from cj in res.DefaultIfEmpty()
                select new CityListView
                {
                    Name = c.Name,
                    RegionName = c.RegionName,
                    CityJobsNum = _context.CityJob.Count(cj => cj.CityName == c.Name)
                }).Distinct().ToListAsync();
            return View(citiesView);
        }

        // GET: Cities/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.City
                .FirstOrDefaultAsync(m => m.Name == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // GET: Cities/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            List<Region> regions = await _context.Region.ToListAsync();

            ViewData["Regions"] = regions;

            return View();
        }

        // POST: Cities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(string name, string regionName)
        {
            City city = new City();

            if (ModelState.IsValid)
            {
                if (!CityExists(name))
                {
                    city.Name = name;
                    city.RegionName = regionName;

                    _context.Add(city);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Name", "City already exists");
            }
            ViewData["Regions"] = await _context.Region.ToListAsync();
            return View(city);
        }

        // GET: Cities/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.City.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }
            ViewData["Regions"] = await _context.Region.ToListAsync();
            return View(city);
        }

        // POST: Cities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string name, string regionName)
        {
            City city = await _context.City.FindAsync(name);

            if (city == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    city.RegionName = regionName;
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CityExists(city.Name))
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
            ViewData["Regions"] = await _context.Region.ToListAsync();
            return View(city);
        }

        // GET: Cities/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _context.City
                .FirstOrDefaultAsync(m => m.Name == id);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var city = await _context.City.FindAsync(id);
            _context.City.Remove(city);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CityExists(string id)
        {
            return _context.City.Any(e => e.Name == id);
        }

        public ActionResult Video()
        {
            return View();
        }
    }
}
