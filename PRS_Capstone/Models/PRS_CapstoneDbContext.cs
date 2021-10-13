using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRS_Capstone.Models
{
    public class PRS_CapstoneDbContext : DbContext {

        // DbSets go here
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Request> Requests { get; set; }


        public PRS_CapstoneDbContext(DbContextOptions<PRS_CapstoneDbContext> options)
            : base(options) { }

        // for fluid API:
        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<User>(e => {
                e.ToTable("Users"); // Table name
                e.HasKey(p => p.Id); // primary key
                e.Property(p => p.Username).HasMaxLength(30).IsRequired(true);
                e.HasIndex(p => p.Username).IsUnique(true);
                e.Property(p => p.Password).HasMaxLength(30).IsRequired(true);
                e.Property(p => p.FirstName).HasMaxLength(30).IsRequired(true);
                e.Property(p => p.LastName).HasMaxLength(30).IsRequired(true);
                e.Property(p => p.Phone).HasMaxLength(12).IsRequired(true);
                e.Property(p => p.Email).HasMaxLength(225);
                e.Property(p => p.IsAdmin).IsRequired(true);
                e.Property(p => p.IsReviewer).IsRequired(true);

            });

            builder.Entity<Request>(e => {
                e.ToTable("Requests");
                e.HasKey(p => p.Id);
                e.Property(p => p.Description).HasMaxLength(80).IsRequired(true);
                e.Property(p => p.Justification).HasMaxLength(80).IsRequired(true);
                e.Property(p => p.RejectionReason).HasMaxLength(80);
                e.Property(p => p.DeliveryMode).HasMaxLength(20).IsRequired(true);
                e.Property(p => p.Status).HasMaxLength(10).IsRequired(true);
                e.Property(p => p.Total).IsRequired(true);
                e.HasOne(p => p.User).WithMany(p => p.Requests).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}
