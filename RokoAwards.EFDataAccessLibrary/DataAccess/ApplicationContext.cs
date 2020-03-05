using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RokoAwards.EFDataAccessLibrary.Models;

namespace RokoAwards.EFDataAccessLibrary.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserAward> UserAwards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAward>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserAwardsReceived)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserAward>()
                .HasOne(ua => ua.AwardReceived)
                .WithMany(a => a.UserAwardsReceived)
                .HasForeignKey(ua => ua.AwardIdReceived)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserAward>()
                .HasOne(ua => ua.FromUser)
                .WithMany(u => u.UserAwardsSent)
                .HasForeignKey(ua => ua.FromUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserAward>()
                .HasOne(ua => ua.AwardSent)
                .WithMany(a => a.UserAwardsSent)
                .HasForeignKey(ua => ua.AwardIdSent)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Award>()
                .HasOne(a => a.Image)
                .WithMany(i => i.Awards)
                .HasForeignKey(a => a.ImageId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Award>()
                .HasOne(a => a.Creater)
                .WithMany(u => u.CreatedAwards)
                .HasForeignKey(a => a.CreaterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.City)
                .WithMany(c => c.Users)
                .HasForeignKey(u => u.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Department)
                .WithMany(d => d.Users)
                .HasForeignKey(u => u.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Image)
                .WithMany(i => i.Users)
                .HasForeignKey(u => u.ImageId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
