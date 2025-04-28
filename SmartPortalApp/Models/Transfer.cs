using System.ComponentModel.DataAnnotations;

namespace SmartPortalApp.Models
{
    public class Transfer
    {
        [Key]
        public int TransferId { get; set; }
        public string? FromCourse { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public string? ToCourse { get; set; }
        public string? Reason { get; set; }
        public DateTime DateCreated { get; set; }
        public string? ApplicationStatus { get; set; } = "Pending";
        public string? AuditId { get; set; }
    }
}
