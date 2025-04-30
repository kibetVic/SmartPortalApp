using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPortalApp.NewModels
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string? Name { get; set; }
        public int SchoolId { get; set; }//to be selected in adropdown
        [ForeignKey("SchoolId")]
        public virtual School? School { get; set; }
    }
}
