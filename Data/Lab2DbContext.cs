using Lab2Test.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab2Test.Data
{
    public class Lab2DbContext : DbContext
    {
        public Lab2DbContext(DbContextOptions<Lab2DbContext> options)
        : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
