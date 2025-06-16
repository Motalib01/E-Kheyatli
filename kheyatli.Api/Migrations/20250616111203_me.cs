using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kheyatli.Api.Migrations
{
    /// <inheritdoc />
    public partial class me : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "ProductMeasurements");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "ProductMeasurements",
                newName: "Waist");

            migrationBuilder.AddColumn<string>(
                name: "Chest",
                table: "ProductMeasurements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Height",
                table: "ProductMeasurements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hip",
                table: "ProductMeasurements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Inseam",
                table: "ProductMeasurements",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SleeveLength",
                table: "ProductMeasurements",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chest",
                table: "ProductMeasurements");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "ProductMeasurements");

            migrationBuilder.DropColumn(
                name: "Hip",
                table: "ProductMeasurements");

            migrationBuilder.DropColumn(
                name: "Inseam",
                table: "ProductMeasurements");

            migrationBuilder.DropColumn(
                name: "SleeveLength",
                table: "ProductMeasurements");

            migrationBuilder.RenameColumn(
                name: "Waist",
                table: "ProductMeasurements",
                newName: "Value");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ProductMeasurements",
                type: "int",
                nullable: true);
        }
    }
}
