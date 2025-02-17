using Microsoft.EntityFrameworkCore;
using SmartPortalApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartPortalApp.Data
{
    public class ApplicationDbContext : DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<TeachingSubject> TeachingSubjects { get; set; }
        public DbSet<MinimumRequirement> MinimumRequirements { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<ResultTable> ResultTable { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Department)
                .WithMany()
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Department>()
                .HasOne(d => d.School)
                .WithMany()
                .HasForeignKey(d => d.SchoolId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Application>()
            //    .HasOne(d => d.MinimumRequirement)
            //    .WithMany()
            //    .HasForeignKey(d => d.MinimumRequirementId);

            //modelBuilder.Entity<Application>()
            //    .HasOne(a => a.TeachingSubject)
            //    .WithMany(ts => ts.Applications)
            //    .HasForeignKey(a => a.TeachingSubjectId);

            //modelBuilder.Entity<TeachingSubject>()
            //    .HasOne(ts => ts.Course)
            //    .WithMany(c => c.TeachingSubjects)
            //    .HasForeignKey(ts => ts.CourseId);

            modelBuilder.Entity<Role>()
                 .HasIndex(c => c.RoleCode)
                 .IsUnique();

            modelBuilder.Entity<Role>()
                 .HasIndex(c => c.RoleName)
                 .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Course>()
                 .HasIndex(c => c.CourseCode)
                 .IsUnique();

            modelBuilder.Entity<Course>()
                 .HasIndex(c => c.CourseName)
                 .IsUnique();

            modelBuilder.Entity<Student>()
                 .HasIndex(s => s.Identity)
                 .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.RegistrationNo)
                .IsUnique();

            modelBuilder.Entity<Department>()
                 .HasIndex(c => c.DepartmentCode)
                 .IsUnique();

            modelBuilder.Entity<Department>()
                 .HasIndex(c => c.DepartmentName)
                 .IsUnique();

            modelBuilder.Entity<Grade>()
                 .HasIndex(c => c.GradeCode)
                 .IsUnique();

            modelBuilder.Entity<Grade>()
                 .HasIndex(c => c.GradeName)
                 .IsUnique();

            modelBuilder.Entity<Point>()
                 .HasIndex(c => c.PointCode)
                 .IsUnique();

            modelBuilder.Entity<Point>()
                 .HasIndex(c => c.AVGPoint)
                 .IsUnique();

            modelBuilder.Entity<School>()
                 .HasIndex(c => c.SchoolCode)
                 .IsUnique();

            modelBuilder.Entity<School>()
                 .HasIndex(c => c.SchoolName)
                 .IsUnique();

            modelBuilder.Entity<Subject>()
                 .HasIndex(c => c.SubjectCode)
                 .IsUnique();

            modelBuilder.Entity<Subject>()
                 .HasIndex(c => c.SubjectName)
                 .IsUnique();

            modelBuilder.Entity<TeachingSubject>()
                 .HasIndex(c => c.TeachingSubjectCode)
                 .IsUnique();

            modelBuilder.Entity<TeachingSubject>()
                 .HasIndex(c => c.TeachingSubjectName)
                 .IsUnique();

            modelBuilder.Entity<MinimumRequirement>()
                 .HasIndex(c => c.MinimumRequirementCode)
                 .IsUnique();

            modelBuilder.Entity<MinimumRequirement>()
                 .HasIndex(c => c.MinimumRequirementName)
                 .IsUnique();

        }
    }
}

