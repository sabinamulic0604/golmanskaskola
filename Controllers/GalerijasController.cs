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


namespace GolSkola.Controllers
{
    public class GalerijasController : Controller
    {
        private readonly GoranpiralicContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public GalerijasController(GoranpiralicContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            webHostEnvironment = webHost;
        }

        // GET: Galerija
        public async Task<IActionResult> Index()
        {
            return View(await _context.Galerijas.ToListAsync());
        }

        // GET: Galerija/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Galerijas == null)
            {
                return NotFound();
            }

            var Galerija = await _context.Galerijas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Galerija == null)
            {
                return NotFound();
            }

            return View(Galerija);
        }

        // GET: Galerija/Create

        public IActionResult Create()
        {
            Galerija Galerija = new Galerija();
            return View(Galerija);
        }


        // POST: Galerija/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Galerija galerija)
        {
            if (galerija.Pomocna != null)
            {
                string path = await UploadImage(galerija.Pomocna);
                galerija.Fajl = path;
                _context.Add(galerija);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Add(galerija);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));

        }

        public async Task<string> UploadImage(IFormFile file)
        {
            var special = Guid.NewGuid().ToString();
            var filePath = Path.Combine(@"Uploads\Galerija", special + "-" + file.FileName);
            using (FileStream ms = new FileStream(Path.Combine(this.webHostEnvironment.WebRootPath, filePath), FileMode.Create))
            {
                await file.CopyToAsync(ms);
            }
            var filename = file.FileName;
            return filePath;

        }
        // GET: Galerija/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Galerijas == null)
            {
                return NotFound();
            }

            var galerija = await _context.Galerijas.FindAsync(id);
            if (galerija == null)
            {
                return NotFound();
            }
            return View(galerija);
        }

        // POST: Galerija/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ime,Fajl,Vidljivost,CreatedAt")][FromForm] Galerija galerija)
        {
            if (id != galerija.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (galerija.Pomocna != null)
                    {
                        string path = await UploadImage(galerija.Pomocna);
                        galerija.Fajl = path;
                        _context.Update(galerija);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _context.Update(galerija);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GalerijaExists(galerija.Id))
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
            return View(galerija);
        }

        // GET: Galerija/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Galerijas == null)
            {
                return NotFound();
            }

            var galerija = await _context.Galerijas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (galerija == null)
            {
                return NotFound();
            }

            return View(galerija);
        }

        // POST: Galerija/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Galerijas == null)
            {
                return Problem("Entity set 'GoranpiralicContext.Galerijas'  is null.");
            }
            var galerija = await _context.Galerijas.FindAsync(id);
            if (galerija != null)
            {
                _context.Galerijas.Remove(galerija);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GalerijaExists(int id)
        {
            return _context.Galerijas.Any(e => e.Id == id);
        }


    }
}
