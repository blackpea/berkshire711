using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using html5up_dopetrope_mvc.Models;

namespace html5up_dopetrope_mvc.Areas.Admin.Controllers
{
    using PagedList;

    public class TB_RoomChargeController : Controller
    {
        private BedAndBreakfastEntities db = new BedAndBreakfastEntities();

        private int pageSize = 10;

        // GET: Admin/TB_RoomCharge
        public ActionResult  Index(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;

            var tB_RoomCharge = db.TB_RoomCharge.Include(t => t.TB_Room);

            return View(tB_RoomCharge.OrderByDescending(desc => desc.CreateDateTime).ToPagedList(currentPage, pageSize));
        }

        // GET: Admin/TB_RoomCharge/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_RoomCharge tB_RoomCharge = await db.TB_RoomCharge.FindAsync(id);
            if (tB_RoomCharge == null)
            {
                return HttpNotFound();
            }
            return View(tB_RoomCharge);
        }

        // GET: Admin/TB_RoomCharge/Create
        public ActionResult Create()
        {
            ViewBag.RoomId = new SelectList(db.TB_Room, "ID", "Name");
            return View();
        }

        // POST: Admin/TB_RoomCharge/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,CreateDateTime,IsDelete,LowSeasonHoliday,LowSeasonWeekday,HighSeasonHoliday,HighSeasonWeekday,RoomId,ExtraBedCharge")] TB_RoomCharge tB_RoomCharge)
        {
            if (ModelState.IsValid)
            {
                db.TB_RoomCharge.Add(tB_RoomCharge);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RoomId = new SelectList(db.TB_Room, "ID", "Name", tB_RoomCharge.RoomId);
            return View(tB_RoomCharge);
        }

        // GET: Admin/TB_RoomCharge/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_RoomCharge tB_RoomCharge = await db.TB_RoomCharge.FindAsync(id);
            if (tB_RoomCharge == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomId = new SelectList(db.TB_Room, "ID", "Name", tB_RoomCharge.RoomId);
            return View(tB_RoomCharge);
        }

        // POST: Admin/TB_RoomCharge/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,CreateDateTime,IsDelete,LowSeasonHoliday,LowSeasonWeekday,HighSeasonHoliday,HighSeasonWeekday,RoomId,ExtraBedCharge")] TB_RoomCharge tB_RoomCharge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tB_RoomCharge).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RoomId = new SelectList(db.TB_Room, "ID", "Name", tB_RoomCharge.RoomId);
            return View(tB_RoomCharge);
        }

        // GET: Admin/TB_RoomCharge/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_RoomCharge tB_RoomCharge = await db.TB_RoomCharge.FindAsync(id);
            if (tB_RoomCharge == null)
            {
                return HttpNotFound();
            }
            return View(tB_RoomCharge);
        }

        // POST: Admin/TB_RoomCharge/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TB_RoomCharge tB_RoomCharge = await db.TB_RoomCharge.FindAsync(id);
            db.TB_RoomCharge.Remove(tB_RoomCharge);
            await db.SaveChangesAsync();
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
