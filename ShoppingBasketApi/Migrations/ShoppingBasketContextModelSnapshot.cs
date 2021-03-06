// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoppingBasketApi.Models;

#nullable disable

namespace ShoppingBasketApi.Migrations
{
    [DbContext(typeof(ShoppingBasketContext))]
    partial class ShoppingBasketContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ShoppingBasketApi.Objects.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ShoppingBasketId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ShoppingBasketId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("ShoppingBasketApi.Objects.ShoppingBasket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.HasKey("Id");

                    b.ToTable("ShoppingBasket");
                });

            modelBuilder.Entity("ShoppingBasketApi.Objects.Item", b =>
                {
                    b.HasOne("ShoppingBasketApi.Objects.ShoppingBasket", null)
                        .WithMany("Basket")
                        .HasForeignKey("ShoppingBasketId");
                });

            modelBuilder.Entity("ShoppingBasketApi.Objects.ShoppingBasket", b =>
                {
                    b.Navigation("Basket");
                });
#pragma warning restore 612, 618
        }
    }
}
