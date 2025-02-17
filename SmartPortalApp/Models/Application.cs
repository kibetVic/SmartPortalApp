using System.ComponentModel.DataAnnotations;

namespace SmartPortalApp.Models
{
    public class Application: UserActivity
    {
        [Key]
        public int ApplicationId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int SchoolId { get; set; }
        public virtual School School { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        //public int TeachingSubjectId { get; set; }
        //public virtual TeachingSubject TeachingSubject { get; set; }
        //public int MinimumRequirementId { get; set; }
        //public virtual MinimumRequirement MinimumRequirement { get; set; }
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
        public string UploadKCSE { get; set; }
        public string UploadKCPE { get; set; }
        public string ReasonForTransfer { get; set; }
        public ApplicationStatus Status { get; set; }
        public string EligibilityStatus { get; set; }
    }

    public enum ApplicationStatus
    {
        Submitted,
        Approved,
        Rejected
    }
}
