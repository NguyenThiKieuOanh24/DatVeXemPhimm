using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DatVeXemPhim.App_Start
{
    public class RoleNhanVien : AuthorizeAttribute
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            var User = SessionConfig.GetNhanVien();


            var a = 5;
            if (User == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new
                    {
                        controller = "Admin",
                        action = "DangNhap"
                    }));
                return;
            }
            return;
        }
    }
}