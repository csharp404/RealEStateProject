﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Presentation.Data;

#nullable disable

namespace Presentation.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users", (string)null);

                    b.HasDiscriminator().HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Presentation.Models.Address", b =>
                {
                    b.Property<string>("AddressID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CityID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CountryID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HoodID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RealESID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressID");

                    b.HasIndex("CityID");

                    b.HasIndex("CountryID");

                    b.HasIndex("HoodID");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Presentation.Models.Category", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Presentation.Models.City", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CountryId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Presentation.Models.Comments", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAT")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RealESID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RealESID");

                    b.HasIndex("UserID");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Presentation.Models.Country", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Presentation.Models.Favorite", b =>
                {
                    b.Property<string>("RealESID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RealESID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("Presentation.Models.Feature", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("Presentation.Models.Hood", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CityId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("CityId");

                    b.ToTable("Hoods");
                });

            modelBuilder.Entity("Presentation.Models.RealES", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddressID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Area_Size")
                        .HasColumnType("int");

                    b.Property<string>("CategoryID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("RoomID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Views")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("AddressID")
                        .IsUnique();

                    b.HasIndex("CategoryID");

                    b.HasIndex("RoomID")
                        .IsUnique();

                    b.HasIndex("UserID");

                    b.ToTable("RealES");
                });

            modelBuilder.Entity("Presentation.Models.RealESFeature", b =>
                {
                    b.Property<string>("FeatureID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RealESID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FeatureID", "RealESID");

                    b.HasIndex("RealESID");

                    b.ToTable("RealESFeatures");
                });

            modelBuilder.Entity("Presentation.Models.RealESImages", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RealESId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ID");

                    b.HasIndex("RealESId");

                    b.ToTable("RealESImages");
                });

            modelBuilder.Entity("Presentation.Models.RealESService", b =>
                {
                    b.Property<string>("ServiceID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RealESId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ServiceID", "RealESId");

                    b.HasIndex("RealESId");

                    b.ToTable("RealESServices");
                });

            modelBuilder.Entity("Presentation.Models.Room", b =>
                {
                    b.Property<string>("RoomID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("N_Bathroom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("N_Bedroom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("N_Floors")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("N_Garage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("N_Kitchen")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("N_LivingRoom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("N_Rooms")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RealESId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoomID");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Presentation.Models.Services", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("Presentation.Models.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<DateTime>("BDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoverName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoverPath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Presentation.Models.Address", b =>
                {
                    b.HasOne("Presentation.Models.City", "City")
                        .WithMany("Addresses")
                        .HasForeignKey("CityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Presentation.Models.Country", "Country")
                        .WithMany("Addresses")
                        .HasForeignKey("CountryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Presentation.Models.Hood", "Hood")
                        .WithMany("Addresses")
                        .HasForeignKey("HoodID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Country");

                    b.Navigation("Hood");
                });

            modelBuilder.Entity("Presentation.Models.City", b =>
                {
                    b.HasOne("Presentation.Models.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Presentation.Models.Comments", b =>
                {
                    b.HasOne("Presentation.Models.RealES", "RealES")
                        .WithMany("Comments")
                        .HasForeignKey("RealESID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Presentation.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RealES");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Presentation.Models.Favorite", b =>
                {
                    b.HasOne("Presentation.Models.RealES", "RealES")
                        .WithMany("Favorites")
                        .HasForeignKey("RealESID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Presentation.Models.User", "User")
                        .WithMany("Favorites")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RealES");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Presentation.Models.Hood", b =>
                {
                    b.HasOne("Presentation.Models.City", "City")
                        .WithMany("hoods")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("Presentation.Models.RealES", b =>
                {
                    b.HasOne("Presentation.Models.Address", "Address")
                        .WithOne("RealES")
                        .HasForeignKey("Presentation.Models.RealES", "AddressID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Presentation.Models.Category", "Category")
                        .WithMany("RealEs")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Presentation.Models.Room", "Room")
                        .WithOne("RealES")
                        .HasForeignKey("Presentation.Models.RealES", "RoomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Presentation.Models.User", "User")
                        .WithMany("RealES")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Category");

                    b.Navigation("Room");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Presentation.Models.RealESFeature", b =>
                {
                    b.HasOne("Presentation.Models.Feature", "Feature")
                        .WithMany("RealESFeatures")
                        .HasForeignKey("FeatureID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Presentation.Models.RealES", "RealES")
                        .WithMany("RealESFeatures")
                        .HasForeignKey("RealESID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Feature");

                    b.Navigation("RealES");
                });

            modelBuilder.Entity("Presentation.Models.RealESImages", b =>
                {
                    b.HasOne("Presentation.Models.RealES", "RealES")
                        .WithMany("Images")
                        .HasForeignKey("RealESId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RealES");
                });

            modelBuilder.Entity("Presentation.Models.RealESService", b =>
                {
                    b.HasOne("Presentation.Models.RealES", "RealES")
                        .WithMany("RealESServices")
                        .HasForeignKey("RealESId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Presentation.Models.Services", "Service")
                        .WithMany("RealESServices")
                        .HasForeignKey("ServiceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RealES");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("Presentation.Models.Address", b =>
                {
                    b.Navigation("RealES")
                        .IsRequired();
                });

            modelBuilder.Entity("Presentation.Models.Category", b =>
                {
                    b.Navigation("RealEs");
                });

            modelBuilder.Entity("Presentation.Models.City", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("hoods");
                });

            modelBuilder.Entity("Presentation.Models.Country", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Cities");
                });

            modelBuilder.Entity("Presentation.Models.Feature", b =>
                {
                    b.Navigation("RealESFeatures");
                });

            modelBuilder.Entity("Presentation.Models.Hood", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("Presentation.Models.RealES", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Favorites");

                    b.Navigation("Images");

                    b.Navigation("RealESFeatures");

                    b.Navigation("RealESServices");
                });

            modelBuilder.Entity("Presentation.Models.Room", b =>
                {
                    b.Navigation("RealES")
                        .IsRequired();
                });

            modelBuilder.Entity("Presentation.Models.Services", b =>
                {
                    b.Navigation("RealESServices");
                });

            modelBuilder.Entity("Presentation.Models.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Favorites");

                    b.Navigation("RealES");
                });
#pragma warning restore 612, 618
        }
    }
}
