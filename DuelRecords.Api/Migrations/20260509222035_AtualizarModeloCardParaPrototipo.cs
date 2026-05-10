using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuelRecords.Api.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarModeloCardParaPrototipo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipoCard",
                table: "Cards",
                newName: "Quantidade");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Cards",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Atributo",
                table: "Cards",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Colecao",
                table: "Cards",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Raridade",
                table: "Cards",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Cards",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TipoDeck",
                table: "Cards",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Colecao",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Raridade",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "TipoDeck",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "Quantidade",
                table: "Cards",
                newName: "TipoCard");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Cards",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Atributo",
                table: "Cards",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
