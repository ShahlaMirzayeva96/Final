using HandmadeFinal.DAL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandmadeFinal.ViewComponents
{
    public class InfoViewComponent:ViewComponent
    {
        private AppDbContext _context;
        public InfoViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var info = _context.ContactInformations.FirstOrDefault();
            return View(await Task.FromResult(info));

        }
    }
}
