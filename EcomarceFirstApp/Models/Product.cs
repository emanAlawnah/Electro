using EcomarceFirstApp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace EcomarceFirstApp.Data
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "product name is Required ")]
        [MinLength(3,ErrorMessage ="the product name must be at least 3 char")]
        [MaxLength(20,ErrorMessage ="the product name must be maximum 20 char")]
        public string Name { get; set; }
        [Required(ErrorMessage ="product description is requierd")]
        [MinLength(10,ErrorMessage ="the description must be at least 10 char or more")]
        public string Description { get; set; }
        [Required(ErrorMessage = "product price is requierd")]
        [Range (.01,int.MaxValue,ErrorMessage ="the product price must be more than .01")]
        public double Price { get; set; }
        [Range(1,5)]
        public int Rate { get; set; }
        [Required(ErrorMessage ="product quantity is required")]
        [Range(1,int.MaxValue)]
        public int Quantity { get; set; }
        [ValidateNever]
        public string Image {  get; set; }
        [ValidateNever]

        public int CategoryId { get; set; }

        public bool IsTopSelling { get; set; }
      
        [Range(1,100)]
        public double? Discount {  get; set; }
        public double SalePrice => Price - (Price * (Discount ?? 0) / 100);
        public DateTime DateAdded { get; set; } = DateTime.Now;
        [ValidateNever]

        public Category Category { get; set; }


    }
}
