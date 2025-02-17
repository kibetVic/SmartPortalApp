using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPortalApp.Models
{
    public class Department : UserActivity
    {
        [Key]
        public int DepartmentId { get; set; }
        public int SchoolId { get; set; }
        public virtual School School { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
    }
}
