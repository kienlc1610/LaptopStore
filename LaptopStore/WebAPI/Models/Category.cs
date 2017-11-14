using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Category
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }

        public int CateId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
