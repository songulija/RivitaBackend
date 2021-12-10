﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RivitaBackend.Models;

namespace RivitaBackend.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RivitaBackend.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Rivita"
                        });
                });

            modelBuilder.Entity("RivitaBackend.Models.Transportation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CargoAcceptanceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DepartureCountryTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DepartureStationTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DestinationCountryTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DestinationStationTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EtsngCargoCode")
                        .HasColumnType("int");

                    b.Property<int>("GngCargoCode")
                        .HasColumnType("int");

                    b.Property<DateTime>("MovementEndDateInBelarus")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("MovementStartDateInBelarus")
                        .HasColumnType("datetime2");

                    b.Property<string>("StationMovementBeginingBelarusTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StationMovementEndBelarusTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransportationNumber")
                        .HasColumnType("int");

                    b.Property<string>("TransportationType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("WagonsCount")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransportationNumber")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Transportations");
                });

            modelBuilder.Entity("RivitaBackend.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("TypeId");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasFilter("[Username] IS NOT NULL");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c9490c27-1b89-4e39-8f2e-99b48dcc709e"),
                            CompanyId = 1,
                            Password = "$2a$11$Kufw.f10S3aacLOosVG9p.fGi2e2pRX5tcKQYz5woDt.YnFa70ana",
                            PhoneNumber = "+37061816214",
                            TypeId = 1,
                            Username = "jevgenijrivita"
                        });
                });

            modelBuilder.Entity("RivitaBackend.Models.UserType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = 2,
                            Title = "USER"
                        });
                });

            modelBuilder.Entity("RivitaBackend.Models.Wagon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NumberOfWagon")
                        .HasColumnType("int");

                    b.Property<Guid>("TransportationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TypeOfWagon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransportationId");

                    b.ToTable("Wagons");
                });

            modelBuilder.Entity("RivitaBackend.Models.Transportation", b =>
                {
                    b.HasOne("RivitaBackend.Models.User", "User")
                        .WithMany("Transportations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RivitaBackend.Models.User", b =>
                {
                    b.HasOne("RivitaBackend.Models.Company", "Company")
                        .WithMany("Users")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RivitaBackend.Models.UserType", "UserType")
                        .WithMany("Users")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("UserType");
                });

            modelBuilder.Entity("RivitaBackend.Models.Wagon", b =>
                {
                    b.HasOne("RivitaBackend.Models.Transportation", "Transportation")
                        .WithMany("Wagons")
                        .HasForeignKey("TransportationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transportation");
                });

            modelBuilder.Entity("RivitaBackend.Models.Company", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("RivitaBackend.Models.Transportation", b =>
                {
                    b.Navigation("Wagons");
                });

            modelBuilder.Entity("RivitaBackend.Models.User", b =>
                {
                    b.Navigation("Transportations");
                });

            modelBuilder.Entity("RivitaBackend.Models.UserType", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
