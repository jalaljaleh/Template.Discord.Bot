using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Template.Data
{
    public class EFContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Guild> Guilds { get; set; }

        public EFContext(DbContextOptions options) : base(options)
        {
            // delete this
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}