using Microsoft.EntityFrameworkCore.Migrations;

namespace RandomJokes.Data.Migrations
{
    public partial class tookawaytype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Jokes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Jokes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
