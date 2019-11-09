using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HandmadeFinal.Models
{
    public class HomeBanner
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Zəhmət olmasa doldurun"),StringLength(100,ErrorMessage ="100-dən artıq simvol istifadə etmək olmaz")]
        public string Title{ get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa doldurun"), StringLength(30, ErrorMessage = "30-dən artıq simvol istifadə etmək olmaz")]
         public string Subtitle { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa doldurun"), StringLength(300)]
        public string Image { get; set; }
        [NotMapped]
        [Required]
        public IFormFile Photo { get; set; }
        [NotMapped]
        public IFormFile UpdatePhoto { get; set; }
    }
}
