using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuroTrim.api.Entities
{
    public class Update
    {
        
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UpdateId { get; set; }

        [MaxLength(50)]
        public string UserUpdate { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public DateTime DateCreated { get; set; }

        [MaxLength(50)]
        public string IP { get; set; }
 
    }
}
