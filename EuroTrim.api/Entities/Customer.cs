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

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(80)]
        public string Company { get; set; }

        [MaxLength(50)]
        public int ContactNumber { get; set; }

        [MaxLength(50)]
        public string Address1 { get; set; }

        [MaxLength(50)]
        public string Address2 { get; set; }

        [MaxLength(20)]
        public string City { get; set; }

        [MaxLength(10)]
        public string PostCode { get; set; }

        public DateTime DateCreated { get; set; }

        public ICollection<Product> Product  { get; set; } = new List<Product>();
    }
}
