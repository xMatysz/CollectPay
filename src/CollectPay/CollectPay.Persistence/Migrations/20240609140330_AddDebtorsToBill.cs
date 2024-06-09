using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectPay.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDebtorsToBill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid[]>(
                name: "Debtors",
                table: "Bills",
                type: "uuid[]",
                nullable: false,
                defaultValue: new Guid[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Debtors",
                table: "Bills");
        }
    }
}
