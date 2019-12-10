using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CaseloadManager.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    LicenseNumber = table.Column<string>(maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    AssessmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestName = table.Column<string>(maxLength: 40, nullable: false),
                    OldestAllowed = table.Column<int>(nullable: false),
                    YoungestAllowed = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.AssessmentId);
                });

            migrationBuilder.CreateTable(
                name: "FacilityTypes",
                columns: table => new
                {
                    FacilityTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityTypes", x => x.FacilityTypeId);
                });

            migrationBuilder.CreateTable(
                name: "StatusTypes",
                columns: table => new
                {
                    StatusTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusTypes", x => x.StatusTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    FacilityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    FacilityTypeId = table.Column<string>(nullable: false),
                    FacilityTypeId1 = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.FacilityId);
                    table.ForeignKey(
                        name: "FK_Facilities_FacilityTypes_FacilityTypeId1",
                        column: x => x.FacilityTypeId1,
                        principalTable: "FacilityTypes",
                        principalColumn: "FacilityTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Facilities_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstInitial = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(maxLength: 25, nullable: false),
                    Birthdate = table.Column<DateTime>(nullable: false),
                    Diagnosis = table.Column<string>(nullable: true),
                    SessionsPerWeek = table.Column<int>(nullable: true),
                    StatusTypeId = table.Column<int>(nullable: false),
                    FacilityTypeId = table.Column<int>(nullable: false),
                    FacilityId = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Clients_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "FacilityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clients_StatusTypes_StatusTypeId",
                        column: x => x.StatusTypeId,
                        principalTable: "StatusTypes",
                        principalColumn: "StatusTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clients_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientAssessments",
                columns: table => new
                {
                    ClientAssessmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StandarizedScore = table.Column<int>(nullable: false),
                    DateAdministered = table.Column<DateTime>(nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    AssessmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientAssessments", x => x.ClientAssessmentId);
                    table.ForeignKey(
                        name: "FK_ClientAssessments_Assessments_AssessmentId",
                        column: x => x.AssessmentId,
                        principalTable: "Assessments",
                        principalColumn: "AssessmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientAssessments_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    GoalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.GoalId);
                    table.ForeignKey(
                        name: "FK_Goals_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TherapySessions",
                columns: table => new
                {
                    TherapySessionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(maxLength: 225, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TherapySessions", x => x.TherapySessionId);
                    table.ForeignKey(
                        name: "FK_TherapySessions_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TherapySessionsGoals",
                columns: table => new
                {
                    TherapySessionGoalId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoalId = table.Column<int>(nullable: false),
                    TherapySessionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TherapySessionsGoals", x => x.TherapySessionGoalId);
                    table.ForeignKey(
                        name: "FK_TherapySessionsGoals_Goals_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goals",
                        principalColumn: "GoalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TherapySessionsGoals_TherapySessions_TherapySessionId",
                        column: x => x.TherapySessionId,
                        principalTable: "TherapySessions",
                        principalColumn: "TherapySessionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LicenseNumber", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "00000000-ffff-ffff-ffff-ffffffffffff", 0, "8add3abb-117a-4550-bcc4-286482677a3e", "admin@admin.com", true, "admin", "admin", "1222019", false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEAYQe479ib5O5xQHboA4VzLMprQo4O6n0muqjybcFlrtr1LWv/5aSrJ2/Q7qTP1Gow==", null, false, "7f434309-a4d9-48e9-9ebb-8803db794577", false, "admin@admin.com" },
                    { "00000000-ffff-ffff-ffff-ffffffffffff1", 0, "cf7ab4bd-1269-4ea2-bc3f-6721e5c17a99", "allie@gmail.com", true, "Allison", "Patton", "1222020", false, null, "ALLIE@GMAIL.COM", "ALLIE@GMAIL.COM", "AQAAAAEAACcQAAAAEEEoMEwt+xrQc80/zow1xNEHlJRpSA7VbniiY4JVMPtwc3TCQdKVKQl+BXPf3oc3MA==", null, false, "7f434309-a4d9-48e9-9ebb-8803db794578", false, "Allie@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "FacilityTypes",
                columns: new[] { "FacilityTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "Elementary School" },
                    { 2, "Middle School" },
                    { 3, "Highschool" },
                    { 4, "Hospital" },
                    { 5, "Skilled Nursing Facility" },
                    { 6, "Clinic" }
                });

            migrationBuilder.InsertData(
                table: "StatusTypes",
                columns: new[] { "StatusTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "Needs Screening" },
                    { 2, "Needs Assessment" },
                    { 3, "Eligible" },
                    { 4, "InEligble" },
                    { 5, "Discharged" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "Birthdate", "Diagnosis", "FacilityId", "FacilityTypeId", "FirstInitial", "LastName", "SessionsPerWeek", "StatusTypeId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(1944, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 5, "E", "Smith", 0, 1, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 18, new DateTime(2010, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Speech Disorder", null, 1, "M", "Harris", 2, 5, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 17, new DateTime(1945, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "TBI", null, 5, "P", "Tellei", 3, 5, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 16, new DateTime(1946, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 5, "O", "Lopez", 0, 4, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 15, new DateTime(1943, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 5, "K", "Brown", 0, 4, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 14, new DateTime(2012, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 1, "H", "Hansen", 0, 4, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 13, new DateTime(2011, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 1, "R", "Ivanov", 0, 4, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddTicks(1934), "Dysphagia", null, 5, "F", "Anderson", 3, 3, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 11, new DateTime(2009, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Language Disorder", null, 1, "E", "Clark", 3, 3, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 10, new DateTime(2011, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Language Disorder", null, 1, "T", "Peters", 1, 3, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 9, new DateTime(1942, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aphasia", null, 5, "J", "Rossi", 2, 3, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 8, new DateTime(1936, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 5, "S", "Hernandez", 0, 2, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 7, new DateTime(2012, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 1, "C", "Murphy", 0, 2, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 6, new DateTime(2009, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 1, "A", "Wilson", 0, 2, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 5, new DateTime(2011, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 1, "L", "Miller", 0, 2, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 4, new DateTime(1940, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 5, "D", "Jones", 0, 1, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 3, new DateTime(2011, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 1, "C", "Williams", 0, 1, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 2, new DateTime(2008, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 1, "B", "Johnson", 0, 1, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 19, new DateTime(2010, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Language Disorder", null, 1, "N", "Kim", 2, 5, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 20, new DateTime(1940, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dysphagia", null, 5, "B", "Taylor", 2, 5, "00000000-ffff-ffff-ffff-ffffffffffff" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAssessments_AssessmentId",
                table: "ClientAssessments",
                column: "AssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientAssessments_ClientId",
                table: "ClientAssessments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_FacilityId",
                table: "Clients",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_StatusTypeId",
                table: "Clients",
                column: "StatusTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UserId",
                table: "Clients",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_FacilityTypeId1",
                table: "Facilities",
                column: "FacilityTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_UserId",
                table: "Facilities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_ClientId",
                table: "Goals",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TherapySessions_ClientId",
                table: "TherapySessions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TherapySessionsGoals_GoalId",
                table: "TherapySessionsGoals",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_TherapySessionsGoals_TherapySessionId",
                table: "TherapySessionsGoals",
                column: "TherapySessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ClientAssessments");

            migrationBuilder.DropTable(
                name: "TherapySessionsGoals");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Assessments");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "TherapySessions");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "StatusTypes");

            migrationBuilder.DropTable(
                name: "FacilityTypes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
