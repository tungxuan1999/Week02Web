using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Week02.Models;

namespace Week02.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Model1 db = new Model1();
            List<SANPHAM> lOAI_SPs = db.SANPHAMs.ToList();
            return View(lOAI_SPs);
        }

        //[ActionName("OverloadedActionName")]
        //public ActionResult Index(int id)
        //{
        //    Model1 db = new Model1();
        //    List<LOAI_SP> lOAI_SPs = db.LOAI_SP.ToList();
        //    return View(lOAI_SPs[id]);
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult ListKind()
        {
            Model1 db = new Model1();
            List<LOAI_SP> lOAI_SPs = db.LOAI_SP.ToList();
            return PartialView(lOAI_SPs);
        }
    }
}