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

        public int Quantity { get; set; }

        public decimal BandA { get; set; }
        public decimal BandB { get; set; }
        public decimal BandC { get; set; }
        public decimal BandD { get; set; }
        public decimal BandE { get; set; }


        public virtual ICollection<DiscountBandDto> DiscountBands { get; set; }
    }
}
