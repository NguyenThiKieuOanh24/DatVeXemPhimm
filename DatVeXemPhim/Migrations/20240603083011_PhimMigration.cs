using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatVeXemPhim.Migrations
{
    /// <inheritdoc />
    public partial class PhimMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Phim",
                columns: table => new
                {
                    iD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tenPhim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    daoDien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dienVien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    theLoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    thoiGianKhoiChieu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    thoiLuong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ngonNgu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phim", x => x.iD);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Phim");
        }
    }
}
