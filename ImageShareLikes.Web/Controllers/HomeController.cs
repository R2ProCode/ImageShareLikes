using ImageShareLikes.Data;
using ImageShareLikes.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ImageShareLikes.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;
        public HomeController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            var repo = new ImagesRepository(_configuration.GetConnectionString("ConStr"));

            return View(new IndexViewModel
            {
                Images = repo.GetImages()
            });
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile image, string title)
        {
            var repo = new ImagesRepository(_configuration.GetConnectionString("ConStr"));

            string fileName = $"{Guid.NewGuid()}-{image.FileName}";
            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileName);

            using FileStream fs = new FileStream(fullPath, FileMode.Create);
            image.CopyTo(fs);

            var uploadedImage = new Image
            {
                FileName = fileName,
                Title = title,
                DateCreated = DateTime.Now,
                Likes = 0
            };

            repo.AddImage(uploadedImage);

            return RedirectToAction("Index");
        }

        public IActionResult ViewImage(int id)
        {
            var repo = new ImagesRepository(_configuration.GetConnectionString("ConStr"));

            var viewModel = new ViewImageViewModel
            {
                Image = repo.GetImageById(id),
                LikedAlready = HttpContext.Session.Get<List<int>>("images-ids-list")
            };

            if (viewModel.Image == null)
            {
                return RedirectToAction("Index");
            }

            return View(viewModel);
        }

        [HttpPost]
        public void UpdateImageLikes(int id)
        {
            var repo = new ImagesRepository(_configuration.GetConnectionString("ConStr"));
            repo.UpdateImageLikes(id);

            List<int> likedImagesId = HttpContext.Session.Get<List<int>>("images-ids-list") ?? new List<int>();
            likedImagesId.Add(id);
            HttpContext.Session.Set("images-ids-list", likedImagesId);
        }

        public IActionResult GetLikes(int id)
        {
            var repo = new ImagesRepository(_configuration.GetConnectionString("ConStr"));
            return Json(repo.GetImageLikes(id));
        }
    }
}
