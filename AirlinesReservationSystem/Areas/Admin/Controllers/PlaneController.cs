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
    [AuthorizeRole("Admin")]
    public class PlaneController : Controller
    {
        private readonly Model1 db = new Model1();
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (AuthHelper.getIdentityEmployeee() == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary(new { Controller = "Security", Action = "Login" }));
            }
            base.OnActionExecuted(filterContext);
        }
        // GET: Admin/Plane
        public ActionResult Index()
        {
            List<Plane> plane = db.Planes.OrderByDescending(x=>x.id).ToList();
            return View(plane);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name,code")] Plane plane)
        {
            Plane planeName = db.Planes.FirstOrDefault(x => x.name == plane.name);
            Plane planeCode = db.Planes.FirstOrDefault(x => x.code == plane.code);
            if (plane.name.Length < 15) { ModelState.AddModelError("name", "Tên không được ngắn hơn 15 ký tự."); }
            if (plane.code.Length < 15) { ModelState.AddModelError("code", "Tên không được ngắn hơn 15 ký tự."); }
            if (planeName != null )
            {
                ModelState.AddModelError("name", "Tên này đã tồn tại.");
                return View(plane);
            }
            if (planeCode != null )
            {
                ModelState.AddModelError("code", "Code này đã tồn tại.");
                return View(plane);
            }
            if (ModelState.IsValid)
            {
                db.Planes.Add(plane);
                db.SaveChanges();
                AlertHelper.setAlert("success", "Tạo dữ liệu máy bay thành công.");
                return RedirectToAction("Index");
            }
            return View(plane);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plane plane = db.Planes.Find(id);
            if (plane == null)
            {
                return HttpNotFound();
            }
            return View(plane);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,code")] Plane plane)
        {

            Plane planeName = db.Planes.FirstOrDefault(x => x.name == plane.name);

            Plane planeCode = db.Planes.FirstOrDefault(x => x.code == plane.code);

            Plane oldPlane = db.Planes.FirstOrDefault(x => x.id == plane.id);

            List<FlightSchedule> flight = db.FlightSchedules.Where(x => x.plane_id == plane.id).ToList();
            if (flight.Count > 0)
            {
                AlertHelper.setAlert("danger", "Cập nhập dữ liệu máy bay thất bại vì có chuyến bay đang sử dụng.");
                return RedirectToAction("Index");
            }

            if (planeName != null &&  planeName.name != oldPlane.name)
            {
                ModelState.AddModelError("name", "Tên này đã tồn tại.");
                return View(plane);
            }
            if (planeCode != null && planeCode.code != oldPlane.code)
            {
                ModelState.AddModelError("code", "Code này đã tồn tại.");
                return View(plane);
            }
            if (ModelState.IsValid)
            {
                oldPlane.name = plane.name;
                oldPlane.code = plane.code;
                db.Entry(oldPlane).State = EntityState.Modified;
                db.SaveChanges();
                AlertHelper.setAlert("success", "Cập nhập dữ liệu máy bay thành công.");
                return RedirectToAction("Index");
            }
            return View(plane);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plane plane = db.Planes.Find(id);
            if (plane == null)
            {
                return HttpNotFound();
            }
            return View(plane);

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plane plane = db.Planes.Find(id);
            List<FlightSchedule> flight = db.FlightSchedules.Where(x => x.plane_id == id).ToList();
            if(flight.Count > 0)
            {
                AlertHelper.setAlert("danger", "Xóa dữ liệu máy bay thất bại vì có chuyến bay đang sử dụng.");
                return RedirectToAction("Index");
            }
            
            db.Planes.Remove(plane);
            db.SaveChanges();
            AlertHelper.setAlert("success", "Xóa dữ liệu máy bay thành công.");
            return RedirectToAction("Index");
        }


    }
}