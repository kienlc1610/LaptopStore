using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class OrderDetailViewModel
    {
        [Display(Name = "Order ID")]
        [Required(ErrorMessage = "Order ID is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Int Number")]
        public int OrderId { get; set; }

        [Display(Name = "Product ID")]
        [Required(ErrorMessage = "Product ID is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Int Number")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Discount is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid Double Number")]
        public double Discount { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid Int Number")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid Double Number")]
        public double UnitPrice { get; set; }
    }

}
