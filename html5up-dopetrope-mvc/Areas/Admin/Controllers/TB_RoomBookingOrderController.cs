using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using html5up_dopetrope_mvc.Models;

namespace html5up_dopetrope_mvc.Areas.Admin.Controllers
{
    using PagedList;



    public class TB_RoomBookingOrderController : Controller
    {
        private BedAndBreakfastEntities db = new BedAndBreakfastEntities();

        private int pageSize = 10;

        // GET: Admin/TB_RoomBookingOrder
        public ActionResult Index(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;

            var tB_RoomBookingOrder = db.TB_RoomBookingOrder.Include(t => t.TB_Room).OrderByDescending(desc => desc.CreateDateTime);

            return View(tB_RoomBookingOrder.ToPagedList(currentPage, pageSize));
        }

        // GET: Admin/TB_RoomBookingOrder/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_RoomBookingOrder tB_RoomBookingOrder = db.TB_RoomBookingOrder.Find(id);
            if (tB_RoomBookingOrder == null)
            {
                return HttpNotFound();
            }
            return View(tB_RoomBookingOrder);
        }

        // GET: Admin/TB_RoomBookingOrder/Create
        public ActionResult Create()
        {
            ViewBag.RoomId = new SelectList(db.TB_Room, "ID", "Name");
            return View();
        }

        // POST: Admin/TB_RoomBookingOrder/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,OrderDateForm,OrderDateTo,IsDelete,CreateDateTime,RoomNum,AdultNum,ChildNum,CustomeName,CustomeEmail,CustomeSex,CustomeTel,TotalAmount,OrderNo,IsComplete,RoomId")] TB_RoomBookingOrder tB_RoomBookingOrder)
        {
            if (ModelState.IsValid)
            {
                db.TB_RoomBookingOrder.Add(tB_RoomBookingOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoomId = new SelectList(db.TB_Room, "ID", "Name", tB_RoomBookingOrder.RoomId);
            return View(tB_RoomBookingOrder);
        }

        // GET: Admin/TB_RoomBookingOrder/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_RoomBookingOrder tB_RoomBookingOrder = db.TB_RoomBookingOrder.Find(id);
            if (tB_RoomBookingOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomId = new SelectList(db.TB_Room, "ID", "Name", tB_RoomBookingOrder.RoomId);
            return View(tB_RoomBookingOrder);
        }

        // POST: Admin/TB_RoomBookingOrder/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,OrderDateForm,OrderDateTo,IsDelete,CreateDateTime,RoomNum,AdultNum,ChildNum,CustomeName,CustomeEmail,CustomeSex,CustomeTel,TotalAmount,OrderNo,IsComplete,RoomId")] TB_RoomBookingOrder tB_RoomBookingOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tB_RoomBookingOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoomId = new SelectList(db.TB_Room, "ID", "Name", tB_RoomBookingOrder.RoomId);
            return View(tB_RoomBookingOrder);
        }

        // GET: Admin/TB_RoomBookingOrder/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_RoomBookingOrder tB_RoomBookingOrder = db.TB_RoomBookingOrder.Find(id);
            if (tB_RoomBookingOrder == null)
            {
                return HttpNotFound();
            }
            return View(tB_RoomBookingOrder);
        }

        // POST: Admin/TB_RoomBookingOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_RoomBookingOrder tB_RoomBookingOrder = db.TB_RoomBookingOrder.Find(id);
            db.TB_RoomBookingOrder.Remove(tB_RoomBookingOrder);
            db.SaveChanges();
            return RedirectToAction("Index");
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
