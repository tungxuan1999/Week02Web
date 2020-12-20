using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Week02.Models;

namespace Week02.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowListBills()
        {
            if (Session["login"] != null)
            {
                KHACHHANG kHACHHANG = (KHACHHANG)Session["login"];
                Model1 model1 = new Model1();
                List<HOADON> hOADONs = model1.HOADONs.Where(abc => abc.KH_ID == kHACHHANG.KH_ID).ToList();
                return View(hOADONs);
            }
            else
            {
                return View("../Account/AccountLogin");
            }
        }

        public ActionResult CheckOut(String mahd)
        {
            if (Session["login"] != null)
            {
                KHACHHANG kHACHHANG = (KHACHHANG)Session["login"];
                Model1 model = new Model1();
                HOADON hOADON = model.HOADONs.Where(abc => abc.MAHD == mahd).FirstOrDefault();
                if (hOADON.KH_ID != kHACHHANG.KH_ID)
                {
                    ViewBag.error = "Check Out not exist";
                    return View();
                }
                else
                {
                    Session["MAHD_ID"] = mahd;
                }
                List<SP_HD> sP_HDs = model.SP_HD.Where(abc => abc.MAHD == mahd).ToList();
                if (sP_HDs.Count == 0)
                {
                    ViewBag.error = "Check Out not exist";
                    return View();
                }
                List<Item> items = new List<Item>();
                foreach (var i in sP_HDs)
                {
                    SANPHAM sANPHAM = model.SANPHAMs.Where(abc => abc.MASP == i.MASP).FirstOrDefault();
                    items.Add(new Item()
                    {
                        MALOAI = sANPHAM.MALOAI,
                        MASP = sANPHAM.MASP,
                        Soluong = Int32.Parse(i.Soluong.ToString()),
                        Anh = sANPHAM.Anh,
                        Gia = Int32.Parse(sANPHAM.Gia.ToString()),
                        Mota = sANPHAM.Mota,
                        Ten = sANPHAM.Ten,
                        TongGia = Int32.Parse(i.Gia.ToString()),
                        TonKho = Int32.Parse(sANPHAM.Soluong.ToString())
                    });
                }
                CheckOutHD checkOutHD = new CheckOutHD();
                checkOutHD.items = items;
                checkOutHD.hOADON = hOADON;
                return View(checkOutHD);
            }
            else
            {
                return View("../Account/AccountLogin");
            }
        }

        [HttpPost]
        public ActionResult AddItemToPay(List<List<Object>> cartitem, String address)
        {
            if (cartitem.Count > 0)
            {
                if (Session["login"] != null)
                {
                    Model1 model = new Model1();

                    //Hoa Don
                    KHACHHANG kHACHHANG = (KHACHHANG)Session["login"];
                    UserManager userManager = new UserManager();
                    HOADON hOADON = new HOADON();
                    hOADON.KH_ID = kHACHHANG.KH_ID;
                    hOADON.MAHD = userManager.GetMAHD_ID();
                    hOADON.Ngaytao = DateTime.Now;
                    hOADON.Trangthai = 0;

                    //SP_HD
                    int sumgia = 0;
                    List<SP_HD> sP_HDs = new List<SP_HD>();
                    foreach (var i in cartitem)
                    {
                        sumgia += Int32.Parse(i[2].ToString()) * Int32.Parse(i[4].ToString());
                        sP_HDs.Add(new SP_HD()
                        {
                            MAHD = hOADON.MAHD,
                            MASP = i[0].ToString(),
                            Gia = Int32.Parse(i[2].ToString()) * Int32.Parse(i[4].ToString()),
                            Soluong = Int32.Parse(i[4].ToString())
                        });
                    }

                    //Save HD
                    hOADON.Tongtien = sumgia;
                    hOADON.Diachi = address;
                    model.HOADONs.Add(hOADON);
                    model.SaveChanges();

                    //Save SP_HD
                    foreach (var i in sP_HDs)
                    {
                        model.SP_HD.Add(i);
                    }
                    Session["MAHD_ID"] = hOADON.MAHD;
                    model.SaveChanges();
                    return Json(hOADON.MAHD, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {

                return Json("", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult PaySuccess()
        {

            if (Session["login"] != null)
            {
                Model1 model = new Model1();
                String mahd = (String)Session["MAHD_ID"];
                if (Session["MAHD_ID"] != null)
                {
                    HOADON hOADON1 = model.HOADONs.Where(abc => abc.MAHD == mahd).FirstOrDefault();
                    hOADON1.Trangthai = 1;
                    model.SaveChanges();
                    KHACHHANG kHACHHANG = (KHACHHANG)Session["login"];
                    Email.Send("<h1>Pay " + mahd + " success</h1><br><h5>Account " + kHACHHANG.Email + "</h5><br><h5>Total: $" + hOADON1.Tongtien + "</h5><br><h5>Check bill: " + DataStatic.HOST + "Cart/CheckOut?mahd=" + hOADON1.MAHD + "</h5><br><h4>Please wait for the shop to approve and ship the goods</h4>", "Shop Jewelry", kHACHHANG.Email);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

                Session["MAHD_ID"] = null;
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

    }
}