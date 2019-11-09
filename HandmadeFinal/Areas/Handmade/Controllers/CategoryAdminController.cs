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
{
    [Area("Handmade")]
    public class CategoryAdminController : Controller
    {
        private AppDbContext _context;
        private IHostingEnvironment _env;
        public CategoryAdminController(AppDbContext context,IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Categories);
        }

        public IActionResult AddItem()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(Category category)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid || ModelState["CategoryName"].ValidationState == ModelValidationState.Invalid )
            {
                return View();
            }
            if (!category.Photo.IsPhoto())
            {
                ModelState.AddModelError("Photo", "Şəkil tipində olmalıdır!");
                return View();
            }

            if (!category.Photo.PhotoSize(3))
            {
                ModelState.AddModelError("Photo", "Şəkilin ölçüsü böyükdür");
                return View();
            }


            string fileName = await category.Photo.CopyPhoto(_env.WebRootPath, "category");

            category.CategoryImage = fileName;
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
           Category category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Category category)
        {
           Category dbcategory = await _context.Categories.FindAsync(id);

            if (dbcategory == null)
            {
                return (RedirectToAction(nameof(Index)));
            }
            if (ModelState["CategoryName"].ValidationState == ModelValidationState.Invalid )
            {
                return View(dbcategory);
            }

            if (category.UpdatePhoto != null)
            {
                if (!category.UpdatePhoto.IsPhoto())
                {
                    ModelState.AddModelError("Photo", "Şəkil tipində olmalıdır");
                    return View();
                }

                if (!category.UpdatePhoto.PhotoSize(2))
                {
                    ModelState.AddModelError("Photo", "Şəkilin ölçüsü böyükdür");
                    return View();
                }

                string fileName = await category.UpdatePhoto.CopyPhoto(_env.WebRootPath, "banner");
                Utility.DeleteImage(_env.WebRootPath, dbcategory.CategoryImage);
                dbcategory.CategoryImage = fileName;

            }
            dbcategory.CategoryName = category.CategoryName;
           

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null) return NotFound();
            Category category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteImage(int? id)
        {
            Category category = await _context.Categories.FindAsync(id);
            Utility.DeleteImage(_env.WebRootPath, category.CategoryImage);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
           Category category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }
    }
}