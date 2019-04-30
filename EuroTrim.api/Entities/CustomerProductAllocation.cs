﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using EuroTrim.api.Models;

namespace EuroTrim.api.Entities
{
    public class CustomerProductAllocation
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime DateOrderCreated { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public Guid CustomerId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }    

        public Guid ProductId { get; set; }

        [ForeignKey("DiscountBandId")]
        public DiscountBand DiscountBand { get; set; }

        public int DiscountBandId { get; set; }

        public decimal DiscountValue { get; set; }

    }
}
