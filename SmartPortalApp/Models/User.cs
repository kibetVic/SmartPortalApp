using System.ComponentModel.DataAnnotations;

namespace SmartPortalApp.Models
{
    public class User : UserActivity
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public DateTime? LastChanged { get; internal set; }
    }
}
