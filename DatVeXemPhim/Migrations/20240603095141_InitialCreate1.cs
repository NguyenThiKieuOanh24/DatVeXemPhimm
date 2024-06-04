using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatVeXemPhim.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ghe",
                columns: table => new
                {
                    iD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    maPhong = table.Column<int>(type: "int", nullable: false),
                    tenGhe = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ghe", x => x.iD);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    iD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    diaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    soDienThoai = table.Column<int>(type: "int", nullable: false),
                    eMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    taiKhoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    matKhau = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.iD);
                });

            migrationBuilder.CreateTable(
                name: "PhongChieu",
                columns: table => new
                {
                    iD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tenPhong = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongChieu", x => x.iD);
                });

            migrationBuilder.CreateTable(
                name: "TheLoaiPhim",
                columns: table => new
                {
                    iD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tenLoaiPhim = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheLoaiPhim", x => x.iD);
                });

            migrationBuilder.CreateTable(
                name: "Ve",
                columns: table => new
                {
                    iD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    maXuatChieu = table.Column<int>(type: "int", nullable: false),
                    maKhachHang = table.Column<int>(type: "int", nullable: false),
                    maNhanVien = table.Column<int>(type: "int", nullable: false),
                    maGhe = table.Column<int>(type: "int", nullable: false),
                    ngayBanVe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tongTien = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ve", x => x.iD);
                });

            migrationBuilder.CreateTable(
                name: "XuatChieu",
                columns: table => new
                {
                    iD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    maPhong = table.Column<int>(type: "int", nullable: false),
                    maPhim = table.Column<int>(type: "int", nullable: false),
                    ngayChieu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gioBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gioKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XuatChieu", x => x.iD);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ghe");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "PhongChieu");

            migrationBuilder.DropTable(
                name: "TheLoaiPhim");

            migrationBuilder.DropTable(
                name: "Ve");

            migrationBuilder.DropTable(
                name: "XuatChieu");
        }
    }
}
