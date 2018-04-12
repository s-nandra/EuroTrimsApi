using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace EuroTrim.api.Models
{
    public class CustomerForCreationDto
    {
        // public Guid Id { get; set; }
        [Required(ErrorMessage ="Name is required")]
        [MaxLength(100, ErrorMessage ="Name shouldn't exceed 100 characters")]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Decription { get; set; }
        public int ContactNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }

        public ICollection<OrderForCreationDto> Orders { get; set; }
            = new List<OrderForCreationDto>();
    }
}
