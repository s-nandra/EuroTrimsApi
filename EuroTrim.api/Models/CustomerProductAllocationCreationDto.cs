using EuroTrim.api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroTrim.api.Models
{
    public class CustomerProductAllocationCreationDto
    {

      
        public Guid CustomerId { get; set; }
 

        public Guid ProductId { get; set; }
         

        public int DiscountBandId { get; set; }

     
    }
}
