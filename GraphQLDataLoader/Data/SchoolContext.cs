using GraphQLDataLoader.Models;
using Microsoft.EntityFrameworkCore;
using GraphiQl;
using GraphQL;

namespace GraphQLDataLoader.Data
{
    public class SchoolContext : DbContext
    {
        private static readonly string connectionString = "Server=(localdb)\\mssqllocaldb;Database=ContosoUniversity3;Trusted_Connection=True;";
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
            //DbInitializer.Initialize(this);
        }

        public SchoolContext() : this(new DbContextOptionsBuilder<SchoolContext>().UseSqlServer(connectionString).Options)
        {
         
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ContosoUniversity3;Trusted_Connection=True;");
        //    }
        //}
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignment");
            modelBuilder.Entity<CourseAssignment>().ToTable("CourseAssignment");

            modelBuilder.Entity<CourseAssignment>()
                .HasKey(c => new { c.CourseID, c.InstructorID });
        }
    }
}