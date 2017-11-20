using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Order = new HashSet<Order>();
            CustomerId = Guid.NewGuid().ToString();
        }

        [Display(Name = "Customer ID")]
        [Required(ErrorMessage = "Customer ID is required")]
        public string CustomerId { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Password is required")]      
        [Range(6, 16, ErrorMessage = "Price must be between {1} and {2} character")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public string Password { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(20)]
        public string Fullname { get; set; }

        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        
        [Display(Name ="Activated")]
        public bool Activated { get; set; }

        public ICollection<Order> Order { get; set; }
    }
}
