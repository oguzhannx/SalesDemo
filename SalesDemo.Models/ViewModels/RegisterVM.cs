using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SalesDemo.Models.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Bu Alanı Doldurmak Zorunludur")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bu Alanı Doldurmak Zorunludur")]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

         [Required(ErrorMessage = "Bu Alanı Doldurmak Zorunludur")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bu Alanı Doldurmak Zorunludur")]
        [StringLength(100, ErrorMessage = "Minumum 6 karakter olmak zorunda", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Bu Alanı Doldurmak Zorunludur")]
        [Compare("Password", ErrorMessage = "Şifreler Eşleşmiyor")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Company Name")]
        [Required(ErrorMessage = "Bu Alanı Doldurmak Zorunludur")]
        public string CompanyName{ get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Bu Alanı Doldurmak Zorunludur")]
        public string Phone { get; set; }
    }
}
