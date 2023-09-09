using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Passingwind.EasyGet.Migrations;

/// <inheritdoc />
public partial class Update4 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Authors",
            table: "AppNuGetPackages",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "LicenseUrl",
            table: "AppNuGetPackages",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "Listed",
            table: "AppNuGetPackages",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<string>(
            name: "Owners",
            table: "AppNuGetPackages",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "PrereleaseVersion",
            table: "AppNuGetPackages",
            type: "bit",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<string>(
            name: "ProjectUrl",
            table: "AppNuGetPackages",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Summary",
            table: "AppNuGetPackages",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Tags",
            table: "AppNuGetPackages",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Title",
            table: "AppNuGetPackages",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "Verified",
            table: "AppNuGetPackages",
            type: "bit",
            nullable: false,
            defaultValue: false);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Authors",
            table: "AppNuGetPackages");

        migrationBuilder.DropColumn(
            name: "LicenseUrl",
            table: "AppNuGetPackages");

        migrationBuilder.DropColumn(
            name: "Listed",
            table: "AppNuGetPackages");

        migrationBuilder.DropColumn(
            name: "Owners",
            table: "AppNuGetPackages");

        migrationBuilder.DropColumn(
            name: "PrereleaseVersion",
            table: "AppNuGetPackages");

        migrationBuilder.DropColumn(
            name: "ProjectUrl",
            table: "AppNuGetPackages");

        migrationBuilder.DropColumn(
            name: "Summary",
            table: "AppNuGetPackages");

        migrationBuilder.DropColumn(
            name: "Tags",
            table: "AppNuGetPackages");

        migrationBuilder.DropColumn(
            name: "Title",
            table: "AppNuGetPackages");

        migrationBuilder.DropColumn(
            name: "Verified",
            table: "AppNuGetPackages");
    }
}
