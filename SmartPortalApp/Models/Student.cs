using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPortalApp.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string? Surname { get; set; }
        public string? OtherNames { get; set; }
        public string? RegNo { get; set; }
        public int CourseId { get; set; }//dropdown
        [ForeignKey("CourseId")]
        public virtual Course? Course{ get; set; }
        public string? Gender { get; set; }//dropdown
        public string? Email { get; set; }
        public string? PhoneNo { get; set; }
    }
}
