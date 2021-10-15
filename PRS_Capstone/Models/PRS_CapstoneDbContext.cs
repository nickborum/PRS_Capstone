using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PRS_Capstone.Models;

namespace PRS_Capstone.Models
{
    public class PRS_CapstoneDbContext : DbContext {

        // DbSets go here
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Request> Requests { get; set; }


        public PRS_CapstoneDbContext(DbContextOptions<PRS_CapstoneDbContext> options)
            : base(options) { }

        // for fluent API:
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

            builder.Entity<Vendor>(e => {
                e.ToTable("Vendors"); // Table name
                e.HasKey(p => p.Id); // primary key
                e.Property(p => p.Code).HasMaxLength(30).IsRequired(true);
                e.HasIndex(p => p.Code).IsUnique(true);
                e.Property(p => p.Name).HasMaxLength(30).IsRequired(true);
                e.Property(p => p.Address).HasMaxLength(30).IsRequired(true);
                e.Property(p => p.City).HasMaxLength(30).IsRequired(true);
                e.Property(p => p.State).HasMaxLength(2).IsRequired(true);
                e.Property(p => p.Zip).HasMaxLength(5);
                e.Property(p => p.Phone).HasMaxLength(12);
                e.Property(p => p.Email).HasMaxLength(255);

            });

            builder.Entity<Product>(e => {
                e.ToTable("Products");
                e.HasKey(p => p.Id);
                e.Property(p => p.PartNbr).HasMaxLength(30).IsRequired(true);
                e.Property(p => p.Name).HasMaxLength(30).IsRequired(true);
                e.Property(p => p.Price).IsRequired(true);
                e.Property(p => p.Unit).HasMaxLength(30).IsRequired(true);
                e.Property(p => p.PhotoPath).HasMaxLength(225);
                e.HasOne(p => p.Vendor).WithMany(p => p.Products).HasForeignKey(p => p.VendorId).OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<RequestLine>(e => {
                e.ToTable("Requestlines");
                e.HasKey(p => p.Id);
                e.Property(p => p.Quantity).IsRequired(true);
                e.HasOne(p => p.Request).WithMany(p => p.RequestLines).HasForeignKey(p => p.RequestId).OnDelete(DeleteBehavior.Restrict);
                e.HasOne(p => p.Product).WithMany(p => p.RequestLines).HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Restrict);

            });




        }

        // for fluent API:
        public DbSet<PRS_Capstone.Models.RequestLine> RequestLine { get; set; }

        // for fluent API:
        public DbSet<PRS_Capstone.Models.Vendor> Vendor { get; set; }

        // for fluent API:
        public DbSet<PRS_Capstone.Models.Product> Product { get; set; }

    }
}
