using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SmartPortalApp.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public int UserId { get; set; }
        [ScaffoldColumn(false)]
        public virtual User? User { get; set; }
        public string? Surname { get; set; }
        public string? OtherNames { get; set; }
        public string? RegNo { get; set; }
        public int CourseId { get; set; }//dropdown
        [ForeignKey("CourseId")]
        public virtual Course? Course{ get; set; }
        public int? GenderId { get; set; }//dropdown
        public string? Email { get; set; }
        public string? PhoneNo { get; set; }
        [NotMapped]
        public IFormFile? UploadKCPEFile { get; set; }
        [NotMapped]
        public IFormFile? UploadKCSEFile { get; set; }

        public string? UploadKCPE { get; set; } // For path saving to DB
        public string? UploadKCSE { get; set; }
    }
}
