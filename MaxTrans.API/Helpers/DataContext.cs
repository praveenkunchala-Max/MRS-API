using Microsoft.EntityFrameworkCore;
using Augadh.SecurityMonitoring.API.Entities;

namespace Augadh.SecurityMonitoring.API.Helpers
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}
