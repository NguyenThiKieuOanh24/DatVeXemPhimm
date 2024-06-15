using DatVeXemPhim.App_Start;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DatVeXemPhim.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class Middleware
    {
        private readonly RequestDelegate _next;

        public Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Kiểm tra nếu người dùng truy cập phần Admin, trừ trang đăng nhập
            if ((context.Request.Path.StartsWithSegments("/Admin")
                || context.Request.Path.StartsWithSegments("/QuanLiKhachHangs")
                || context.Request.Path.StartsWithSegments("/QuanLiXuatChieus")
                || context.Request.Path.StartsWithSegments("/QuanLiVes"))
                && !context.Request.Path.Equals("/Admin/DangNhap"))
            {
                var nhanVien = SessionConfig.GetNhanVien();

                // Nếu chưa có session, chuyển hướng đến trang đăng nhập
                if (nhanVien == null)
                {
                    context.Response.Redirect("/Admin/DangNhap");
                    return;
                }
            }

            // Nếu không phải truy cập phần Admin, thì không làm gì
            await _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware>();
        }
    }
}