using Microsoft.EntityFrameworkCore;
using Customer.Domain.Entities;

namespace Customer.Infrastructure.Data;

public partial class CustomerContext : DbContext
{
    public CustomerContext()
    {
    }

    public CustomerContext(DbContextOptions<CustomerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustomerModel> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerModel>(entity =>
        {
            entity.HasIndex(e => e.City, "City");

            entity.HasIndex(e => e.CompanyName, "CompanyName");

            entity.HasIndex(e => e.PostalCode, "PostalCode");

            entity.HasIndex(e => e.Region, "Region");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("CustomerID");
            entity.Property(e => e.Address).HasMaxLength(60);
            entity.Property(e => e.City).HasMaxLength(15);
            entity.Property(e => e.CompanyName).HasMaxLength(40);
            entity.Property(e => e.ContactName).HasMaxLength(30);
            entity.Property(e => e.ContactTitle).HasMaxLength(30);
            entity.Property(e => e.Country).HasMaxLength(15);
            entity.Property(e => e.Fax).HasMaxLength(24);
            entity.Property(e => e.Phone).HasMaxLength(24);
            entity.Property(e => e.PostalCode).HasMaxLength(10);
            entity.Property(e => e.Region).HasMaxLength(15);
            entity.HasKey(e => e.CustomerId);

            //entity.HasMany(d => d.CustomerTypes).WithMany(p => p.Customers)
            //    .UsingEntity<Dictionary<string, object>>(
            //        "CustomerCustomerDemo",
            //        r => r.HasOne<CustomerDemographic>().WithMany()
            //            .HasForeignKey("CustomerTypeId")
            //            .OnDelete(DeleteBehavior.ClientSetNull)
            //            .HasConstraintName("FK_CustomerCustomerDemo"),
            //        l => l.HasOne<Customer>().WithMany()
            //            .HasForeignKey("CustomerId")
            //            .OnDelete(DeleteBehavior.ClientSetNull)
            //            .HasConstraintName("FK_CustomerCustomerDemo_Customers"),
            //        j =>
            //        {
            //            j.HasKey("CustomerId", "CustomerTypeId").IsClustered(false);
            //            j.ToTable("CustomerCustomerDemo");
            //            j.IndexerProperty<string>("CustomerId")
            //                .HasMaxLength(5)
            //                .IsFixedLength()
            //                .HasColumnName("CustomerID");
            //            j.IndexerProperty<string>("CustomerTypeId")
            //                .HasMaxLength(10)
            //                .IsFixedLength()
            //                .HasColumnName("CustomerTypeID");
            //        });
        });
    }
}