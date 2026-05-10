using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuelRecords.Api.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarImagemUrlCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagemUrl",
                table: "Cards",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemUrl",
                table: "Cards");
        }
    }
}
