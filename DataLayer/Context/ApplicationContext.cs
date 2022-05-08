using DataLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemFieldName> ItemFieldNames { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<AdditionalItemField> AdditionalItemFields { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
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

            modelBuilder.Entity<ItemFieldName>()
                .HasOne(p => p.Collection)
                .WithMany(t => t.ItemFields)
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
                .HasForeignKey(p => p.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

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