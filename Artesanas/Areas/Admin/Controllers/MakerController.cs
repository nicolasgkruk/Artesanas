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
    public class MakerController : Controller
    {
        private readonly ApplicationDbContext _db;
        public MakerController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET
        public async Task<IActionResult> Index()
        {
            return View(await _db.Maker.ToListAsync());
        }

        //GET - CREATE
        public IActionResult Create()
        {
            return View();
        }


        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Maker maker)
        {
            if (ModelState.IsValid)
            {
                //if valid
                _db.Maker.Add(maker);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            return View(maker);
        }

        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var maker = await _db.Maker.FindAsync(id);
            if (maker == null)
            {
                return NotFound();
            }
            return View(maker);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Maker maker)
        {
            if (ModelState.IsValid)
            {
                _db.Update(maker);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(maker);
        }



        //GET - DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var maker = await _db.Maker.FindAsync(id);
            if (maker == null)
            {
                return NotFound();
            }
            return View(maker);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var maker = await _db.Maker.FindAsync(id);

            if (maker == null)
            {
                return View();
            }
            _db.Maker.Remove(maker);
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

            var maker = await _db.Maker.FindAsync(id);
            if (maker == null)
            {
                return NotFound();
            }

            return View(maker);
        }

    }
}