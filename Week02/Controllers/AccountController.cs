using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Providers.Entities;
using Week02.Models;

namespace Week02.Controllers
{
    public class AccountController : Controller
    {
        RegisterModel registerModel;
        LoginModel loginModel;

        [HttpPost]
        public ActionResult Register(RegisterModel user)
        {
            registerModel = new RegisterModel();
            if (ModelState.IsValid)
            {
                try
                {

                    //if (user.MK == user.CFMK)
                    //{
                    UserManager userManager = new UserManager();
                    Model1 model1 = new Model1();

                    //String checknull = userManager.CheckNullRegister(user);
                    //String checkminmax = userManager.CheckMinMaxRegister(user);
                    //if (checknull.Equals(""))
                    //{
                    //    if (checkminmax.Equals(""))
                    //    {
                    Boolean checkEmailExist = userManager.CheckEmail(user.Email);
                    Boolean checkPhone = userManager.CheckPhone(user.SDT);
                    if (checkEmailExist)
                    {
                        if (checkPhone)
                        {
                            KHACHHANG kHACHHANG = new KHACHHANG();
                            kHACHHANG.Diachi = user.Diachi;
                            kHACHHANG.Email = user.Email;
                            kHACHHANG.SDT = user.SDT;
                            kHACHHANG.MK = user.MK;
                            kHACHHANG.Ngaysinh = user.Ngaysinh;
                            kHACHHANG.Ten = user.Ten;
                            kHACHHANG.Quyen = "User";
                            kHACHHANG.Trangthai = 0;
                            kHACHHANG.KH_ID = userManager.GetKH_ID();
                            model1.KHACHHANGs.Add(kHACHHANG);
                            model1.SaveChanges();
                            Email.Send("<h1>Register success</h1><br><h5>Account " + kHACHHANG.Email + "</h5><br><h5>Welcome: " + DataStatic.HOST + "</h5>", "Shop Jewelry", kHACHHANG.Email);
                            ViewBag.error = "Register success";
                        }
                        else
                        {
                            ViewBag.error = "Phone exist";
                        }
                    }
                    else
                    {
                        ViewBag.error = "Email exist";
                    }
                    //}
                    //    else
                    //    {
                    //        ViewBag.error = checkminmax;
                    //    }
                    //}
                    //else
                    //{
                    //    ViewBag.error = checknull;
                    //}
                    //}
                    //else
                    //{
                    //    ViewBag.error = "Password don't like Password Confirm";
                    //}

                    return View("AccountRegister", registerModel);
                }
                catch (Exception ex)
                {
                    //TempData["errorMessage"] = "Error: " + ex.Message + " - " + ex.InnerException;
                    ViewBag.error = "Error: " + ex.Message + " - " + ex.InnerException;
                    return View("AccountRegister", registerModel);
                }
            }
            else
            {
                ViewBag.error = "Error: Please fill the form correctly";
                return View("AccountRegister", registerModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(RegisterModel user)
        {
            registerModel = new RegisterModel();
            if (Session["login"] != null)
            {
                if (ModelState.IsValid)
                {
                    KHACHHANG kh = (KHACHHANG)Session["login"];
                    try
                    {
                        UserManager userManager = new UserManager();
                        Model1 model1 = new Model1();
                        Boolean checkEmailExist = userManager.CheckEmail(user.Email);
                        Boolean checkPhone = userManager.CheckPhone(user.SDT);
                        if (checkEmailExist || user.Email == kh.Email)
                        {
                            if (checkPhone || user.SDT == kh.SDT)
                            {
                                KHACHHANG kHACHHANG = model1.KHACHHANGs.Where(abc => abc.KH_ID == kh.KH_ID).FirstOrDefault();
                                kHACHHANG.Diachi = user.Diachi;
                                kHACHHANG.Email = user.Email;
                                kHACHHANG.SDT = user.SDT;
                                kHACHHANG.MK = user.MK;
                                kHACHHANG.Ngaysinh = user.Ngaysinh;
                                kHACHHANG.Ten = user.Ten;
                                kHACHHANG.Trangthai = 0;
                                model1.SaveChanges();
                                Email.Send("<h1>Update account success</h1><br><h5>Account " + kHACHHANG.Email + "</h5><br><h5>Welcome: " + DataStatic.HOST + "</h5>", "Shop Jewelry", kHACHHANG.Email);
                                ViewBag.error = "Update account success";
                            }
                            else
                            {
                                ViewBag.error = "Phone exist";
                            }
                        }
                        else
                        {
                            ViewBag.error = "Email exist";
                        }
                        //}
                        //    else
                        //    {
                        //        ViewBag.error = checkminmax;
                        //    }
                        //}
                        //else
                        //{
                        //    ViewBag.error = checknull;
                        //}
                        //}
                        //else
                        //{
                        //    ViewBag.error = "Password don't like Password Confirm";
                        //}

                        return View("EditMyAccount", registerModel);
                    }
                    catch (Exception ex)
                    {
                        //TempData["errorMessage"] = "Error: " + ex.Message + " - " + ex.InnerException;
                        ViewBag.error = "Error: " + ex.Message + " - " + ex.InnerException;
                        return View("EditMyAccount", registerModel);
                    }
                }
                else
                {
                    ViewBag.error = "Error: Please fill the form correctly";
                    return View("EditMyAccount", registerModel);
                }
            }
            else
            {
                return View("EditMyAccount", registerModel);
            }
        }

        [HttpPost]
        public ActionResult Login(LoginModel user)
        {
            loginModel = new LoginModel();
            if (ModelState.IsValid)
            {
                try
                {
                    UserManager userManager = new UserManager();
                    //String checkNull = userManager.CheckNullLogin(user);
                    //String checkMinMax = userManager.CheckMinMaxLogin(user);

                    //if (checkNull.Equals(""))
                    //{
                    //    if (checkMinMax.Equals(""))
                    //    {
                    //Boolean checkEmailExist = userManager.CheckEmail(user.Email);
                    //if (!checkEmailExist)
                    //{
                    KHACHHANG kh = userManager.CheckLogin(user);
                    if (kh != null)
                    {
                        Session["login"] = kh;
                        //ViewBag.error = "Login success";
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.error = "Login fail, pasword don't correct or email don't exitst";
                    }
                    //}
                    //else
                    //{
                    //    ViewBag.error = "Email don't register";
                    //}
                    //    }
                    //    else
                    //    {
                    //        ViewBag.error = checkMinMax;
                    //    }
                    //}
                    //else
                    //{
                    //    ViewBag.error = checkNull;
                    //}

                    return View("AccountLogin", loginModel);
                }
                catch (Exception ex)
                {
                    //TempData["errorMessage"] = "Error: " + ex.Message + " - " + ex.InnerException;
                    ViewBag.error = "Error: " + ex.Message + " - " + ex.InnerException;
                    return View("AccountLogin", loginModel);
                }
            }
            else
            {
                ViewBag.error = "Error: Please fill the form correctly";
                return View("AccountLogin", loginModel);
            }
        }

        [HttpPost]
        public ActionResult Logout()
        {
            if (Session["login"] != null)
            {
                Session["login"] = null;
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AccountRegister()
        {
            if (Session["login"] != null)
                return View("AccountLogin");
            else
                return View();
        }

        public ActionResult AccountLogin()
        {
            if (Session["login"] != null)
            {
                KHACHHANG kHACHHANG = (KHACHHANG)Session["login"];
                return View("MyAccount", kHACHHANG);
            }
            else
                return View();
        }

        public ActionResult EditMyAccount()
        {
            if (Session["login"] == null)
            {
                return View("AccountLogin");
            }
            else
            {
                KHACHHANG kHACHHANG = (KHACHHANG)Session["login"];
                RegisterModel registerModel = new RegisterModel();
                registerModel.Email = kHACHHANG.Email;
                registerModel.Diachi = kHACHHANG.Diachi;
                registerModel.Ngaysinh = DateTime.Parse(kHACHHANG.Ngaysinh.ToString());
                registerModel.SDT = kHACHHANG.SDT;
                registerModel.Ten = kHACHHANG.Ten;
                return View(registerModel);
            }
        }

        [ChildActionOnly]
        public ActionResult AcctionLoginForm()
        {
            return PartialView();
        }
    }
}