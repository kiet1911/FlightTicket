﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AirlinesReservationSystem.Helper;

namespace AirlinesReservationSystem.Areas.Admin.Controllers
{
    public class DefaultController : AuthController
    {
        // GET: Admin/Default
        [AuthorizeRole("Employee", "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            if (AuthHelper.isLoginEmployeee() == true)
            {
                AuthHelper.removeIdentityEmployee();
            }
            return RedirectToAction("Login", "Security");
        }
    }
}
