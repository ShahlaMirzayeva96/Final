using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HandmadeFinal.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa doldurun"), StringLength(100)]

        public string CategoryName { get; set; }

        public string CategoryImage { get; set; }
        [Required]
        [NotMapped]
        public IFormFile Photo { get; set; }
        [NotMapped]
        public IFormFile UpdatePhoto { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
