using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public int CateId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public bool Status { get; set; }

        public Category Cate { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
