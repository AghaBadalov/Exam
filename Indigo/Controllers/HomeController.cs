using Indigo.DAL;
using Indigo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Indigo.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            List<Post> posts = _dbContext.Posts.ToList();

            return View(posts);
        }

        
    }
}