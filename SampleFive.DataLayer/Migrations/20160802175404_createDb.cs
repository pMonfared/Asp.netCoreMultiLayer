using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SampleFive.DataLayer.Migrations
{
    public partial class createDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SysB.AppRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysB.AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysB.AppUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysB.AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysB.AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysB.AppUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "SysB.AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysB.AppRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysB.AppRoleClaims_SysB.AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SysB.AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysB.AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysB.AppUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysB.AppUserClaims_SysB.AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "SysB.AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysB.AppUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysB.AppUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_SysB.AppUserLogins_SysB.AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "SysB.AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysB.AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysB.AppUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_SysB.AppUserRoles_SysB.AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "SysB.AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SysB.AppUserRoles_SysB.AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "SysB.AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserUsedPassword",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppUserId = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    HashPassword = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserUsedPassword", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserUsedPassword_SysB.AppUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "SysB.AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "SysB.AppRoles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_SysB.AppRoleClaims_RoleId",
                table: "SysB.AppRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "SysB.AppUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "SysB.AppUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SysB.AppUserClaims_UserId",
                table: "SysB.AppUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SysB.AppUserLogins_UserId",
                table: "SysB.AppUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SysB.AppUserRoles_RoleId",
                table: "SysB.AppUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SysB.AppUserRoles_UserId",
                table: "SysB.AppUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserUsedPassword_AppUserId",
                table: "ApplicationUserUsedPassword",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SysB.AppRoleClaims");

            migrationBuilder.DropTable(
                name: "SysB.AppUserClaims");

            migrationBuilder.DropTable(
                name: "SysB.AppUserLogins");

            migrationBuilder.DropTable(
                name: "SysB.AppUserRoles");

            migrationBuilder.DropTable(
                name: "SysB.AppUserTokens");

            migrationBuilder.DropTable(
                name: "ApplicationUserUsedPassword");

            migrationBuilder.DropTable(
                name: "SysB.AppRoles");

            migrationBuilder.DropTable(
                name: "SysB.AppUsers");
        }
    }
}
