using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace membership.Migrations
{
    /// <inheritdoc />
    public partial class LevelPointsToTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinPoint",
                table: "Levels",
                newName: "MinTotalTransaction");

            migrationBuilder.RenameColumn(
                name: "MaxPoint",
                table: "Levels",
                newName: "MaxTotalTransaction");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MinTotalTransaction",
                table: "Levels",
                newName: "MinPoint");

            migrationBuilder.RenameColumn(
                name: "MaxTotalTransaction",
                table: "Levels",
                newName: "MaxPoint");
        }
    }
}
