using System.ComponentModel.DataAnnotations;

namespace SmartPortalApp.Models
{
    public class Grade
    {
        [Key]
        public int GradeId { get; set; }
        public string? Name { get; set; }
    }
}
