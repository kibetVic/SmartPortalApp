using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace SmartPortalApp.Models
{
    public class MinimumRequirement : UserActivity
    {
        [Key]
        public int MinimumRequirementId { get; set; }
        public int CourseId { get; set; }

        public virtual Course? Course { get; set; }
        public string ?MinimumRequirementCode { get; set; }
        public string? MinimumRequirementName { get; set; }
    }
}
