﻿using System;
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
using System.IO;
using Microsoft.AspNetCore.Http;

namespace JobApp.Controllers
{
    public class SeekersController : Controller
    {
        private readonly JobAppContext _context;
        private readonly long _fileSizeLimit = 5 * 1024 * 1024; // 5MB

        public SeekersController(JobAppContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Seeker")]
        // GET: Publishers
        public async Task<IActionResult> Index()
        {
            var x = await _context.Seeker.ToListAsync();
            return View(x);
        }

        // GET: Seekers
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List()
        {
            return View(await _context.Seeker.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind("Name,Email,PhoneNum,Password")] Seeker seeker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seeker);
                await _context.SaveChangesAsync();
                SignIn(seeker);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }


        public IActionResult Login(string name, string password)
        {
            var identity = (ClaimsIdentity)User.Identity;
            if(identity.IsAuthenticated)
            {
                IEnumerable<Claim> claims = identity.Claims;
                Claim role = claims.Where(claim => claim.Type == ClaimTypes.Role).First();
                if (role != null && role.Value == "Seeker")
                {
                    return RedirectToAction("Index");
                }

            }

            var seekers = _context.Seeker.Where(seeker => seeker.Name == name && seeker.Password == password).ToList();
            if (seekers != null && seekers.Count() > 0)
            {
                SignIn(seekers.First());
                return RedirectToAction("Index");
            }

            return View();
        }

        private async void SignIn(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.ID.ToString()),
                new Claim("Email", user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, "Seeker"),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperites = new AuthenticationProperties
            {

            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperites);
        }

        // GET: Seekers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seeker = await _context.Seeker
                .FirstOrDefaultAsync(m => m.ID == id);
            if (seeker == null)
            {
                return NotFound();
            }

            return View(seeker);
        }

        public async Task<IActionResult> GetDisplayPDF(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seeker = await _context.Seeker
                .FirstOrDefaultAsync(m => m.ID == id);
            if (seeker == null)
            {
                return NotFound();
            }

            return DisplayPDF(seeker);
        }

        private FileResult DisplayPDF(Seeker seeker)
        {
            byte[] byteArray = seeker.CV;
            MemoryStream pdfStream = new MemoryStream();
           
            pdfStream.Write(byteArray, 0, byteArray.Length);
            pdfStream.Position = 0;
            return File(pdfStream, System.Net.Mime.MediaTypeNames.Application.Octet, "mycv.pdf");
        }

        // GET: Seekers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Seekers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CVObj,ID,Name,Email,PhoneNum,Password")] Seeker seeker)
        {
            if (ModelState.IsValid)
            {
                _context.Add(seeker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seeker);
        }

        // GET: Seekers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim idClaim = claims.Where(claim => claim.Type == "Id").First();

            if (id == null)
            {
                return RedirectToAction("Error", "Home");
            }

            if (idClaim.Value != id.ToString())
            {
                return RedirectToAction("NoPermission", "Home");
            }

            var seeker = await _context.Seeker.FindAsync(id);
            if (seeker == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(seeker);
        }

        // POST: Seekers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CVObj,ID,Name,Email,PhoneNum,Password")] Seeker seeker)
        {
            if (id != seeker.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    byte[] cv = seeker.CV;
                    IFormFile CVObj = seeker.CVObj;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        seeker.CVObj.CopyTo(ms);
                        seeker.CV = ms.ToArray();

                        if (seeker.CVObj.Length > _fileSizeLimit)
                        {
                            seeker.CV = cv;
                            seeker.CVObj = CVObj;
                            
                            ModelState.AddModelError("CVObj", "File size is over 5MB");
                            return View(seeker);
                        }
                    }

                    _context.Update(seeker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeekerExists(seeker.ID))
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
            return View(seeker);
        }

        public async Task<IActionResult> DownloadCV(int id)
        {
            return await GetDisplayPDF(id);
        }

        // GET: Seekers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seeker = await _context.Seeker
                .FirstOrDefaultAsync(m => m.ID == id);
            if (seeker == null)
            {
                return NotFound();
            }

            return View(seeker);
        }

        // POST: Seekers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seeker = await _context.Seeker.FindAsync(id);
            _context.Seeker.Remove(seeker);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeekerExists(int id)
        {
            return _context.Seeker.Any(e => e.ID == id);
        }
    }
}
