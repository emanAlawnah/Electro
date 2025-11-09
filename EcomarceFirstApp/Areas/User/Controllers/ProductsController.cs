using EcomarceFirstApp.Data;
using EcomarceFirstApp.Models;
using EcomarceFirstApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcomarceFirstApp.Areas.User.Controllers
{
    [Area("User")]
    public class ProductsController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public IActionResult Category(int id, int page = 1)
        {
            int pageSize = 8; // عدد المنتجات في كل صفحة

            var cat = context.Categories.Find(id);
            if (cat == null)
                return NotFound();

            // اجلب المنتجات الخاصة بالفئة المطلوبة
            var query = context.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == id)
                .OrderByDescending(p => p.DateAdded);

            // حساب إجمالي المنتجات وعدد الصفحات
            int totalProducts = query.Count();
            int totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

            // جلب المنتجات حسب الصفحة
            var products = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // تحويلها إلى ViewModel
            var productVm = products.Select(item => new ProductsViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                ImageUrl = $"{Request.Scheme}://{Request.Host}/images/{item.Image}",
                CategoryName = item.Category.Name
            }).ToList();

            // تمرير البيانات إلى الواجهة
            ViewBag.CategoryName = cat.Name;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.CategoryId = id;

            return View(productVm);
        }
    }
}
