using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HaberModuluProjesi.Data;
using HaberModuluProjesi.Entities;
using HaberModuluProjesi.Utils;
using Microsoft.AspNetCore.Authorization;

namespace HaberModuluProjesi.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Policy = "AdminPolicy")]
    public class NewsController : Controller
    {
        private readonly DatabaseContext _context;

        public NewsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/News
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.News.Include(n => n.Category);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Admin/News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: Admin/News/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(c=>c.IsActive), "Id", "Name"); //kategori listesini ekrana gönderirken koymuş olduğumuz ısActive özelliği sayesinde sadece aktif kategorileri listeledik.
            return View();
        }

        // POST: Admin/News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(News news,IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                if(Image is not null) news.Image=await FileHelper.FileLoaderAsync(Image);
                await _context.AddAsync(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(c => c.IsActive), "Id", "Name", news.CategoryId);
            return View(news);
        }

        // GET: Admin/News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(c => c.IsActive), "Id", "Name", news.CategoryId);
            return View(news);
        }

        // POST: Admin/News/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,News news,IFormFile? Image,bool? deleteImage)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (deleteImage == true) //eğer yüklenen resmi kaldırmak istersek bu method devreye girecek.
                    {
                        news.Image = string.Empty;
                    }
                    if (Image is not null) news.Image = await FileHelper.FileLoaderAsync(Image); // FileHelper classında FileLoaderAsync mothodunu yazarak sürekli kullanacağımız resim yükleme metodunu tekrar tekrar yazmaktan kurtulduk.
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch 
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(c => c.IsActive), "Id", "Name", news.CategoryId);
            return View(news);
        }

        // GET: Admin/News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: Admin/News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.News == null)
            {
                return Problem("Entity set 'DatabaseContext.News'  is null.");
            }
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
