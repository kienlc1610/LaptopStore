using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class FilterViewModel
    {
    }

    public class FilterProduct
    {
        public FilterProduct()
        {
            this.PageIndex = 0;
        }

        public int CateID { get; set; }
        public int PriceFrom { get; set; }
        public int PriceTo { get; set; }
        public float Discount { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }

    }
}
