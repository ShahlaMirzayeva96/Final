using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandmadeFinal.Models;

namespace HandmadeFinal.ViewModels
{
    public class BasketProduct
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Details { get; set; }
        public double Price { get; set; }
        public byte Discount { get; set; }
        public int Quantity { get; set; }

        public string Image { get;set; }

    
    }
}
