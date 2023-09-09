using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Passingwind.EasyGet.Migrations;

/// <inheritdoc />
public partial class Update8 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_AppPackages_Name",
            table: "AppPackages");

        migrationBuilder.DropIndex(
            name: "IX_AppNuGetPackages_Name_Version",
            table: "AppNuGetPackages");

        migrationBuilder.RenameColumn(
            name: "PrereleaseVersion",
            table: "AppNuGetPackages",
            newName: "Prerelease");

        migrationBuilder.AddColumn<bool>(
            name: "IsSemVer2",
            table: "AppNuGetPackages",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.CreateTable(
            name: "AppNuGetPackageTypes",
            columns: table => new
            {
                NuGetPackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PackageType = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                Version = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AppNuGetPackageTypes", x => new { x.NuGetPackageId, x.PackageType, x.Version });
                table.ForeignKey(
                    name: "FK_AppNuGetPackageTypes_AppNuGetPackages_NuGetPackageId",
                    column: x => x.NuGetPackageId,
                    principalTable: "AppNuGetPackages",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_AppPackages_FeedId_Name",
            table: "AppPackages",
            columns: new[] { "FeedId", "Name" });

        migrationBuilder.CreateIndex(
            name: "IX_AppNuGetPackages_FeedId_Name",
            table: "AppNuGetPackages",
            columns: new[] { "FeedId", "Name" });

        migrationBuilder.CreateIndex(
            name: "IX_AppNuGetPackages_PackageId",
            table: "AppNuGetPackages",
            column: "PackageId");

        migrationBuilder.AddForeignKey(
            name: "FK_AppNuGetPackages_AppPackages_PackageId",
            table: "AppNuGetPackages",
            column: "PackageId",
            principalTable: "AppPackages",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_AppNuGetPackages_AppPackages_PackageId",
            table: "AppNuGetPackages");

        migrationBuilder.DropTable(
            name: "AppNuGetPackageTypes");

        migrationBuilder.DropIndex(
            name: "IX_AppPackages_FeedId_Name",
            table: "AppPackages");

        migrationBuilder.DropIndex(
            name: "IX_AppNuGetPackages_FeedId_Name",
            table: "AppNuGetPackages");

        migrationBuilder.DropIndex(
            name: "IX_AppNuGetPackages_PackageId",
            table: "AppNuGetPackages");

        migrationBuilder.DropColumn(
            name: "IsSemVer2",
            table: "AppNuGetPackages");

        migrationBuilder.RenameColumn(
            name: "Prerelease",
            table: "AppNuGetPackages",
            newName: "PrereleaseVersion");

        migrationBuilder.CreateIndex(
            name: "IX_AppPackages_Name",
            table: "AppPackages",
            column: "Name");

        migrationBuilder.CreateIndex(
            name: "IX_AppNuGetPackages_Name_Version",
            table: "AppNuGetPackages",
            columns: new[] { "Name", "Version" });
    }
}
