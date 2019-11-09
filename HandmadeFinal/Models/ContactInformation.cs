using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandmadeFinal.Models
{
    public class ContactInformation
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa doldurun"), StringLength(50)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa doldurun"), StringLength(50)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa doldurun"), StringLength(80)]
        public string Address { get; set; }
    }
}
