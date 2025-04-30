using System.ComponentModel.DataAnnotations;

namespace SmartPortalApp.NewModels
{
    public class Grade
    {
        [Key]
        public int GradeId { get; set; }
        public string? Name { get; set; }
    }
}
