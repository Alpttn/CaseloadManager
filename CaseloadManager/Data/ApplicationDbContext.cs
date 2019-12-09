using System;
using System.Collections.Generic;
using System.Text;
using CaseloadManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CaseloadManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<TherapySession> TherapySessions { get; set; }
        //Do I want these for the models for building type
        public DbSet<BuildingType> BuildingTypes { get; set; }
        public DbSet<StatusType> StatusTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Create a new user for Identity Framework
            ApplicationUser user = new ApplicationUser
            {
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff"
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            ApplicationUser user2 = new ApplicationUser
            {
                FirstName = "Allison",
                LastName = "Patton",
                UserName = "Allie@gmail.com",
                NormalizedUserName = "ALLIE@GMAIL.COM",
                Email = "allie@gmail.com",
                NormalizedEmail = "ALLIE@GMAIL.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794578",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff1"
            };
            user2.PasswordHash = passwordHash.HashPassword(user2, "SLP8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user2);

            //create facility types
            modelBuilder.Entity<BuildingType>().HasData(
                new BuildingType()
                {
                    BuildingTypeId = 1,
                    Name = "Elementary School"
                },
                new BuildingType()
                {
                    BuildingTypeId = 2,
                    Name = "Middle School"
                },
                new BuildingType()
                {
                    BuildingTypeId = 3,
                    Name = "Highschool"
                },
                new BuildingType()
                {
                    BuildingTypeId = 4,
                    Name = "Hospital"
                },
                new BuildingType()
                {
                    BuildingTypeId = 5,
                    Name = "Skilled Nursing Facility"
                },
                new BuildingType()
                {
                    BuildingTypeId = 6,
                    Name = "Clinic"
                }
               );

            //create Status types
            modelBuilder.Entity<StatusType>().HasData(
                new StatusType()
                {
                    StatusTypeId = 1,
                    Name = "Needs Screening"
                },
                new StatusType()
                {
                    StatusTypeId = 2,
                    Name = "Needs Assessment"
                },
                new StatusType()
                {
                    StatusTypeId = 3,
                    Name = "Eligible"
                },
                new StatusType()
                {
                    StatusTypeId = 4,
                    Name = "InEligble"
                },
                new StatusType()
                {
                    StatusTypeId = 5,
                    Name = "Discharged"
                }
               );

            //create clients
            modelBuilder.Entity<Client>().HasData(
                //needs screening
                new Client()
                {
                    FirstInitial = "E",
                    LastName = "Smith",
                    Birthdate = new DateTime(1944, 01, 02),
                    Diagnosis = null,
                    SessionsPerWeek = 0,
                    FacilityId = 5,
                    StatusId = 1,
                    UserId = user.Id, //Do I need this?
                },
                new Client()
                {
                    FirstInitial = "B",
                    LastName = "Johnson",
                    Birthdate = new DateTime(2008, 02, 05),
                    Diagnosis = null,
                    SessionsPerWeek = 0,
                    FacilityId = 1,
                    StatusId = 1,
                    UserId = user.Id,
                },
                new Client()
                {
                    FirstInitial = "C",
                    LastName = "Williams",
                    Birthdate = new DateTime(2011, 03, 15),
                    Diagnosis = null,
                    SessionsPerWeek = 0,
                    FacilityId = 1,
                    StatusId = 1,
                    UserId = user.Id,
                },
                new Client()
                {
                    FirstInitial = "D",
                    LastName = "Jones",
                    Birthdate = new DateTime(1940, 10, 01),
                    Diagnosis = null,
                    SessionsPerWeek = 0,
                    FacilityId = 5,
                    StatusId = 1,
                    UserId = user.Id,
                },
                //needs Assessment
                new Client()
                {
                    FirstInitial = "L",
                    LastName = "Miller",
                    Birthdate = new DateTime(2011, 01, 02),
                    Diagnosis = null,
                    SessionsPerWeek = 0,
                    FacilityId = 1,
                    StatusId = 2,
                    UserId = user.Id, //Do I need this?
                },
                new Client()
                {
                    FirstInitial = "A",
                    LastName = "Wilson",
                    Birthdate = new DateTime(2009, 11, 20),
                    Diagnosis = null,
                    SessionsPerWeek = 0,
                    FacilityId = 1,
                    StatusId = 2,
                    UserId = user.Id,
                },
                new Client()
                {
                    FirstInitial = "C",
                    LastName = "Murphy",
                    Birthdate = new DateTime(2012, 09, 06),
                    Diagnosis = null,
                    SessionsPerWeek = 0,
                    FacilityId = 1,
                    StatusId = 2,
                    UserId = user.Id,
                },
                new Client()
                {
                    FirstInitial = "S",
                    LastName = "Hernandez",
                    Birthdate = new DateTime(1936, 07, 12),
                    Diagnosis = null,
                    SessionsPerWeek = 0,
                    FacilityId = 5,
                    StatusId = 2,
                    UserId = user.Id,
                },
                // eligible
                new Client()
                {
                    FirstInitial = "J",
                    LastName = "Rossi",
                    Birthdate = new DateTime(1942, 09, 04),
                    Diagnosis = "Aphasia",
                    SessionsPerWeek = 2,
                    FacilityId = 5,
                    StatusId = 3,
                    UserId = user.Id,
                },
                new Client()
                {
                    FirstInitial = "T",
                    LastName = "Peters",
                    Birthdate = new DateTime(2011, 08, 20),
                    Diagnosis = "Language Disorder",
                    SessionsPerWeek = 1,
                    FacilityId = 1,
                    StatusId = 3,
                    UserId = user.Id,
                },
                new Client()
                {
                    FirstInitial = "E",
                    LastName = "Clark",
                    Birthdate = new DateTime(2009, 11, 26),
                    Diagnosis = "Language Disorder",
                    SessionsPerWeek = 3,
                    FacilityId = 1,
                    StatusId = 3,
                    UserId = user.Id,
                },
                new Client()
                {
                    FirstInitial = "F",
                    LastName = "Anderson",
                    Birthdate = new DateTime(1944 - 05 - 05),
                    Diagnosis = "Dysphagia",
                    SessionsPerWeek = 3,
                    FacilityId = 5,
                    StatusId = 3,
                    UserId = user.Id,
                },
                new Client()
                {
                    FirstInitial = "F",
                    LastName = "Anderson",
                    Birthdate = new DateTime(1944 - 05 - 05),
                    Diagnosis = "Dysphagia",
                    SessionsPerWeek = 3,
                    FacilityId = 5,
                    StatusId = 3,
                    UserId = user.Id,
                },
                // ineligible
                new Client()
                {
                    FirstInitial = "R",
                    LastName = "Ivanov",
                    Birthdate = new DateTime(2011, 01, 26),
                    Diagnosis = null,
                    SessionsPerWeek = 0,
                    FacilityId = 1,
                    StatusId = 4,
                    UserId = user.Id,
                },
                new Client()
                {
                    FirstInitial = "H",
                    LastName = "Hansen",
                    Birthdate = new DateTime(2012, 12, 03),
                    Diagnosis = null,
                    SessionsPerWeek = 0,
                    FacilityId = 1,
                    StatusId = 4,
                    UserId = user.Id,
                },
                new Client()
                {
                    FirstInitial = "K",
                    LastName = "Brown",
                    Birthdate = new DateTime(1943, 10, 01),
                    Diagnosis = null,
                    SessionsPerWeek = 0,
                    FacilityId = 5,
                    StatusId = 4,
                    UserId = user.Id,
                },
                new Client()
                {
                    FirstInitial = "O",
                    LastName = "Lopez",
                    Birthdate = new DateTime(1946, 03, 30),
                    Diagnosis = null,
                    SessionsPerWeek = 0,
                    FacilityId = 5,
                    StatusId = 4,
                    UserId = user.Id,
                },
                // discharged
                new Client()
                {
                    FirstInitial = "P",
                    LastName = "Tellei",
                    Birthdate = new DateTime(1945, 05, 15),
                    Diagnosis = "TBI",
                    SessionsPerWeek = 3,
                    FacilityId = 5,
                    StatusId = 5,
                    UserId = user.Id,
                },
                new Client()
                {
                    FirstInitial = "M",
                    LastName = "Harris",
                    Birthdate = new DateTime(2010, 07, 23),
                    Diagnosis = "Speech Disorder",
                    SessionsPerWeek = 2,
                    FacilityId = 1,
                    StatusId = 5,
                    UserId = user.Id,
                },
                new Client()
                {
                    FirstInitial = "N",
                    LastName = "Kim",
                    Birthdate = new DateTime(2010, 03, 12),
                    Diagnosis = "Language Disorder",
                    SessionsPerWeek = 2,
                    FacilityId = 1,
                    StatusId = 5,
                    UserId = user.Id,
                },
                new Client()
                {
                    FirstInitial = "B",
                    LastName = "Taylor",
                    Birthdate = new DateTime(1940, 06, 15),
                    Diagnosis = "Dysphagia",
                    SessionsPerWeek = 2,
                    FacilityId = 5,
                    StatusId = 5,
                    UserId = user.Id,
                }
                );


        }
    }
}
