using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroTrim.api.Models
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public DateTime DateOrderCreated { get; set; }

        public Guid CustomerId { get; set; }

        public Guid ProductId { get; set; }
    }
}
