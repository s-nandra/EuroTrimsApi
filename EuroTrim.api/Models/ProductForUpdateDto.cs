using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EuroTrim.api.Models
{
    public class ProductForUpdateDto : ProductForManipulationDto
    {
        [Required(ErrorMessage = "Please enter a description")]
        public override string Description
        {
            get => base.Description;
            set => base.Description = value;
        }
    }
}
