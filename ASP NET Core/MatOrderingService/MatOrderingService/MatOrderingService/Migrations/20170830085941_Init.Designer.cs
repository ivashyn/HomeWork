using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MatOrderingService.Services.Storage.EFContext;
using MatOrderingService.Domain.Models;

namespace MatOrderingService.Migrations
{
    [DbContext(typeof(OrdersDbContext))]
    [Migration("20170830085941_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MatOrderingService.Domain.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:DefaultValueSql", "GETDATE()");

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("OrderCode")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("OrderDetails")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.HasIndex("OrderCode")
                        .IsUnique();

                    b.ToTable("Orders");
                });
        }
    }
}
