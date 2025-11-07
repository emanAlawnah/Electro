using EcomarceFirstApp.Data;
using System.ComponentModel.DataAnnotations;

namespace EcomarceFirstApp.Models
{
    public class Category
    {
        public int Id { get; set; }

        [MinLength(3)]
        [Required]
        [MaxLength(15)]
        public string Name { get; set; }

       
        [MaxLength(200)]
        public string? Description { get; set; }
        List<Product> Products { get; set; }

    }
}
