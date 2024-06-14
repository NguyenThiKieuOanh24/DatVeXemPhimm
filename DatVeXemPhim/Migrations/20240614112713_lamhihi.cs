using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatVeXemPhim.Migrations
{
    /// <inheritdoc />
    public partial class lamhihi : Migration
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

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "thoiLuong",
                table: "Phim",
                type: "time",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "posterPhim",
                table: "Phim",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "taiKhoan",
                table: "NhanVien",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "taiKhoan",
                table: "KhachHang",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "soDienThoai",
                table: "KhachHang",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "matKhau",
                table: "KhachHang",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "hoTen",
                table: "KhachHang",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "KhachHang",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.AlterColumn<string>(
                name: "thoiLuong",
                table: "Phim",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<string>(
                name: "posterPhim",
                table: "Phim",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "taiKhoan",
                table: "NhanVien",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "taiKhoan",
                table: "KhachHang",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "soDienThoai",
                table: "KhachHang",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AlterColumn<string>(
                name: "matKhau",
                table: "KhachHang",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "hoTen",
                table: "KhachHang",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "KhachHang",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

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
