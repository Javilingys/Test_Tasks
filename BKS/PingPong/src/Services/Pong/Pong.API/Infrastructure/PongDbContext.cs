using Microsoft.EntityFrameworkCore;
using Pong.API.Entities;
using Pong.API.Infrastructure.Configurations;

namespace Pong.API.Infrastructure
{
    public class PongDbContext : DbContext
    {
        public PongDbContext(DbContextOptions<PongDbContext> options) 
            : base(options)
        {
        }

        /// <summary>
        /// Сообщения
        /// </summary>
        public DbSet<PongMessage> PongMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new MessageEntityTypeConfiguration());
        }
    }
}
