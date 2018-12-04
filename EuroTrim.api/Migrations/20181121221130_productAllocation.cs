using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EuroTrim.api.Migrations
{
    public partial class productAllocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerProductAllocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    DateOrderCreated = table.Column<DateTime>(nullable: false),
                    DiscountBandId = table.Column<int>(nullable: false),
                    DiscountValue = table.Column<decimal>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerProductAllocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerProductAllocations_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerProductAllocations_DiscountBands_DiscountBandId",
                        column: x => x.DiscountBandId,
                        principalTable: "DiscountBands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerProductAllocations_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProductAllocations_CustomerId",
                table: "CustomerProductAllocations",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProductAllocations_DiscountBandId",
                table: "CustomerProductAllocations",
                column: "DiscountBandId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerProductAllocations_ProductId",
                table: "CustomerProductAllocations",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerProductAllocations");
        }
    }
}
