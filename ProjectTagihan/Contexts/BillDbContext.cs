using Microsoft.EntityFrameworkCore;
using ProjectTagihan.Entities;

namespace ProjectTagihan.Contexts;

public partial class BillDbContext : DbContext
{
    public BillDbContext(DbContextOptions<BillDbContext> options) : base(options)
    {
    }


    public virtual DbSet<Bill> Bills { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Bill>(entity =>
        {

            entity.HasKey(e => e.Guid)
                .HasName("PRIMARY");

            entity.ToTable("bill");

            entity.UseCollation("utf8mb4_general_ci");

            entity.Property(e => e.Guid)
                .HasMaxLength(36)
                .HasColumnName("guid");

            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasColumnName("description");

            entity.Property(e => e.BillAmount)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("bill_amount");

            entity.Property(e => e.DueDate)
                .HasColumnType("date")
                .HasColumnName("due_date");

            entity.Property(e => e.PaymentNo)
                .HasMaxLength(50)
                .HasColumnName("payment_no");

            entity.Property(e => e.PaymentDate)
                .HasColumnType("date")
                .HasColumnName("payment_date");

            entity.Property(e => e.PaymentAmount)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("payment_amount");

        });
    }
}
