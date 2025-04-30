using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPortalApp.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string? Name { get; set; }
        public string? KsceMeanGrade { get; set; }//dropdown selection from grades
        public int DepartmentId { get; set; }//to be selected in a dropdown
        [ForeignKey("DepartmentId")]
        public virtual Department? Department { get; set; }

    }
}
