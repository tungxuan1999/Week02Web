using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Week02.Models;

namespace Week02.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetDetail(String id)
        {
            Model1 db = new Model1();
            SANPHAM detail = db.SANPHAMs.Where(abc => abc.MASP == id).SingleOrDefault();
            return View(detail);
        }

        public ActionResult Products(String maloai)
        {
            Model1 db = new Model1();
            List<SANPHAM> sp;
            if (maloai == null)
                sp = db.SANPHAMs.ToList();
            else
            {
                sp = db.SANPHAMs.Where(abc => abc.MALOAI == maloai).ToList();
            }
            return View(sp);
        }
    }
}