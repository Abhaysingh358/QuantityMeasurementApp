using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuantityMeasurementApp.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeasurementHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Operation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Operand1Value = table.Column<double>(type: "float", nullable: false),
                    Operand1Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Operand1MeasurementType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Operand2Value = table.Column<double>(type: "float", nullable: true),
                    Operand2Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Operand2MeasurementType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ResultType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ResultQuantityValue = table.Column<double>(type: "float", nullable: true),
                    ResultQuantityUnit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ResultQuantityMeasurementType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ResultScalar = table.Column<double>(type: "float", nullable: true),
                    ResultComparison = table.Column<bool>(type: "bit", nullable: true),
                    IsError = table.Column<bool>(type: "bit", nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementHistory", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeasurementHistory");
        }
    }
}
