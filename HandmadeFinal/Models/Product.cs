using HandmadeFinal.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandmadeFinal.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa doldurun"), StringLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa doldurun"), StringLength(400)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa doldurun"), StringLength(1000)]
        public string Text { get; set; }  
        public string IconBag { get; set; }
        public string IconLike { get; set; }
        public bool Deleted { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa doldurun")]
        public int Count { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa doldurun")]
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts { get; set; }


        public static implicit operator BasketProduct(Product product)
        {
            return new BasketProduct
            {
                Id = product.Id,
                Name = product.Name,
                Details = product.Description,
                Price = product.Price,
                Image = product.ProductImages.FirstOrDefault()?.Image,
                Quantity = 1
            };
        }
    }
}
