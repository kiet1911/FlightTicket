using AirlinesReservationSystem.Helper;
using AirlinesReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AirlinesReservationSystem.Areas.Admin.Controllers
{
    [AuthorizeRole("Admin")]
    public class AirPortController : Controller
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
        // GET: Admin/AirPort
        public ActionResult Index()
        {
            List<AirPort> airport = db.AirPorts.OrderByDescending(x => x.id).ToList();
            return View(airport);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name,code,address")] AirPort airPort)
        {
            AirPort airPortname = db.AirPorts.FirstOrDefault(x => x.name == airPort.name);
            AirPort airPortcode = db.AirPorts.FirstOrDefault(x => x.code == airPort.code);
            AirPort airPortaddress = db.AirPorts.FirstOrDefault(x => x.address == airPort.address);

            if(airPort.name.Length < 10)
            {
                ModelState.AddModelError("name", "Tên không được dưới 10 kí tự.");
            }
            if(airPort.code.Length < 10)
            {
                ModelState.AddModelError("name", "Code không được dưới 10 kí tự.");
            }
            if (airPort.address.Length < 10)
            {
                ModelState.AddModelError("address", "Địa chỉ không được dưới 10 kí tự.");
            }

            if (airPortname != null)
            {
                ModelState.AddModelError("name", "Tên này đã tồn tại.");
                return View(airPort);
            }
            if (airPortcode != null)
            {
                ModelState.AddModelError("code", "Code này đã tồn tại.");
                return View(airPort);
            }
            if (ModelState.IsValid)
            {
                db.AirPorts.Add(airPort);
                db.SaveChanges();
                AlertHelper.setAlert("success", "Tạo dữ liệu địa điểm thành công.");
                return RedirectToAction("Index");
            }


            return View(airPort);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AirPort airPort = db.AirPorts.Find(id);
            if (airPort == null)
            {
                return HttpNotFound();
            }
            return View(airPort);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,code,address")] AirPort airPort)
        {
            AirPort airPortname = db.AirPorts.FirstOrDefault(x => x.name == airPort.name);
            AirPort airPortcode = db.AirPorts.FirstOrDefault(x => x.code == airPort.code);
            AirPort airPortaddress = db.AirPorts.FirstOrDefault(x => x.address == airPort.address);

            AirPort airportOld = db.AirPorts.Find(airPort.id);

            if (airPort.name.Length < 10)
            {
                ModelState.AddModelError("name", "Tên không được dưới 10 kí tự.");
            }
            if (airPort.code.Length < 10)
            {
                ModelState.AddModelError("name", "Code không được dưới 10 kí tự.");
            }
            if (airPort.address.Length < 10)
            {
                ModelState.AddModelError("address", "Địa chỉ không được dưới 10 kí tự.");
            }

            if (airPortname != null && airPortname.name != airportOld.name)
            {
                ModelState.AddModelError("name", "Tên này đã tồn tại.");
                return View(airPort);
            }
            if (airPortcode != null && airPortname.code != airportOld.code)
            {
                ModelState.AddModelError("code", "Code này đã tồn tại.");
                return View(airPort);
            }

            List<FlightSchedule> flight = db.FlightSchedules.Where(x => x.from_airport == airPort.id || x.to_airport == airPort.id).ToList();
            if (flight.Count > 0)
            {
                AlertHelper.setAlert("danger", "Cập nhập dữ liệu địa điểm thất bại vì có chuyến bay đang sử dụng.");
                return RedirectToAction("Index");
            }

            if (airPortname != null && airPortname.name != airportOld.name)
            {
                ModelState.AddModelError("name", "Tên này đã tồn tại.");
                return View(airPort);
            }
            if (airPortcode != null && airPortcode.code != airportOld.code)
            {
                ModelState.AddModelError("code", "Code này đã tồn tại.");
                return View(airPort);
            }
            if (ModelState.IsValid)
            {
                airportOld.name = airPort.name;
                airportOld.code = airPort.code;
                airportOld.address = airPort.address;

                db.Entry(airportOld).State = EntityState.Modified;
                db.SaveChanges();
                AlertHelper.setAlert("success", "Cập nhập dữ liệu địa điểm thành công.");
                return RedirectToAction("Index");
            }
            return View(airPort);
        }





        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AirPort airPort = db.AirPorts.Find(id);
            if (airPort == null)
            {
                return HttpNotFound();
            }
            return View(airPort);

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AirPort airPort = db.AirPorts.Find(id);
            List<FlightSchedule> flight = db.FlightSchedules.Where(x => x.from_airport == id).ToList();
            if (flight.Count > 0)
            {
                AlertHelper.setAlert("danger", "Xóa dữ liệu địa điểm thất bại vì có chuyến bay đang sử dụng.");
                return RedirectToAction("Index");
            }


            try {
                db.AirPorts.Remove(airPort);
                db.SaveChanges();
                AlertHelper.setAlert("success", "Xóa dữ liệu địa điểm thành công.");
                return RedirectToAction("Index");
            }
            catch(Exception ex) {
                AlertHelper.setAlert("danger", "Xóa dữ liệu địa điểm thất bại vì đã bị xóa rồi.");
                return RedirectToAction("Index");
            }
            
        }
    }
}