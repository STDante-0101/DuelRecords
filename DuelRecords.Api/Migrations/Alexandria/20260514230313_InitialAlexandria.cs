using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DuelRecords.Api.Migrations.Alexandria
{
    /// <inheritdoc />
    public partial class InitialAlexandria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "YgoCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    YgoId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    FrameType = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Race = table.Column<string>(type: "text", nullable: true),
                    Attribute = table.Column<string>(type: "text", nullable: true),
                    Archetype = table.Column<string>(type: "text", nullable: true),
                    Attack = table.Column<int>(type: "integer", nullable: true),
                    Defense = table.Column<int>(type: "integer", nullable: true),
                    Level = table.Column<int>(type: "integer", nullable: true),
                    Scale = table.Column<int>(type: "integer", nullable: true),
                    LinkValue = table.Column<int>(type: "integer", nullable: true),
                    LinkMarkers = table.Column<string>(type: "text", nullable: true),
                    HumanReadableCardType = table.Column<string>(type: "text", nullable: true),
                    LocalImagePath = table.Column<string>(type: "text", nullable: true),
                    ImportedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastSyncedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YgoCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YgoCardImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    YgoCardId = table.Column<int>(type: "integer", nullable: false),
                    ImageYgoId = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    ImageUrlSmall = table.Column<string>(type: "text", nullable: true),
                    ImageUrlCropped = table.Column<string>(type: "text", nullable: true),
                    LocalImagePath = table.Column<string>(type: "text", nullable: true),
                    LocalSmallImagePath = table.Column<string>(type: "text", nullable: true),
                    LocalCroppedImagePath = table.Column<string>(type: "text", nullable: true),
                    Downloaded = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YgoCardImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YgoCardImages_YgoCards_YgoCardId",
                        column: x => x.YgoCardId,
                        principalTable: "YgoCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "YgoCardPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    YgoCardId = table.Column<int>(type: "integer", nullable: false),
                    CardMarketPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    TcgPlayerPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    EbayPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    AmazonPrice = table.Column<decimal>(type: "numeric", nullable: true),
                    CoolStuffIncPrice = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YgoCardPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YgoCardPrices_YgoCards_YgoCardId",
                        column: x => x.YgoCardId,
                        principalTable: "YgoCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "YgoCardSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    YgoCardId = table.Column<int>(type: "integer", nullable: false),
                    SetName = table.Column<string>(type: "text", nullable: false),
                    SetCode = table.Column<string>(type: "text", nullable: false),
                    SetRarity = table.Column<string>(type: "text", nullable: true),
                    SetRarityCode = table.Column<string>(type: "text", nullable: true),
                    SetPrice = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YgoCardSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YgoCardSets_YgoCards_YgoCardId",
                        column: x => x.YgoCardId,
                        principalTable: "YgoCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_YgoCardImages_YgoCardId",
                table: "YgoCardImages",
                column: "YgoCardId");

            migrationBuilder.CreateIndex(
                name: "IX_YgoCardPrices_YgoCardId",
                table: "YgoCardPrices",
                column: "YgoCardId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_YgoCards_YgoId",
                table: "YgoCards",
                column: "YgoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_YgoCardSets_YgoCardId",
                table: "YgoCardSets",
                column: "YgoCardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YgoCardImages");

            migrationBuilder.DropTable(
                name: "YgoCardPrices");

            migrationBuilder.DropTable(
                name: "YgoCardSets");

            migrationBuilder.DropTable(
                name: "YgoCards");
        }
    }
}
