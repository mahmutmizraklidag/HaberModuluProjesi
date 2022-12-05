using HaberModuluProjesi.Controllers;
using HaberModuluProjesi.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace HaberModuluProjesi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<DatabaseContext>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
            {
                x.LoginPath = "/Login"; //login gerektiren sayfalar bu actiona istek gidecek ve oturum açma talep edilcek.
                x.AccessDeniedPath = "/Home/AccessDenied"; //yetkisiz biri yetki isteyen bir sayfaya gitmek isterse bu ssayfaya yönlendirilecek.
                x.LogoutPath = "/Logout"; //oturumu kapatmak için bo actiona istek gidecek.
                x.Cookie.Name = "Admin";
                x.Cookie.MaxAge = TimeSpan.FromHours(1); //oluþan cookie 1 saat çalýþýcak sonra otomatik olarak oturum kapatýlacak.
                x.Cookie.IsEssential = true;
            });

            builder.Services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminPolicy", p => p.RequireClaim("Role", "Admin"));
                x.AddPolicy("UserPolicy", p => p.RequireClaim("Role", "User"));
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Default}/{action=Index}/{id?}"
          );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}