using HaberModuluProjesi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HaberModuluProjesi.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class DefaultController : Controller
    {
        private readonly DatabaseContext _context;

        public DefaultController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Search(string word)
        {
            var search = await _context.News.Where(s=>s.Title.Contains(word)||s.Description.Contains(word)).ToListAsync(); //başlık veya açıklama kısmında aranan kelime varsa listeye ekleyip sayfaya gönderdik.
            return View(search);
        }
    }
}
