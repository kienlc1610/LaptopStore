﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class ProductViewModel
    {
        public ProductViewModel() { }

        public int CateId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }
        public double Discount { get; set; }

        public bool Status { get; set; }
    }
}
