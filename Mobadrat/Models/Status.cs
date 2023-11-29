using Amazon.EC2;
using System.ComponentModel.DataAnnotations;

namespace Mobadrat.Models
{
    public class Status
    {
        public int StatusID { get; set; }

        [Required]
        [Display(Name="اسم الحالة")]
        public string StatusName { get; set; }
    }
}
