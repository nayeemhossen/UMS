using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using UniversityCourseResultManagementSystem.Models;

namespace UniversityCourseResultManagementSystem
{
    public class UniversityCourseResultManagementSystemDbContext : DbContext
    {
        public UniversityCourseResultManagementSystemDbContext()
            : base("name=UniversityCourseResultManagementSystemDBConnection")
        {
            Database.SetInitializer<UniversityCourseResultManagementSystemDbContext>(
                new CreateDatabaseIfNotExists<UniversityCourseResultManagementSystemDbContext>());
        }

        public DbSet<Day> Days { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<CourseAssign> CourseAssigns { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<ClassRoomAllocation> ClassRoomAllocations { get; set; }
        public DbSet<EnrollCourse> EnrollCourses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<UniversityCourseResultManagementSystemDbContext>(
                new CreateDatabaseIfNotExists<UniversityCourseResultManagementSystemDbContext>());
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //modelBuilder.Entity<ItemDb>().Property(p => p.SalePrice).HasPrecision(18, 2); // or whatever your schema specifies

        }
    }
}