using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobileRecharge.Migrations
{
    public partial class setForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "RechargeModel",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_RechargeModel_PlanId",
                table: "RechargeModel",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_RechargeModel_UserId",
                table: "RechargeModel",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RechargeModel_AspNetUsers_UserId",
                table: "RechargeModel",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RechargeModel_MobilePlanModel_PlanId",
                table: "RechargeModel",
                column: "PlanId",
                principalTable: "MobilePlanModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RechargeModel_AspNetUsers_UserId",
                table: "RechargeModel");

            migrationBuilder.DropForeignKey(
                name: "FK_RechargeModel_MobilePlanModel_PlanId",
                table: "RechargeModel");

            migrationBuilder.DropIndex(
                name: "IX_RechargeModel_PlanId",
                table: "RechargeModel");

            migrationBuilder.DropIndex(
                name: "IX_RechargeModel_UserId",
                table: "RechargeModel");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "RechargeModel",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
