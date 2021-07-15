using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Donate.Models
{
    public partial class DonationsDbContext : DbContext
    {
        public DonationsDbContext()
        {
        }

        public DonationsDbContext(DbContextOptions<DonationsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DonationSummary> DonationSummaries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-P9JN8M5;Database=DonationsDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<DonationSummary>(entity =>
            {
                entity.HasKey(e => e.DonationId)
                    .HasName("PK__Donation__C5082EDB03D0372D");

                entity.ToTable("DonationSummary");

                entity.Property(e => e.DonationId).HasColumnName("DonationID");

                entity.Property(e => e.DonationCashValue).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.DonationDate).HasColumnType("date");

                entity.Property(e => e.DonationType)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(60)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
