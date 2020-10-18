using Microsoft.EntityFrameworkCore.Migrations;

namespace YSKProje.ToDo.DataAccess.Migrations
{
    public partial class CreateTableAciliyet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AciliyetId",
                table: "Gorev",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Aciliyet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tanim = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aciliyet", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gorev_AciliyetId",
                table: "Gorev",
                column: "AciliyetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gorev_Aciliyet_AciliyetId",
                table: "Gorev",
                column: "AciliyetId",
                principalTable: "Aciliyet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gorev_Aciliyet_AciliyetId",
                table: "Gorev");

            migrationBuilder.DropTable(
                name: "Aciliyet");

            migrationBuilder.DropIndex(
                name: "IX_Gorev_AciliyetId",
                table: "Gorev");

            migrationBuilder.DropColumn(
                name: "AciliyetId",
                table: "Gorev");
        }
    }
}
