using EcomarceFirstApp.Data;
using Microsoft.AspNetCore.Mvc;
using EcomarceFirstApp.Models;

namespace EcomarceFirstApp.Areas.Admin.Controllers
{

    

    [Area("Admin")]
    public class CategoriesController : Controller

    {
        ApplicationDbContext Context = new ApplicationDbContext();

        
        public IActionResult Index()
        {
            var cats = Context.Categories.ToList();                
                
            return View(cats);
        }

        public IActionResult Create()
        {
            return View(new Category());
        }

        public IActionResult Store(Category request) {

            if (!ModelState.IsValid) {
                return View("Create", request);
            }
            Context.Categories.Add(request);
            Context.SaveChanges();
            return RedirectToAction(nameof(Index));


        }
        public IActionResult Edit(int id)
        {
            var cat = Context.Categories.Find(id);
            return View(cat);

        }

        public IActionResult Update(Category request)
        {
            if (!ModelState.IsValid) {
                return View("Edit", request);
            }
            var cat=Context.Categories.Find(request.Id);
            cat.Name = request.Name;
            cat.Description = request.Description;
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
