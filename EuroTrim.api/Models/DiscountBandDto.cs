using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroTrim.api.Models
{
    public class DiscountBandDto
    {
        public int Id { get; set; }
        public string DiscountName { get; set; }
        public decimal DiscountValue { get; set; }
        public string DiscountKey { get; set; }
    }
}
