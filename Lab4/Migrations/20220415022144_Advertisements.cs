using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment02NET.Migrations
{
    public partial class Advertisements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Advertisements",
                columns: table => new
                {
                    advertisementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrokerageId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisements", x => x.advertisementID);
                    table.ForeignKey(
                        name: "FK_Advertisements_Brokerages_BrokerageId",
                        column: x => x.BrokerageId,
                        principalTable: "Brokerages",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_BrokerageId",
                table: "Advertisements",
                column: "BrokerageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertisements");
        }
    }
}
