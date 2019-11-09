using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandmadeFinal.DAL;
using HandmadeFinal.Extention;
using HandmadeFinal.Models;
using HandmadeFinal.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HandmadeFinal.Areas.Handmade.Controllers
{[Area("Handmade")]
    public class HomeBrandsController : Controller
    {
        private AppDbContext _context;
        private IHostingEnvironment _env;
        public HomeBrandsController(AppDbContext context,IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.BrandIcons);
        }

        public IActionResult AddItem()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(BrandIcon icon)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) { 
                return View();
            }
            if (!icon.Photo.IsPhoto())
            {
                ModelState.AddModelError("Photo", "Şəkil tipində olmalıdır!");
                return View();
            }

            if (!icon.Photo.PhotoSize(3))
            {
                ModelState.AddModelError("Photo", "Şəkilin ölçüsü böyükdür");
                return View();
            }


            string fileName = await icon.Photo.CopyPhoto(_env.WebRootPath, "brand-logo");

            icon.Icon = fileName;
            await _context.BrandIcons.AddAsync(icon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
           BrandIcon icon = await _context.BrandIcons.FindAsync(id);
            if (icon == null) return NotFound();

            return View(icon);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, BrandIcon icon)
        {
            if (id == null) return NotFound();
            BrandIcon dbicon = await _context.BrandIcons.FindAsync(id);

            if (dbicon == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                return View(dbicon);
            }

            if (icon.UpdatePhoto != null)
            {
                if (!icon.UpdatePhoto.IsPhoto())
                {
                    ModelState.AddModelError("Photo", "Şəkil tipində olmalıdır");
                    return View();
                }

                if (!icon.UpdatePhoto.PhotoSize(2))
                {
                    ModelState.AddModelError("Photo", "Şəkilin ölçüsü böyükdür");
                    return View();
                }

                string fileName = await icon.UpdatePhoto.CopyPhoto(_env.WebRootPath, "brand-logo");
                Utility.DeleteImage(_env.WebRootPath, dbicon.Icon);
                dbicon.Icon = fileName;

            }
           


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null) return NotFound();
            BrandIcon icon = await _context.BrandIcons.FindAsync(id);
            if (icon == null) return NotFound();

            return View(icon);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteImage(int? id)
        {
            BrandIcon icon = await _context.BrandIcons.FindAsync(id);
            Utility.DeleteImage(_env.WebRootPath, icon.Icon);
            _context.BrandIcons.Remove(icon);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            BrandIcon icon = await _context.BrandIcons.FindAsync(id);
            if (icon == null) return NotFound();

            return View(icon);
        }
    }
}