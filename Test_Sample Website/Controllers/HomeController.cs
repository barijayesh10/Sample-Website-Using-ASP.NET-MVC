using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test_Sample_Website;

namespace Test_Sample_Website.Controllers
{
    public class HomeController : Controller
    {
        Class1 clsobj = new Class1();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [Route("fields")]
        public ActionResult fields()
        {
            return View();
        }

        [Route("about")]
        public ActionResult about()
        {
            return View();
        }

        [Route("contact")]
        public ActionResult contact()
        {
            var countrylist = clsobj.getcountry();
            ViewBag.countrylist = countrylist;
            return View();
        }

        [Route("feedback")]
        public ActionResult feedback()
        {
            return View();
        }

        [Route("getmember")]
        public ActionResult GetMember()
        {
            DataTable memberlist = clsobj.getmember();
            return View(memberlist);
        }



        //[ChildActionOnly]
        //public PartialViewResult login()
        //{
        //    return PartialView("_login");//logsec
        //}

        //[ChildActionOnly]
        //public PartialViewResult register()
        //{
        //    return PartialView("_register");
        //}





        [HttpPost]
        public ActionResult login(string login)
        {
            string result;
            #region work
            string User = Request.Form["username"];
            string Pass = Request.Form["password"];
            if (User.Trim() != "" && Pass.Trim() != "")
            {
                try
                {
                    int check_login = clsobj.Login(User, Pass);
                    if (check_login == 0)
                    {
                        Session["username"] = User;
                        Session["login_check"] = "Yes";
                        result = "0";
                    }
                    else
                    {
                        ViewBag.UserName = User;
                        ViewBag.Password = Pass;
                        result = "1";
                    }
                }
                catch (Exception ex)
                {
                    result = "2";
                }
            }
            else
            {
                result = "3";
            }
            #endregion

            return Content(result);
        }

        [HttpPost]
        public ActionResult register(string Register)
        {
            string result;
            #region work
            string User = Request.Form["reg_username"];
            string Pass = Request.Form["reg_password"];
            string Con_Pass = Request.Form["reg_con_pssword"];

            if (User.Trim() != "" && Pass.Trim() != "" && Con_Pass.Trim() != "")
            {
                if (Pass == Con_Pass)
                {
                    int check_reg = clsobj.Register(User, Pass, Con_Pass);
                    if (check_reg == 0)
                    {
                        Session["username"] = User;
                        Session["login_check"] = "Yes";
                        result = "0";
                    }
                    else if (check_reg == 1)
                    {
                        ViewBag.reg_username = User;
                        ViewBag.reg_password = Pass;
                        ViewBag.reg_con_pssword = Con_Pass;
                        result = "1";
                    }
                    else
                    {
                        ViewBag.reg_username = User;
                        ViewBag.reg_password = Pass;
                        ViewBag.reg_con_pssword = Con_Pass;
                        result = "2";
                    }
                }
                else
                {
                    ViewBag.reg_username = User;
                    ViewBag.reg_password = Pass;
                    ViewBag.reg_con_pssword = Con_Pass;
                    result = "3";
                }
            }
            else
            {
                result = "4";
            }
            #endregion

            return Content(result);
        }

        [HttpPost]
        [Route("contact")]
        public ActionResult contact(string Contact)
        {
            string result;
            #region work
            string Name = Request.Form["name"];
            string Email = Request.Form["email"];
            string Number = Request.Form["number"];
            string Message = Request.Form["message"];
            string country = Request.Form["SelectedCountryId"];
            if (Name.Trim() != "" && Email.Trim() != "" && Number.Trim() != "" && Message.Trim() != "")
            {
                int check_login = clsobj.Contact(Name, Email, Number, Message);

                if (check_login == 0)
                {
                    //try
                    //{
                    //    MailMessage mail = new MailMessage();
                    //    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                    //    smtp.EnableSsl = true;
                    //    smtp.UseDefaultCredentials = true;
                    //    smtp.Port = 587;

                    //    mail.From = new MailAddress("barijay56@gmail.com");
                    //    mail.To.Add(Email);
                    //    mail.Subject = "Contact Mail";
                    //    mail.Body = "Thanks For Contacting Us. I will be right back";

                    //    NetworkCredential NetworkCred = new System.Net.NetworkCredential("barijay56@gmail.com", "sqadpfcwqcaqkkkj");
                    //    smtp.Credentials = NetworkCred;
                    //    smtp.EnableSsl = true;

                    //    smtp.Send(mail);
                    //}
                    //catch (Exception)
                    //{
                    //    throw;
                    //}
                    result = "<b>Thank you for reaching out to us. We appreciate your interest and the time " +
                        "you've taken to contact us. Your inquiry is important to us, and we will do our best to provide you with a prompt and helpful response." +
                        "Our team is currently reviewing your message, and you can expect to hear back from us within 24-48 hours</b>";
                }
                else
                {
                    result = "<b>Something Wrong...Please Contact after some time. Thank You.</b>";
                }
            }
            else
            {
                result = "<b>All Fields are required.</b>";
            }
            #endregion
            return Content(result);
        }



        [Route("logout")]
        public ActionResult logout()
        {
            Session.Remove("username");
            Session.Remove("login_check");
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}