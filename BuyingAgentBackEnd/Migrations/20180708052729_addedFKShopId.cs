using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BuyingAgentBackEnd.Migrations
{
    public partial class addedFKShopId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "Visits",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visits_ShopId",
                table: "Visits",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Shops_ShopId",
                table: "Visits",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Shops_ShopId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_ShopId",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Visits");
        }
    }
}
