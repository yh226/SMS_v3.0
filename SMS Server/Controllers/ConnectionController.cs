using SMS_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SMS_Server.Controllers
{
    public class ConnectionController : Controller
    {
        // GET: Connection
        public DBModels DB = new DBModels();
        public ActionResult Index()
        {
            bool i = DB.Open();
            if (i == true)
            {
                return Content(i.ToString());
            }
            else
            {
                return Content(i.ToString());
            }


            return View();
        }
    }
}