using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class addupdatetimpstamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeStamp",
                table: "Things",
                newName: "UpdateTimeStamp");

            migrationBuilder.RenameColumn(
                name: "CreatTime",
                table: "Things",
                newName: "CreateTime");

            migrationBuilder.AddColumn<long>(
                name: "CreateTimeStamp",
                table: "Things",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTimeStamp",
                table: "Things");

            migrationBuilder.RenameColumn(
                name: "UpdateTimeStamp",
                table: "Things",
                newName: "TimeStamp");

            migrationBuilder.RenameColumn(
                name: "CreateTime",
                table: "Things",
                newName: "CreatTime");
        }
    }
}
