using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public string Address { get; set; }
        public double Amount { get; set; }
        public DateTime RequireDate { get; set; }
        public DateTime OrderDate { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }

        public Customer Customer { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
