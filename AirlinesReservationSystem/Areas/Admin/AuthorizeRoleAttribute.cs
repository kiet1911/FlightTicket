using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AirlinesReservationSystem.Models;
namespace AirlinesReservationSystem.Areas
{
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedRoles;

        public AuthorizeRoleAttribute(params string[] roles)
        {
            this.allowedRoles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var userRole = httpContext.Session["Role"]?.ToString();

            if (userRole == null)
            {
                return false; // Người dùng chưa đăng nhập
            }

            // Kiểm tra xem Role của người dùng có trong danh sách cho phép không
            return Array.Exists(allowedRoles, role => role.Equals(userRole, StringComparison.OrdinalIgnoreCase));
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Chuyển hướng đến trang lỗi hoặc đăng nhập nếu không được phép
            //xoa session 

            //HttpContext.Current.Session["loginSesstionEmployee"] = null;
            filterContext.Result = new RedirectResult("/Admin/Security/Login");
        }

    }
}