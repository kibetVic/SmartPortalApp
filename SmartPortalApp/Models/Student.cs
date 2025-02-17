using System.ComponentModel.DataAnnotations;

namespace SmartPortalApp.Models
{
    public class Student : UserActivity
    {
        [Key]
        public int StudentId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {MiddleName} {LastName}";
        public string RegistrationNo { get; set; }
        public string Identity { get; set; }
        public string KCSEGrade { get; set; }
        public int SchoolId { get; set; }
        public virtual School School { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
