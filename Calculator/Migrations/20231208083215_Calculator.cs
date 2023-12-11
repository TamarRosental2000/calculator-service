using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calculator.Migrations
{
    /// <inheritdoc />
    public partial class Calculator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    OperationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Operator = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CountLastMonth = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.OperationId);
                });

            migrationBuilder.CreateTable(
                name: "CalculateMemory",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldA = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    FieldB = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Result = table.Column<string>(type: "nvarchar(1200)", maxLength: 1200, nullable: false),
                    OperationId = table.Column<int>(type: "int", nullable: false),
                    CalculateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculateMemory", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_CalculateMemory_Operations_OperationId",
                        column: x => x.OperationId,
                        principalTable: "Operations",
                        principalColumn: "OperationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalculateMemory_OperationId",
                table: "CalculateMemory",
                column: "OperationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculateMemory");

            migrationBuilder.DropTable(
                name: "Operations");
        }
    }
}
