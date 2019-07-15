using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace userwebapi.Models
{
    public partial class TestDbContext : DbContext
    {
        public TestDbContext()
        {
        }

        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> User { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=855823004.database.windows.net;Database=testDb;UID=system;PWD=jU5dFw39kr5v7tjX;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birthDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("creationDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PassWordHash)
                    .HasColumnName("passWordHash")
                    .IsUnicode(false);

                entity.Property(e => e.PassWordSalt)
                    .HasColumnName("passWordSalt")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasColumnName("userName")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });
        }
    }
}
