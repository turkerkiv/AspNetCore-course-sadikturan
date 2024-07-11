using Microsoft.EntityFrameworkCore;

namespace EFCoreApp.Data
{
    public class CourseContext : DbContext
    {
        public CourseContext(DbContextOptions<CourseContext> options) : base(options)
        {

        }

        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<CourseRegistiration> CourseRegistirations => Set<CourseRegistiration>();
    }
}