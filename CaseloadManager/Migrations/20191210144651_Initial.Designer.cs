﻿// <auto-generated />
using System;
using CaseloadManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CaseloadManager.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20191210144651_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CaseloadManager.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LicenseNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(7)")
                        .HasMaxLength(7);

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

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
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "00000000-ffff-ffff-ffff-ffffffffffff",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "8add3abb-117a-4550-bcc4-286482677a3e",
                            Email = "admin@admin.com",
                            EmailConfirmed = true,
                            FirstName = "admin",
                            LastName = "admin",
                            LicenseNumber = "1222019",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@ADMIN.COM",
                            NormalizedUserName = "ADMIN@ADMIN.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEAYQe479ib5O5xQHboA4VzLMprQo4O6n0muqjybcFlrtr1LWv/5aSrJ2/Q7qTP1Gow==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                            TwoFactorEnabled = false,
                            UserName = "admin@admin.com"
                        },
                        new
                        {
                            Id = "00000000-ffff-ffff-ffff-ffffffffffff1",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "cf7ab4bd-1269-4ea2-bc3f-6721e5c17a99",
                            Email = "allie@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Allison",
                            LastName = "Patton",
                            LicenseNumber = "1222020",
                            LockoutEnabled = false,
                            NormalizedEmail = "ALLIE@GMAIL.COM",
                            NormalizedUserName = "ALLIE@GMAIL.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEEEoMEwt+xrQc80/zow1xNEHlJRpSA7VbniiY4JVMPtwc3TCQdKVKQl+BXPf3oc3MA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794578",
                            TwoFactorEnabled = false,
                            UserName = "Allie@gmail.com"
                        });
                });

            modelBuilder.Entity("CaseloadManager.Models.Assessment", b =>
                {
                    b.Property<int>("AssessmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OldestAllowed")
                        .HasColumnType("int");

                    b.Property<string>("TestName")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<int>("YoungestAllowed")
                        .HasColumnType("int");

                    b.HasKey("AssessmentId");

                    b.ToTable("Assessments");
                });

            modelBuilder.Entity("CaseloadManager.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Diagnosis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FacilityId")
                        .HasColumnType("int");

                    b.Property<int>("FacilityTypeId")
                        .HasColumnType("int");

                    b.Property<string>("FirstInitial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(25)")
                        .HasMaxLength(25);

                    b.Property<int?>("SessionsPerWeek")
                        .HasColumnType("int");

                    b.Property<int>("StatusTypeId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ClientId");

                    b.HasIndex("FacilityId");

                    b.HasIndex("StatusTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            ClientId = 1,
                            Birthdate = new DateTime(1944, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FacilityTypeId = 5,
                            FirstInitial = "E",
                            LastName = "Smith",
                            SessionsPerWeek = 0,
                            StatusTypeId = 1,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            ClientId = 2,
                            Birthdate = new DateTime(2008, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FacilityTypeId = 1,
                            FirstInitial = "B",
                            LastName = "Johnson",
                            SessionsPerWeek = 0,
                            StatusTypeId = 1,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            ClientId = 3,
                            Birthdate = new DateTime(2011, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FacilityTypeId = 1,
                            FirstInitial = "C",
                            LastName = "Williams",
                            SessionsPerWeek = 0,
                            StatusTypeId = 1,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            ClientId = 4,
                            Birthdate = new DateTime(1940, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FacilityTypeId = 5,
                            FirstInitial = "D",
                            LastName = "Jones",
                            SessionsPerWeek = 0,
                            StatusTypeId = 1,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            ClientId = 5,
                            Birthdate = new DateTime(2011, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FacilityTypeId = 1,
                            FirstInitial = "L",
                            LastName = "Miller",
                            SessionsPerWeek = 0,
                            StatusTypeId = 2,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            ClientId = 6,
                            Birthdate = new DateTime(2009, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FacilityTypeId = 1,
                            FirstInitial = "A",
                            LastName = "Wilson",
                            SessionsPerWeek = 0,
                            StatusTypeId = 2,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            ClientId = 7,
                            Birthdate = new DateTime(2012, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FacilityTypeId = 1,
                            FirstInitial = "C",
                            LastName = "Murphy",
                            SessionsPerWeek = 0,
                            StatusTypeId = 2,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            ClientId = 8,
                            Birthdate = new DateTime(1936, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FacilityTypeId = 5,
                            FirstInitial = "S",
                            LastName = "Hernandez",
                            SessionsPerWeek = 0,
                            StatusTypeId = 2,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            ClientId = 9,
                            Birthdate = new DateTime(1942, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Diagnosis = "Aphasia",
                            FacilityTypeId = 5,
                            FirstInitial = "J",
                            LastName = "Rossi",
                            SessionsPerWeek = 2,
                            StatusTypeId = 3,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            ClientId = 10,
                            Birthdate = new DateTime(2011, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Diagnosis = "Language Disorder",
                            FacilityTypeId = 1,
                            FirstInitial = "T",
                            LastName = "Peters",
                            SessionsPerWeek = 1,
                            StatusTypeId = 3,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            ClientId = 11,
                            Birthdate = new DateTime(2009, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Diagnosis = "Language Disorder",
                            FacilityTypeId = 1,
                            FirstInitial = "E",
                            LastName = "Clark",
                            SessionsPerWeek = 3,
                            StatusTypeId = 3,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            ClientId = 12,
                            Birthdate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1934),
                            Diagnosis = "Dysphagia",
                            FacilityTypeId = 5,
                            FirstInitial = "F",
                            LastName = "Anderson",
                            SessionsPerWeek = 3,
                            StatusTypeId = 3,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            ClientId = 13,
                            Birthdate = new DateTime(2011, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FacilityTypeId = 1,
                            FirstInitial = "R",
                            LastName = "Ivanov",
                            SessionsPerWeek = 0,
                            StatusTypeId = 4,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            ClientId = 14,
                            Birthdate = new DateTime(2012, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FacilityTypeId = 1,
                            FirstInitial = "H",
                            LastName = "Hansen",
                            SessionsPerWeek = 0,
                            StatusTypeId = 4,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            ClientId = 15,
                            Birthdate = new DateTime(1943, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FacilityTypeId = 5,
                            FirstInitial = "K",
                            LastName = "Brown",
                            SessionsPerWeek = 0,
                            StatusTypeId = 4,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            ClientId = 16,
                            Birthdate = new DateTime(1946, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            FacilityTypeId = 5,
                            FirstInitial = "O",
                            LastName = "Lopez",
                            SessionsPerWeek = 0,
                            StatusTypeId = 4,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            ClientId = 17,
                            Birthdate = new DateTime(1945, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Diagnosis = "TBI",
                            FacilityTypeId = 5,
                            FirstInitial = "P",
                            LastName = "Tellei",
                            SessionsPerWeek = 3,
                            StatusTypeId = 5,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            ClientId = 18,
                            Birthdate = new DateTime(2010, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Diagnosis = "Speech Disorder",
                            FacilityTypeId = 1,
                            FirstInitial = "M",
                            LastName = "Harris",
                            SessionsPerWeek = 2,
                            StatusTypeId = 5,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            ClientId = 19,
                            Birthdate = new DateTime(2010, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Diagnosis = "Language Disorder",
                            FacilityTypeId = 1,
                            FirstInitial = "N",
                            LastName = "Kim",
                            SessionsPerWeek = 2,
                            StatusTypeId = 5,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            ClientId = 20,
                            Birthdate = new DateTime(1940, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Diagnosis = "Dysphagia",
                            FacilityTypeId = 5,
                            FirstInitial = "B",
                            LastName = "Taylor",
                            SessionsPerWeek = 2,
                            StatusTypeId = 5,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        });
                });

            modelBuilder.Entity("CaseloadManager.Models.ClientAssessment", b =>
                {
                    b.Property<int>("ClientAssessmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AssessmentId")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateAdministered")
                        .HasColumnType("datetime2");

                    b.Property<int>("StandarizedScore")
                        .HasColumnType("int");

                    b.HasKey("ClientAssessmentId");

                    b.HasIndex("AssessmentId");

                    b.HasIndex("ClientId");

                    b.ToTable("ClientAssessments");
                });

            modelBuilder.Entity("CaseloadManager.Models.Facility", b =>
                {
                    b.Property<int>("FacilityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FacilityTypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FacilityTypeId1")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("FacilityId");

                    b.HasIndex("FacilityTypeId1");

                    b.HasIndex("UserId");

                    b.ToTable("Facilities");
                });

            modelBuilder.Entity("CaseloadManager.Models.FacilityType", b =>
                {
                    b.Property<int>("FacilityTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FacilityTypeId");

                    b.ToTable("FacilityTypes");

                    b.HasData(
                        new
                        {
                            FacilityTypeId = 1,
                            Name = "Elementary School"
                        },
                        new
                        {
                            FacilityTypeId = 2,
                            Name = "Middle School"
                        },
                        new
                        {
                            FacilityTypeId = 3,
                            Name = "Highschool"
                        },
                        new
                        {
                            FacilityTypeId = 4,
                            Name = "Hospital"
                        },
                        new
                        {
                            FacilityTypeId = 5,
                            Name = "Skilled Nursing Facility"
                        },
                        new
                        {
                            FacilityTypeId = 6,
                            Name = "Clinic"
                        });
                });

            modelBuilder.Entity("CaseloadManager.Models.Goal", b =>
                {
                    b.Property<int>("GoalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GoalId");

                    b.HasIndex("ClientId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("CaseloadManager.Models.StatusType", b =>
                {
                    b.Property<int>("StatusTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatusTypeId");

                    b.ToTable("StatusTypes");

                    b.HasData(
                        new
                        {
                            StatusTypeId = 1,
                            Name = "Needs Screening"
                        },
                        new
                        {
                            StatusTypeId = 2,
                            Name = "Needs Assessment"
                        },
                        new
                        {
                            StatusTypeId = 3,
                            Name = "Eligible"
                        },
                        new
                        {
                            StatusTypeId = 4,
                            Name = "InEligble"
                        },
                        new
                        {
                            StatusTypeId = 5,
                            Name = "Discharged"
                        });
                });

            modelBuilder.Entity("CaseloadManager.Models.TherapySession", b =>
                {
                    b.Property<int>("TherapySessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(225)")
                        .HasMaxLength(225);

                    b.HasKey("TherapySessionId");

                    b.HasIndex("ClientId");

                    b.ToTable("TherapySessions");
                });

            modelBuilder.Entity("CaseloadManager.Models.TherapySessionGoal", b =>
                {
                    b.Property<int>("TherapySessionGoalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GoalId")
                        .HasColumnType("int");

                    b.Property<int>("TherapySessionId")
                        .HasColumnType("int");

                    b.HasKey("TherapySessionGoalId");

                    b.HasIndex("GoalId");

                    b.HasIndex("TherapySessionId");

                    b.ToTable("TherapySessionsGoals");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CaseloadManager.Models.Client", b =>
                {
                    b.HasOne("CaseloadManager.Models.Facility", "Facility")
                        .WithMany("Clients")
                        .HasForeignKey("FacilityId");

                    b.HasOne("CaseloadManager.Models.StatusType", "StatusType")
                        .WithMany("Clients")
                        .HasForeignKey("StatusTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CaseloadManager.Models.ApplicationUser", "User")
                        .WithMany("Clients")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CaseloadManager.Models.ClientAssessment", b =>
                {
                    b.HasOne("CaseloadManager.Models.Assessment", "Assessment")
                        .WithMany("ClientAssessmets")
                        .HasForeignKey("AssessmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CaseloadManager.Models.Client", "Client")
                        .WithMany("ClientAssessmets")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CaseloadManager.Models.Facility", b =>
                {
                    b.HasOne("CaseloadManager.Models.FacilityType", "FacilityType")
                        .WithMany("Facilities")
                        .HasForeignKey("FacilityTypeId1");

                    b.HasOne("CaseloadManager.Models.ApplicationUser", "User")
                        .WithMany("Facilities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CaseloadManager.Models.Goal", b =>
                {
                    b.HasOne("CaseloadManager.Models.Client", "Client")
                        .WithMany("Goals")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CaseloadManager.Models.TherapySession", b =>
                {
                    b.HasOne("CaseloadManager.Models.Client", "Client")
                        .WithMany("TherapySessions")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CaseloadManager.Models.TherapySessionGoal", b =>
                {
                    b.HasOne("CaseloadManager.Models.Goal", "Goal")
                        .WithMany("TherapySessionGoals")
                        .HasForeignKey("GoalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CaseloadManager.Models.TherapySession", "TherapySession")
                        .WithMany("TherapySessionGoals")
                        .HasForeignKey("TherapySessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                    b.HasOne("CaseloadManager.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CaseloadManager.Models.ApplicationUser", null)
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

                    b.HasOne("CaseloadManager.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CaseloadManager.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
