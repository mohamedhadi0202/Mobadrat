using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mobadrat.Models.ViewModels
{
    public class NewAttachmentViewModel
    {
        public int FileId { get; set; }

        [Required]
        [Display(Name = "ملف التحميل")]
        public IFormFile FilePath { get; set; }
        [Required]
        public int MobadraID { get; set; }

        [Required]
        public bool isDeleted { get; set; } = false;

        [Required]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [Required]
        public int UserId { get; set; }

        [ForeignKey("MobadraID")]
        public Mobadra Mobadra { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
