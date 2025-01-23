using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace membership.Migrations
{
    /// <inheritdoc />
    public partial class addedMemberRedemption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemberRedemptions",
                columns: table => new
                {
                    RedemptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    RedemptionItemId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberRedemptions", x => x.RedemptionId);
                    table.ForeignKey(
                        name: "FK_MemberRedemptions_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "MemberId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MemberRedemptions_RedemptionItems_RedemptionItemId",
                        column: x => x.RedemptionItemId,
                        principalTable: "RedemptionItems",
                        principalColumn: "RedemptionItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberRedemptions_MemberId",
                table: "MemberRedemptions",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberRedemptions_RedemptionItemId",
                table: "MemberRedemptions",
                column: "RedemptionItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberRedemptions");
        }
    }
}
