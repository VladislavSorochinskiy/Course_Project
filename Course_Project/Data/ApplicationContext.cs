using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Course_Project.Data
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Collection>()
                .HasOne(p => p.User)
                .WithMany(t => t.Collections)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Comment>()
                .HasOne(p => p.User)
                .WithMany(t => t.Comments)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Like>()
                .HasOne(p => p.User)
                .WithMany(t => t.Likes)
                .HasForeignKey(p => p.UserId);
        }
    }
}
