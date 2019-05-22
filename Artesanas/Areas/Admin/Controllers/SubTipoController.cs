using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Artesanas.Data;
using Artesanas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Artesanas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubTipoController : Controller
    {
        private readonly ApplicationDbContext _db;

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
    }
}