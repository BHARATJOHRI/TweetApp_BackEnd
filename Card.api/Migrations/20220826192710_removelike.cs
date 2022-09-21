using Microsoft.EntityFrameworkCore.Migrations;

namespace Card.api.Migrations
{
    public partial class removelike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikeCount",
                table: "Likes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LikeCount",
                table: "Likes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
