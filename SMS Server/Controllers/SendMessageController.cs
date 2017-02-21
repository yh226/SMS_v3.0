using SMS_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;


namespace SMS_Server.Controllers
{
    public class SendMessageController : Controller
    {
        // GET: SendMessage
        protected DBModels db_DBModels = new DBModels();
        protected CompanyContext db_Company = new CompanyContext();
        protected CustomerContext db_Customer = new CustomerContext();
        //protected CustomerContext db_Customer = new CustomerContext();
        CompanyInfor ci;



        public ActionResult Index()
        {
            List<Customer> allCust = new List<Customer>();
            using (CustomerContext dc = new CustomerContext())
            {
                allCust = dc.Customers.OrderBy(a => a.Id).ToList();
            }

            var getCompany = db_Company.Companies.ToList();
            SelectList list = new SelectList(getCompany, "Id", "CompanyName");
            ViewBag.DropDownValues = list;//new SelectList(new[] { "-" });

            return View(allCust);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDataMessage(string MessageTo, string MessageOut, string MessageText)
        {
            if (ModelState.IsValid)
            {
                db_DBModels.Open();
                string queryInsert = "INSERT INTO MessageOut(MessageTo, MessageText,MessageFrom) " +
                "VALUES('" + MessageTo + "','" + MessageText + "','" + MessageOut + "')";
                int i = db_DBModels.DataInsert(queryInsert);

                if (i > 0)
                {
                    ModelState.AddModelError("Success", "Save Success");
                }
                else
                {
                    ModelState.AddModelError("Error", "Save Error");
                }
                db_DBModels.Close();
            }
            return RedirectToAction("Index", "SendMessage");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDataMessageByGroup(string MessageText)
        {
            var CompanyList = db_Company.Companies.ToList();
            var CustomerList = db_Customer.Customers.ToList();
            string selectValueID = Request.Form["MydropDown"].ToString();
            List<string> MessageFromList = new List<string>();
            MessageFromList.Clear();
            for (int j = 0; j < CompanyList.Count(); j++)
            {
                if (CompanyList[j].Id.ToString().Equals(selectValueID))
                {
                    ci = new CompanyInfor(CompanyList[j].Contact, CompanyList[j].CompanyName);
                    break;
                }
            }

            for (int j2 = 0; j2 < CustomerList.Count(); j2++)
            {
                if (CustomerList[j2].GroupName.Equals(ci.getName()))
                {
                    MessageFromList.Add(CustomerList[j2].Mobile);
                    //MessageFromList.Add(CustomerList[j].Mobile);
                }

            }


            if (ModelState.IsValid)
            {
                db_DBModels.Open();
                foreach (string mobile in MessageFromList)
                {
                    string queryInsert = "INSERT INTO MessageOut(MessageTo, MessageText,MessageFrom) " +
                    "VALUES('" + mobile + "','" + MessageText + "','" + ci.getNumber() + "')";
                    int i = db_DBModels.DataInsert(queryInsert);
                }

                db_DBModels.Close();

            }

            return RedirectToAction("Index", "SendMessage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string[] ids, string MessageText, string MessageFrom)
        {
            //var CompanyList = db_Company.Companies.ToList();
            // var CustomerList = db_Customer.ToList();

            List<string> MessageFromList = new List<string>();
            MessageFromList.Clear();

            //string selectValueID = Request.Form["MydropDown"].ToString();


            //for (int j = 0; j < CompanyList.Count(); j++)
            //{
            //    if (CompanyList[j].Id.ToString().Equals(selectValueID))
            //    {
            //        ci = new CompanyInfor(CompanyList[j].Contact, CompanyList[j].CompanyName);
            //        break;
            //    }
            //}



            List<Customer> allCust = new List<Customer>();
            using (CustomerContext dc = new CustomerContext())
            {
                allCust = dc.Customers.OrderBy(a => a.Id).ToList();
            }

            // In the real application you can ids 

            if (ids != null)
            {

                ViewBag.Message = "You have selected following Customer ID(s):" + string.Join(", ", ids);
            }
            else
            {
                ViewBag.Message = "No record selected";
            }



            //////send
            if (ModelState.IsValid)
            {
                db_DBModels.Open();
                foreach (string mobile in ids)
                {
                    string queryInsert = "INSERT INTO MessageOut(MessageTo, MessageText,MessageFrom) " +
                     "VALUES('" + mobile + "','" + MessageText + "','" + MessageFrom + "')";
                    int i = db_DBModels.DataInsert(queryInsert);
                }
                db_DBModels.Close();
            }


            return RedirectToAction("Index", "SendMessage");

            /// return View(allCust);
        }



    }
}


