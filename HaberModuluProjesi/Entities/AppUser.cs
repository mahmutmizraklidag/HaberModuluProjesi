using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HaberModuluProjesi.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        [Display(Name = "Adı"), Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        public string Name { get; set; }
        [Display(Name = "Soyadı"), Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        public string Surname { get; set; }
        [Display(Name = "Email"), Required(ErrorMessage = "{0} alanı boş geçilemez!"), EmailAddress(ErrorMessage ="Lütfen mail formatında bir veri giriniz!")]
        public string Email { get; set; }
        [Display(Name = "Telefon"),DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
        [Display(Name = "Kullanıcı Adı")]
        public string? Username { get; set; }
        [Display(Name = "Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez!"), MinLength(5, ErrorMessage = "Şifre 5 karekterden az olamaz!")]
        public string Password { get; set; }
        [Display(Name = "Şifre Tekrar"), Required(ErrorMessage = "{0} alanı boş geçilemez!"),MinLength(5, ErrorMessage = "Şifre 5 karekterden az olamaz!"),Compare("Password",ErrorMessage ="Girilen Şifreler Aynı Olmalıdır!")]
        public string confirmPassword { get; set; }

        [Display(Name = "Aktif?")]
        public bool IsActive { get; set; }
        [Display(Name = "Admin?")]
        public bool IsAdmin { get; set; } //Bu özellik sayesinde Admin tipindeki kullanıcılar admin paneline giriş veya admin panelindeki bazı sayfalara sadece admin özelliği olanlar girebilsin şartını yeriene getirmiş olacak.
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
    }
}
