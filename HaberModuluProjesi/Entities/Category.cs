using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HaberModuluProjesi.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Display(Name = "Kategori Adı"), StringLength(50), Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        public string Name { get; set; }
        [Display(Name = "Aktif?")]
        public bool IsActive { get; set; }
        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)] 
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public virtual List<News> News { get; set; }
        public Category() //null referance hatası almamak için listeyi new ledim.
        {
            News = new List<News>();
        }

    }
}
