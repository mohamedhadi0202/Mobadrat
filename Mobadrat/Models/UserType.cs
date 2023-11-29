using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Mobadrat.Models
{
    public class UserType
    {
        [Key]
        [Required]
        public int UserTypeId { get; set; }
        [Required]
        [Display(Name = "نوع المستخدم")]
        public string UserTypeName { get; set; }
    }
}
