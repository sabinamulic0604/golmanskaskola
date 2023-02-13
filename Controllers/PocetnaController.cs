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
using System.Dynamic;

namespace GolSkola.Controllers
{
    public class PocetnaController : Controller
    {
        private readonly GoranpiralicContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;


        public PocetnaController(GoranpiralicContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            webHostEnvironment = webHost;
        }
        public IActionResult Index()
        {
            dynamic mymodel = new ExpandoObject();

            mymodel.Golmani = GetGolmani();
            mymodel.Vijesti = GetVijesti();
            mymodel.Galerija = GetGalerija();
            

            return View(mymodel);

        }

        public IActionResult Vijesti()
        {
            dynamic mymodel = new ExpandoObject();

          
            mymodel.Vijesti = GetSveVijesti();


            return View(mymodel);

        }


        public List<Golmani> GetGolmani()
        {
            List<Golmani> golmani = _context.Golmanis.Where(x => x.Vidljivost == 1).ToList();
            return golmani;
        }

        public List<Vijesti> GetVijesti()
        {
            List<Vijesti> vijesti = _context.Vijestis.OrderByDescending(x => x.CreatedAt).Where(x => x.Vidljivost == 1).Take(3).ToList();
            return vijesti;
        }

        public List<Galerija> GetGalerija()
        {
            List<Galerija> galerija = _context.Galerijas.OrderByDescending(x => x.CreatedAt).Where(x => x.Vidljivost == 1).ToList();
            return galerija;
        }

        public async Task<IActionResult> Detalji(int? id)
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


        public List<Vijesti> GetSveVijesti()
        {
            List<Vijesti> svevijesti = _context.Vijestis.OrderByDescending(x => x.CreatedAt).Where(x => x.Vidljivost == 1).ToList();
            return svevijesti;
        }



    }
}
