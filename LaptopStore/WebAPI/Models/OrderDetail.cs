using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public double Discount { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
