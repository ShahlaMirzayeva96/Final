using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HandmadeFinal.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa doldurun")]
        public string Image { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required(ErrorMessage = "Zəhmət olmasa doldurun")]
        [NotMapped]
        public List<IFormFile> Photo{ get; set; }
        [NotMapped]
        public List<IFormFile> UpdatePhoto { get; set; }

    }
}
