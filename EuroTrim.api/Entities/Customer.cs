using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuroTrim.api.Entities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
 
        public string Name { get; set; }
        public string Decription { get; set; }
        public int ContactNumber { get; set; }
 
        public ICollection<Product> Product  { get; set; } = new List<Product>();
    }
}
