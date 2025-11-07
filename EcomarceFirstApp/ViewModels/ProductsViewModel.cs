using EcomarceFirstApp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace EcomarceFirstApp.ViewModels
{
    public class ProductsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public double Price { get; set; }

        public string ImageUrl { get; set; }
       
        public string CategoryName { get; set; }
    }
}
