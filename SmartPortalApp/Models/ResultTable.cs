namespace SmartPortalApp.Models
{
    public class ResultTable : UserActivity
    {
        public int ResultTableId { get; set; }
        public virtual Subject Subject { get; set; }
        public int SubjectId { get; set; }
        public virtual Grade Grades { get; set; }
        public int GradeId { get; set; }
        public virtual Point Points { get; set; }
        public int PointId { get; set; }
    }
}
