using HandmadeFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandmadeFinal.ViewModels
{
    public class ProductsView
    {
        public List<Product> Products { get; set; }
        public Product Product { get; set; }
        public IEnumerable<BrandIcon> BrandIcons { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<ProductImage> ProductImages { get; set; }
    }
}