using System;
using System.Linq;
using System.Web.Mvc;

namespace html5up_dopetrope_mvc.Controllers
{
    using html5up_dopetrope_mvc.Models;
    using html5up_dopetrope_mvc.Repositories;
    using html5up_dopetrope_mvc.Tools;
    using html5up_dopetrope_mvc.ViewModels;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Text;

    using Microsoft.Ajax.Utilities;

    public class RoomsController : Controller
    {
        //private readonly NewsRepository newsRepository;
        private readonly RoomRepository roomRepository;

        public RoomsController()
        {
            //newsRepository = new NewsRepository();
            roomRepository = new RoomRepository();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

                roomRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: RoomBooking
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Booking()
        {
            return View();
        }

        /// <summary>
        /// 房型介紹
        /// </summary>
        /// <returns></returns>
        public ActionResult Introduction()
        {
            var model = new HomeViewModel
            {
                AdultNum = 0,
                ChildNum = 0,
                OrderDateForm = DateTime.Now.Date,
                OrderDateTo = DateTime.Now.AddDays(1).Date,
                RoomNum = 0,
                TbNewsList = null,
                TbRoomsList = roomRepository.Find<TB_Room>(find => find.IsDelete == false)
            };

            ViewBag.Title = "Dopetrope by HTML5 UP";
            return View(model: model);
        }

        /// <summary>
        /// 線上訂房
        /// </summary>
        /// <returns></returns>
        public ActionResult Reservation()
        {
            var model = new RoomBookingOrderViewModel
            {
                //OrderDateForm = DateTime.Now.Date,
                //OrderDateTo = DateTime.Now.AddDays(1).Date,
                TbRoomsList = roomRepository.Find<TB_Room>(find => find.IsDelete == false),
                RoomBookingOrder = new TB_RoomBookingOrder()
                {
                    OrderDateForm = DateTime.Now.Date,
                    OrderDateTo = DateTime.Now.AddDays(1).Date,
                    AdultNum = 1,
                    RoomNum = 1,
                    TotalAmount = 0,
                    RoomId = 1,
                    CustomeSex = "先生/小姐"
                }
            };

            return View(model);
        }

        /// <summary>
        /// 線上訂房
        /// </summary>
        /// <param name="postViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reservation(RoomBookingOrderViewModel postViewModel)
        {
            postViewModel.RoomBookingOrder.CreateDateTime = DateTime.Now;
            postViewModel.RoomBookingOrder.OrderNo = DateTime.Now.ToFileTimeUtc().ToString();
            postViewModel.TbRoomsList = roomRepository.Find<TB_Room>(find => find.IsDelete == false);

            if (postViewModel.RoomBookingOrder.OrderDateForm.Date < DateTime.Now.Date && postViewModel.RoomBookingOrder.OrderDateTo.Date < DateTime.Now.Date)
            {
                this.ViewBag.Message = "訂房開始、結束日期，不可小於今日!";

                return View(postViewModel);
            }

            if (postViewModel.RoomBookingOrder.OrderDateTo < postViewModel.RoomBookingOrder.OrderDateForm)
            {
                this.ViewBag.Message = "訂房結束日期不可小於開始日期!";

                return View(postViewModel);
            }

            // 訂房不可預約超過22晚，
            if (postViewModel.RoomBookingOrder.OrderDateTo.Date > postViewModel.RoomBookingOrder.OrderDateForm.AddDays(22).Date)
            {
                this.ViewBag.Message = "訂房不可預約超過22晚!";

                return View(postViewModel);
            }

            if (!ModelState.IsValid)
            {
                return View(postViewModel);
            }

            if (roomRepository.Find<TB_RoomBookingOrder>(find => find.IsDelete == false &&
                                                                 find.RoomId == postViewModel.RoomBookingOrder.RoomId && (
                                                                 (postViewModel.RoomBookingOrder.OrderDateForm >= find.OrderDateForm &&
                                                                 postViewModel.RoomBookingOrder.OrderDateForm < find.OrderDateTo) || (
                                                              postViewModel.RoomBookingOrder.OrderDateTo > find.OrderDateForm &&
                                                              postViewModel.RoomBookingOrder.OrderDateTo <= find.OrderDateTo))).Any())
            {
                ViewBag.Message = "訂房失敗! 該房型已被預訂!";

                return View(postViewModel);
            }


            if (postViewModel.InputCode == null)
            {
                ViewBag.Message = "請輸入驗證碼!";

                return View(postViewModel);
            }


            if (!postViewModel.InputCode.Trim().ToLower().Equals(Session["ValidateCode"].ToString().ToLower()))
            {

                ViewBag.Message = "驗證碼，驗證失敗!";

                return View(postViewModel);
            }

            postViewModel.RoomBookingOrder.TotalAmount = this.GetMoney(postViewModel);

            postViewModel.RoomBookingOrder.CustomeSex = "先生/小姐";
            var isSuccess = roomRepository.AddOrUpdate(postViewModel.RoomBookingOrder);
            this.ViewBag.Message = isSuccess ? "訂房成功!" : "訂房失敗!";

            if (this.ViewBag.Message.Equals("訂房成功!"))
            {
                var roomObject = roomRepository.Find<TB_Room>(find => find.ID == postViewModel.RoomBookingOrder.RoomId && find.IsDelete == false).FirstOrDefault();

                postViewModel.RoomBookingOrder.TB_Room = roomObject;

                SendMail(postViewModel.RoomBookingOrder.CustomeEmail, "波克夏民宿-恭喜您訂房成功!", "<html><body>"
                        + string.Format(@"<div style='font-family: arial, sans-serif; font-size: 14px;'><span style='font-size:12px;'><span style='font-family:times new roman,times,serif;'><small><font color='#003366'>Dear {0} 先生/小姐 您好!</font></small></span></span></div>

<div style='font-family: arial, sans-serif; font-size: 14px;'><span style='font-size:12px;'><span style='font-family:times new roman,times,serif;'><small><font color='#003366'>恭喜您已經成功地訂房，</font><span style='color: rgb(93, 93, 93); line-height: 25.6666679382324px; white-space: pre-wrap;'>線上訂房程序完成。</span></small></span></span></div>

<div style='font-family: arial, sans-serif; font-size: 14px;'><span style='font-size:12px;'><span style='font-family:times new roman,times,serif;'><small><span style='color: rgb(93, 93, 93); line-height: 25.6666679382324px; white-space: pre-wrap;'>請於</span><span style='color:#ff0000;'><span style='line-height: 25.6666679382324px; white-space: pre-wrap;'>三日內</span></span><span style='color: rgb(93, 93, 93); line-height: 25.6666679382324px; white-space: pre-wrap;'>以匯款或轉帳方式繳付房費，方可保留客房。若三日內未完成匯款，則視為無效訂房，房間將重新開放預訂。</span></small></span></span></div>

<div style='font-family: arial, sans-serif; font-size: 14px;'>※連續假日及特殊節日，以旺季價位計算，價差就現場收費</div>

<table border='1' cellpadding='1' cellspacing='1' style='width: 500px;' summary='您的訂房資料'>
	<tbody>
		<tr>
			<td valign='top'>
			<section class='box'>
			<header>
			<h3><strong>您的訂房資料</strong></h3>
			</header>

			<form action='/Rooms/Reservation' method='post'>
			<div>
			<div class='form-group'><strong><label>訂房單號</label></strong><strong>：{1}</strong></div>

			<div class='form-group'>&nbsp;</div>

			<div class='form-group'><strong><label>入住日期</label>：{2}</strong></div>

			<div class='form-group'><strong><label>退房日期</label>：{3}&nbsp;共 <span class='live-num'>{4}</span> 晚</strong></div>

			<div class='form-group'><strong><label>房型</label>：{5}</strong></div>

			<div class='form-group'><strong><label>大人人數</label> {6} 位</strong></div>

			<div class='form-group'><strong><label>小孩人數</label> {7} 位</strong></div>

			<div class='form-group'><strong><label>加床數 </label> {8} 位</strong></div>

			<div class='form-group'><strong><label>訂房人姓名</label>：{9}</strong></div>

			<div class='form-group'><strong><label>訂房人Email</label>：{10}</strong></div>

			<div class='form-group'><strong><label>訂房人電話</label>：{11}</strong></div>

			<div class='form-group'><strong><label>住宿金額</label>：{12}</strong></div>
			</div>

			<footer class='actions'>&nbsp;</footer>
			</form>
			</section>
			</td>
			<td valign='top'>
			<section class='box'>
			<header>
			<h3><strong>波克夏民宿「銀行匯款資訊」</strong></h3>
			</header>

			<div>
			<div class='form-group'><strong><label>銀行名稱</label>：郵局-七美分局</strong></div>

			<div class='form-group'><strong><label>銀行代碼</label>：700</strong></div>

			<div class='form-group'><strong><label>帳戶名稱</label>：夏陳秀梅</strong></div>

			<div class='form-group'><strong><label>銀行帳號：</label>0241-041-004-2331</strong></div>
			</div>

			<footer class='actions'>&nbsp;</footer>
			</section>
			</td>
		</tr>
	</tbody>
</table>

<p>&nbsp;</p>

", postViewModel.RoomBookingOrder.CustomeName, postViewModel.RoomBookingOrder.OrderNo, postViewModel.RoomBookingOrder.OrderDateForm.ToString("yyyy/MM/dd"), postViewModel.RoomBookingOrder.OrderDateTo.ToString("yyyy/MM/dd"), EachDay(postViewModel.RoomBookingOrder.OrderDateForm, postViewModel.RoomBookingOrder.OrderDateTo).Count(), postViewModel.RoomBookingOrder.TB_Room.Name, postViewModel.RoomBookingOrder.AdultNum, postViewModel.RoomBookingOrder.ChildNum, postViewModel.RoomBookingOrder.ExtraBadNum, postViewModel.RoomBookingOrder.CustomeName, postViewModel.RoomBookingOrder.CustomeEmail, postViewModel.RoomBookingOrder.CustomeTel, postViewModel.RoomBookingOrder.TotalAmount.ToString("C0")) + "</body></html>");

                return View("partial_BookingSuccess", postViewModel.RoomBookingOrder);
            }

            return View(postViewModel);
        }

        /// <summary>
        /// 發送信件
        /// </summary>
        /// <returns></returns>

        private bool SendMail(string UserMail, string Subject, string Body)
        {
            if (string.IsNullOrEmpty(UserMail) && string.IsNullOrEmpty(Subject) && string.IsNullOrEmpty(Body)) //Subject 主題  Body 內文
            {
                return false;
            }

            SmtpSender smtp = new SmtpSender(
               "smtp.gmail.com", //從資料庫中取server
                587,//取port
                "berkshire711@gmail.com",//mail server account
                "Berk7-11",//password
                true);
            return smtp.Send("berkshire711@gmail.com", UserMail, Subject, Body); //UserMail Subjet Body
        }

        public int GetMoney(RoomBookingOrderViewModel postViewModel)
        {
            var roomid = postViewModel.RoomBookingOrder.RoomId;
            var roomCharge = roomRepository.Find<TB_RoomCharge>(w => w.RoomId == roomid).First();
            bool IsHolidays = false;
            bool IsHighSeason = false;

            int TotalAmount = 0;

            foreach (
                DateTime day in
                    EachDay(postViewModel.RoomBookingOrder.OrderDateForm, postViewModel.RoomBookingOrder.OrderDateTo))
            {
                IsHolidays = GetIsHolidays(day);
                IsHighSeason = GetIsHighSeason(day);

                if (IsHighSeason && IsHolidays)
                {
                    TotalAmount += roomCharge.HighSeasonHoliday;
                }
                else if (IsHighSeason && !IsHolidays)
                {
                    TotalAmount += roomCharge.HighSeasonWeekday;
                }
                else if (!IsHighSeason && IsHolidays)
                {
                    TotalAmount += roomCharge.LowSeasonHoliday;
                }
                else if (!IsHighSeason && !IsHolidays)
                {
                    TotalAmount += roomCharge.LowSeasonWeekday;
                }

                if (postViewModel.RoomBookingOrder.IsExtraBed)
                {
                    TotalAmount += roomCharge.ExtraBedCharge * postViewModel.RoomBookingOrder.ExtraBadNum;
                }
            }

            return TotalAmount;
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date < thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public bool GetIsHolidays(DateTime date)
        {
            // 週休二日
            if (date.DayOfWeek == DayOfWeek.Friday || date.DayOfWeek == DayOfWeek.Saturday)
            {
                return true;
            }

            return false;
        }

        public bool GetIsHighSeason(DateTime date)
        {
            //旺季是３月１６日～１０月１５日；淡季是１０月１６日～３月１５日
            CustomDateRange customDateRangeHighSeason = new CustomDateRange()
            {
                StartDate = new DateTime(DateTime.Now.Year, 3, 16),
                EndDate = new DateTime(DateTime.Now.Year, 10, 15)
            };
 
            // 週休二日
            return customDateRangeHighSeason.StartDate.Date <= date.Date
                   && customDateRangeHighSeason.EndDate.Date >= date.Date;
 
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public JsonResult GetDiaryEvents(double start, double end)
        {

            var fromDate = HtmlHelperExtensions.ConvertFromUnixTimestamp(start);
            var toDate = HtmlHelperExtensions.ConvertFromUnixTimestamp(end);

            var ApptListForDate = roomRepository.Find<TB_RoomBookingOrder>(find => find.IsDelete == false &&
                                                                                   find.OrderDateForm >= fromDate &&
                                                                                   find.OrderDateTo <= toDate).ToList();

            var PaymentOrders = ApptListForDate.Where(w => w.IsComplete && w.IsDelete == false).ToList();

            var nonPaymentOrders = ApptListForDate.Where(w => !w.IsComplete &&
                //w.CreateDateTime.Date >= DateTime.Now.Date &&
                DateTime.Now.Date <= w.CreateDateTime.Date.AddDays(3) &&
                w.IsDelete == false).ToList();

            PaymentOrders.AddRange(nonPaymentOrders);

            foreach (var bookingOrder in PaymentOrders)
            {
                bookingOrder.TB_Room =
                    this.roomRepository.Find<TB_Room>(f => f.IsDelete == false && f.ID == bookingOrder.RoomId)
                        .FirstOrDefault();
            }

            var eventList = from e in PaymentOrders
                            select new
                            {
                                id = e.ID,
                                title = e.TB_Room != null ? e.TB_Room.Name + (e.IsComplete ? "(已匯款)" : "(未匯款)") : "",
                                start = e.OrderDateForm.Date.ToString("yyyy-MM-dd"),
                                end = e.OrderDateTo.Date.ToString("yyyy-MM-dd"),
                                color = e.TB_Room != null ? e.TB_Room.Color : "",
                                textColor = "#FFFFFF",
                                someKey = e.ID,
                                allDay = false
                            };

            var rows = eventList.ToArray();

            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 房型介紹-明細
        /// </summary>
        /// <param name="tbRoomId"></param>
        /// <returns></returns>
        public ActionResult Details(int? tbRoomId)
        {
            if (!tbRoomId.HasValue)
            {
                return RedirectToAction("Introduction", "Rooms");
            }

            var model = new RoomInfoViewModel
            {
                Room = roomRepository.Find<TB_Room>(find => find.IsDelete == false && find.ID == tbRoomId).First(),
                RoomCharge = roomRepository.Find<TB_RoomCharge>(find => find.IsDelete == false && find.ID == tbRoomId).First()
            };

            ViewBag.Title = "Dopetrope by HTML5 UP";
            return View(model: model);
        }

        public void VerificationCode()
        {
            // 是否產生驗證碼
            bool isCreate = true;

            // Session["CreateTime"]: 建立驗證碼的時間
            if (Session["CreateTime"] == null)
            {
                Session["CreateTime"] = DateTime.Now;
            }
            else
            {
                DateTime startTime = Convert.ToDateTime(Session["CreateTime"]);
                DateTime endTime = Convert.ToDateTime(DateTime.Now);
                TimeSpan ts = endTime - startTime;


                // 重新產生驗證碼的間隔
                if (ts.Minutes > 15)
                {
                    isCreate = true;
                    Session["CreateTime"] = DateTime.Now;
                }
                else
                {
                    isCreate = false;
                }
            }

            Response.ContentType = "image/gif";
            //建立 Bitmap 物件和繪製
            Bitmap basemap = new Bitmap(200, 60);
            Graphics graph = Graphics.FromImage(basemap);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, 200, 60);
            Font font = new Font(FontFamily.GenericSerif, 24, FontStyle.Bold, GraphicsUnit.Pixel);
            Random random = new Random();
            // 英數
            string letters = "ABCDEFGHIJKLMNPQRSTUVWXYZabcdefghijklmnpqrstuvwxyz0123456789";
            // 天干地支生肖
            //string letters = "甲乙丙丁戊己庚辛壬癸子丑寅卯辰巳午未申酉戍亥鼠牛虎免龍蛇馬羊猴雞狗豬";
            string letter;
            StringBuilder sb = new StringBuilder();


            if (isCreate)
            {
                // 加入隨機二個字
                // 英文4 ~ 5字，中文2 ~ 3字
                for (int word = 0; word < 5; word++)
                {
                    letter = letters.Substring(random.Next(0, letters.Length - 1), 1);
                    sb.Append(letter);

                    // 繪製字串 
                    graph.DrawString(letter, font, new SolidBrush(Color.Black), word * 38, random.Next(0, 15));
                }
            }
            else
            {
                // 使用先前的驗證碼來產生
                string currentCode = Session["ValidateCode"].ToString();
                sb.Append(currentCode);

                foreach (char item in currentCode)
                {
                    letter = item.ToString();
                    // 繪製字串
                    graph.DrawString(letter, font, new SolidBrush(Color.Black), currentCode.IndexOf(item) * 38, random.Next(0, 15));
                }
            }


            // 混亂背景
            Pen linePen = new Pen(new SolidBrush(Color.Black), 2);
            for (int x = 0; x < 10; x++)
            {
                graph.DrawLine(linePen, new Point(random.Next(0, 199), random.Next(0, 59)), new Point(random.Next(0, 199), random.Next(0, 59)));
            }

            // 儲存圖片並輸出至stream      
            basemap.Save(Response.OutputStream, ImageFormat.Gif);
            // 將產生字串儲存至 Sesssion
            Session["ValidateCode"] = sb.ToString();
            Response.End();
        }
    }
}