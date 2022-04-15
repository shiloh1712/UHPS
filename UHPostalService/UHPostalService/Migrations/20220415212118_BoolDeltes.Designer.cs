﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UHPostalService.Data;

#nullable disable

namespace UHPostalService.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220415212118_BoolDeltes")]
    partial class BoolDeltes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("UHPostalService.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Zipcode")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.HasKey("Id");

                    b.HasIndex("StreetAddress", "City", "State", "Zipcode")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("UHPostalService.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AddressID")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("UHPostalService.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AddressID")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Role")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(2);

                    b.Property<int?>("StoreID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressID");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.HasIndex("StoreID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("UHPostalService.Models.Package", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AddressID")
                        .HasColumnType("int");

                    b.Property<int>("ClassID")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<float>("Depth")
                        .HasColumnType("real");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Express")
                        .HasColumnType("bit");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<int?>("ReceiverID")
                        .HasColumnType("int");

                    b.Property<int?>("SenderID")
                        .HasColumnType("int");

                    b.Property<float?>("ShipCost")
                        .HasColumnType("real");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.Property<float>("Width")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("AddressID");

                    b.HasIndex("ClassID");

                    b.HasIndex("ReceiverID");

                    b.HasIndex("SenderID");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("UHPostalService.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Desc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<float>("UnitCost")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("UHPostalService.Models.Sale", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int?>("BuyerID")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int?>("ProductID")
                        .HasColumnType("int");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<float?>("Total")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.HasIndex("BuyerID");

                    b.HasIndex("ProductID");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("UHPostalService.Models.ShipmentClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Desc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("ExpressCost")
                        .HasColumnType("real");

                    b.Property<float>("GroundCost")
                        .HasColumnType("real");

                    b.Property<float>("MaxHeight")
                        .HasColumnType("real");

                    b.Property<float>("MaxLength")
                        .HasColumnType("real");

                    b.Property<float>("MaxWidth")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("ShipmentClasses");
                });

            modelBuilder.Entity("UHPostalService.Models.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AddressID")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SupID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressID")
                        .IsUnique()
                        .HasFilter("[AddressID] IS NOT NULL");

                    b.HasIndex("SupID");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("UHPostalService.Models.TrackingRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int?>("Destination")
                        .HasColumnType("int");

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int?>("StoreId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime?>("TimeIn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TimeOut")
                        .HasColumnType("datetime2");

                    b.Property<int>("TrackNum")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Destination");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("StoreId");

                    b.HasIndex("TrackNum");

                    b.ToTable("TrackingRecords");
                });

            modelBuilder.Entity("UHPostalService.Models.Customer", b =>
                {
                    b.HasOne("UHPostalService.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Address");
                });

            modelBuilder.Entity("UHPostalService.Models.Employee", b =>
                {
                    b.HasOne("UHPostalService.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UHPostalService.Models.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreID")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Address");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("UHPostalService.Models.Package", b =>
                {
                    b.HasOne("UHPostalService.Models.Address", "Destination")
                        .WithMany()
                        .HasForeignKey("AddressID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UHPostalService.Models.ShipmentClass", "Type")
                        .WithMany()
                        .HasForeignKey("ClassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UHPostalService.Models.Customer", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverID")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("UHPostalService.Models.Customer", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderID")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Destination");

                    b.Navigation("Receiver");

                    b.Navigation("Sender");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("UHPostalService.Models.Sale", b =>
                {
                    b.HasOne("UHPostalService.Models.Customer", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerID")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("UHPostalService.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Buyer");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("UHPostalService.Models.Store", b =>
                {
                    b.HasOne("UHPostalService.Models.Address", "Address")
                        .WithOne()
                        .HasForeignKey("UHPostalService.Models.Store", "AddressID")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("UHPostalService.Models.Employee", "Supervisor")
                        .WithMany()
                        .HasForeignKey("SupID")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Address");

                    b.Navigation("Supervisor");
                });

            modelBuilder.Entity("UHPostalService.Models.TrackingRecord", b =>
                {
                    b.HasOne("UHPostalService.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("Destination")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("UHPostalService.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("UHPostalService.Models.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UHPostalService.Models.Package", "Package")
                        .WithMany()
                        .HasForeignKey("TrackNum")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Employee");

                    b.Navigation("Package");

                    b.Navigation("Store");
                });
#pragma warning restore 612, 618
        }
    }
}
