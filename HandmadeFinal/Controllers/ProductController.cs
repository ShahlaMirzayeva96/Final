using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandmadeFinal.DAL;
using HandmadeFinal.Models;
using HandmadeFinal.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HandmadeFinal.Controllers
{
    public class ProductController : Controller
    {
        private AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Index( int? categoryid)
        {
          List<Product> product = _context.Products
                
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Where(p => categoryid == null ? true : p.CategoryId == categoryid).ToList();
            ProductsView productsView = new ProductsView
            {
                Products = product,
                BrandIcons = _context.BrandIcons,
                Categories = _context.Categories,
                ProductImages = _context.ProductImages
            };
            ViewBag.ProductCount =product.Count();

            return View(productsView);
        }
         
      
     

        public IActionResult CategoryAside(int? categoryid,int skip)
        {
            List<Product> product = _context.Products

               .Include(p => p.Category)
               .Include(p => p.ProductImages)
               .Where(p => categoryid == null ? true : p.CategoryId == categoryid).Skip(skip).Take(2).ToList();
            ProductsView productsView = new ProductsView
            {
                Products = product,
                BrandIcons = _context.BrandIcons,
                Categories = _context.Categories,
                ProductImages = _context.ProductImages
            };
            return PartialView("_LoadProductPartialView",productsView);
        }


        

       

        public async Task<IActionResult> AddBasket(int id)
        {
            Product product = await _context.Products.Include(p=>p.ProductImages).FirstOrDefaultAsync(p=>p.Id==id);
            BasketProduct basketProduct = product;

            
            string cart = HttpContext.Session.GetString("cart");

            List<BasketProduct> products = new List<BasketProduct>();

            if (cart != null)
            {
                products = JsonConvert.DeserializeObject<List<BasketProduct>>(cart);
            }

            var selected = products.FirstOrDefault(p => p.Id == id);

            if (selected == null)
            {
                products.Add(basketProduct);
            }
            else
            {
                selected.Quantity += 1;
            }
        
            string productsJSON = JsonConvert.SerializeObject(products);
            HttpContext.Session.SetString("cart", productsJSON);

            return RedirectToAction("Index","Product");
        }

        public IActionResult RemoveBasket(int id)
        {


            string cart = HttpContext.Session.GetString("cart");

            List<Product> products = new List<Product>();

            if (cart != null)
            {
                products = JsonConvert.DeserializeObject<List<Product>>(cart);
            }

            Product product = products.FirstOrDefault(p => p.Id == id);
            products.Remove(product);


            string productsJSON = JsonConvert.SerializeObject(products);
            HttpContext.Session.SetString("cart", productsJSON);

            return RedirectToAction("Basket", "Product");
        }



        public IActionResult Basket()
        {
            string cart = HttpContext.Session.GetString("cart");
           List<BasketProduct> basketProducts = new List<BasketProduct>();
            if (cart != null)
            {
                basketProducts = JsonConvert.DeserializeObject<List<BasketProduct>>(cart);
            }
            return View(basketProducts);

            
        }

       
        public async Task<IActionResult> SingleProduct(int ? id)
        {
            if (id == null) return NotFound();
            
            Product proId = await _context.Products.Include(p => p.Category).Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == id);
            if (proId == null) return NotFound();
           

            return View(proId);
        }

        //public IActionResult Categories(int skip)
        //{
        //    var model = _context.Products.Skip(skip).Take(2);
        //    return PartialView("_LoadProductPartialView", model);
        //}
    }
}

