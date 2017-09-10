using MatOrderingService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static MatOrderingService.Domain.Models.Order;

namespace MatOrderingService.Services.Storage.EFContext
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapOrders(modelBuilder.Entity<Order>());
        }

        protected void MapOrders(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.Property(p => p.Id)
                .IsRequired();
            builder.Property(p => p.Status)
                .IsRequired()
                .HasDefaultValue(OrderStatus.New);
            builder.Property(p => p.CreatorId)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(p => p.OrderDetails)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(p => p.OrderCode)
                .IsRequired()
                .HasMaxLength(20);
            builder.HasIndex(p => p.OrderCode)
                .IsUnique();

            builder.Property(p => p.IsDeleted)
                .IsRequired();
            builder.Property(p => p.CreateDate)
                .IsRequired()
                .ForSqlServerHasDefaultValueSql("GETDATE()");
            builder.HasKey(p => p.Id);
        }
    }
}
