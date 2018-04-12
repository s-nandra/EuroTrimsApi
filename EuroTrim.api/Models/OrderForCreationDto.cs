using System;


namespace EuroTrim.api.Models
{
    public class OrderForCreationDto
    {
        public DateTime DateOrderCreated { get; set; }

        public Guid ProductId { get; set; }
    }
}
