using BulkOperationsEFCoreBulkOperations.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace BulkOperationsEFCoreBulkOperations.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}