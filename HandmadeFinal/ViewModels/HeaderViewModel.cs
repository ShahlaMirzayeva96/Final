using HandmadeFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandmadeFinal.ViewModels
{
    public class HeaderViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Logo> Logos { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
