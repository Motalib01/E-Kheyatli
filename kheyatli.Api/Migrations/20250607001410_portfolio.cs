using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kheyatli.Api.Migrations
{
    /// <inheritdoc />
    public partial class portfolio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tailors_PortfolioId",
                table: "Tailors");

            migrationBuilder.AlterColumn<Guid>(
                name: "PortfolioId",
                table: "Tailors",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Tailors_PortfolioId",
                table: "Tailors",
                column: "PortfolioId",
                unique: true,
                filter: "[PortfolioId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tailors_PortfolioId",
                table: "Tailors");

            migrationBuilder.AlterColumn<Guid>(
                name: "PortfolioId",
                table: "Tailors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tailors_PortfolioId",
                table: "Tailors",
                column: "PortfolioId",
                unique: true);
        }
    }
}
