using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class OrderEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Addresses",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    BuyerEmail = table.Column<string>(type: "TEXT", nullable: false),
                    ShippingAddress_Name = table.Column<string>(type: "TEXT", nullable: false),
                    ShippingAddress_Line1 = table.Column<string>(type: "TEXT", nullable: false),
                    ShippingAddress_Line2 = table.Column<string>(type: "TEXT", nullable: true),
                    ShippingAddress_City = table.Column<string>(type: "TEXT", nullable: false),
                    ShippingAddress_State = table.Column<string>(type: "TEXT", nullable: false),
                    ShippingAddress_PostalCode = table.Column<string>(type: "TEXT", nullable: false),
                    ShippingAddress_Country = table.Column<string>(type: "TEXT", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Subtotal = table.Column<long>(type: "INTEGER", nullable: false),
                    DeliveryFee = table.Column<long>(type: "INTEGER", nullable: false),
                    Discount = table.Column<long>(type: "INTEGER", nullable: false),
                    PaymentIntentId = table.Column<string>(type: "TEXT", nullable: true),
                    orderStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    paymentSummary_Last4 = table.Column<int>(type: "INTEGER", nullable: false),
                    paymentSummary_Brand = table.Column<string>(type: "TEXT", nullable: false),
                    paymentSummary_ExpMonth = table.Column<int>(type: "INTEGER", nullable: false),
                    paymentSummary_ExpYear = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemOrdered_productId = table.Column<int>(type: "INTEGER", nullable: false),
                    ItemOrdered_Name = table.Column<string>(type: "TEXT", nullable: false),
                    ItemOrdered_PictureUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<long>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => new { x.OrderId, x.Id });
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "Addresses",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
