﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PCShop.Data;

namespace PCShop.Migrations
{
    [DbContext(typeof(PcShopContext))]
    partial class PcShopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PCShop.Data.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(511)")
                        .HasMaxLength(511);

                    b.Property<int>("ProductComponentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductComponentId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("PCShop.Data.Entities.ProductAttribute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(511)")
                        .HasMaxLength(511);

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(127)")
                        .HasMaxLength(127);

                    b.HasKey("Id");

                    b.ToTable("ProductAttribute");
                });

            modelBuilder.Entity("PCShop.Data.Entities.ProductComponent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(511)")
                        .HasMaxLength(511);

                    b.Property<int?>("ParentProductComponentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentProductComponentId");

                    b.ToTable("ProductComponent");
                });

            modelBuilder.Entity("PCShop.Data.Entities.ProductComponentAttributeMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductAttributeId")
                        .HasColumnType("int");

                    b.Property<int>("ProductComponentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductAttributeId");

                    b.HasIndex("ProductComponentId");

                    b.ToTable("ProductComponentAttributeMap");
                });

            modelBuilder.Entity("PCShop.Data.Entities.Product", b =>
                {
                    b.HasOne("PCShop.Data.Entities.ProductComponent", "ProductComponent")
                        .WithMany("Products")
                        .HasForeignKey("ProductComponentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("PCShop.Data.Entities.ProductComponent", b =>
                {
                    b.HasOne("PCShop.Data.Entities.ProductComponent", "ParentProductComponent")
                        .WithMany("ProductComponents")
                        .HasForeignKey("ParentProductComponentId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("PCShop.Data.Entities.ProductComponentAttributeMap", b =>
                {
                    b.HasOne("PCShop.Data.Entities.ProductAttribute", "ProductAttribute")
                        .WithMany("ProductAttributesMap")
                        .HasForeignKey("ProductAttributeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("PCShop.Data.Entities.ProductComponent", "ProductComponent")
                        .WithMany("ProductAttributesMap")
                        .HasForeignKey("ProductComponentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}