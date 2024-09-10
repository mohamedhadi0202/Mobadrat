using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Mobadrat.Models
{
    public class Department
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int DepartmentId { get; set; }

        [Required]
        [Display(Name = "الإدارة")]
        public string DepartmentName { get; set; }
    }
}
