using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace insurance_partners_management_system.Models
{
    [Table("Policy")]
    public partial class Policy
    {
        [Key]
        public int IdPolicy { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 10)]
        public string PolicyNumber { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "PolicyAmount must be a positive value.")]
        public decimal PolicyAmount { get; set; }

        [Required]
        public int PartnerId { get; set; }

        [ForeignKey(nameof(PartnerId))]
        public virtual Partner Partner { get; set; }
    }
}