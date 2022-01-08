using Microsoft.EntityFrameworkCore;

namespace Net6_Middlewares.Models
{
    public class LoggerDbContext : DbContext
    {
        public DbSet<ErrorLogger> ErrorLogger { get; set; }
        public LoggerDbContext(DbContextOptions<LoggerDbContext> dbContext) :base(dbContext)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
