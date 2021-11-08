using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class renameDictDbs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Dicts",
                table: "Dicts");

            migrationBuilder.RenameTable(
                name: "Dicts",
                newName: "DictDbs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DictDbs",
                table: "DictDbs",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DictDbs",
                table: "DictDbs");

            migrationBuilder.RenameTable(
                name: "DictDbs",
                newName: "Dicts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dicts",
                table: "Dicts",
                column: "Id");
        }
    }
}
