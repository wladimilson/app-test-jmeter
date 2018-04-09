
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore;
using app_test_jmeter.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;

namespace app_test_jmeter.Data
{
    public class AdmContext: IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid> {
        public DbSet<User> AdmUsers { get; set; }

        public AdmContext(DbContextOptions<AdmContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<User>()
                        .Property(u => u.Id)
                        .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                        .Property(u => u.Name)
                        .IsRequired();

            modelBuilder.Entity<User>()
                        .Property(u => u.Age)
                        .IsRequired();

            modelBuilder.Entity<User>()
                        .Property(u => u.Username)
                        .IsRequired();
            modelBuilder.Entity<User>()
                        .Property(u => u.Password)
                        .IsRequired();
        }
    }
}