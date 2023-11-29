using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Mobadrat.Models
{
    public class Department
    {
        [Key]
        [Required]
        public int DepartmentId { get; set; }

        [Required]
        [Display(Name = "الإدارة")]
        public string DepartmentName { get; set; }
    }
}
