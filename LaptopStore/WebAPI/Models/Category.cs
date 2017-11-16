using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public partial class Category
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }

        [Display(Name = "CategoryID")]
        public int CateId { get; set; }

        [Display(Name = "CategoryName")]
        [Required(ErrorMessage = "Category Name is required")]
        public string Name { get; set; }

        [Display(Name = "Alias")]
        [Required(ErrorMessage = "Alias is required")]
        public string Alias { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
