﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroTrim.api.Models
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Decription { get; set; }
        public int ContactNumber { get; set; }
        public string Address1 { get; set; }

        public ICollection<ProductDto> Product { get; set; } 
            = new List<ProductDto>();
    }
}
