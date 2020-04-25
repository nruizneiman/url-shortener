using Microsoft.EntityFrameworkCore;
using static US.Application.Connection.ConnectionString;
using US.Domain.Entities;

namespace US.Infrastructure.Context
{
    public class DataContext : DbContext
    {
        public DataContext()
        {}

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(SqlServerConnectionString);
            }
        }

        // Entities
        public DbSet<ShortUrl> ShortUrls { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
    }
}
