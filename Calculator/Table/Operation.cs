using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Calculator.Table
{
    [Table("Operations")]
    public class Operation
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OperationId { get; set; }
        [Required]
        [StringLength(1, MinimumLength = 1)]
        public string Operator { get; set; }
        [Required]
        public DateTime LastUpdate { get; set; }
        public int CountLastMonth { get; set; }


    }
}
