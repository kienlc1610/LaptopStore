using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        [Display(Name="Product ID")]
        public int ProductId { get; set; }

        [Display(Name="Category ID")]
        [Required(ErrorMessage = "Category ID is required")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public int CateId { get; set; }

        [Display(Name="Product Name")]
        [Required(ErrorMessage = " Product Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = " Price is required")]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid Double Number")]
        public double Price { get; set; }

        public string Image { get; set; }

        [StringLength(150)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = " Quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Int Number")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = " Discount is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid Double Number")]
        public double Discount { get; set; }

        public bool Status { get; set; }

        public Category Cate { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
