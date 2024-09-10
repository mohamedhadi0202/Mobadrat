using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mobadrat.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "User ID")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "اسم الدخول")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "كلمة المرور")]
        public string UserPassword { get; set; }

        [Display(Name = "رقم الهوية")]
        public int IdentityNumber { get; set; }
        
        [Required]
        [Display(Name = "الاسم الكامل")]
        public string EmployeeFullName{ get; set; }

        [Required]
        public int BranchID { get; set; }
        [Required]
        public string BranchName { get; set; }

        [Required]
        public int DepartmentID { get; set; }
        [Required]
        public string DepartmentName{ get; set; }
        [Required]
        public int EMPLOYEE_ID { get; set; }
        [Required]
        public int UserTypeId { get; set; } = 3;
        [Required]
        public string MobileNumber{ get; set; }
        public string SexName { get; set; }
        public string Email{ get; set; }

        public bool isActive { get; set; } = true;

        public bool isDeleted { get; set; } = false;
        [Required]
        public DateTime CreateDateGorgian { get; set; } = DateTime.Now;

        [Required] 
        public string CreateDateHijry { get; set; }
        public UserType UserType { get; set; }
        public Branch Branch { get; set; }

        public Department Department { get; set; }
    }
}
