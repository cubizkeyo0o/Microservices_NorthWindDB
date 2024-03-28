using SalePayment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using SalePayment.Domain;

namespace SalePayment.Infrastructure.Data
{
    public class SalePaymentContext : DbContext
    {
        public SalePaymentContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(true);
        }

        public SalePaymentContext(DbContextOptions<SalePaymentContext> options)
            : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }
        
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.CustomerId, "CustomerID");

                entity.HasIndex(e => e.CustomerId, "CustomersOrders");

                entity.HasIndex(e => e.EmployeeId, "EmployeeID");

                entity.HasIndex(e => e.EmployeeId, "EmployeesOrders");

                entity.HasIndex(e => e.OrderDate, "OrderDate");

                entity.HasIndex(e => e.ShipPostalCode, "ShipPostalCode");

                entity.HasIndex(e => e.ShippedDate, "ShippedDate");

                entity.HasIndex(e => e.ShipVia, "ShippersOrders");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");
                entity.Property(e => e.CustomerId)
                    .HasMaxLength(5)
                    .IsFixedLength()
                    .HasColumnName("CustomerID");
                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
                entity.Property(e => e.Freight)
                    .HasDefaultValue(0m)
                    .HasColumnType("money");
                entity.Property(e => e.OrderDate).HasColumnType("datetime");
                entity.Property(e => e.RequiredDate).HasColumnType("datetime");
                entity.Property(e => e.ShipAddress).HasMaxLength(60);
                entity.Property(e => e.ShipCity).HasMaxLength(15);
                entity.Property(e => e.ShipCountry).HasMaxLength(15);
                entity.Property(e => e.ShipName).HasMaxLength(40);
                entity.Property(e => e.ShipPostalCode).HasMaxLength(10);
                entity.Property(e => e.ShipRegion).HasMaxLength(15);
                entity.Property(e => e.ShippedDate).HasColumnType("datetime");

                //entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                //    .HasForeignKey(d => d.CustomerId)
                //    .HasConstraintName("FK_Orders_Customers");

                //entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                //    .HasForeignKey(d => d.EmployeeId)
                //    .HasConstraintName("FK_Orders_Employees");

                //entity.HasOne(d => d.ShipViaNavigation).WithMany(p => p.Orders)
                //    .HasForeignKey(d => d.ShipVia)
                //    .HasConstraintName("FK_Orders_Shippers");
            });
            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId }).HasName("PK_Order_Details");

                entity.ToTable("Order Details");

                entity.HasIndex(e => e.OrderId, "OrderID");

                entity.HasIndex(e => e.OrderId, "OrdersOrder_Details");

                entity.HasIndex(e => e.ProductId, "ProductID");

                entity.HasIndex(e => e.ProductId, "ProductsOrder_Details");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");
                entity.Property(e => e.ProductId).HasColumnName("ProductID");
                entity.Property(e => e.Quantity).HasDefaultValue((short)1);
                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Details_Orders");

                //entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                //    .HasForeignKey(d => d.ProductId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Order_Details_Products");
            });

        }
    }
}
