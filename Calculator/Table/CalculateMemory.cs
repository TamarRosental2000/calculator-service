using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Calculator.Table
{
    public class CalculateMemory
    {
        [Key]

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }
        [StringLength(80, MinimumLength = 1)]
        public string FieldA { get; set; }
        [StringLength(80, MinimumLength = 1)]
        public string FieldB { get; set; }
        [StringLength(1200, MinimumLength = 1)]
        public string Result { get; set; }
        [Required]
        public Operation Operation { get; set; }
        [Required]
        public DateTime CalculateDate { get; set; }

    }
}
