using System.ComponentModel.DataAnnotations;

namespace SmartPortalApp.Models
{
    public class Subject : UserActivity
    {
        [Key]
        public int SubjectId { get; set; }
        public int SubjectCode { get; set; }
        public string SubjectName { get; set; }
    }
}
