using Microsoft.EntityFrameworkCore.Migrations;

namespace OceanStore.DataAccesLayer.Migrations
{
    public partial class AddIsDeactiveColumnPositionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeactive",
                table: "Positions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeactive",
                table: "Positions");
        }
    }
}
