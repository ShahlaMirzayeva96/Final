using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandmadeFinal.DAL;
using HandmadeFinal.Extention;
using HandmadeFinal.Models;
using HandmadeFinal.Utilities;
using HandmadeFinal.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace HandmadeFinal.Areas.Handmade.Controllers
{
    [Area("Handmade")]
    public class ProductAdminController : Controller
    {
        private AppDbContext _context;
        private IHostingEnvironment _env;

        public ProductAdminController(AppDbContext context,IHostingEnvironment env)
        {
            _context = context;
            _env = env;

        }
        public IActionResult Index()
        {
            return View(_context.Products.Include(p=>p.Category).Include(p=>p.ProductImages));
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Product product = await _context.Products.Include(p=>p.ProductImages).Include(p=>p.Category).FirstOrDefaultAsync(p=>p.Id==id);
            if (product == null) return NotFound();

            return View(product);
        }
       
        public async Task< IActionResult >AddItem()
        {
            ViewBag.Category = await _context.Categories.ToListAsync();
           

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(Product product,ProductImage productImage)
        {
            if (ModelState["Name"].ValidationState == ModelValidationState.Invalid ||
         ModelState["Description"].ValidationState == ModelValidationState.Invalid ||
         ModelState["Count"].ValidationState == ModelValidationState.Invalid ||
         ModelState["Price"].ValidationState == ModelValidationState.Invalid ||
         ModelState["Text"].ValidationState == ModelValidationState.Invalid
         )
            {
                return View();
            }
            Product products = new Product()
            {

                Name = product.Name,
                Text = product.Text,
                Description = product.Description,
                Price = product.Price,
                Count = product.Count,


            };
            await _context.SaveChangesAsync();

            var productid = _context.Products.LastOrDefault();


            foreach (var item in productImage.Photo)
            {
                if (!item.IsPhoto())
                {
                    ModelState.AddModelError("Photo", "Secdiginiz fayl sekil formatinda deyil!!! Zehmet Olmasa Sekil secin");
                    return View();
                }

                if (!item.PhotoSize(1))
                {
                    ModelState.AddModelError("Photo", "Sekilin olcusu 1 mb dan az olmalidir");
                    return View();

                }

                string FileName = await item.CopyPhoto(_env.WebRootPath, "category");
                productImage.Image = FileName;
           

                
                
                await _context.ProductImages.AddRangeAsync(new ProductImage
                {
                    ProductId=productid.Id,
                    Image=productImage.Image

                });
                await _context.SaveChangesAsync();

            }

           
            return RedirectToAction(nameof(Index));
            // return Content("Create Olundu");


        }

        public async Task<IActionResult>Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
       Product proDb = await _context.Products.Include(p => p.ProductImages).Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (proDb == null) NotFound();
            ProductsView productsView = new ProductsView
            {
                Product= proDb,
                Categories = _context.Categories,
                ProductImages=_context.ProductImages.Where(p=>p.ProductId==id)


            };

            return View(productsView);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Product product, ProductImage productImage)
        {
            if (id == null) NotFound();

            Product proDb = await _context.Products.Include(p => p.ProductImages).Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (proDb == null) NotFound();
            if (ModelState.IsValid)
            {
                return View(proDb);
            }
            if (productImage.UpdatePhoto != null)
            {
                foreach (var item in productImage.UpdatePhoto)
                {
                    if (!item.IsPhoto())
                    {
                        ModelState.AddModelError("Photo", "Secdiginiz fayl sekil formatinda deyil!!! Zehmet Olmasa Sekil secin");
                        return View();
                    }

                    if (!item.PhotoSize(1))
                    {
                        ModelState.AddModelError("Photo", "Sekilin olcusu 1 mb dan az olmalidir");
                        return View();

                    }
                    string FileName = await item.CopyPhoto(_env.WebRootPath, "category");
                    Utility.DeleteImage(_env.WebRootPath, productImage.Image);

                    productImage.Image = FileName;

                }



            }
            proDb.Name = product.Name;
            proDb.Text = product.Text;
            proDb.Description = product.Description;
            proDb.Price = product.Price;
            proDb.Count = product.Count;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult>Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product proDb = await _context.Products.Include(p => p.ProductImages).Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (proDb == null) NotFound();
            ProductsView productsView = new ProductsView
            {
                Product = proDb,
                Categories = _context.Categories,
                ProductImages = _context.ProductImages.Where(p => p.ProductId == id)


            };

            return View(productsView);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null) NotFound();
            Product product = await _context.Products.Include(p => p.ProductImages).Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) NotFound();
            ProductImage productImage = await _context.ProductImages.FirstOrDefaultAsync(p => p.ProductId == id);
            if(productImage != null)
            {
                Utility.DeleteImage(_env.WebRootPath, productImage.Image);
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}