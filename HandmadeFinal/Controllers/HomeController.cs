using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandmadeFinal.DAL;
using HandmadeFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeFinal.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeModel homemodel = new HomeModel
            {
                HomeBanners=_context.HomeBanners,
                ShippingDetails=_context.ShippingDetails,
                BrandIcons=_context.BrandIcons,
                Categories=_context.Categories

            };
            return View(homemodel);
        }

        public IActionResult Search(string str)
        {
            HeaderViewModel headerViewModel = new HeaderViewModel
            {
                Logos = _context.Logos,
                Categories = _context.Categories,
                Products = _context.Products.Where(p => p.Name.Contains(str)).Take(4)
       
            };
           
            return PartialView("_SearchPartialView", headerViewModel);
        }


        public IActionResult ErrorPage()
        {
            return View();
        }
    }
}