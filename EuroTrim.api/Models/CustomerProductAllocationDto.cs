using EuroTrim.api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroTrim.api.Models
{
    public class CustomerProductAllocationDto
    {

        public Guid Id { get; set; }

        public DateTime DateOrderCreated { get; set; }

 
        public Customer Customer { get; set; }

        public Guid CustomerId { get; set; }

        public Product Product { get; set; }

        public Guid ProductId { get; set; }

        public DiscountBand DiscountBand { get; set; }

        public Guid DiscountBandId { get; set; }

        public decimal DiscountValue { get; set; }
    }
}
