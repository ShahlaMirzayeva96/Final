using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HandmadeFinal.ViewModels
{
    public class RegisterSellerView
    {
        [Required(ErrorMessage = "Zəhmət olmasa doldurun"), StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa doldurun"), StringLength(50)]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa doldurun"), StringLength(50)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa doldurun"), StringLength(50)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa doldurun"), StringLength(200), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa doldurun"), StringLength(200), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa doldurun"), StringLength(200), DataType(DataType.Password), Compare(nameof(Password),ErrorMessage ="Şifrəni düzgün daxil edin!")]
        public string ConfirmPassword { get; set; }
    }
}
