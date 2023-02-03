using Microsoft.EntityFrameworkCore;
using Testowe_GRPC.Models;

namespace Testowe_GRPC.Context
{
    public class MessageDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
        public DbSet<Message> Messages { get; set; } = null!;
    }
}
