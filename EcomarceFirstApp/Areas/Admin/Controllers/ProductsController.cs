using EcomarceFirstApp.Data;
using EcomarceFirstApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcomarceFirstApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public IActionResult Index()
        {
            var products = context.Products.Include(p => p.Category).ToList();
            var productVm = new List<ProductsViewModel>();
            foreach(var item in products)
            {
                var vm = new ProductsViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    Discount = item.Discount,
                    ImageUrl = Url.Content($"~/images/{item.Image}"),
                    CategoryName = item.Category.Name

                };
                productVm.Add(vm);
            }
            return View(productVm);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = context.Categories.ToList();
            return View(new Product());
        }

        [ValidateAntiForgeryToken]
        public IActionResult Store(Product product,IFormFile file) { 
            if(file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString();
                fileName += Path.GetExtension(file.FileName);
;               var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
                product.Image = fileName;
                context.Products.Add(product);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = context.Categories.ToList();
            return View("Create", product);
        }

        public IActionResult Remove(int id)
        {
            var product = context.Products.Find(id);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", product.Image);
            System.IO.File.Delete(filePath);
            context.Products.Remove(product);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));

            
            
        }

        public IActionResult Edit(int id)
        {
            var product = context.Products.Find(id);
            ViewBag.Categories = context.Categories.ToList();
            return View(product);

        }

        public IActionResult Update(Product request,IFormFile?file)
        {
           

            var product = context.Products.Find(request.Id);
            product.Name=request.Name;
            product.Description=request.Description;
            product.Price = request.Price;
            product.Discount=request.Discount;
            product.Quantity = request.Quantity;
            product.CategoryId=request.CategoryId;
            product.IsTopSelling=request.IsTopSelling;
            if(file !=null && file.Length > 0)
            {
                var oldfilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", product.Image);
                System.IO.File.Delete(oldfilePath);
                var fileName = Guid.NewGuid().ToString();
                fileName += Path.GetExtension(file.FileName);
                ; var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", fileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
                product.Image = fileName;
            }
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
