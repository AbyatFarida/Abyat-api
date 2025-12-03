using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Abyat.Da.Migrations
{
    /// <inheritdoc />
    public partial class SyncMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
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
                    LoginProvider = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
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
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
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
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
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
                name: "TbAuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EntityId = table.Column<int>(type: "int", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbAuditLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbAuditLogs_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbAuditLogs_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TbFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbFeatures_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbFeatures_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TbImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbImages_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbImages_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TbProcesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbProcess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbProcesses_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbProcesses_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TbProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DescriptionAr = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbProducts_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbProducts_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TbServiceCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbServiceCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbServiceCategories_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbServiceCategories_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TbImageSizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SmallSizeId = table.Column<int>(type: "int", nullable: true),
                    MediumSizeId = table.Column<int>(type: "int", nullable: false),
                    LargeSizeId = table.Column<int>(type: "int", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbImageSize", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbImageSizes_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbImageSizes_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbImageSizes_TbImages_LargeSizeId",
                        column: x => x.LargeSizeId,
                        principalTable: "TbImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbImageSizes_TbImages_MediumSizeId",
                        column: x => x.MediumSizeId,
                        principalTable: "TbImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbImageSizes_TbImages_SmallSizeId",
                        column: x => x.SmallSizeId,
                        principalTable: "TbImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TbProcessSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    DescriptionAr = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ProcessId = table.Column<int>(type: "int", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbProcessStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbProcessSteps_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbProcessSteps_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbProcessSteps_TbProcesses_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "TbProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DescriptionAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContentEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ContentAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WhyEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    WhyAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ServiceCategoryId = table.Column<int>(type: "int", nullable: false),
                    ProcessId = table.Column<int>(type: "int", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbServices_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbServices_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbServices_TbProcesses_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "TbProcesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbServices_TbServiceCategories_ServiceCategoryId",
                        column: x => x.ServiceCategoryId,
                        principalTable: "TbServiceCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TbCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DescriptionAr = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    AddressEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AddressAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LogoId = table.Column<int>(type: "int", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbCompany", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbCompanies_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbCompanies_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbCompanies_TbImageSizes_LogoId",
                        column: x => x.LogoId,
                        principalTable: "TbImageSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TbProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImageSizeId = table.Column<int>(type: "int", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbProductImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbProductImages_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbProductImages_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbProductImages_TbImageSizes_ImageSizeId",
                        column: x => x.ImageSizeId,
                        principalTable: "TbImageSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbProductImages_TbProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "TbProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbSliders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DescriptionAr = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ButtonTextEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ButtonTextAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ButtonUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ImageSizeId = table.Column<int>(type: "int", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbSlider", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbSliders_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbSliders_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbSliders_TbImageSizes_ImageSizeId",
                        column: x => x.ImageSizeId,
                        principalTable: "TbImageSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TbServiceFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbServiceFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbServiceFeatures_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbServiceFeatures_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbServiceFeatures_TbFeatures_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "TbFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbServiceFeatures_TbServices_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "TbServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbServiceImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    ImageSizeId = table.Column<int>(type: "int", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbServiceImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbServiceImages_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbServiceImages_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbServiceImages_TbImageSizes_ImageSizeId",
                        column: x => x.ImageSizeId,
                        principalTable: "TbImageSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbServiceImages_TbServices_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "TbServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbServiceProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbServiceProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbServiceProducts_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbServiceProducts_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbServiceProducts_TbProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "TbProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbServiceProducts_TbServices_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "TbServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbClients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbClient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbClients_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbClients_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbClients_TbCompanies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "TbCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TbProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TitleAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DescriptionEn = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DescriptionAr = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbProjects_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbProjects_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbProjects_TbClients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "TbClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TbTestimonials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TxtEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TxtAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbTestimonial", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbTestimonials_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbTestimonials_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbTestimonials_TbClients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "TbClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbProjectImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ImageSizeId = table.Column<int>(type: "int", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbProjectImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbProjectImages_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbProjectImages_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbProjectImages_TbImageSizes_ImageSizeId",
                        column: x => x.ImageSizeId,
                        principalTable: "TbImageSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbProjectImages_TbProjects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "TbProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbTestimonialImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestimonialId = table.Column<int>(type: "int", nullable: false),
                    ImageSizeId = table.Column<int>(type: "int", nullable: false),
                    TbImageSizeId = table.Column<int>(type: "int", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbTestimonialImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbTestimonialImages_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbTestimonialImages_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TbTestimonialImages_TbImageSizes_ImageSizeId",
                        column: x => x.ImageSizeId,
                        principalTable: "TbImageSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbTestimonialImages_TbImageSizes_TbImageSizeId",
                        column: x => x.TbImageSizeId,
                        principalTable: "TbImageSizes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TbTestimonialImages_TbTestimonials_TestimonialId",
                        column: x => x.TestimonialId,
                        principalTable: "TbTestimonials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_TbAuditLogs_CreatedBy",
                table: "TbAuditLogs",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbAuditLogs_UpdatedBy",
                table: "TbAuditLogs",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbClients_CompanyId",
                table: "TbClients",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TbClients_CreatedBy",
                table: "TbClients",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbClients_UpdatedBy",
                table: "TbClients",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbCompanies_CreatedBy",
                table: "TbCompanies",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbCompanies_LogoId",
                table: "TbCompanies",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_TbCompanies_NameAr",
                table: "TbCompanies",
                column: "NameAr",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbCompanies_NameEn",
                table: "TbCompanies",
                column: "NameEn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbCompanies_UpdatedBy",
                table: "TbCompanies",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbFeatures_CreatedBy",
                table: "TbFeatures",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbFeatures_TitleAr",
                table: "TbFeatures",
                column: "TitleAr",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbFeatures_TitleEn",
                table: "TbFeatures",
                column: "TitleEn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbFeatures_UpdatedBy",
                table: "TbFeatures",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbImages_CreatedBy",
                table: "TbImages",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbImages_Slug",
                table: "TbImages",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbImages_UpdatedBy",
                table: "TbImages",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbImageSizes_CreatedBy",
                table: "TbImageSizes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbImageSizes_LargeSizeId",
                table: "TbImageSizes",
                column: "LargeSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_TbImageSizes_MediumSizeId",
                table: "TbImageSizes",
                column: "MediumSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_TbImageSizes_SmallSizeId",
                table: "TbImageSizes",
                column: "SmallSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_TbImageSizes_UpdatedBy",
                table: "TbImageSizes",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbProcesses_CreatedBy",
                table: "TbProcesses",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbProcesses_Title",
                table: "TbProcesses",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbProcesses_UpdatedBy",
                table: "TbProcesses",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbProcessSteps_CreatedBy",
                table: "TbProcessSteps",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbProcessSteps_ProcessId_Order",
                table: "TbProcessSteps",
                columns: new[] { "ProcessId", "Order" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbProcessSteps_UpdatedBy",
                table: "TbProcessSteps",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbProductImages_CreatedBy",
                table: "TbProductImages",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbProductImages_ImageSizeId",
                table: "TbProductImages",
                column: "ImageSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_TbProductImages_ProductId_ImageSizeId",
                table: "TbProductImages",
                columns: new[] { "ProductId", "ImageSizeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbProductImages_UpdatedBy",
                table: "TbProductImages",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbProducts_CreatedBy",
                table: "TbProducts",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbProducts_TitleAr",
                table: "TbProducts",
                column: "TitleAr",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbProducts_TitleEn",
                table: "TbProducts",
                column: "TitleEn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbProducts_UpdatedBy",
                table: "TbProducts",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbProjectImages_CreatedBy",
                table: "TbProjectImages",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbProjectImages_ImageSizeId",
                table: "TbProjectImages",
                column: "ImageSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_TbProjectImages_ProjectId_ImageSizeId",
                table: "TbProjectImages",
                columns: new[] { "ProjectId", "ImageSizeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbProjectImages_UpdatedBy",
                table: "TbProjectImages",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbProjects_ClientId",
                table: "TbProjects",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TbProjects_CreatedBy",
                table: "TbProjects",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbProjects_Order",
                table: "TbProjects",
                column: "Order",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbProjects_TitleAr",
                table: "TbProjects",
                column: "TitleAr",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbProjects_TitleEn",
                table: "TbProjects",
                column: "TitleEn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbProjects_UpdatedBy",
                table: "TbProjects",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbServiceCategories_CreatedBy",
                table: "TbServiceCategories",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbServiceCategories_Title",
                table: "TbServiceCategories",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbServiceCategories_UpdatedBy",
                table: "TbServiceCategories",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbServiceFeatures_CreatedBy",
                table: "TbServiceFeatures",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbServiceFeatures_FeatureId",
                table: "TbServiceFeatures",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_TbServiceFeatures_ServiceId_FeatureId",
                table: "TbServiceFeatures",
                columns: new[] { "ServiceId", "FeatureId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbServiceFeatures_UpdatedBy",
                table: "TbServiceFeatures",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbServiceImages_CreatedBy",
                table: "TbServiceImages",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbServiceImages_ImageSizeId",
                table: "TbServiceImages",
                column: "ImageSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_TbServiceImages_ServiceId_ImageSizeId",
                table: "TbServiceImages",
                columns: new[] { "ServiceId", "ImageSizeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbServiceImages_UpdatedBy",
                table: "TbServiceImages",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbServiceProducts_CreatedBy",
                table: "TbServiceProducts",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbServiceProducts_ProductId_ServiceId",
                table: "TbServiceProducts",
                columns: new[] { "ProductId", "ServiceId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbServiceProducts_ServiceId",
                table: "TbServiceProducts",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TbServiceProducts_UpdatedBy",
                table: "TbServiceProducts",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbServices_CreatedBy",
                table: "TbServices",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbServices_ProcessId",
                table: "TbServices",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_TbServices_ServiceCategoryId",
                table: "TbServices",
                column: "ServiceCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TbServices_Slug",
                table: "TbServices",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbServices_TitleAr",
                table: "TbServices",
                column: "TitleAr",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbServices_TitleEn",
                table: "TbServices",
                column: "TitleEn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbServices_UpdatedBy",
                table: "TbServices",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbSliders_CreatedBy",
                table: "TbSliders",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbSliders_ImageSizeId",
                table: "TbSliders",
                column: "ImageSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_TbSliders_Order",
                table: "TbSliders",
                column: "Order",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbSliders_TitleAr",
                table: "TbSliders",
                column: "TitleAr",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbSliders_TitleEn",
                table: "TbSliders",
                column: "TitleEn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbSliders_UpdatedBy",
                table: "TbSliders",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbTestimonialImages_CreatedBy",
                table: "TbTestimonialImages",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbTestimonialImages_ImageSizeId",
                table: "TbTestimonialImages",
                column: "ImageSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_TbTestimonialImages_TbImageSizeId",
                table: "TbTestimonialImages",
                column: "TbImageSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_TbTestimonialImages_TestimonialId_ImageSizeId",
                table: "TbTestimonialImages",
                columns: new[] { "TestimonialId", "ImageSizeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbTestimonialImages_UpdatedBy",
                table: "TbTestimonialImages",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbTestimonials_ClientId",
                table: "TbTestimonials",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TbTestimonials_CreatedBy",
                table: "TbTestimonials",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TbTestimonials_UpdatedBy",
                table: "TbTestimonials",
                column: "UpdatedBy");
        }

        /// <inheritdoc />
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
                name: "TbAuditLogs");

            migrationBuilder.DropTable(
                name: "TbProcessSteps");

            migrationBuilder.DropTable(
                name: "TbProductImages");

            migrationBuilder.DropTable(
                name: "TbProjectImages");

            migrationBuilder.DropTable(
                name: "TbServiceFeatures");

            migrationBuilder.DropTable(
                name: "TbServiceImages");

            migrationBuilder.DropTable(
                name: "TbServiceProducts");

            migrationBuilder.DropTable(
                name: "TbSliders");

            migrationBuilder.DropTable(
                name: "TbTestimonialImages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "TbProjects");

            migrationBuilder.DropTable(
                name: "TbFeatures");

            migrationBuilder.DropTable(
                name: "TbProducts");

            migrationBuilder.DropTable(
                name: "TbServices");

            migrationBuilder.DropTable(
                name: "TbTestimonials");

            migrationBuilder.DropTable(
                name: "TbProcesses");

            migrationBuilder.DropTable(
                name: "TbServiceCategories");

            migrationBuilder.DropTable(
                name: "TbClients");

            migrationBuilder.DropTable(
                name: "TbCompanies");

            migrationBuilder.DropTable(
                name: "TbImageSizes");

            migrationBuilder.DropTable(
                name: "TbImages");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
