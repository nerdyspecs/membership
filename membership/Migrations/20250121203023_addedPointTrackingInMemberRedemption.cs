using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace membership.Migrations
{
    /// <inheritdoc />
    public partial class addedPointTrackingInMemberRedemption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PointsDeducted",
                table: "MemberRedemptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PointsRemaining",
                table: "MemberRedemptions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PointsDeducted",
                table: "MemberRedemptions");

            migrationBuilder.DropColumn(
                name: "PointsRemaining",
                table: "MemberRedemptions");
        }
    }
}
