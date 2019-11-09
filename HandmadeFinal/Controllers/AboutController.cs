using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandmadeFinal.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeFinal.Controllers
{
    
    public class AboutController : Controller
    {
        private AppDbContext _context;
        public AboutController(AppDbContext context)
        {

            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Abouts.AsEnumerable());
        }
    }
}