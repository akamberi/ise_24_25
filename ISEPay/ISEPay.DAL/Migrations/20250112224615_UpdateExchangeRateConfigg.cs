using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISEPay.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExchangeRateConfigg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Currencies_FromCurrencyId",
                table: "ExchangeRates");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Currencies_ToCurrencyId",
                table: "ExchangeRates");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRates_FromCurrencyId_ToCurrencyId_EffectiveDate",
                table: "ExchangeRates");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_FromCurrencyId",
                table: "ExchangeRates",
                column: "FromCurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Currencies_FromCurrencyId",
                table: "ExchangeRates",
                column: "FromCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Currencies_ToCurrencyId",
                table: "ExchangeRates",
                column: "ToCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Currencies_FromCurrencyId",
                table: "ExchangeRates");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Currencies_ToCurrencyId",
                table: "ExchangeRates");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRates_FromCurrencyId",
                table: "ExchangeRates");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_FromCurrencyId_ToCurrencyId_EffectiveDate",
                table: "ExchangeRates",
                columns: new[] { "FromCurrencyId", "ToCurrencyId", "EffectiveDate" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Currencies_FromCurrencyId",
                table: "ExchangeRates",
                column: "FromCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Currencies_ToCurrencyId",
                table: "ExchangeRates",
                column: "ToCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
