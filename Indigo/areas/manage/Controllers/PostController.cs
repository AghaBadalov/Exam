using Indigo.DAL;
using Indigo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Indigo.areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles ="SuperAdmin,Admin")]
    public class PostController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PostController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Post> posts = _context.Posts.ToList();

            return View(posts);
        }
        public IActionResult Create()
        {
            Post post = new Post();
            return View(post);
        }
        [HttpPost]
        public IActionResult Create(Post post)
        {
            if (!ModelState.IsValid) return View();
            if (post.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Image can't be null");
                return View();
            }
            if(post.ImageFile.ContentType!="image/png" && post.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Wrong file type , files must be png ,jpeg or jpg");
                return View();
            }
            if (post.ImageFile.Length> 2097152)
            {
                ModelState.AddModelError("ImageFile", "Wrong file size , file's size  must be 2mb or lower");
                return View();
            }

            string filename = post.ImageFile.FileName;
            if (filename.Length > 64)
            {
                filename=filename.Substring(filename.Length-64,64);
            }
            filename=Guid.NewGuid().ToString()+filename;
            string path = Path.Combine(_env.WebRootPath, "uploads/posts", filename);
            using(FileStream fileStream=new FileStream(path, FileMode.Create))
            {
                post.ImageFile.CopyTo(fileStream);
            }
            post.ImageUrl= filename;
            _context.Posts.Add(post);
            _context.SaveChanges();
            


            return RedirectToAction("index","post");
        }
        public IActionResult Update(int id)
        {
            Post post = _context.Posts.FirstOrDefault(x => x.Id == id);
            if (post is null) return NotFound();

            return View(post);
        }
        [HttpPost]
        public IActionResult Update(Post post)
        {
            if (!ModelState.IsValid) return View();
            Post exstpost=_context.Posts.FirstOrDefault(x => x.Id == post.Id);
            if(exstpost is null) return NotFound();
            if(post.ImageFile != null)
            {
                if (post.ImageFile.ContentType != "image/png" && post.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "Wrong file type , files must be png ,jpeg or jpg");
                    return View();
                }
                if (post.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Wrong file size , file's size  must be 2mb or lower");
                    return View();
                }
                string path1 = Path.Combine(_env.WebRootPath, "uploads/posts", exstpost.ImageUrl);
                if (System.IO.File.Exists(path1))
                {
                    System.IO.File.Delete(path1);
                }
                string filename = post.ImageFile.FileName;
                if (filename.Length > 64)
                {
                    filename = filename.Substring(filename.Length - 64, 64);
                }
                filename = Guid.NewGuid().ToString() + filename;
                string path = Path.Combine(_env.WebRootPath, "uploads/posts", filename);
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    post.ImageFile.CopyTo(fileStream);
                }
                exstpost.ImageUrl = filename;
            }

            exstpost.Desc = post.Desc;
            exstpost.Tittle = post.Tittle;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            Post post = _context.Posts.FirstOrDefault(x => x.Id == id);
            if (post == null) return NotFound();
            string path1 = Path.Combine(_env.WebRootPath, "uploads/posts", post.ImageUrl);
            if (System.IO.File.Exists(path1))
            {
                System.IO.File.Delete(path1);
            }
            _context.Posts.Remove(post);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
