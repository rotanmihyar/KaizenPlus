﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using kaizenplus.DataAccess;

#nullable disable

namespace kaizenplus.Migrations
{
    [DbContext(typeof(DatabaseService))]
    [Migration("20240528083521_warehouseItems")]
    partial class warehouseItems
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("kaizenplus.Domain.Countries.Country", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Country");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Jordan"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Germany"
                        });
                });

            modelBuilder.Entity("kaizenplus.Domain.Roles.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Management"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Auditor"
                        });
                });

            modelBuilder.Entity("kaizenplus.Domain.UserRoles.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("8240573d-becc-4aae-b2ab-974979de96a1"),
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("kaizenplus.Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Active")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastLoginDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Picture")
                        .HasColumnType("TEXT");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("RefreshTokenValidUntil")
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("8240573d-becc-4aae-b2ab-974979de96a1"),
                            Active = true,
                            CreatedDate = new DateTime(1989, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@happywarehouse.com",
                            FirstName = "system",
                            IsVerified = true,
                            LastName = "admin",
                            PasswordHash = new byte[] { 190, 247, 154, 193, 153, 218, 178, 199, 132, 13, 39, 44, 87, 229, 149, 12, 139, 125, 121, 179, 73, 116, 161, 59, 101, 159, 196, 139, 210, 170, 175, 79, 45, 15, 103, 191, 130, 10, 20, 78, 180, 152, 210, 48, 111, 112, 120, 244, 5, 159, 72, 109, 12, 202, 225, 211, 108, 156, 141, 102, 188, 127, 89, 234 },
                            PasswordSalt = new byte[] { 173, 146, 185, 223, 241, 235, 40, 208, 13, 89, 63, 157, 111, 247, 249, 250, 138, 114, 114, 164, 235, 187, 125, 173, 82, 156, 253, 194, 83, 215, 184, 172, 233, 11, 55, 65, 21, 239, 153, 225, 157, 174, 58, 92, 48, 132, 25, 220, 48, 3, 141, 38, 157, 32, 116, 123, 111, 41, 200, 185, 224, 59, 227, 70, 240, 190, 5, 210, 140, 16, 29, 119, 66, 7, 171, 78, 118, 124, 122, 31, 238, 114, 100, 135, 184, 243, 122, 140, 34, 64, 46, 138, 76, 209, 170, 8, 181, 65, 151, 62, 166, 98, 93, 146, 208, 36, 15, 145, 2, 189, 159, 171, 11, 153, 143, 20, 156, 197, 203, 16, 234, 157, 6, 134, 53, 23, 148, 146 },
                            PhoneNumber = "07950430205",
                            Username = "admin@happywarehouse.com"
                        });
                });

            modelBuilder.Entity("kaizenplus.Domain.WarehouseItems.WarehouseItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("CostPrice")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("MSRPPrice")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<long>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SKU")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.Property<long>("WarehouseId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("DeletedById");

                    b.HasIndex("UpdatedById");

                    b.HasIndex("WarehouseId");

                    b.ToTable("WarehouseItem");
                });

            modelBuilder.Entity("kaizenplus.Domain.Warehouses.Warehouse", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<long>("CountryId")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UpdatedById")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("DeletedById");

                    b.HasIndex("UpdatedById");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("kaizenplus.Domain.UserRoles.UserRole", b =>
                {
                    b.HasOne("kaizenplus.Domain.Roles.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("kaizenplus.Domain.Users.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("kaizenplus.Domain.WarehouseItems.WarehouseItem", b =>
                {
                    b.HasOne("kaizenplus.Domain.Users.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("kaizenplus.Domain.Users.User", "DeletedBy")
                        .WithMany()
                        .HasForeignKey("DeletedById");

                    b.HasOne("kaizenplus.Domain.Users.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.HasOne("kaizenplus.Domain.Warehouses.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("DeletedBy");

                    b.Navigation("UpdatedBy");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("kaizenplus.Domain.Warehouses.Warehouse", b =>
                {
                    b.HasOne("kaizenplus.Domain.Countries.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("kaizenplus.Domain.Users.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("kaizenplus.Domain.Users.User", "DeletedBy")
                        .WithMany()
                        .HasForeignKey("DeletedById");

                    b.HasOne("kaizenplus.Domain.Users.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedById");

                    b.Navigation("Country");

                    b.Navigation("CreatedBy");

                    b.Navigation("DeletedBy");

                    b.Navigation("UpdatedBy");
                });

            modelBuilder.Entity("kaizenplus.Domain.Roles.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("kaizenplus.Domain.Users.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
