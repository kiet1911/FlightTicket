using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AirlinesReservationSystem.Models;
using AirlinesReservationSystem.Helper;

namespace AirlinesReservationSystem.Areas.Admin.Controllers
{
    public class SecurityController : Controller
    {
        private Model1 db = new Model1();
        // GET: Admin/Security
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            if (AuthHelper.isLoginEmployeee())
            {
                return RedirectToAction("Index", "FlightSchedules");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User _user)
        {
            var user = db.Admin.Where(s => s.email == _user.email).SingleOrDefault();
            if (user != null)
            {
                if (user.user_type == 1)
                {
                    ModelState.AddModelError("email", "Bạn không phải nhân viên.");
                }
                else if (user.password == _user.password)
                {
                    User uses = new User() ;
                    uses.name = user.name;
                    uses.email = user.email;
                    uses.user_type = user.user_type;
                    if(user.user_type == 0)
                    {
                        HttpContext.Session["Role"] = "Employee";
                    }
                    else
                    {
                        HttpContext.Session["Role"] = "Admin";
                    }
                    AuthHelper.setIdentityEmployee(uses);
                    return RedirectToAction("Index", "FlightSchedules");
                }
                else
                {
                    ModelState.AddModelError("password", "Mật khẩu không hợp lệ.");
                }

            }
            else
            {
                ModelState.AddModelError("email", "Email không được tìm thấy.");
            }
            return View();
        }
        // function calles when each time switch controller with function checkAuthor()
        //kiem tra voi ma codeExpri con ton tai thi : cho phep tiep tuc , gan ma kiemtra codeExpri vao session  





        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginWithAdmin(User _user)
        {
            var user = db.Admin.Where(s => s.email == _user.email).SingleOrDefault();
            if (user != null)
            {
                if (user.user_type != 2)
                {
                    ModelState.AddModelError("email", "Bạn không phải admin.");
                }
                else if (user.password == _user.password)
                {
                    User uses = new User();
                    uses.name = user.name;
                    uses.email = user.email;
                    uses.user_type = user.user_type;
                    AuthHelper.setIdentityEmployee(uses);

                    Session["Role"] = "Empoyee";

                    return RedirectToAction("Index", "FlightSchedules");
                }
                else
                {
                    ModelState.AddModelError("password", "Mật khẩu không hợp lệ.");
                }

            }
            else
            {
                ModelState.AddModelError("email", "Email không được tìm thấy.");
            }
            return View();
        }


    }
}