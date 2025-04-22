using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPortalApp.Models
{
    public class Student : UserActivity
    {
        [Key]
        public int StudentId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? FullName => $"{FirstName} {MiddleName} {LastName}";
        public string? RegistrationNo { get; set; }
        public string? Identity { get; set; }
        public string? KCSEGrade { get; set; }
        public int SchoolId { get; set; }
        [ForeignKey("SchoolId")]
        public virtual School? School { get; set; }
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department? Department { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course? Course { get; set; }

        public string? Maths { get; set; }
        public string? English { get; set; }
        public string? Kiswahili { get; set; }
        public string? Chemistry { get; set; }
        public string? Biology { get; set; }
        public string? BusinessStudies { get; set; }
        public string? Agriculture { get; set; }
    }
}
