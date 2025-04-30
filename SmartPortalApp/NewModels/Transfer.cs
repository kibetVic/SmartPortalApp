using System.ComponentModel.DataAnnotations;

namespace SmartPortalApp.NewModels
{
    public class Transfer
    {
        [Key]
        public int TransferId { get; set; }
        public int StudentId { get; set; }
        public int FromCourseId { get; set; }
        public int ToCourseId { get; set; }
        public string? Reason { get; set; }
        public string? TransferStatus { get; set; }
        public string? DeanApproval { get; set; }
        public string? ChairamnApproval { get; set; }
    }
}
