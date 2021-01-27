using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2Core.ViewModel
{
    public class ProductCreateViewModel
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public IFormFile Photopath { get; set; }

        public bool IsActive { get; set; }
    }
}
