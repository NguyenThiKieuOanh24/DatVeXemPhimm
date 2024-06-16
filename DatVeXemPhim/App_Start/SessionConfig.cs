using DatVeXemPhim.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DatVeXemPhim.App_Start
{
    public static class SessionConfig
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static void SaveUser(NhanVien nv)
        {
            var jsonString = JsonConvert.SerializeObject(nv);
            _httpContextAccessor.HttpContext.Session.SetString("NhanVien", jsonString);
        }

        public static NhanVien GetNhanVien()
        {
            var jsonString = _httpContextAccessor.HttpContext.Session.GetString("NhanVien");
            return jsonString == null ? null : JsonConvert.DeserializeObject<NhanVien>(jsonString);
        }


        public static void SaveKhachHang(KhachHang kh)
        {
            var jsonString = JsonConvert.SerializeObject(kh);
            _httpContextAccessor.HttpContext.Session.SetString("KhachHang", jsonString);
        }

        public static KhachHang GetKhachHang()
        {
            var jsonString = _httpContextAccessor.HttpContext.Session.GetString("KhachHang");
            return jsonString == null ? null : JsonConvert.DeserializeObject<KhachHang>(jsonString);
        }

        public static void LogOut()
        {
            // Xoá thông tin người dùng khỏi session
            _httpContextAccessor.HttpContext.Session.Remove("NhanVien");
        }
        public static void LogOutKhachHang()
        {
            // Xoá thông tin người dùng khỏi session
            _httpContextAccessor.HttpContext.Session.Remove("KhachHang");
        }
    }
}