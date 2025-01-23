using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace membership.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRedemptionItemModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequiredLevel",
                table: "RedemptionItems",
                newName: "LevelId");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "RedemptionItems",
                newName: "RedemptionItemId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "RedemptionItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RedemptionItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RedemptionItems_LevelId",
                table: "RedemptionItems",
                column: "LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_RedemptionItems_Levels_LevelId",
                table: "RedemptionItems",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedemptionItems_Levels_LevelId",
                table: "RedemptionItems");

            migrationBuilder.DropIndex(
                name: "IX_RedemptionItems_LevelId",
                table: "RedemptionItems");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "RedemptionItems");

            migrationBuilder.RenameColumn(
                name: "LevelId",
                table: "RedemptionItems",
                newName: "RequiredLevel");

            migrationBuilder.RenameColumn(
                name: "RedemptionItemId",
                table: "RedemptionItems",
                newName: "ItemId");

            migrationBuilder.AlterColumn<int>(
                name: "Title",
                table: "RedemptionItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
