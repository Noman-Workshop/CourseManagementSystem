﻿// <auto-generated />
using System;
using CourseManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CourseManagementSystem.Migrations
{
    [DbContext(typeof(CMSDbContext))]
    [Migration("20221120110304_AuditLog updated")]
    partial class AuditLogupdated
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CourseManagementSystem.Areas.Addresses.Models.Address", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("House")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("CourseManagementSystem.Areas.Auth.Models.Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id", "Name");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("UserEmail");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CourseManagementSystem.Areas.Auth.Models.User", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Email");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CourseManagementSystem.Areas.Budgets.Models.Budget", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DepartmentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId", "DepartmentName");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("CourseManagementSystem.Areas.Budgets.Models.BudgetAuditLog", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BudgetId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedByEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BudgetId");

                    b.HasIndex("CreatedByEmail");

                    b.HasIndex("UpdatedByEmail");

                    b.ToTable("AuditLogs");
                });

            modelBuilder.Entity("CourseManagementSystem.Areas.Courses.Models.Course", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<float>("Credits")
                        .HasColumnType("real");

                    b.Property<string>("DepartmentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId", "DepartmentName");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("CourseManagementSystem.Areas.Departments.Models.Department", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("HeadId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id", "Name");

                    b.HasIndex("HeadId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("CourseManagementSystem.Areas.Students.Models.Student", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddressId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("CourseManagementSystem.Areas.Teachers.Models.Teacher", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddressId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("CourseManagementSystem.Models.Enrollment", b =>
                {
                    b.Property<string>("CourseId")
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Grade")
                        .HasColumnType("int");

                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("CourseTeacher", b =>
                {
                    b.Property<string>("CoursesId")
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("TeachersId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CoursesId", "TeachersId");

                    b.HasIndex("TeachersId");

                    b.ToTable("CourseTeacher");
                });

            modelBuilder.Entity("CourseManagementSystem.Areas.Auth.Models.Role", b =>
                {
                    b.HasOne("CourseManagementSystem.Areas.Auth.Models.User", null)
                        .WithMany("Roles")
                        .HasForeignKey("UserEmail");
                });

            modelBuilder.Entity("CourseManagementSystem.Areas.Budgets.Models.Budget", b =>
                {
                    b.HasOne("CourseManagementSystem.Areas.Departments.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId", "DepartmentName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("CourseManagementSystem.Areas.Budgets.Models.BudgetAuditLog", b =>
                {
                    b.HasOne("CourseManagementSystem.Areas.Budgets.Models.Budget", null)
                        .WithMany("AuditLogs")
                        .HasForeignKey("BudgetId");

                    b.HasOne("CourseManagementSystem.Areas.Auth.Models.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedByEmail")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CourseManagementSystem.Areas.Auth.Models.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedByEmail")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("CourseManagementSystem.Areas.Courses.Models.Course", b =>
                {
                    b.HasOne("CourseManagementSystem.Areas.Departments.Models.Department", "Department")
                        .WithMany("Courses")
                        .HasForeignKey("DepartmentId", "DepartmentName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("CourseManagementSystem.Areas.Departments.Models.Department", b =>
                {
                    b.HasOne("CourseManagementSystem.Areas.Teachers.Models.Teacher", "Head")
                        .WithMany()
                        .HasForeignKey("HeadId");

                    b.Navigation("Head");
                });

            modelBuilder.Entity("CourseManagementSystem.Areas.Students.Models.Student", b =>
                {
                    b.HasOne("CourseManagementSystem.Areas.Addresses.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("CourseManagementSystem.Areas.Teachers.Models.Teacher", b =>
                {
                    b.HasOne("CourseManagementSystem.Areas.Addresses.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("CourseManagementSystem.Models.Enrollment", b =>
                {
                    b.HasOne("CourseManagementSystem.Areas.Courses.Models.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CourseManagementSystem.Areas.Students.Models.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("CourseTeacher", b =>
                {
                    b.HasOne("CourseManagementSystem.Areas.Courses.Models.Course", null)
                        .WithMany()
                        .HasForeignKey("CoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CourseManagementSystem.Areas.Teachers.Models.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeachersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CourseManagementSystem.Areas.Auth.Models.User", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("CourseManagementSystem.Areas.Budgets.Models.Budget", b =>
                {
                    b.Navigation("AuditLogs");
                });

            modelBuilder.Entity("CourseManagementSystem.Areas.Courses.Models.Course", b =>
                {
                    b.Navigation("Enrollments");
                });

            modelBuilder.Entity("CourseManagementSystem.Areas.Departments.Models.Department", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("CourseManagementSystem.Areas.Students.Models.Student", b =>
                {
                    b.Navigation("Enrollments");
                });
#pragma warning restore 612, 618
        }
    }
}
