using System.ComponentModel.DataAnnotations;

namespace HaberModuluProjesi.Models
{
    public class AdminLoginViewModel
    {
        [Required(ErrorMessage = "Email alanı boş geçilemez!")]
        public string Email { get; set; }
        [Display(Name = "Şifre"), StringLength(30), Required(ErrorMessage = "Şifre alanı boş geçilemez!"), DataType(DataType.Password)]
        public string password { get; set; }
    }
}
