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

        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _db.Tipo.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Tipo tipo)
        {
            if (ModelState.IsValid)
            {
                _db.Update(tipo);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(tipo);
        }



        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var tipo = await _db.Tipo.FindAsync(id);
            if (tipo == null)
            {
                return NotFound();
            }
            return View(tipo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var tipo = await _db.Tipo.FindAsync(id);

            if (tipo == null)
            {
                return View();
            }
            _db.Tipo.Remove(tipo);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        //GET - DETAILS
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipo = await _db.Tipo.FindAsync(id);
            if (tipo == null)
            {
                return NotFound();
            }

            return View(tipo);
        }





    }
}