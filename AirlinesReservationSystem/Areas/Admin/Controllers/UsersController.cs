using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AirlinesReservationSystem.Models;
using AirlinesReservationSystem.Helper;
using System.Text.RegularExpressions;

namespace AirlinesReservationSystem.Areas.Admin.Controllers
{
    [AuthorizeRole("Employee", "Admin")]
    public class UsersController : AuthController
    {

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (AuthHelper.getIdentityEmployeee() == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(new { Controller = "Security", Action = "Login" }));
            }
            base.OnActionExecuted(filterContext);
        }

        private Model1 db = new Model1();

        // GET: Admin/Users
        public ActionResult Index()
        {
            return View(db.Users.Where(x=>x.user_type == 1).ToList());
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,email,cccd,address,phone_number,password,user_type")] User user)
        {
            var emailExit = db.Users.FirstOrDefault(u => u.email == user.email);
            if(emailExit != null)
            {
                ModelState.AddModelError("email", "Email đã tồn tại.");
                return View(user);
            }
            if (checkCCCD(user.cccd) != true)
            {
                ModelState.AddModelError("cccd", "Sai định dạng căn cước công dân.");
                return View(user);
            }
            if (checkPhoneNumber(user.phone_number) != true)
            {
                ModelState.AddModelError("phone_number", "Sai định dạng số điện thoại.");
                return View(user);
            }
            if (IsPasswordStrong(user.password) != true)
            {
                ModelState.AddModelError("password", "Password phải nhiều hơn 8 và chứa ít nhất 1 từ ghi hoa và 1 ký tự đặt biệt.");
                return View(user);
            }
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                AlertHelper.setAlert("success", "Tạo dữ liệu người dùng thành công.");
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,email,cccd,address,phone_number,password,user_type")] User user)
        {
            //old 
            User emailExit = db.Users.FirstOrDefault(u => u.email == user.email);
            //new 
            User emailOld = db.Users.Find(user.id);

            if (emailExit != null && emailOld.email.Trim() != user.email.Trim() )
            {
                ModelState.AddModelError("email", "Email đã tồn tại.");
                return View(user);
            }
            if (checkCCCD(user.cccd) != true)
            {
                ModelState.AddModelError("cccd", "Sai định dạng căn cước công dân.");
                return View(user);
            }
            if (checkPhoneNumber(user.phone_number) != true)
            {
                ModelState.AddModelError("phone_number", "Sai định dạng số điện thoại.");
                return View(user);
            }
            if (IsPasswordStrong(user.password) != true)
            {
                ModelState.AddModelError("password", "Password phải nhiều hơn 8 và chứa ít nhất 1 từ ghi hoa và 1 ký tự đặt biệt.");
                return View(user);
            }
            if (ModelState.IsValid)
            {
                emailOld.name = user.name;
                emailOld.email = user.email;
                emailOld.cccd = user.cccd;
                emailOld.address = user.address;
                emailOld.phone_number = user.phone_number;
                emailOld.password = user.password;
                db.Entry(emailOld).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/Users/Delete/5
        public ActionResult Delete(int? id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            AlertHelper.setAlert("success", "Xóa dữ liệu người dùng thành công.");
            return RedirectToAction("Index");
        }
        public bool checkCCCD(String cccd)
        {
            String regex = "^\\d{12}$";
            if (Regex.IsMatch(cccd, regex))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkPhoneNumber(String phoneNumber)
        {
            String regex = @"^\+84\d{9,10}$";
            if (Regex.IsMatch(phoneNumber, regex))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool checkGmail(String gmail)
        {
            string pattern = @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])";
            if (Regex.IsMatch(gmail, pattern , RegexOptions.IgnoreCase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool IsPasswordStrong(string password)
        {
            // Thêm logic kiểm tra độ mạnh của mật khẩu ở đây
            return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsDigit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
