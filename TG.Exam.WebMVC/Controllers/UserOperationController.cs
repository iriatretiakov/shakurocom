using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TG.Exam.WebMVC.Models;

namespace Salestech.Exam.WebMVC.Controllers
{
    public class UserOperationController : ApiController
    {
        // GET: api/UserOperation
        public IEnumerable<User> GetAllUsers()
        {
            return TG.Exam.WebMVC.Models.User.GetAll();
        }
    }
}
