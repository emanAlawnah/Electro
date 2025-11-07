using EcomarceFirstApp.Data;
using EcomarceFirstApp.Models;
using EcomarceFirstApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcomarceFirstApp.Areas.User.Controllers
{
    [Area("User")]
    public class ProductsController : Controller
    {
       ApplicationDbContext context = new ApplicationDbContext();

        public IActionResult Category(int id)
        {

            var cat = context.Categories.Find(id);
            if (cat == null)
            return NotFound();


            var products = context.Products.Include(p => p.Category).Where(p => p.CategoryId == id).ToList();
            var productVm = new List<ProductsViewModel>();

            foreach(var item in products)
            {
                var vm = new ProductsViewModel
                {
                    Id=item.Id,
                    Name=item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    ImageUrl = $"{Request.Scheme}://{Request.Host}/images/{item.Image}",
                    CategoryName = item.Category.Name
                };
                productVm.Add(vm);
                
            }

            ViewBag.CategoryName = cat.Name;
            return View(productVm);


        }

    }
}
