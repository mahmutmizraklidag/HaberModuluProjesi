using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HaberModuluProjesi.Data;
using HaberModuluProjesi.Entities;

namespace HaberModuluProjesi.Controllers
{

    public class AppUsersController : Controller
    {
        private readonly DatabaseContext _context;

        public AppUsersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/AppUsers
        public IActionResult Index()
        {
            return View();
        }

        // GET: Admin/AppUsers/Create
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                var control = await _context.AppUsers.FirstOrDefaultAsync(c=>c.Email==appUser.Email);
                if (control!=null)
                {
                    ModelState.AddModelError("", "Mail adresi daha önce kullanılmış! Lütfen farklı bir mail adresi deneyin.");
                }
                else
                {
                    _context.Add(appUser);
                    var user = await _context.SaveChangesAsync();
                    if (user > 0)
                    {
                        TempData["mesaj"] = "<div class='alert alert-success'>Üyeliğiniz başarıyla oluşturulmuştur...</div>";
                    }

                    return RedirectToAction(nameof(Create));
                }
                
            }
            return View(appUser);
        }

        public IActionResult ForgotPassword()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var account = await _context.AppUsers.FirstOrDefaultAsync(a => a.Email == email);
            if (account == null)
            {
                ModelState.AddModelError("", "Mail adresi bulunamadı! Lütfen mail adresinizi kontrol ediniz.");
            }
            else
            {
                TempData["account"] = account.Id; 
                return RedirectToAction(nameof(Edit));
                //normal şartlarda bu tarzda bir şifremi unuttum sistemi kullanılmıyor her kullanıcıya ait guid değeriyle bir mail gönderilip o mail aracılığıyla şifremi unuttum sistem çalışıyor.Bu test case de böyle bir durum mümküm olmadığından ben sırf fonksiyon aktif çalışabilsin diye ekrandan aldığım mail bilgisini veri tabanından taratıp eğer kullanıcı varsa bu kullanıcının Id sini TempData ile edit actionuna gönderip güncelleme işlemini sıkıntısız şekilde çalışır duruma getirdim.
            }
                
            
            return View();
        }


        // GET: Admin/AppUsers/Edit/5
        public async Task<IActionResult> Edit()
        {
            var account = TempData["account"];
            var user = await _context.AppUsers.FindAsync(account);
            return View(user);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,AppUser appUser)
        {
            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appUser);
                    await _context.SaveChangesAsync();

                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
                TempData["sifre"] = "<div class='alert alert-success'>Şifreniz başarıyla Değiştirilmiştir...</div>";
                return RedirectToAction(nameof(ForgotPassword));
            }
            return View(appUser);
        }
    }
}
