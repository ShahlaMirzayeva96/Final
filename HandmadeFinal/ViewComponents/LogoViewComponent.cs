using HandmadeFinal.DAL;
using HandmadeFinal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandmadeFinal.ViewComponents
{
    public class LogoViewComponent:ViewComponent
    {
        private AppDbContext _context;
        public LogoViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            HeaderViewModel headerViewModel = new HeaderViewModel
            {
                Logos = _context.Logos,
                Categories=_context.Categories,
                Products=_context.Products
            };
           
            return View(headerViewModel);
        }
       
    }
}
