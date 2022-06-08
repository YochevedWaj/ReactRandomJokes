using Microsoft.EntityFrameworkCore.Migrations;

namespace RandomJokes.Data.Migrations
{
    public partial class addedsourceid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SourceID",
                table: "Jokes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourceID",
                table: "Jokes");
        }
    }
}
