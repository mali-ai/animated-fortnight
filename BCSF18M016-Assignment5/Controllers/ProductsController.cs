using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace BCSF18M016_Assignment5.Controllers
{
    public class ProductsController : Controller
    {

        public ActionResult HomeScreen()
        {
            ViewBag.User = Session["user"];
            ViewBag.Products = DAL.getAllProducts();
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(int ID, string email)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Product product = DAL.getProductById(ID);
                    var senderEmail = new MailAddress("tempgm321@gmail.com", "Hafiz Muhammad Ali - BCSF18M016");
                    var receiverEmail = new MailAddress(email, "Receiver");
                    var password = "temp123@$.com";
                    var sub = "Assignment 5";
                    var body = $"Product Name: {product.name}\nProduct Type: {product.typeName}\nPrice: {product.price}\nDescription: {product.description}";
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = sub,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return Json(new { data = true });
        }

        public ActionResult Delete(int id)
        {
            return Json(new { data = DAL.deleteProductById(id)});
        }

        public ActionResult Add()
        {
            if (Session["user"] != null)
            {
                ViewBag.Types = DAL.getAllTypes();
                return View();
            }
            else
            {
                return RedirectToAction("HomeScreen", "Products");
            }
        }

        [HttpPost]
        public ActionResult Add(String name, Double price, int type, String desc, String picURL)
        {
            Product product = new Product();
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
                    product.picURL = uniqueName;
                }
                else
                {
                    product.picURL = "pepsi.jpg";
                }
            }


            product.name = name;
            product.price = price;
            product.typeId = type;
            product.description = desc;
            product.updatedOn = DateTime.Now;
            product.updatedBy = ((User)Session["user"]).userId;
            product.isActive = true;



            DAL.addProduct(product);
            return RedirectToAction("HomeScreen", "Products");
        }

        public ActionResult GetProductNames()
        {
            return Json(new { data = DAL.getAllProductNames() });
        }


        public ActionResult Edit(string id)
        {
            if (Session["user"] != null)
            {
                Product product = DAL.getProductById(Int32.Parse(id));
                ViewBag.Product = product;
                ViewBag.Types = DAL.getAllTypes();
                return View();
            }
            else
            {
                return RedirectToAction("HomeScreen", "Products");
            }
        }


        [HttpPost]
        public ActionResult Edit(int id, String name, Double price, int type, String desc, String picURL)
        {
            Product product = new Product();
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
                    product.picURL = uniqueName;
                }
                else
                {
                    product.picURL = "pepsi.jpg";
                }
            }

            product.productId = id;
            product.name = name;
            product.price = price;
            product.typeId = type;
            product.description = desc;
            product.updatedOn = DateTime.Now;
            product.updatedBy = ((User)Session["user"]).userId;
            product.isActive = true;
            DAL.updateProduct(product);
            return RedirectToAction("HomeScreen", "Products");
        }



        // GET: Products
        public ActionResult Index()
        {
            return View();
        }
    }
}