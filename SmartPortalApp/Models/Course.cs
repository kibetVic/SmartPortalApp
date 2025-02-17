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
        public virtual Department Department { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }

    }
}
