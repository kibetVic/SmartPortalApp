using System.ComponentModel.DataAnnotations;

namespace SmartPortalApp.Models
{
    public class School
    {
        [Key]
        public int SchoolId { get; set; }
        public string? Name { get; set; }
        
    }
}
