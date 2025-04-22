using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPortalApp.Models
{
    public class Course : UserActivity
    {
        internal readonly object TeachingSubjectS;

        [Key]
        public int CourseId { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
        public string? CourseCode { get; set; }
        public string? CourseName { get; set; }
        public string? KcsMeangrade { get; set; }
        public string? Maths { get; set; }
        public string? English { get; set; }
        public string? Kiswahili { get; set; }
        public string? Chemistry { get; set; }
        public string? Biology { get; set; }
        public string? BusinessStudies { get; set; }
        public string? Agriculture { get; set; }

    }
}
