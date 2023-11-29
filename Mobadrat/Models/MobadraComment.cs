using System;
using System.ComponentModel.DataAnnotations;

namespace Mobadrat.Models
{
    public class MobadraComment
    {
        [Key]
        [Required]
        public int RecordID { get; set; }

        [Required]
        [Display(Name = "السبب")]
        public string MobadraCommet { get; set; }


        public int MobadraID { get; set; }

        public int CreateUser { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public string MobadratStatus { get; set; }
    }
}
