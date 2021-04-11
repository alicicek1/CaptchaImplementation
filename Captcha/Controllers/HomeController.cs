using Captcha.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Web;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Headers;

namespace Captcha.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login(string returnUrl = "")
        {
            try
            {
                TempData["Alert"] = TempData["Alert"] ?? string.Empty;
                ViewBag.Username = string.Empty;
                ViewBag.Password = string.Empty;
                ViewBag.checkCapctcha = "False";
                ViewBag.ReturnUrl = !string.IsNullOrEmpty(returnUrl.Trim()) ? returnUrl.Trim() : string.Empty;
            }
            catch (Exception ex)
            {
                TempData["Alert"] = $"swAlert('Hata','{ex.Message}','warning')";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM login)
        {
            string returnUrl = "";
            try
            {
                RequestHeaders header = HttpContext.Request.GetTypedHeaders();
                Uri uriReferer = header.Referer;
                if (uriReferer.Query.Contains("returnUrl"))
                    returnUrl = uriReferer.Query.ToString().Replace("?returnUrl=", "").ToString();


                var checkCapctcha = ViewBag.CaptchaCheck;

                TempData["Alert"] = TempData["Alert"] ?? string.Empty;
                if (login.CaptchaCheck == false)
                {
                    TempData["Alert"] = "swal({title: 'Warning',text: 'Security code is invalid!',icon:'warning'}); ";
                    return View("Login");
                }
                string whereCondition = $"USERNAME = :username AND PASSWORD = :password";
                if (login.Username != "admin" || login.Password != "123")
                {
                    TempData["Alert"] = $"swAlert('Error','Incorrect Email / Password. Please try again.','warning')";
                    return View("Login");
                }
            }
            catch
            {

            }
            return View("Index");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
