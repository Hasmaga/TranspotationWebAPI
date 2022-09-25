﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TranspotationAPI.DbContexts;

#nullable disable

namespace TranspotationWebAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TranspotationWebAPI.Model.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int")
                        .HasColumnName("CompanyId");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Password");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Phone");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("RoleId");

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("Status");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("RoleId");

                    b.ToTable("Account", "dbo");
                });

            modelBuilder.Entity("TranspotationWebAPI.Model.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Car", "dbo");
                });

            modelBuilder.Entity("TranspotationWebAPI.Model.CarType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Image");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<string>("TypeCar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TypeCar");

                    b.HasKey("Id");

                    b.ToTable("CarType", "dbo");
                });

            modelBuilder.Entity("TranspotationWebAPI.Model.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Address");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Phone");

                    b.HasKey("Id");

                    b.ToTable("Company", "dbo");
                });

            modelBuilder.Entity("TranspotationWebAPI.Model.CompanyTrip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CarId")
                        .HasColumnType("int")
                        .HasColumnName("CarId");

                    b.Property<int?>("CarTypeId")
                        .HasColumnType("int")
                        .HasColumnName("CarTypeId");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int")
                        .HasColumnName("CompanyId");

                    b.Property<int?>("Price")
                        .HasColumnType("int")
                        .HasColumnName("Price");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("StartTime");

                    b.Property<int?>("StationId")
                        .HasColumnType("int")
                        .HasColumnName("StationId");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("Status");

                    b.Property<int?>("TripId")
                        .HasColumnType("int")
                        .HasColumnName("TripId");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("CarTypeId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("StationId");

                    b.HasIndex("TripId");

                    b.ToTable("CompanyTrip", "dbo");
                });

            modelBuilder.Entity("TranspotationWebAPI.Model.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Location", "dbo");
                });

            modelBuilder.Entity("TranspotationWebAPI.Model.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Authority")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Authority");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Role", "dbo");
                });

            modelBuilder.Entity("TranspotationWebAPI.Model.Station", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Address");

                    b.Property<int?>("LocationId")
                        .HasColumnType("int")
                        .HasColumnName("LocationId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Station", "dbo");
                });

            modelBuilder.Entity("TranspotationWebAPI.Model.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AccountId")
                        .HasColumnType("int")
                        .HasColumnName("AccountId");

                    b.Property<int?>("CompanyTripId")
                        .HasColumnType("int")
                        .HasColumnName("CompanyTripId");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<string>("SeatName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SeatName");

                    b.Property<bool>("Status")
                        .HasColumnType("bit")
                        .HasColumnName("Status");

                    b.Property<int?>("Total")
                        .HasColumnType("int")
                        .HasColumnName("Total");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("CompanyTripId");

                    b.ToTable("Ticket", "dbo");
                });

            modelBuilder.Entity("TranspotationWebAPI.Model.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("From_Id")
                        .HasColumnType("int");

                    b.Property<int?>("To_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("From_Id");

                    b.HasIndex("To_Id");

                    b.ToTable("Trip", "dbo");
                });

            modelBuilder.Entity("TranspotationWebAPI.Model.Account", b =>
                {
                    b.HasOne("TranspotationWebAPI.Model.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("TranspotationWebAPI.Model.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TranspotationWebAPI.Model.CompanyTrip", b =>
                {
                    b.HasOne("TranspotationWebAPI.Model.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId");

                    b.HasOne("TranspotationWebAPI.Model.CarType", "CarType")
                        .WithMany()
                        .HasForeignKey("CarTypeId");

                    b.HasOne("TranspotationWebAPI.Model.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId");

                    b.HasOne("TranspotationWebAPI.Model.Station", "Station")
                        .WithMany()
                        .HasForeignKey("StationId");

                    b.HasOne("TranspotationWebAPI.Model.Trip", "Trip")
                        .WithMany()
                        .HasForeignKey("TripId");

                    b.Navigation("Car");

                    b.Navigation("CarType");

                    b.Navigation("Company");

                    b.Navigation("Station");

                    b.Navigation("Trip");
                });

            modelBuilder.Entity("TranspotationWebAPI.Model.Station", b =>
                {
                    b.HasOne("TranspotationWebAPI.Model.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("TranspotationWebAPI.Model.Ticket", b =>
                {
                    b.HasOne("TranspotationWebAPI.Model.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("TranspotationWebAPI.Model.CompanyTrip", "CompanyTrip")
                        .WithMany()
                        .HasForeignKey("CompanyTripId");

                    b.Navigation("Account");

                    b.Navigation("CompanyTrip");
                });

            modelBuilder.Entity("TranspotationWebAPI.Model.Trip", b =>
                {
                    b.HasOne("TranspotationWebAPI.Model.Location", "From")
                        .WithMany("FromTrip")
                        .HasForeignKey("From_Id");

                    b.HasOne("TranspotationWebAPI.Model.Location", "To")
                        .WithMany("ToTrip")
                        .HasForeignKey("To_Id");

                    b.Navigation("From");

                    b.Navigation("To");
                });

            modelBuilder.Entity("TranspotationWebAPI.Model.Location", b =>
                {
                    b.Navigation("FromTrip");

                    b.Navigation("ToTrip");
                });
#pragma warning restore 612, 618
        }
    }
}
