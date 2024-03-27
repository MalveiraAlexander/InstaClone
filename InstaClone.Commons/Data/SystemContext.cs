using InstaClone.Commons.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Data
{
    public class SystemContext : DbContext
    {
        public SystemContext(DbContextOptions<SystemContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostReaction> PostReactions { get; set; }
        public DbSet<ReactionType> ReactionTypes { get; set; }
        public DbSet<UserUser> UserUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReactionType>().HasData(
                new ReactionType { Id = 1, Name = "Like" }
            );


            modelBuilder.Entity<UserUser>()
                .HasKey(uu => new { uu.UserId, uu.FollowerId });


            modelBuilder.Entity<UserUser>()
                .HasOne(uu => uu.User)
                .WithMany(u => u.Followers)
                .HasForeignKey(uu => uu.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserUser>()
                .HasOne(uu => uu.Follower)
                .WithMany()
                .HasForeignKey(uu => uu.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
