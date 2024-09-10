using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Mobadrat.Models
{
    public class Mobadra
    {
        [Key]
        [Display(Name = "Mobadra ID")]
        public int MobadraID { get; set; }

        [Required]
        [Display(Name = "عنوان المبادرة")]
        public string MobadraTitle { get; set; }

        [Required]
        [Display(Name = "وصف المبادرة")]
        public string MobadraDesc { get; set; }

        [Required]
        [Display(Name = "الفرع")]
        public int BranchID { get; set; } // Lookup Table

        [Required]
        [Display(Name = "الإدارة")]
        public int DepartmentId { get; set; } // Lookup Table

        [Required]
        [Display(Name = "الفئة المستهدفة")]
        public int Geha_TragetID { get; set; } // Lookup Table

        [Required]
        [Display(Name = "هدف المبادرة")]
        public string MobadraTarget { get; set; }

        [Required]
        [Display(Name = "آلية تنفيذ المبادرة")]
        public string MobadraImplement { get; set; }

        [Required]
        [Display(Name = "مشاركة المتطوعين")]
        public int VolunteerID { get; set; } //Lookup Table

        [Required]
        [Display(Name = "مدة التنفيذ")]
        public int DurationTimeID { get; set; } // Lookup Table

        [Required]
        [Display(Name = "الحالة")]
        public int StatusID { get; set; } = 1; // Lookup Table

        [Required]
        public int CurrentSpotID { get; set; } = 1; // Lookup Table


        [Required]
        public bool isDeleted { get; set; } = false;

        [Required]
        public bool isActive { get; set; } = true;

		[Required]
		public int UserId { get; set; } //Lookup Table

		[Required]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [ForeignKey("BranchID")]
        public Branch Branch { get; set; }

        [ForeignKey("UserId")]
		public User User { get; set; }

        [ForeignKey("DepartmentId")]
		public Department Department { get; set; }

        [ForeignKey("CurrentSpotID")]
		public CurrentSpot CurrentSpot { get; set; }

        [ForeignKey("VolunteerID")]
		public Volunteer Volunteer { get; set; }

        [ForeignKey("StatusID")]
		public Status Status { get; set; }

        [ForeignKey("Geha_TragetID")]
		public Geha_Traget Geha_Traget { get; set; }

		[ForeignKey("DurationTimeID")]
		public DurationTime DurationTime { get; set; }
	}

}
