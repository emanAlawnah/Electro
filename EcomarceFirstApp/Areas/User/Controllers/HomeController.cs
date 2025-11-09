using EcomarceFirstApp.Data;
using EcomarceFirstApp.Models;
using EcomarceFirstApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcomarceFirstApp.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApplicationDbContext _context = new ApplicationDbContext();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            ViewBag.cats = _context.Categories.ToList();
            var latestProducts = _context.Products.OrderByDescending(p => p.DateAdded).Take(5).ToList();
            var latestProductsVm = latestProducts.Select(p => new ProductsViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryName = p.Category.Name,
                IsTopSelling=p.IsTopSelling,
                ImageUrl = $"{Request.Scheme}://{Request.Host}/images/{p.Image}"
            }).ToList();
            var topSellingProducts = _context.Products.Where(p => p.IsTopSelling).OrderByDescending(p => p.DateAdded).Select(p => new ProductsViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Discount = p.Discount,
                ImageUrl = $"{Request.Scheme}://{Request.Host}/images/{p.Image}",
                CategoryName = p.Category.Name,
                IsTopSelling = p.IsTopSelling
            }).Take(12).ToList();
            
            ViewBag.latestProducts = latestProductsVm;
            ViewBag.TopSellingProducts = topSellingProducts;

            return View("Index");


        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
