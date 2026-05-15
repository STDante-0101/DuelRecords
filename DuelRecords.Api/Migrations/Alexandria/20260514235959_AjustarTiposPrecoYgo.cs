using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuelRecords.Api.Migrations.Alexandria
{
    /// <inheritdoc />
    public partial class AjustarTiposPrecoYgo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SetPrice",
                table: "YgoCardSets",
                type: "text",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SetPrice",
                table: "YgoCardSets",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
