using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EuroTrim.api.Entities
{
    public class CustomerOrder
    {

        [Key]
        public Guid Id { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public Guid ProductId { get; set; }

        public DateTime DateOrdered { get; set; }
    }
}
