using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GolSkola.Data;
using GolSkola.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using NuGet.DependencyResolver;


namespace GolSkola.Controllers
{
    public class GolmaniController : Controller
    {
        private readonly GoranpiralicContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public GolmaniController(GoranpiralicContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            webHostEnvironment = webHost;
        }
       
        // GET: Golmani
        public async Task<IActionResult> Index()
        {
            return View(await _context.Golmanis.ToListAsync());
        }

        // GET: Golmani/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Golmanis == null)
            {
                return NotFound();
            }

            var golmani = await _context.Golmanis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (golmani == null)
            {
                return NotFound();
            }

            return View(golmani);
        }

        // GET: Golmani/Create

        public IActionResult Create()
        {
            Golmani golmani = new Golmani();
            return View(golmani);
        }


        // POST: Golmani/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Golmani golmani)
        {
            if (golmani.Profilna != null)
            {
                string path = await UploadImage(golmani.Profilna);
                golmani.Slika = path;
                _context.Add(golmani);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Add(golmani);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));

        }

        public async Task<string> UploadImage(IFormFile file)
        {
            var special = Guid.NewGuid().ToString();
            var filePath = Path.Combine(@"Uploads\Images", special + "-" + file.FileName);
            using (FileStream ms = new FileStream(Path.Combine(this.webHostEnvironment.WebRootPath,filePath), FileMode.Create))
            {
                await file.CopyToAsync(ms);
            }
            var filename = file.FileName;
            return filePath;

        }
        // GET: Golmani/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Golmanis == null)
            {
                return NotFound();
            }

            var golmani = await _context.Golmanis.FindAsync(id);
            if (golmani == null)
            {
                return NotFound();
            }
            return View(golmani);
        }

        // POST: Golmani/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Godiste,Klub,Slika,Vidljivost,CreatedAt")][FromForm] Golmani golmani)
        {
            if (id != golmani.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (golmani.Profilna != null)
                    {
                        string path = await UploadImage(golmani.Profilna);
                        golmani.Slika = path;
                        _context.Update(golmani);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _context.Update(golmani);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GolmaniExists(golmani.Id))
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
            return View(golmani);
        }

        // GET: Golmani/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Golmanis == null)
            {
                return NotFound();
            }

            var golmani = await _context.Golmanis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (golmani == null)
            {
                return NotFound();
            }

            return View(golmani);
        }

        // POST: Golmani/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Golmanis == null)
            {
                return Problem("Entity set 'GoranpiralicContext.Golmanis'  is null.");
            }
            var golmani = await _context.Golmanis.FindAsync(id);
            if (golmani != null)
            {
                _context.Golmanis.Remove(golmani);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GolmaniExists(int id)
        {
            return _context.Golmanis.Any(e => e.Id == id);
        }


    }
}
