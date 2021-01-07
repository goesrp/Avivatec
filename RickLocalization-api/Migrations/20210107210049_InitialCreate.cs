using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RickLocalization_api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dimensions",
                columns: table => new
                {
                    Dimensionid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dimensions", x => x.Dimensionid);
                });

            migrationBuilder.CreateTable(
                name: "Mortys",
                columns: table => new
                {
                    Mortyid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mortys", x => x.Mortyid);
                });

            migrationBuilder.CreateTable(
                name: "Ricks",
                columns: table => new
                {
                    Rickid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dimensionid = table.Column<int>(type: "int", nullable: false),
                    Mortyid = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ricks", x => x.Rickid);
                    table.ForeignKey(
                        name: "FK_Ricks_Dimensions_Dimensionid",
                        column: x => x.Dimensionid,
                        principalTable: "Dimensions",
                        principalColumn: "Dimensionid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ricks_Mortys_Mortyid",
                        column: x => x.Mortyid,
                        principalTable: "Mortys",
                        principalColumn: "Mortyid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Travels",
                columns: table => new
                {
                    Traveid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rickid = table.Column<int>(type: "int", nullable: false),
                    DimensionIdSourceDimensionid = table.Column<int>(type: "int", nullable: true),
                    DimensionIdTargetDimensionid = table.Column<int>(type: "int", nullable: true),
                    TravelDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travels", x => x.Traveid);
                    table.ForeignKey(
                        name: "FK_Travels_Dimensions_DimensionIdSourceDimensionid",
                        column: x => x.DimensionIdSourceDimensionid,
                        principalTable: "Dimensions",
                        principalColumn: "Dimensionid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Travels_Dimensions_DimensionIdTargetDimensionid",
                        column: x => x.DimensionIdTargetDimensionid,
                        principalTable: "Dimensions",
                        principalColumn: "Dimensionid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Travels_Ricks_Rickid",
                        column: x => x.Rickid,
                        principalTable: "Ricks",
                        principalColumn: "Rickid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ricks_Dimensionid",
                table: "Ricks",
                column: "Dimensionid");

            migrationBuilder.CreateIndex(
                name: "IX_Ricks_Mortyid",
                table: "Ricks",
                column: "Mortyid");

            migrationBuilder.CreateIndex(
                name: "IX_Travels_DimensionIdSourceDimensionid",
                table: "Travels",
                column: "DimensionIdSourceDimensionid");

            migrationBuilder.CreateIndex(
                name: "IX_Travels_DimensionIdTargetDimensionid",
                table: "Travels",
                column: "DimensionIdTargetDimensionid");

            migrationBuilder.CreateIndex(
                name: "IX_Travels_Rickid",
                table: "Travels",
                column: "Rickid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Travels");

            migrationBuilder.DropTable(
                name: "Ricks");

            migrationBuilder.DropTable(
                name: "Dimensions");

            migrationBuilder.DropTable(
                name: "Mortys");
        }
    }
}
