using EMSystem.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace EMSystem.Data.DbConfig
{
    public class WebDbContext : DbContext, IWebDbContext
    {
        public WebDbContext(DbContextOptions<WebDbContext> opts) : base(opts)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
