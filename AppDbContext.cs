using Microsoft.EntityFrameworkCore;

namespace RegisterDI
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<StudyGroup> StudyGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudyGroup>()
                .HasMany(sg => sg.Users)
                .WithMany(); // Many-to-many, can be customized further
        }
    }
}
