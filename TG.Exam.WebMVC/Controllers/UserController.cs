using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Salestech.Exam.WebMVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            var users = TG.Exam.WebMVC.Models.User.GetAll();
            return View(users);
        }

        public ActionResult CreateUser()
        {
            return View();
        }
    }
}
