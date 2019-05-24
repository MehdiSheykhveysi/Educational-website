﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Site.Core.DataBase.Context;

namespace Site.Core.DataBase.Migrations
{
    [DbContext(typeof(LearningSiteDbContext))]
    partial class LearningSiteDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Site.Core.Domain.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CourseDescription")
                        .HasMaxLength(400);

                    b.Property<int?>("CourseGroupId");

                    b.Property<int?>("CourseLevelId");

                    b.Property<decimal>("CoursePrice")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int?>("CourseStatusId");

                    b.Property<string>("CourseTitle")
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreateDate");

                    b.Property<Guid?>("CustomUserId");

                    b.Property<string>("DemoFileName")
                        .HasMaxLength(255);

                    b.Property<string>("ImageName")
                        .HasMaxLength(255);

                    b.Property<bool>("IsDeleted");

                    b.Property<TimeSpan>("TotalEpisodTime");

                    b.Property<DateTime?>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("CourseGroupId");

                    b.HasIndex("CourseLevelId");

                    b.HasIndex("CourseStatusId");

                    b.HasIndex("CustomUserId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("Site.Core.Domain.Entities.CourseEpisod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CourseId");

                    b.Property<TimeSpan>("EpisodeTime");

                    b.Property<string>("FileName");

                    b.Property<bool>("IsFree");

                    b.Property<string>("Title")
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseEpisod");
                });

            modelBuilder.Entity("Site.Core.Domain.Entities.CourseGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted");

                    b.Property<int?>("ParentId");

                    b.Property<string>("Title")
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("CourseGroup");
                });

            modelBuilder.Entity("Site.Core.Domain.Entities.CourseLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("CourseLevel");
                });

            modelBuilder.Entity("Site.Core.Domain.Entities.CourseStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title")
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.ToTable("CourseStatus");
                });

            modelBuilder.Entity("Site.Core.Domain.Entities.CustomUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<int>("AccessFailedCount");

                    b.Property<decimal>("AccountBalance")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("Avatar")
                        .HasMaxLength(300);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("PaymentToken")
                        .HasMaxLength(20);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(11);

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("datetime");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("ShowUserName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Site.Core.Domain.Entities.DisCount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count");

                    b.Property<int>("DisCountPercent");

                    b.Property<DateTime>("MaxDate");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("Title")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("DisCount");
                });

            modelBuilder.Entity("Site.Core.Domain.Entities.Keyword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CourseId");

                    b.Property<string>("Title")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Keyword");
                });

            modelBuilder.Entity("Site.Core.Domain.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<string>("AnonymousUserId")
                        .HasMaxLength(14);

                    b.Property<Guid?>("ClientId");

                    b.Property<bool>("IsBought");

                    b.Property<DateTime>("OrderingTime");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Site.Core.Domain.Entities.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseId");

                    b.Property<Guid?>("OrderId");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("Site.Core.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Site.Core.Domain.Entities.Transact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("newsequentialid()");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<Guid>("CustomUserId");

                    b.Property<string>("Description")
                        .HasMaxLength(300);

                    b.Property<DateTime>("TransactDate")
                        .HasColumnType("datetime");

                    b.Property<string>("TransactId");

                    b.Property<int>("TransactType");

                    b.HasKey("Id");

                    b.HasIndex("CustomUserId");

                    b.ToTable("Transact");
                });

            modelBuilder.Entity("Site.Core.Domain.Entities.UserRole", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.Property<Guid?>("RoleId1");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("RoleId1");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Site.Core.Domain.Entities.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Site.Core.Domain.Entities.CustomUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Site.Core.Domain.Entities.CustomUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Site.Core.Domain.Entities.CustomUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Core.Domain.Entities.Course", b =>
                {
                    b.HasOne("Site.Core.Domain.Entities.CourseGroup", "CourseGroup")
                        .WithMany("Courses")
                        .HasForeignKey("CourseGroupId");

                    b.HasOne("Site.Core.Domain.Entities.CourseLevel", "CourseLevel")
                        .WithMany("Courses")
                        .HasForeignKey("CourseLevelId");

                    b.HasOne("Site.Core.Domain.Entities.CourseStatus", "CourseStatus")
                        .WithMany("Courses")
                        .HasForeignKey("CourseStatusId");

                    b.HasOne("Site.Core.Domain.Entities.CustomUser", "CustomUser")
                        .WithMany("Courses")
                        .HasForeignKey("CustomUserId");
                });

            modelBuilder.Entity("Site.Core.Domain.Entities.CourseEpisod", b =>
                {
                    b.HasOne("Site.Core.Domain.Entities.Course", "Course")
                        .WithMany("CourseEpisods")
                        .HasForeignKey("CourseId");
                });

            modelBuilder.Entity("Site.Core.Domain.Entities.CourseGroup", b =>
                {
                    b.HasOne("Site.Core.Domain.Entities.CourseGroup", "ParentCourseGroup")
                        .WithMany("Groups")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("Site.Core.Domain.Entities.Keyword", b =>
                {
                    b.HasOne("Site.Core.Domain.Entities.Course", "Course")
                        .WithMany("Keywordkeys")
                        .HasForeignKey("CourseId");
                });

            modelBuilder.Entity("Site.Core.Domain.Entities.Order", b =>
                {
                    b.HasOne("Site.Core.Domain.Entities.CustomUser", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId");
                });

            modelBuilder.Entity("Site.Core.Domain.Entities.OrderDetail", b =>
                {
                    b.HasOne("Site.Core.Domain.Entities.Course", "Course")
                        .WithMany("OrderDetails")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Site.Core.Domain.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Core.Domain.Entities.Transact", b =>
                {
                    b.HasOne("Site.Core.Domain.Entities.CustomUser", "CustomUser")
                        .WithMany("Transactions")
                        .HasForeignKey("CustomUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Site.Core.Domain.Entities.UserRole", b =>
                {
                    b.HasOne("Site.Core.Domain.Entities.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Site.Core.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId1");

                    b.HasOne("Site.Core.Domain.Entities.CustomUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
