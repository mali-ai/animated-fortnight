using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BCSF18M016_Assignment5.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(String login, String password)
        {
            var obj = DAL.isUserPresent(login, password);
            if (obj != null)
            {
                Session["user"] = obj;
                TempData["user"] = (User)Session["user"];
                return RedirectToAction("HomeScreen", "Products");
            }
            ViewBag.MSG = "Invalid Login/Password";
            ViewBag.Login = login;
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(String name, String password, String login, String picURL)
        {
            User user = new User();
            var uniqueName = "";

            if (Request.Files["picURL"] != null)
            {
                var file = Request.Files["picURL"];
                if (file.FileName != "")
                {
                    var ext = System.IO.Path.GetExtension(file.FileName);
                    uniqueName = Guid.NewGuid().ToString() + ext;
                    var rootPath = Server.MapPath("~/UploadedFiles");
                    var fileSavePath = System.IO.Path.Combine(rootPath, uniqueName);
                    file.SaveAs(fileSavePath);
                    user.picURL = uniqueName;
                }
                else
                {
                    user.picURL = "pepsi.jpg";
                }
            }


            user.name = name;
            user.password = password;
            user.login = login;
            user.createdOn = DateTime.Now;
            user.isActive = true;
            user.isAdmin = false;



            DAL.addUser(user);
            Session["user"] = user;
            TempData["user"] = user;
            return RedirectToAction("HomeScreen", "Products");

        }

        [HttpPost]
        public ActionResult API(string action)
        {
            if (action == "getLogins")
            {
                return Json(new { data = DAL.getAllLogins() });
            }
            return Json(new { data = "" });
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        public ActionResult Edit(string id)
        {
            if (Session["user"] != null)
            {
                User user = DAL.getUserbyID(((User)Session["user"]).userId);
                ViewBag.User = user;
                return View();
            }
            else
            {
                return RedirectToAction("HomeScreen", "Products");
            }
        }


        [HttpPost]
        public ActionResult Edit(int id, String name, String password, String login, String picURL)
        {

            User user = new User();
            var uniqueName = "";

            if (Request.Files["picURL"] != null)
            {
                var file = Request.Files["picURL"];
                if (file.FileName != "")
                {
                    var ext = System.IO.Path.GetExtension(file.FileName);
                    uniqueName = Guid.NewGuid().ToString() + ext;
                    var rootPath = Server.MapPath("~/UploadedFiles");
                    var fileSavePath = System.IO.Path.Combine(rootPath, uniqueName);
                    file.SaveAs(fileSavePath);
                    user.picURL = uniqueName;
                }
                else
                {
                    user.picURL = "pepsi.jpg";
                }
            }

            user.userId = id;
            user.name = name;
            user.password = password;
            user.login = login;
            if (DAL.updateUser(user))
            {
                Session["user"] = DAL.getUserbyID(id);
            }
            
            return RedirectToAction("HomeScreen", "Products");

        }
    }
}