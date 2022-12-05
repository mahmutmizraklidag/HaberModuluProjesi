using HaberModuluProjesi.Data;
using HaberModuluProjesi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HaberModuluProjesi.Controllers
{
   
    public class LoginController : Controller
    {
        private readonly DatabaseContext _context;

        public LoginController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(AdminLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.IsActive && u.Email == model.Email && u.Password == model.password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Mail ve Şifre Bilgilerini Kontrol Ediniz veya Pasif Kullanıcı Girişi !");
                    
                }
                else
                {
                    try
                    {

                        var claims = new List<Claim>()
                        {
                        new Claim(ClaimTypes.Email,user.Email),
                        new Claim("Role",user.IsAdmin ? "Admin":"User"), //program.cs de tanımladığımız yetkilerle eğer giriş yapan admin özellikli değilse admin özelliği isteyen sayfalara giriş yapamayacak.
                        new Claim("UserId",user.Id.ToString()),
                        };
                        var userIdentiy = new ClaimsIdentity(claims, "Login");
                        ClaimsPrincipal principal = new(userIdentiy);
                        await HttpContext.SignInAsync(principal);
                        return Redirect("/Admin/Default");
                    }

                    catch
                    {
                        ModelState.AddModelError("", "Hata Oluştu");

                    }
                }
            }
            return View();
        }
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
