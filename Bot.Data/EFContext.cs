using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Template.Data
{
    public class EFContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionItem> QuestionsChildern { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public EFContext(DbContextOptions options) : base(options)
        {
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