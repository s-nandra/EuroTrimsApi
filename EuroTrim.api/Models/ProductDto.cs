using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroTrim.api.Models
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string PartNo { get; set; }

        public string ProdName { get; set; }

        public string Description { get; set; }

        public CategoryDto Category { get; set; }
        // Category 

        //Sub-Category 

        public int Per { get; set; }
        public string Colour { get; set; }
        public string Size { get; set; }
        public decimal BuyPrice { get; set; }
        public decimal DutyPrice { get; set; }
        public decimal ListPrice { get; set; }
        public decimal Discount1 { get; set; }
        public decimal Discount2 { get; set; }
        public decimal Discount3 { get; set; }
        public decimal Discount4 { get; set; }




    }
}
