using DataLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
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

            modelBuilder.Entity<Item>()
                .HasOne(p => p.Collection)
                .WithMany(t => t.Items)
                .HasForeignKey(p => p.CollectionId);

            modelBuilder.Entity<AdditionalItemField>()
                .HasOne(p => p.Item)
                .WithMany(t => t.AdditionalItemFields)
                .HasForeignKey(p => p.ItemId);

            modelBuilder.Entity<Comment>()
                .HasOne(p => p.User)
                .WithMany(t => t.Comments)
                .HasForeignKey(p => p.UserId);
            modelBuilder.Entity<Comment>()
                .HasOne(p => p.Item)
                .WithMany(t => t.Comments)
                .HasForeignKey(p => p.ItemId);

            modelBuilder.Entity<Like>()
                .HasOne(p => p.User)
                .WithMany(t => t.Likes)
                .HasForeignKey(p => p.UserId);
            modelBuilder.Entity<Like>()
                .HasOne(p => p.Item)
                .WithMany(t => t.Likes)
                .HasForeignKey(p => p.ItemId);
            modelBuilder.Entity<Like>()
                .HasKey(p => p.ItemId);
        }
    }
}