using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatVeXemPhim.Migrations
{
    /// <inheritdoc />
    public partial class update2foreigkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ve_XuatChieu_XuatChieuid",
                table: "Ve");

            migrationBuilder.DropIndex(
                name: "IX_Ve_XuatChieuid",
                table: "Ve");

            migrationBuilder.DropColumn(
                name: "XuatChieuid",
                table: "Ve");

            migrationBuilder.CreateIndex(
                name: "IX_Ve_maXuatChieu",
                table: "Ve",
                column: "maXuatChieu");

            migrationBuilder.AddForeignKey(
                name: "FK_Ve_XuatChieu_maXuatChieu",
                table: "Ve",
                column: "maXuatChieu",
                principalTable: "XuatChieu",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ve_XuatChieu_maXuatChieu",
                table: "Ve");

            migrationBuilder.DropIndex(
                name: "IX_Ve_maXuatChieu",
                table: "Ve");

            migrationBuilder.AddColumn<int>(
                name: "XuatChieuid",
                table: "Ve",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ve_XuatChieuid",
                table: "Ve",
                column: "XuatChieuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Ve_XuatChieu_XuatChieuid",
                table: "Ve",
                column: "XuatChieuid",
                principalTable: "XuatChieu",
                principalColumn: "id");
        }
    }
}
