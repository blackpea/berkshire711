using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace html5up_dopetrope_mvc.Controllers
{
    using html5up_dopetrope_mvc.Models;
    using html5up_dopetrope_mvc.Repositories;
    using html5up_dopetrope_mvc.ViewModels;

    public class HomeController : Controller
    {
        private readonly NewsRepository newsRepository;
        private readonly RoomRepository roomRepository;

        public HomeController()
        {
            newsRepository = new NewsRepository();
            roomRepository = new RoomRepository();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                newsRepository.Dispose();
                roomRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var model = new HomeViewModel
            {
                AdultNum = 0,
                ChildNum = 0,
                OrderDateForm = DateTime.Now.Date,
                OrderDateTo = DateTime.Now.AddDays(1).Date,
                RoomNum = 0,
                TbNewsList = null,
                TbRoomsList = roomRepository.Find<TB_Room>(find => find.IsDelete == false),
     
                RoomBookingOrder = new TB_RoomBookingOrder()
                {
                    OrderDateForm = DateTime.Now.Date,
                    OrderDateTo = DateTime.Now.AddDays(1).Date,
                    RoomNum = 1,
                    TotalAmount = 0,
                    RoomId = 1,
                    AdultNum = 1,
                    CustomeSex = "先生/小姐"
                }
            };

            ViewBag.Title = "Dopetrope by HTML5 UP";
            return View(model: model);
            //return View(new HomeViewModel());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LeftSidebar()
        {
            ViewBag.Title = "Left Sidebar - Dopetrope by HTML5 UP";
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult NoSidebar()
        {
            ViewBag.Title = "No Sidebar - Dopetrope by HTML5 UP";
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult RightSidebar()
        {
            ViewBag.Title = "Right Sidebar - Dopetrope by HTML5 UP";
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Error()
        {
            return View();
        }
    }
}
