using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContosoUniversity.Migrations
{
    /// <inheritdoc />
    public partial class create346 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "recentViolation",
                table: "Delinquents",
                newName: "RecentViolation");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Delinquents",
                newName: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecentViolation",
                table: "Delinquents",
                newName: "recentViolation");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Delinquents",
                newName: "Id");
        }
    }
}
