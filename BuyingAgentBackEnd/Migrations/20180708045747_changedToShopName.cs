using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BuyingAgentBackEnd.Migrations
{
    public partial class changedToShopName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Shop",
                table: "Visits",
                newName: "ShopName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShopName",
                table: "Visits",
                newName: "Shop");
        }
    }
}
