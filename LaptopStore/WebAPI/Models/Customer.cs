using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Order = new HashSet<Order>();
        }

        public string CustomerId { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public bool Activated { get; set; }

        public ICollection<Order> Order { get; set; }
    }
}
