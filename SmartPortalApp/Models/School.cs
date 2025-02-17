using System.ComponentModel.DataAnnotations;

namespace SmartPortalApp.Models
{
    public class School : UserActivity
    {
        [Key]
        public int SchoolId { get; set; }
        public string SchoolCode { get; set; }
        public string SchoolName { get; set; }
    }
}
