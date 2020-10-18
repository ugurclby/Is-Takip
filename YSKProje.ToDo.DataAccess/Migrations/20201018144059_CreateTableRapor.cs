using Microsoft.EntityFrameworkCore.Migrations;

namespace YSKProje.ToDo.DataAccess.Migrations
{
    public partial class CreateTableRapor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gorev_AspNetUsers_AppUserId",
                table: "Gorev");

            migrationBuilder.AddColumn<int>(
                name: "RaporId",
                table: "Gorev",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Tanim",
                table: "Aciliyet",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Rapor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tanim = table.Column<string>(maxLength: 200, nullable: false),
                    Detay = table.Column<string>(type: "ntext", nullable: true),
                    GorevId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rapor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rapor_Gorev_GorevId",
                        column: x => x.GorevId,
                        principalTable: "Gorev",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rapor_GorevId",
                table: "Rapor",
                column: "GorevId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gorev_AspNetUsers_AppUserId",
                table: "Gorev",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gorev_AspNetUsers_AppUserId",
                table: "Gorev");

            migrationBuilder.DropTable(
                name: "Rapor");

            migrationBuilder.DropColumn(
                name: "RaporId",
                table: "Gorev");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Tanim",
                table: "Aciliyet",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Gorev_AspNetUsers_AppUserId",
                table: "Gorev",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
