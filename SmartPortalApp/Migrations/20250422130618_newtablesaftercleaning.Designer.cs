﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartPortalApp.Data;

#nullable disable

namespace SmartPortalApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250422130618_newtablesaftercleaning")]
    partial class newtablesaftercleaning
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SmartPortalApp.Models.Application", b =>
                {
                    b.Property<int>("ApplicationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ApplicationId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("EligibilityStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReasonForTransfer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SchoolId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<string>("UploadKCPE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UploadKCSE")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ApplicationId");

                    b.HasIndex("CourseId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("SchoolId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("SmartPortalApp.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<string>("Agriculture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Biology")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessStudies")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chemistry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CourseName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("English")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KcsMeangrade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kiswahili")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Maths")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("CourseId");

                    b.HasIndex("CourseCode")
                        .IsUnique()
                        .HasFilter("[CourseCode] IS NOT NULL");

                    b.HasIndex("CourseName")
                        .IsUnique()
                        .HasFilter("[CourseName] IS NOT NULL");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("SmartPortalApp.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentId"));

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("DepartmentCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("SchoolId")
                        .HasColumnType("int");

                    b.HasKey("DepartmentId");

                    b.HasIndex("DepartmentCode")
                        .IsUnique();

                    b.HasIndex("DepartmentName")
                        .IsUnique();

                    b.HasIndex("SchoolId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("SmartPortalApp.Models.Grade", b =>
                {
                    b.Property<int>("GradeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GradeId"));

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("GradeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GradeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("GradeId");

                    b.HasIndex("GradeCode")
                        .IsUnique();

                    b.HasIndex("GradeName")
                        .IsUnique();

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("SmartPortalApp.Models.MinimumRequirement", b =>
                {
                    b.Property<int>("MinimumRequirementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MinimumRequirementId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("MinimumRequirementCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MinimumRequirementName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("MinimumRequirementId");

                    b.HasIndex("CourseId");

                    b.HasIndex("MinimumRequirementCode")
                        .IsUnique()
                        .HasFilter("[MinimumRequirementCode] IS NOT NULL");

                    b.HasIndex("MinimumRequirementName")
                        .IsUnique()
                        .HasFilter("[MinimumRequirementName] IS NOT NULL");

                    b.ToTable("MinimumRequirements");
                });

            modelBuilder.Entity("SmartPortalApp.Models.Point", b =>
                {
                    b.Property<int>("PointId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PointId"));

                    b.Property<string>("AVGPoint")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("PointCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PointId");

                    b.HasIndex("AVGPoint")
                        .IsUnique();

                    b.HasIndex("PointCode")
                        .IsUnique();

                    b.ToTable("Points");
                });

            modelBuilder.Entity("SmartPortalApp.Models.ResultTable", b =>
                {
                    b.Property<int>("ResultTableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResultTableId"));

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("GradeId")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("PointId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("ResultTableId");

                    b.HasIndex("GradeId");

                    b.HasIndex("PointId");

                    b.HasIndex("SubjectId");

                    b.ToTable("ResultTable");
                });

            modelBuilder.Entity("SmartPortalApp.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("RoleCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RoleId");

                    b.HasIndex("RoleCode")
                        .IsUnique();

                    b.HasIndex("RoleName")
                        .IsUnique();

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("SmartPortalApp.Models.School", b =>
                {
                    b.Property<int>("SchoolId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SchoolId"));

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("SchoolCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SchoolName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SchoolId");

                    b.HasIndex("SchoolCode")
                        .IsUnique();

                    b.HasIndex("SchoolName")
                        .IsUnique();

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("SmartPortalApp.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                    b.Property<string>("Agriculture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Biology")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BusinessStudies")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Chemistry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("English")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Identity")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("KCSEGrade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kiswahili")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Maths")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("RegistrationNo")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SchoolId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("StudentId");

                    b.HasIndex("CourseId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("Identity")
                        .IsUnique()
                        .HasFilter("[Identity] IS NOT NULL");

                    b.HasIndex("RegistrationNo")
                        .IsUnique()
                        .HasFilter("[RegistrationNo] IS NOT NULL");

                    b.HasIndex("SchoolId");

                    b.HasIndex("UserId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("SmartPortalApp.Models.Subject", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubjectId"));

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("SubjectCode")
                        .HasColumnType("int");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SubjectId");

                    b.HasIndex("SubjectCode")
                        .IsUnique();

                    b.HasIndex("SubjectName")
                        .IsUnique();

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("SmartPortalApp.Models.TeachingSubject", b =>
                {
                    b.Property<int>("TeachingSubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeachingSubjectId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("TeachingSubjectCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TeachingSubjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TeachingSubjectId");

                    b.HasIndex("CourseId");

                    b.HasIndex("TeachingSubjectCode")
                        .IsUnique();

                    b.HasIndex("TeachingSubjectName")
                        .IsUnique();

                    b.ToTable("TeachingSubjects");
                });

            modelBuilder.Entity("SmartPortalApp.Models.Transfer", b =>
                {
                    b.Property<int>("TransferId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransferId"));

                    b.Property<string>("ApplicationStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuditId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("FromCourse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ToCourse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TransferId");

                    b.HasIndex("UserId");

                    b.ToTable("CourseTransfers");
                });

            modelBuilder.Entity("SmartPortalApp.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("LastChanged")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SmartPortalApp.Models.Application", b =>
                {
                    b.HasOne("SmartPortalApp.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartPortalApp.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartPortalApp.Models.School", "School")
                        .WithMany()
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartPortalApp.Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartPortalApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Department");

                    b.Navigation("School");

                    b.Navigation("Subject");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartPortalApp.Models.Course", b =>
                {
                    b.HasOne("SmartPortalApp.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("SmartPortalApp.Models.Department", b =>
                {
                    b.HasOne("SmartPortalApp.Models.School", "School")
                        .WithMany()
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("School");
                });

            modelBuilder.Entity("SmartPortalApp.Models.MinimumRequirement", b =>
                {
                    b.HasOne("SmartPortalApp.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("SmartPortalApp.Models.ResultTable", b =>
                {
                    b.HasOne("SmartPortalApp.Models.Grade", "Grades")
                        .WithMany()
                        .HasForeignKey("GradeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartPortalApp.Models.Point", "Points")
                        .WithMany()
                        .HasForeignKey("PointId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartPortalApp.Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grades");

                    b.Navigation("Points");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("SmartPortalApp.Models.Student", b =>
                {
                    b.HasOne("SmartPortalApp.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartPortalApp.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartPortalApp.Models.School", "School")
                        .WithMany()
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartPortalApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Department");

                    b.Navigation("School");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartPortalApp.Models.TeachingSubject", b =>
                {
                    b.HasOne("SmartPortalApp.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("SmartPortalApp.Models.Transfer", b =>
                {
                    b.HasOne("SmartPortalApp.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartPortalApp.Models.User", b =>
                {
                    b.HasOne("SmartPortalApp.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
