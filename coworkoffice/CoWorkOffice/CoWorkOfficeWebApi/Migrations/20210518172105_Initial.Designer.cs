﻿// <auto-generated />
using System;
using CoWorkOffice.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoWorkOfficeWebApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210518172105_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoWorkOfficeModel.Models.ConfortParameter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConfortValue")
                        .HasColumnType("int");

                    b.Property<string>("Parameter")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ConfortParameters");
                });

            modelBuilder.Entity("CoWorkOfficeModel.Models.Gateway", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.Property<string>("Protocol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("User")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Gateways");
                });

            modelBuilder.Entity("CoWorkOfficeModel.Models.IoTDevice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gateway_Id")
                        .HasColumnType("int");

                    b.Property<string>("ProbeType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegistrationIdentifier")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Room_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Gateway_Id");

                    b.HasIndex("Room_Id");

                    b.ToTable("IoTDevices");
                });

            modelBuilder.Entity("CoWorkOfficeModel.Models.Measure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("IoTDevice_Id")
                        .HasColumnType("int");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("IoTDevice_Id");

                    b.ToTable("Measures");
                });

            modelBuilder.Entity("CoWorkOfficeModel.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTimeEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateTimeStart")
                        .HasColumnType("datetime2");

                    b.Property<int>("NCustomerExpected")
                        .HasColumnType("int");

                    b.Property<int>("OfficeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OfficeId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("CoWorkOfficeModel.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CoWorkOfficeModel.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rooms");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Room");
                });

            modelBuilder.Entity("CoWorkOfficeModel.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CoWorkOfficeModel.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("CoWorkOfficeModel.Models.Office", b =>
                {
                    b.HasBaseType("CoWorkOfficeModel.Models.Room");

                    b.Property<int?>("WaitingRoomId")
                        .HasColumnType("int");

                    b.HasIndex("WaitingRoomId");

                    b.HasDiscriminator().HasValue("Office");
                });

            modelBuilder.Entity("CoWorkOfficeModel.Models.WaitingRoom", b =>
                {
                    b.HasBaseType("CoWorkOfficeModel.Models.Room");

                    b.Property<int>("NMaxCustomer")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("WaitingRoom");
                });

            modelBuilder.Entity("CoWorkOfficeModel.Models.IoTDevice", b =>
                {
                    b.HasOne("CoWorkOfficeModel.Models.Gateway", "Gateway")
                        .WithMany()
                        .HasForeignKey("Gateway_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoWorkOfficeModel.Models.Room", "Room")
                        .WithMany("IoTDevices")
                        .HasForeignKey("Room_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gateway");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("CoWorkOfficeModel.Models.Measure", b =>
                {
                    b.HasOne("CoWorkOfficeModel.Models.IoTDevice", "IoTDevice")
                        .WithMany()
                        .HasForeignKey("IoTDevice_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IoTDevice");
                });

            modelBuilder.Entity("CoWorkOfficeModel.Models.Reservation", b =>
                {
                    b.HasOne("CoWorkOfficeModel.Models.Office", "Office")
                        .WithMany()
                        .HasForeignKey("OfficeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoWorkOfficeModel.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Office");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CoWorkOfficeModel.Models.UserRole", b =>
                {
                    b.HasOne("CoWorkOfficeModel.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoWorkOfficeModel.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CoWorkOfficeModel.Models.Office", b =>
                {
                    b.HasOne("CoWorkOfficeModel.Models.WaitingRoom", "WaitingRoom")
                        .WithMany()
                        .HasForeignKey("WaitingRoomId");

                    b.Navigation("WaitingRoom");
                });

            modelBuilder.Entity("CoWorkOfficeModel.Models.Room", b =>
                {
                    b.Navigation("IoTDevices");
                });
#pragma warning restore 612, 618
        }
    }
}
