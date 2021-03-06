﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Artesanas.Data;
using Artesanas.Models;
using Artesanas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Artesanas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubTipoController : Controller
    {
        private readonly ApplicationDbContext _db;

        [TempData]
        public string StatusMessage { get; set; }

        public SubTipoController(ApplicationDbContext db)
        {
            _db = db;
        }

        //Get INDEX
        public async Task<IActionResult> Index()
        {
            var subTipos = await _db.SubTipo.Include(s => s.Tipo).ToListAsync();
            return View(subTipos);
        }


        //GET - CREATE
        public async Task<IActionResult> Create()
        {
            TipoAndSubTipoViewModel model = new TipoAndSubTipoViewModel()
            {
                TipoList = await _db.Tipo.ToListAsync(),
                SubTipo = new Models.SubTipo(),
                SubTipoList = await _db.SubTipo.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToListAsync()
            };

            return View(model);
        }

        //POST - CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TipoAndSubTipoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesSubTipoExists = _db.SubTipo.Include(s => s.Tipo)
                    .Where(s => s.Name == model.SubTipo.Name && s.Tipo.Id == model.SubTipo.TipoId);            

                if (doesSubTipoExists.Count() > 0)
                {
                    //Error
                    StatusMessage = "Error : El subTipo creado ya existe en la categoría: " + doesSubTipoExists.First().Tipo.Name + ". Por favor utilice otro nombre.";
                }
                else
                {
                    _db.SubTipo.Add(model.SubTipo);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            TipoAndSubTipoViewModel modelVM = new TipoAndSubTipoViewModel()
            {
                TipoList = await _db.Tipo.ToListAsync(),
                SubTipo = model.SubTipo,
                SubTipoList = await _db.SubTipo.OrderBy(p => p.Name).Select(p => p.Name).ToListAsync(),
                StatusMessage = StatusMessage
            };
            return View(modelVM);
        }

        [ActionName("GetSubTipo")]
        public async Task<IActionResult> GetSubTipo(int id)
        {
            List<SubTipo> subTipos = new List<SubTipo>();

            subTipos = await (from subTipo in _db.SubTipo
                                   where subTipo.TipoId == id
                                   select subTipo).ToListAsync();
            return Json(new SelectList(subTipos, "Id", "Name"));
        }


        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subTipo = await _db.SubTipo.SingleOrDefaultAsync(m => m.Id == id);

            if (subTipo == null)
            {
                return NotFound();
            }

            TipoAndSubTipoViewModel model = new TipoAndSubTipoViewModel()
            {
                TipoList = await _db.Tipo.ToListAsync(),
                SubTipo = subTipo,
                SubTipoList = await _db.SubTipo.OrderBy(p => p.Name).Select(p => p.Name).Distinct().ToListAsync()
            };

            return View(model);
        }

        //POST - EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TipoAndSubTipoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var doesSubTipoExists = _db.SubTipo
                    .Include(s => s.Tipo)
                    .Where(s => s.Name == model.SubTipo.Name && s.Tipo.Id == model.SubTipo.TipoId);

                if (doesSubTipoExists.Count() > 0)
                {
                    //Error
                    StatusMessage = "Error : Sub Category exists under " + doesSubTipoExists.First().Tipo.Name + " category. Please use another name.";
                }
                else
                {
                    var subCatFromDb = await _db.SubTipo.FindAsync(model.SubTipo.Id);
                    subCatFromDb.Name = model.SubTipo.Name;

                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            TipoAndSubTipoViewModel modelVM = new TipoAndSubTipoViewModel()
            {
                TipoList = await _db.Tipo.ToListAsync(),
                SubTipo = model.SubTipo,
                SubTipoList = await _db.SubTipo.OrderBy(p => p.Name).Select(p => p.Name).ToListAsync(),
                StatusMessage = StatusMessage
            };
            //modelVM.SubCategory.Id = id;
            return View(modelVM);
        }

        //GET Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var subTipo = await _db.SubTipo.Include(s => s.Tipo).SingleOrDefaultAsync(m => m.Id == id);
            if (subTipo == null)
            {
                return NotFound();
            }

            return View(subTipo);
        }

        //GET Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var subTipo = await _db.SubTipo.Include(s => s.Tipo).SingleOrDefaultAsync(m => m.Id == id);
            if (subTipo == null)
            {
                return NotFound();
            }

            return View(subTipo);
        }

        //POST Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subTipo = await _db.SubTipo.SingleOrDefaultAsync(m => m.Id == id);
            _db.SubTipo.Remove(subTipo);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}