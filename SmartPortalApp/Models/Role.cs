using SmartPortalApp.Models;
using System.ComponentModel.DataAnnotations;

namespace SmartPortalApp.Models
{
    public class Role 
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
    }
}
