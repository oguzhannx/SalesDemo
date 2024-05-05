using System.ComponentModel.DataAnnotations;

namespace SalesDemo.Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Bu Alanı Doldurmak Zorunludur")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Bu Alanı Doldurmak Zorunludur")]
        [StringLength(100, ErrorMessage = "Minumum 6 karakter olmak zorunda", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
