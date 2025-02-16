using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISEPay.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddNavigationPropertiesToCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId",
                table: "ExchangeRates",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId1",
                table: "ExchangeRates",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_CurrencyId",
                table: "ExchangeRates",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_CurrencyId1",
                table: "ExchangeRates",
                column: "CurrencyId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Currencies_CurrencyId",
                table: "ExchangeRates",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Currencies_CurrencyId1",
                table: "ExchangeRates",
                column: "CurrencyId1",
                principalTable: "Currencies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Currencies_CurrencyId",
                table: "ExchangeRates");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Currencies_CurrencyId1",
                table: "ExchangeRates");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRates_CurrencyId",
                table: "ExchangeRates");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRates_CurrencyId1",
                table: "ExchangeRates");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "ExchangeRates");

            migrationBuilder.DropColumn(
                name: "CurrencyId1",
                table: "ExchangeRates");
        }
    }
}
