using AirlinesReservationSystem.Helper;
using AirlinesReservationSystem.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AirlinesReservationSystem.Areas.Admin.Controllers
{
    [AuthorizeRole("Employee", "Admin")]
    public class BaggageController : Controller
    {
        private Model1 db = new Model1();
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (AuthHelper.getIdentityEmployeee() == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(new { Controller = "Security", Action = "Login" }));
            }
            base.OnActionExecuted(filterContext);
        }
        // GET: Admin/Baggage
        public ActionResult Index()
        {
            var BaggageManagers = db.Baggage.OrderByDescending(x => x.id);

            return View(BaggageManagers);
        }
        // GET: Admin/TicketManagers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Baggage baggage = db.Baggage.Find(id);
            if (baggage == null)
            {
                return HttpNotFound();
            }
            return View(baggage);
        }

        //Create 
        [HttpGet]
        public JsonResult GetUserOptions(String searchValue)
        {
            var users = db.Users
           .Select(u => new { u.id, u.name, u.phone_number })
           .ToList();
            // Lấy danh sách người dùng từ cơ sở dữ liệu
            if (searchValue.Trim() == "" || searchValue == null)
            {
                return Json(users, JsonRequestBehavior.AllowGet);
            }
            users = db.Users
              .Select(u => new { u.id, u.name, u.phone_number })
              .Where(u => u.name.Contains(searchValue.Trim()) || u.phone_number.Contains(searchValue.Trim()))
              .ToList();
            // Trả về dữ liệu dưới dạng JSON
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.user_id = new SelectList(
                db.Users.Select(u => new
                {
                    id = u.id,
                    name_phone = u.name + " - " + u.phone_number
                }),
                "id",
                "name_phone"
            );

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "user_id,carryon_bag,signed_luggage,code")] Baggage baggage)
        {
            
            //find code baggage in table Baggage if already has one 
            Baggage bagCode = db.Baggage.FirstOrDefault(x => x.code == baggage.code);
            if (bagCode != null)
            {

                ModelState.AddModelError("code", "mã này đã tồn tại trong dữ liệu Baggage rồi.");
                ViewBag.user_id = new SelectList(
               db.Users.Select(u => new
               {
                   id = u.id,
                   name_phone = u.name + " - " + u.phone_number
               }),
               "id",
               "name_phone"
                 );
                return View(baggage);
            }
            else
            {
                try {
                    baggage.code = baggage.code.Trim();
                    db.Baggage.Add(baggage);
                    db.SaveChanges();
                    AlertHelper.setAlert("success", "Tạo dữ liệu ký gửi thành công.");
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                  
                    ViewBag.user_id = new SelectList(
                   db.Users.Select(u => new
                   {
                       id = u.id,
                       name_phone = u.name + " - " + u.phone_number
                   }),
                   "id",
                   "name_phone"
                     );
                    AlertHelper.setAlert("danger", "Tạo dữ liệu ký gửi thất bại.");
                    return View(baggage);
                }
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Baggage baggage = db.Baggage.Find(id);
            if (baggage == null)
            {
                return HttpNotFound();
            }
            return View(baggage);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Baggage baggage = db.Baggage.Find(id);
                db.Baggage.Remove(baggage);
                db.SaveChanges();
                AlertHelper.setAlert("success", "Xóa dữ liệu ký gửi thành công.");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {   
                AlertHelper.setAlert("danger", "Xóa dữ liệu ký gửi thất bại.");
                return RedirectToAction("Delete", "Baggage", id);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Baggage baggage = db.Baggage.Find(id);
            
            if (baggage == null)
            {
                return HttpNotFound();
            }
            ViewBag.user_id = new SelectList(
                db.Users.Select(u => new
                {
                    id = u.id,
                    name_phone = u.name + " - " + u.phone_number
                }),
                "id",
                "name_phone",
                baggage.user_id
            );
            baggage.code = baggage.code.Trim();
            return View(baggage);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,user_id,carryon_bag,signed_luggage,code")] Baggage baggage)
        {

            //find code baggage in table Baggage if already has one 
            Baggage bagCode = db.Baggage.FirstOrDefault(x => x.code == baggage.code);
            //find old code 
            Baggage old = db.Baggage.FirstOrDefault(x => x.id == baggage.id);
            if(old.code.Trim() == baggage.code.Trim())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        old.user_id = baggage.user_id;
                        old.carryon_bag = baggage.carryon_bag;
                        old.signed_luggage = baggage.signed_luggage;
                        old.code = baggage.code.Trim();
                        
                        db.Entry(old).State = EntityState.Modified;
                        db.SaveChanges();
                        AlertHelper.setAlert("success", "Cập nhập dữ liệu ký gửi thành công.");
                        return RedirectToAction("Index");
                    }


                }
                catch (Exception ex)
                {

                    ViewBag.user_id = new SelectList(
                   db.Users.Select(u => new
                   {
                       id = u.id,
                       name_phone = u.name + " - " + u.phone_number
                   }),
                   "id",
                   "name_phone"
                     );
                    AlertHelper.setAlert("danger", "Cập nhập dữ liệu ký gửi thất bại.");
                    return View(baggage);
                }
            }
            if (bagCode != null)
            {
                ModelState.AddModelError("code", "mã này đã tồn tại trong dữ liệu Baggage rồi.");
                ViewBag.user_id = new SelectList(
               db.Users.Select(u => new
               {
                   id = u.id,
                   name_phone = u.name + " - " + u.phone_number
               }),
               "id",
               "name_phone"
                 );
                return View(baggage);
            }
            else
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        old.user_id = baggage.user_id;
                        old.carryon_bag = baggage.carryon_bag;
                        old.signed_luggage = baggage.signed_luggage;
                        old.code = baggage.code.Trim();

                        db.Entry(old).State = EntityState.Modified;
                        db.SaveChanges();
                        AlertHelper.setAlert("success", "Cập nhập dữ liệu ký gửi thành công.");
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {

                    ViewBag.user_id = new SelectList(
                   db.Users.Select(u => new
                   {
                       id = u.id,
                       name_phone = u.name + " - " + u.phone_number
                   }),
                   "id",
                   "name_phone"
                     );
                    AlertHelper.setAlert("danger", "Cập nhập dữ liệu ký gửi thất bại.");
                    return View(baggage);
                }
            }
            ViewBag.user_id = new SelectList(
                db.Users.Select(u => new
                {
                    id = u.id,
                    name_phone = u.name + " - " + u.phone_number
                }),
                "id",
                "name_phone"
                  );
            AlertHelper.setAlert("danger", "Cập nhập dữ liệu ký gửi thất bại.");
            return View(baggage);
        }

    }
}