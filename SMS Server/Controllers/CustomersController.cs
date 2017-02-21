using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SMS_Server.Models;
using System.Text;
using Newtonsoft.Json;
using System.Collections;

namespace SMS_Server.Controllers
{
    public class CustomersController : Controller
    {
        private CustomerContext db = new CustomerContext();
        protected CompanyContext db_Company = new CompanyContext();
        protected DBModels db_DBModels = new DBModels();
        CompanyInfor ci;


        // GET: Customers
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
            

          

            //return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Mobile,GroupName,isSelect")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.isSelect = false;
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Mobile,GroupName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        ////////////////////////////////////////////////








        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Send(string MessageText)
        //{
        //    var CompanyList = db_Company.Companies.ToList();
        //    var CustomerList = db.Customers.ToList();

        //    string selectValueID = Request.Form["MydropDown"].ToString();
        //    List<string> MessageFromList = new List<string>();
        //    MessageFromList.Clear();
        //    for (int j = 0; j < CompanyList.Count(); j++)
        //    {
        //        if (CompanyList[j].Id.ToString().Equals(selectValueID))
        //        {
        //            ci = new CompanyInfor(CompanyList[j].Contact, CompanyList[j].CompanyName);
        //            break;
        //        }
        //    }



        //    // ViewBag.sele


        //    //for (int j2 = 0; j2 < CustomerList.Count(); j2++)
        //    //{
        //    //    for (int j = 0; j < collection.Count; j++)
        //    //    {
        //    //        if (CustomerList[j2].Id.ToString() == collection.Keys[j].ToString())
        //    //        {
        //    //            MessageFromList.Add(CustomerList[j2].Mobile);
        //    //            //MessageFromList.Add(CustomerList[j].Mobile);
        //    //        }
        //    //    }
        //    //}

        //    //for (int j2 = 0; j2 < CustomerList.Count(); j2++)
        //    //{

        //    //        if (CustomerList[j2].Id.ToString() == )
        //    //        {
        //    //            MessageFromList.Add(CustomerList[j2].Mobile);
        //    //            //MessageFromList.Add(CustomerList[j].Mobile);
        //    //        }

        //    //}
        //    //for (int j = 0; j < CustomerList.Count(); j++)
        //    //{

        //    //    if (CustomerList[j].isSelect==true )
        //    //    {
        //    //        MessageFromList.Add(CustomerList[j].Mobile);
        //    //        //MessageFromList.Add(CustomerList[j].Mobile);
        //    //    }

        //    //}


        //    if (ModelState.IsValid)
        //    {
        //        db_DBModels.Open();
        //        //foreach (string mobile in MessageFromList)
        //        //{
        //        string queryInsert = "INSERT INTO MessageOut(MessageTo, MessageText,MessageFrom) " +
        //        "VALUES('" + "2222" + "','" + CustomerList[0].isSelect + "','" + ci.getNumber() + "')";
        //        int i = db_DBModels.DataInsert(queryInsert);
        //        //}

        //        db_DBModels.Close();

        //    }


        //    return RedirectToAction("Index", "Customers");
        //}
        /////////////////////////////
   


        //////////////////////////////////////////


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string[] ids,string MessageText)
        {
            var CompanyList = db_Company.Companies.ToList();
            var CustomerList = db.Customers.ToList();

            List<string> MessageFromList = new List<string>();
            MessageFromList.Clear();

            string selectValueID = Request.Form["MydropDown"].ToString();


            for (int j = 0; j < CompanyList.Count(); j++)
            {
                if (CompanyList[j].Id.ToString().Equals(selectValueID))
                {
                    ci = new CompanyInfor(CompanyList[j].Contact, CompanyList[j].CompanyName);
                    break;
                }
            }



            List<Customer> allCust = new List<Customer>();
            using (CustomerContext dc = new CustomerContext())
            {
                allCust = dc.Customers.OrderBy(a => a.Id).ToList();
            }

            // In the real application you can ids 

            if (ids != null)
            {

                ViewBag.Message =  "You have selected following Customer ID(s):" + string.Join(", ", ids);
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
                     "VALUES('" + mobile + "','" + MessageText + "','" + ci.getNumber() + "')";
                    int i = db_DBModels.DataInsert(queryInsert);
                }
                db_DBModels.Close();
            }


            return RedirectToAction("Index", "Customers");

            /// return View(allCust);
        }
    }
}
