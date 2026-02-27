using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniCMS.Web.Data;
using MiniCMS.Web.Models;

namespace MiniCMS.Web.Controllers
{
    // Public MVC controller (no [Authorize]) for viewing articles
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ArticlesController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET /Articles
        public async Task<IActionResult> Index()
        {
            var articles = await _db.Articles
                .AsNoTracking()
                .OrderByDescending(a => a.UpdatedAt)
                .ToListAsync();

            return View(articles);
        }

        // GET /Articles/{id}
        public async Task<IActionResult> Details(int id)
        {
            var article = await _db.Articles
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
                return NotFound();

            return View(article);
        }
    }
}