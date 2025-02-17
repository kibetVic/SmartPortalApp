using System.ComponentModel.DataAnnotations;

namespace SmartPortalApp.Models
{
    public class Grade : UserActivity
    {
        [Key]
        public int GradeId { get; set; }
        public string GradeCode { get; set; }
        public string GradeName { get; set; }
    }
}
