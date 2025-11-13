using EcomarceFirstApp.Data;
using EcomarceFirstApp.Models;
using EcomarceFirstApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcomarceFirstApp.Areas.Admin.Controllers
{

    

    [Area("Admin")]
    public class CategoriesController : Controller

    {
        ApplicationDbContext Context = new ApplicationDbContext();

        
        public IActionResult Index()
        {
            var cats = Context.Categories.ToList();
            var catsVm = cats.Select(cat => new CategoriesViewModel
            {
                Id = cat.Id,
                Name = cat.Name,
                Description = cat.Description,
                ImageUrl = Url.Content($"~/images/{cat.Image}"), 

            }

            ).ToList();
                
            return View(catsVm);
        }

        public IActionResult Create()
        {
            return View(new CategoriesViewModel());

        }

        [HttpPost]
        public IActionResult Store(CategoriesViewModel request)
        {
            if (!ModelState.IsValid)
                return View("Create", request);

            var cat = new Category
            {
                Name = request.Name,
                Description = request.Description
            };

            // رفع الصورة إن وجدت
            if (request.ImageFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(request.ImageFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    request.ImageFile.CopyTo(stream);
                }

                cat.Image = fileName;
            }

            Context.Categories.Add(cat);
            Context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var cat = Context.Categories.Find(id);

            var vm = new CategoriesViewModel
            {
                Id = cat.Id,
                Name = cat.Name,
                Description = cat.Description,
                ImageUrl = Url.Content($"~/images/{cat.Image}"),
                ExistingImage = cat.Image

            };

            return View(vm);
        }


        [HttpPost]
        public IActionResult Update(CategoriesViewModel request)
        {
            if (!ModelState.IsValid)
                return View("Edit", request);

            var cat = Context.Categories.Find(request.Id);

            cat.Name = request.Name;
            cat.Description = request.Description;

            // تحديث الصورة إذا رفع المستخدم واحدة جديدة
            if (request.ImageFile != null)
            {
                // حذف القديمة
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", cat.Image);
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);

                // رفع الجديدة
                var fileName = Guid.NewGuid() + Path.GetExtension(request.ImageFile.FileName);
                var newPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = System.IO.File.Create(newPath))
                {
                    request.ImageFile.CopyTo(stream);
                }

                cat.Image = fileName;
            }

            Context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            var cat=Context.Categories.Find(id);
            Context.Categories.Remove(cat);
            Context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var cat = Context.Categories.Find(id);

            return View(cat);


        }


    }
}
