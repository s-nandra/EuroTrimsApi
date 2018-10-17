using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroTrim.api.Models
{
    public class UpdateDto
    {
        public int UpdateId { get; set; }

 
        public string UserUpdate { get; set; }

 
        public UserDto User { get; set; }

        public DateTime DateCreated { get; set; }

         
        public string IP { get; set; }
    }
}
