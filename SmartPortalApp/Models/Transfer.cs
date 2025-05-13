using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPortalApp.Models
{
    public class Transfer
    {
        [Key]
        public int TransferId { get; set; }
        public int StudentId { get; set; }
        public virtual Student? Student { get; set; }
        public int FromCourseId { get; set; }
        [ForeignKey("FromCourseId")]
        
        public virtual Course? FromCourse { get; set; }
        public int ToCourseId { get; set; }
        [ForeignKey("ToCourseId")]
      
        public virtual Course? ToCourse { get; set; }
    
       
        public string? Reason { get; set; }
        public string? TransferStatus { get; set; } = "Pending";
        public string? DeanApproval { get; set; } = "Pending";
        public string? ChairamnApproval { get; set; } = "Pending";
    }
}
