using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        [Display(Name = "OrderID")]
        public int OrderId { get; set; }

        [Display(Name = "Customer ID")]
        [Required(ErrorMessage = "CustomerID is required")]
        public string CustomerId { get; set; }

        [StringLength(150)]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Address is required" )]
        public string Address { get; set; }

        [Required(ErrorMessage = " Amount is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid Double Number")]
        public double Amount { get; set; }

        [Display(Name = "Require Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = " Required Date is required")]
        public DateTime RequireDate { get; set; }
        
        [Display(Name = "Order Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = " Order Date is required")]
        public DateTime OrderDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public bool Status { get; set; }

        public Customer Customer { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
