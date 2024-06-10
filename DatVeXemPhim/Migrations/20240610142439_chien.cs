using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatVeXemPhim.Migrations
{
    /// <inheritdoc />
    public partial class chien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.RenameColumn(
                name: "maPhim",
                table: "XuatChieu",
                newName: "PhimiD");

            migrationBuilder.AddColumn<int>(
                name: "PhongChieuiD",
                table: "XuatChieu",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GheiD",
                table: "Ve",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KhachHangiD",
                table: "Ve",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NhanVieniD",
                table: "Ve",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "XuatChieuiD",
                table: "Ve",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "posterPhim",
                table: "Phim",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "TheLoaiPhimiD",
                table: "Phim",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_XuatChieu_PhongChieuiD",
                table: "XuatChieu",
                column: "PhongChieuiD");

            migrationBuilder.CreateIndex(
                name: "IX_Ve_GheiD",
                table: "Ve",
                column: "GheiD");

            migrationBuilder.CreateIndex(
                name: "IX_Ve_KhachHangiD",
                table: "Ve",
                column: "KhachHangiD");

            migrationBuilder.CreateIndex(
                name: "IX_Ve_NhanVieniD",
                table: "Ve",
                column: "NhanVieniD");

            migrationBuilder.CreateIndex(
                name: "IX_Ve_XuatChieuiD",
                table: "Ve",
                column: "XuatChieuiD");

            migrationBuilder.CreateIndex(
                name: "IX_Phim_TheLoaiPhimiD",
                table: "Phim",
                column: "TheLoaiPhimiD");

            migrationBuilder.AddForeignKey(
                name: "FK_Phim_TheLoaiPhim_TheLoaiPhimiD",
                table: "Phim",
                column: "TheLoaiPhimiD",
                principalTable: "TheLoaiPhim",
                principalColumn: "iD",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ve_Ghe_GheiD",
                table: "Ve",
                column: "GheiD",
                principalTable: "Ghe",
                principalColumn: "iD",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ve_KhachHang_KhachHangiD",
                table: "Ve",
                column: "KhachHangiD",
                principalTable: "KhachHang",
                principalColumn: "iD",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ve_NhanVien_NhanVieniD",
                table: "Ve",
                column: "NhanVieniD",
                principalTable: "NhanVien",
                principalColumn: "iD",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ve_XuatChieu_XuatChieuiD",
                table: "Ve",
                column: "XuatChieuiD",
                principalTable: "XuatChieu",
                principalColumn: "iD",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_XuatChieu_Phim_PhimiD",
                table: "XuatChieu",
                column: "PhimiD",
                principalTable: "Phim",
                principalColumn: "iD",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_XuatChieu_PhongChieu_PhongChieuiD",
                table: "XuatChieu",
                column: "PhongChieuiD",
                principalTable: "PhongChieu",
                principalColumn: "iD");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phim_TheLoaiPhim_TheLoaiPhimiD",
                table: "Phim");

            migrationBuilder.DropForeignKey(
                name: "FK_Ve_Ghe_GheiD",
                table: "Ve");

            migrationBuilder.DropForeignKey(
                name: "FK_Ve_KhachHang_KhachHangiD",
                table: "Ve");

            migrationBuilder.DropForeignKey(
                name: "FK_Ve_NhanVien_NhanVieniD",
                table: "Ve");

            migrationBuilder.DropForeignKey(
                name: "FK_Ve_XuatChieu_XuatChieuiD",
                table: "Ve");

            migrationBuilder.DropForeignKey(
                name: "FK_XuatChieu_Phim_PhimiD",
                table: "XuatChieu");

            migrationBuilder.DropForeignKey(
                name: "FK_XuatChieu_PhongChieu_PhongChieuiD",
                table: "XuatChieu");

            migrationBuilder.DropIndex(
                name: "IX_XuatChieu_PhongChieuiD",
                table: "XuatChieu");

            migrationBuilder.DropIndex(
                name: "IX_Ve_GheiD",
                table: "Ve");

            migrationBuilder.DropIndex(
                name: "IX_Ve_KhachHangiD",
                table: "Ve");

            migrationBuilder.DropIndex(
                name: "IX_Ve_NhanVieniD",
                table: "Ve");

            migrationBuilder.DropIndex(
                name: "IX_Ve_XuatChieuiD",
                table: "Ve");

            migrationBuilder.DropIndex(
                name: "IX_Phim_TheLoaiPhimiD",
                table: "Phim");

            migrationBuilder.DropColumn(
                name: "PhongChieuiD",
                table: "XuatChieu");

            migrationBuilder.DropColumn(
                name: "GheiD",
                table: "Ve");

            migrationBuilder.DropColumn(
                name: "KhachHangiD",
                table: "Ve");

            migrationBuilder.DropColumn(
                name: "NhanVieniD",
                table: "Ve");

            migrationBuilder.DropColumn(
                name: "XuatChieuiD",
                table: "Ve");

            migrationBuilder.DropColumn(
                name: "TheLoaiPhimiD",
                table: "Phim");

            migrationBuilder.RenameColumn(
                name: "PhimiD",
                table: "XuatChieu",
                newName: "MaPhim");

            migrationBuilder.RenameIndex(
                name: "IX_XuatChieu_PhimiD",
                table: "XuatChieu",
                newName: "IX_XuatChieu_MaPhim");

            migrationBuilder.AlterColumn<string>(
                name: "posterPhim",
                table: "Phim",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_XuatChieu_Phim_MaPhim",
                table: "XuatChieu",
                column: "MaPhim",
                principalTable: "Phim",
                principalColumn: "iD",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
