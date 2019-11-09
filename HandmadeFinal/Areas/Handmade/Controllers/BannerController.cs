
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandmadeFinal.DAL;
using Microsoft.AspNetCore.Mvc;
using HandmadeFinal.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using HandmadeFinal.Extention;
using Microsoft.AspNetCore.Hosting;
using HandmadeFinal.Utilities;

namespace HandmadeFinal.Areas.Handmade.Controllers
{[Area("Handmade")]
    public class BannerController : Controller
    {
        private AppDbContext _context;
        private IHostingEnvironment _env;
        public BannerController(AppDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;



        }
        public IActionResult Index()
        {
            return View(_context.HomeBanners);
        }

        public IActionResult AddItem()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> AddItem(HomeBanner homeBanner)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid || ModelState["Title"].ValidationState == ModelValidationState.Invalid || ModelState["Subtitle"].ValidationState == ModelValidationState.Invalid)
            {
                return View();
            }
            if (!homeBanner.Photo.IsPhoto())
            {
                ModelState.AddModelError("Photo", "Şəkil tipində olmalıdır!");
                return View();
            }

            if (!homeBanner.Photo.PhotoSize(3))
            {
                ModelState.AddModelError("Photo", "Şəkilin ölçüsü böyükdür");
                return View();
            }


            string fileName = await homeBanner.Photo.CopyPhoto(_env.WebRootPath,"banner");

            homeBanner.Image = fileName;
            await _context.HomeBanners.AddAsync(homeBanner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            HomeBanner homeSlider = await _context.HomeBanners.FindAsync(id);
            if (homeSlider == null) return NotFound();

            return View(homeSlider);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, HomeBanner image)
        {
            HomeBanner dbBanner = await _context.HomeBanners.FindAsync(id);

            if (dbBanner == null)
            {
                return (RedirectToAction(nameof(Index)));
            }
            if (ModelState["Title"].ValidationState == ModelValidationState.Invalid || ModelState["Subtitle"].ValidationState == ModelValidationState.Invalid)
            {
                return View(dbBanner);
            }

            if (image.UpdatePhoto != null)
            {
                if (!image.UpdatePhoto.IsPhoto())
                {
                    ModelState.AddModelError("Photo", "Şəkil tipində olmalıdır");
                    return View();
                }

                if (!image.UpdatePhoto.PhotoSize(2))
                {
                    ModelState.AddModelError("Photo", "Şəkilin ölçüsü böyükdür");
                    return View();
                }

                string fileName = await image.UpdatePhoto.CopyPhoto(_env.WebRootPath, "banner");
                Utility.DeleteImage(_env.WebRootPath, dbBanner.Image);
                dbBanner.Image = fileName;

            }
            dbBanner.Title = image.Title;
            dbBanner.Subtitle = image.Subtitle;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null) return NotFound();
            HomeBanner homeBanner = await _context.HomeBanners.FindAsync(id);
            if (homeBanner == null) return NotFound();

            return View(homeBanner);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public async Task<IActionResult> DeleteImage(int? id)
        {
            HomeBanner homeBanner = await _context.HomeBanners.FindAsync(id);
            Utility.DeleteImage(_env.WebRootPath, homeBanner.Image);
            _context.HomeBanners.Remove(homeBanner);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            HomeBanner homeBanner = await _context.HomeBanners.FindAsync(id);
            if (homeBanner == null) return NotFound();

            return View(homeBanner);
        }
    }
}