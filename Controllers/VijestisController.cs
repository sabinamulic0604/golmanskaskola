using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GolSkola.Data;
using GolSkola.Models;

namespace GolSkola.Controllers
{
    public class VijestisController : Controller
    {
        private readonly GoranpiralicContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public VijestisController(GoranpiralicContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            webHostEnvironment = webHost;
        }

        // GET: Vijestis
        public async Task<IActionResult> Index()
        {
              return View(await _context.Vijestis.ToListAsync());
        }

        // GET: Vijestis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vijestis == null)
            {
                return NotFound();
            }

            var vijesti = await _context.Vijestis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vijesti == null)
            {
                return NotFound();
            }

            return View(vijesti);
        }

        public async Task<string> UploadImage(IFormFile file)
        {
            var special = Guid.NewGuid().ToString();
            var filePath = Path.Combine(@"Uploads\News", special + "-" + file.FileName);
            using (FileStream ms = new FileStream(Path.Combine(this.webHostEnvironment.WebRootPath, filePath), FileMode.Create))
            {
                await file.CopyToAsync(ms);
            }
            var filename = file.FileName;
            return filePath;
        }
        // GET: Vijestis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vijestis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Vijesti vijesti)
        {
            if (vijesti.Okvir != null)
            {
                string path = await UploadImage(vijesti.Okvir);
                vijesti.Baner = path;
                _context.Add(vijesti);
                await _context.SaveChangesAsync();
            }
            else
            {
                _context.Add(vijesti);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Vijestis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vijestis == null)
            {
                return NotFound();
            }

            var vijesti = await _context.Vijestis.FindAsync(id);
            if (vijesti == null)
            {
                return NotFound();
            }
            return View(vijesti);
        }

        // POST: Vijestis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naslov,Podnaslov,Tekst,Baner,Vidljivost,CreatedAt")] Vijesti vijesti, FormFile file)
        {
            if (id != vijesti.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {


                   _context.Update(vijesti);
                   await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VijestiExists(vijesti.Id))
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
            return View(vijesti);
        }

        // GET: Vijestis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vijestis == null)
            {
                return NotFound();
            }

            var vijesti = await _context.Vijestis
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vijesti == null)
            {
                return NotFound();
            }

            return View(vijesti);
        }

        // POST: Vijestis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vijestis == null)
            {
                return Problem("Entity set 'GoranpiralicContext.Vijestis'  is null.");
            }
            var vijesti = await _context.Vijestis.FindAsync(id);
            if (vijesti != null)
            {
                _context.Vijestis.Remove(vijesti);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VijestiExists(int id)
        {
          return _context.Vijestis.Any(e => e.Id == id);
        }
    }
}
