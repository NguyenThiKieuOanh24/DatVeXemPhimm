using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatVeXemPhim.Migrations
{
    /// <inheritdoc />
    public partial class chienupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    soDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    taiKhoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    matKhau = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    diaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    soDienThoai = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    taiKhoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    matKhau = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PhongChieu",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tenPhong = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongChieu", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TheLoaiPhim",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tenLoaiPhim = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheLoaiPhim", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Ghe",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    maPhong = table.Column<int>(type: "int", nullable: false),
                    tenGhe = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ghe", x => x.id);
                    table.ForeignKey(
                        name: "FK_Ghe_PhongChieu_maPhong",
                        column: x => x.maPhong,
                        principalTable: "PhongChieu",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phim",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    posterPhim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tenPhim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    daoDien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dienVien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    maLoaiPhim = table.Column<int>(type: "int", nullable: false),
                    thoiGianKhoiChieu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    thoiLuong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngonNgu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phim", x => x.id);
                    table.ForeignKey(
                        name: "FK_Phim_TheLoaiPhim_maLoaiPhim",
                        column: x => x.maLoaiPhim,
                        principalTable: "TheLoaiPhim",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "XuatChieu",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    maPhong = table.Column<int>(type: "int", nullable: false),
                    maPhim = table.Column<int>(type: "int", nullable: false),
                    ngayChieu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gioBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gioKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XuatChieu", x => x.id);
                    table.ForeignKey(
                        name: "FK_XuatChieu_Phim_maPhim",
                        column: x => x.maPhim,
                        principalTable: "Phim",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_XuatChieu_PhongChieu_maPhong",
                        column: x => x.maPhong,
                        principalTable: "PhongChieu",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ve",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    maXuatChieu = table.Column<int>(type: "int", nullable: false),
                    maKhachHang = table.Column<int>(type: "int", nullable: false),
                    maNhanVien = table.Column<int>(type: "int", nullable: false),
                    maGhe = table.Column<int>(type: "int", nullable: false),
                    ngayBanVe = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tongTien = table.Column<int>(type: "int", nullable: false),
                    XuatChieuid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ve", x => x.id);
                    table.ForeignKey(
                        name: "FK_Ve_Ghe_maGhe",
                        column: x => x.maGhe,
                        principalTable: "Ghe",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ve_KhachHang_maKhachHang",
                        column: x => x.maKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ve_NhanVien_maNhanVien",
                        column: x => x.maNhanVien,
                        principalTable: "NhanVien",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ve_XuatChieu_XuatChieuid",
                        column: x => x.XuatChieuid,
                        principalTable: "XuatChieu",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ghe_maPhong",
                table: "Ghe",
                column: "maPhong");

            migrationBuilder.CreateIndex(
                name: "IX_Phim_maLoaiPhim",
                table: "Phim",
                column: "maLoaiPhim");

            migrationBuilder.CreateIndex(
                name: "IX_Ve_maGhe",
                table: "Ve",
                column: "maGhe");

            migrationBuilder.CreateIndex(
                name: "IX_Ve_maKhachHang",
                table: "Ve",
                column: "maKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_Ve_maNhanVien",
                table: "Ve",
                column: "maNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_Ve_XuatChieuid",
                table: "Ve",
                column: "XuatChieuid");

            migrationBuilder.CreateIndex(
                name: "IX_XuatChieu_maPhim",
                table: "XuatChieu",
                column: "maPhim");

            migrationBuilder.CreateIndex(
                name: "IX_XuatChieu_maPhong",
                table: "XuatChieu",
                column: "maPhong");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ve");

            migrationBuilder.DropTable(
                name: "Ghe");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "XuatChieu");

            migrationBuilder.DropTable(
                name: "Phim");

            migrationBuilder.DropTable(
                name: "PhongChieu");

            migrationBuilder.DropTable(
                name: "TheLoaiPhim");
        }
    }
}
