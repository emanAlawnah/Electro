using EcomarceFirstApp.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace EcomarceFirstApp.ViewModels
{
    public class CategoriesViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        public string? ImageUrl { get; set; }
        public string? ExistingImage { get; set; }

        public IFormFile? ImageFile { get; set; } 
    }

}
