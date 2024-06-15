using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectPay.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDebtorsToPayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid[]>(
                name: "Debtors",
                table: "Payments",
                type: "uuid[]",
                nullable: false,
                defaultValue: new Guid[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Debtors",
                table: "Payments");
        }
    }
}
