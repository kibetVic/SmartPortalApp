using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPortalApp.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        public string? Name { get; set; }
        
    }
}
