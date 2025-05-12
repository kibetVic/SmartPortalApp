using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPortalApp.Models
{
    public class CourseSubject
    {
        [Key]
        public int CourseSubjectsId { get; set; }
        public int CourseId { get; set; }//dropdown selection
        [ForeignKey("CourseId")]
        public virtual Course? Course { get; set; }
        public int SubjectId { get; set; }//dropdown selection
        [ForeignKey("SubjectId")]
        public virtual Subject? Subject { get; set; }
        public int GradeId { get; set; }//dropdown selection
        [ForeignKey("GradeId")]
        public virtual Grade? Grade { get; set; }
    }
}
