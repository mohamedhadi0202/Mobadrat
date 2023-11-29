using System.ComponentModel.DataAnnotations;

namespace Mobadrat.Models
{
    public class Sex
    {
        [Key]
        [Required]
        public int SexID { get; set; }

        [Required]
        [Display(Name = "الجنس")]
        public string SexName { get; set; }
    }
}

