using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class userproductid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProduct_OrderContent_OrderContentId",
                table: "UserProduct");

            migrationBuilder.DropIndex(
                name: "IX_UserProduct_OrderContentId",
                table: "UserProduct");

            migrationBuilder.DropColumn(
                name: "OrderContentId",
                table: "UserProduct");

            migrationBuilder.AddColumn<Guid>(
                name: "UserProductId",
                table: "OrderContent",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_OrderContent_UserProductId",
                table: "OrderContent",
                column: "UserProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderContent_UserProduct_UserProductId",
                table: "OrderContent",
                column: "UserProductId",
                principalTable: "UserProduct",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderContent_UserProduct_UserProductId",
                table: "OrderContent");

            migrationBuilder.DropIndex(
                name: "IX_OrderContent_UserProductId",
                table: "OrderContent");

            migrationBuilder.DropColumn(
                name: "UserProductId",
                table: "OrderContent");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderContentId",
                table: "UserProduct",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserProduct_OrderContentId",
                table: "UserProduct",
                column: "OrderContentId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProduct_OrderContent_OrderContentId",
                table: "UserProduct",
                column: "OrderContentId",
                principalTable: "OrderContent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
