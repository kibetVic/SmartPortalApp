using Microsoft.EntityFrameworkCore;
using SmartPortalApp.Models;
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
        public DbSet<School> Schools { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<CourseSubject> CourseSubjects { get; set; }
        public DbSet<Transfer> Transfers { get; set; }

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
                 .HasIndex(c => c.Name)
                 .IsUnique();

            modelBuilder.Entity<Student>()
                 .HasIndex(s => s.Email)
                 .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.RegNo)
                .IsUnique();

            modelBuilder.Entity<Department>()
                 .HasIndex(c => c.Name)
                 .IsUnique();

            modelBuilder.Entity<Grade>()
                 .HasIndex(c => c.Name)
                 .IsUnique();

            modelBuilder.Entity<School>()
                 .HasIndex(c => c.Name)
                 .IsUnique();

            modelBuilder.Entity<Subject>()
                 .HasIndex(c => c.Name)
                 .IsUnique();
        }
    }
}

