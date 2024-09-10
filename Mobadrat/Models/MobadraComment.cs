
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Display(Name = "القرار")]
		public int StatusID { get; set; } // drop down value

		public int MobadraID { get; set; }

        public int UserId { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        public bool isDeleted { get; set; } = false;


		[ForeignKey("MobadraID")]
		public Mobadra Mobadra { get; set; }

		[ForeignKey("UserId")]
        public User User { get; set; }

		[ForeignKey("StatusID")]
		public Status Status { get; set; }
	}
}
