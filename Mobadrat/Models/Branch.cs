using System.ComponentModel.DataAnnotations;

namespace Mobadrat.Models
{
    public class Branch
    {
        [Key]
        [Required]
        public int BranchID { get; set; }

        [Required]
        [Display(Name = "الفرع")]
        public string BranchName { get; set; }
    }
}
