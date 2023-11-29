using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace Mobadrat.Models
{
    public class MobadraUploadfile
    {
        [Key]
        public int FileId { get; set; }

        [Required]
        [Display(Name = "ملف التحميل")]
        public string FilePath { get; set; }

        //public string MobadraFileDesc { get; set; } = string.Empty;

        [Required]
        public int MobadraID { get; set; }

        [Required]
        public bool isDeleted { get; set; } = false;

        [Required]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Required]
        public int CreateUser { get; set; }

        [ForeignKey("MobadraID")]
        public Mobadra Mobadra{ get; set; }
    }
}
