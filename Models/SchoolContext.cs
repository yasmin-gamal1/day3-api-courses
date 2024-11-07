using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace d3.Models
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

        public DbSet<Course> Courses { get; set; }
    }
}
