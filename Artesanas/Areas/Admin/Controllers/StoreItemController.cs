using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Artesanas.Data;
using Artesanas.Models;
using Artesanas.Models.ViewModels;
using Artesanas.Utility;

namespace Artesanas.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StoreItemController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _hostingEnvironment;

        [BindProperty]
        public StoreItemViewModel StoreItemVM { get; set; }

        public StoreItemController(ApplicationDbContext db, IHostingEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            StoreItemVM = new StoreItemViewModel()
            {
                Tipo = _db.Tipo,
                Maker = _db.Maker,
                StoreItem = new StoreItem()
            };
        }

        public async Task<IActionResult> Index()
        {
            var storeItems = await _db.StoreItem
                .Include(m => m.Tipo)
                .Include(m => m.SubTipo)
                .Include(m => m.Maker)
                .ToListAsync();
            return View(storeItems);
        }

        //GET - CREATE
        public IActionResult Create()
        {
            return View(StoreItemVM);
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            StoreItemVM.StoreItem.SubTipoId = Convert.ToInt32(Request.Form["SubTipoId"].ToString());

            if (!ModelState.IsValid)
            {
                return View(StoreItemVM);
            }

            _db.StoreItem.Add(StoreItemVM.StoreItem);
            await _db.SaveChangesAsync();

            //Work on the image saving section

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var storeItemFromDb = await _db.StoreItem.FindAsync(StoreItemVM.StoreItem.Id);

            if (files.Count > 0)
            {
                //files has been uploaded
                var uploads = Path.Combine(webRootPath, "images");
                var extension = Path.GetExtension(files[0].FileName);

                using (var filesStream = new FileStream(Path.Combine(uploads, StoreItemVM.StoreItem.Id + extension), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                storeItemFromDb.Image = @"\images\" + StoreItemVM.StoreItem.Id + extension;
            }
            else
            {
                //no file was uploaded, so use default
                var uploads = Path.Combine(webRootPath, @"images\" + SD.DefaultFoodImage);
                System.IO.File.Copy(uploads, webRootPath + @"\images\" + StoreItemVM.StoreItem.Id + ".png");
                storeItemFromDb.Image = @"\images\" + StoreItemVM.StoreItem.Id + ".png";
            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        //GET - EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StoreItemVM.StoreItem = await _db.StoreItem.Include(m => m.Tipo).Include(m => m.SubTipo).SingleOrDefaultAsync(m => m.Id == id);
            StoreItemVM.SubTipo = await _db.SubTipo.Where(s => s.TipoId == StoreItemVM.StoreItem.TipoId).ToListAsync();

            if (StoreItemVM.StoreItem == null)
            {
                return NotFound();
            }
            return View(StoreItemVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPOST(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            StoreItemVM.StoreItem.SubTipoId = Convert.ToInt32(Request.Form["SubTipoId"].ToString());

            if (!ModelState.IsValid)
            {
                StoreItemVM.SubTipo = await _db.SubTipo.Where(s => s.TipoId == StoreItemVM.StoreItem.TipoId).ToListAsync();
                return View(StoreItemVM);
            }

            //Work on the image saving section

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var storeItemFromDb = await _db.StoreItem.FindAsync(StoreItemVM.StoreItem.Id);

            if (files.Count > 0)
            {
                //New Image has been uploaded
                var uploads = Path.Combine(webRootPath, "images");
                var extension_new = Path.GetExtension(files[0].FileName);

                //Delete the original file
                var imagePath = Path.Combine(webRootPath, storeItemFromDb.Image.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                //we will upload the new file
                using (var filesStream = new FileStream(Path.Combine(uploads, StoreItemVM.StoreItem.Id + extension_new), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                storeItemFromDb.Image = @"\images\" + StoreItemVM.StoreItem.Id + extension_new;
            }

            storeItemFromDb.Name = StoreItemVM.StoreItem.Name;
            storeItemFromDb.HighlightedWords = StoreItemVM.StoreItem.HighlightedWords;
            storeItemFromDb.Description = StoreItemVM.StoreItem.Description;
            storeItemFromDb.Pairing = StoreItemVM.StoreItem.Pairing;
            storeItemFromDb.IBU = StoreItemVM.StoreItem.IBU;
            storeItemFromDb.Gravity = StoreItemVM.StoreItem.Gravity;
            storeItemFromDb.Alcohol = StoreItemVM.StoreItem.Alcohol;
            storeItemFromDb.Bitterness = StoreItemVM.StoreItem.Bitterness;
            storeItemFromDb.Amount = StoreItemVM.StoreItem.Amount;
            storeItemFromDb.Price = StoreItemVM.StoreItem.Price;
            storeItemFromDb.Image = StoreItemVM.StoreItem.Image;
            storeItemFromDb.TipoId = StoreItemVM.StoreItem.TipoId;
            storeItemFromDb.MakerId = StoreItemVM.StoreItem.MakerId;
            storeItemFromDb.SubTipoId = StoreItemVM.StoreItem.SubTipoId;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //GET : Details StoreItem
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StoreItemVM.StoreItem = await _db.StoreItem.Include(m => m.Tipo).Include(m => m.SubTipo).SingleOrDefaultAsync(m => m.Id == id);

            if (StoreItemVM.StoreItem == null)
            {
                return NotFound();
            }

            return View(StoreItemVM);
        }

        //GET : Delete StoreItem
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StoreItemVM.StoreItem = await _db.StoreItem.Include(m => m.Tipo).Include(m => m.SubTipo).SingleOrDefaultAsync(m => m.Id == id);

            if (StoreItemVM.StoreItem == null)
            {
                return NotFound();
            }

            return View(StoreItemVM);
        }

        //POST Delete StoreItem
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            StoreItem storeItem = await _db.StoreItem.FindAsync(id);

            if (storeItem != null)
            {
                var imagePath = Path.Combine(webRootPath, storeItem.Image.TrimStart('\\'));

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _db.StoreItem.Remove(storeItem);
                await _db.SaveChangesAsync();

            }

            return RedirectToAction(nameof(Index));
        }
    }
}