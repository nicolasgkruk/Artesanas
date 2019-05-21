using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Artesanas.Data;
using Artesanas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Artesanas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TipoController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TipoController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET
        public async Task<IActionResult> Index()
        {
             return  View(await _db.Tipo.ToListAsync());
        }

        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }


        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tipo tipo)
        {
            if(ModelState.IsValid)
            {
                //if valid
                _db.Tipo.Add(tipo);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            return View(tipo);
        }
    }
}