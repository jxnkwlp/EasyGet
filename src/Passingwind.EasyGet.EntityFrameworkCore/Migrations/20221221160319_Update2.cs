using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Passingwind.EasyGet.Migrations;

public partial class Update2 : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "AppNuGetPackages",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                PackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                Version = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                DownloadCount = table.Column<long>(type: "bigint", nullable: false),
                Size = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_AppNuGetPackages", x => x.Id));

        migrationBuilder.CreateTable(
            name: "AppPackages",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                FeedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                FeedType = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LatestVersion = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Size = table.Column<long>(type: "bigint", nullable: false),
                DownloadCount = table.Column<long>(type: "bigint", nullable: false),
                PackageCount = table.Column<long>(type: "bigint", nullable: false),
                TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_AppPackages", x => x.Id));

        migrationBuilder.CreateIndex(
            name: "IX_AppNuGetPackages_Name_Version",
            table: "AppNuGetPackages",
            columns: new[] { "Name", "Version" });

        migrationBuilder.CreateIndex(
            name: "IX_AppPackages_Name",
            table: "AppPackages",
            column: "Name");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "AppNuGetPackages");

        migrationBuilder.DropTable(
            name: "AppPackages");
    }
}
