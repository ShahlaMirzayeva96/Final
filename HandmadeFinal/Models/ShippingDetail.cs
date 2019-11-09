using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandmadeFinal.Models
{
    public class ShippingDetail
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa doldurun"), StringLength(70, ErrorMessage = "100-dən artıq simvol istifadə etmək olmaz")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa doldurun"), StringLength(100, ErrorMessage = "100-dən artıq simvol istifadə etmək olmaz")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa doldurun"), StringLength(200)]
        public string Icon { get; set; }
    }
}
