using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HaberModuluProjesi.Entities
{
    public class News
    {
        public int Id { get; set; }
        [Display(Name ="Başlık"),StringLength(50), Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        public string Title { get; set; }
        [Display(Name = "Haber Metni"), Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        public string Description { get; set; }
        [Display(Name = "Resim")]
        public string? Image { get; set; }
        [Display(Name = "Aktif?")]
        public bool IsActive { get; set; } //Bu özellik sayesinde pasife çekilen haberler gösterimden kalkar.
        [Display(Name = "Güncel?")]
        public bool IsUpToDate { get; set; } //Bu özellik sayesinde eğer bir haber güncel veya son dakika ise slider veya son dakika haberleri kısmında gösterilebilir.
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }
        [Display(Name = "Kategori")]
        public virtual Category? Category { get; set; }
    }
}
