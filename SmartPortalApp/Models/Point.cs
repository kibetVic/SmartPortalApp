using System.ComponentModel.DataAnnotations;

namespace SmartPortalApp.Models
{
    public class Point : UserActivity
    {
        [Key]
        public int PointId { get; set; }
        public string PointCode { get; set; }
        public string AVGPoint { get; set; }
    }
}
