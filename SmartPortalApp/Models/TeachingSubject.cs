using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPortalApp.Models
{
    public class TeachingSubject : UserActivity
    {
        [Key]
        public int TeachingSubjectId { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public string TeachingSubjectCode { get; set; }
        public string TeachingSubjectName { get; set; }       

    }
}
