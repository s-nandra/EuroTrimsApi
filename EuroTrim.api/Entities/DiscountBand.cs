using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EuroTrim.api.Entities
{
    public class DiscountBand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DiscountName { get; set; }
        public decimal DiscountValue { get; set; }
        public bool IsActive { get; set; }
        public string DiscountKey { get; set; }

    }
}