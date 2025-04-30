using System.ComponentModel.DataAnnotations;

namespace SmartPortalApp.NewModels
{
    public class CourseSubjects
    {
        [Key]
        public int CourseSubjectsId { get; set; }
        public int CourseId { get; set; }//dropdown selection
        public int SubjectId { get; set; }//dropdown selection
        public int GradeId { get; set; }//dropdown selection
    }
}
