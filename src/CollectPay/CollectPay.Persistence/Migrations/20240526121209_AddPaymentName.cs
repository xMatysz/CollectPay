using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectPay.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Payments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Payments");
        }
    }
}
