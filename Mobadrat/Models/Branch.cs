using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mobadrat.Models
{
    public class Branch
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int BranchID { get; set; }

        [Required]
        [Display(Name = "الفرع")]
        public string BranchName { get; set; }
    }
}
