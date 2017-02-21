using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SMS_Server;
using SMS_Server.Models;

namespace SMS_Server.Controllers
{
    public class CustomerController : Controller
    {
        private CustomerContext db = new CustomerContext();
        protected CompanyContext db_Company = new CompanyContext();
        protected DBModels db_DBModels = new DBModels();
        CompanyInfor ci;

        // GET: Customer
        public ActionResult Index()
        {
            var getCompany = db_Company.Companies.ToList();
            SelectList list = new SelectList(getCompany, "Id", "CompanyName");

            ViewBag.DropDownValues = list;//new SelectList(new[] { "-" });

            return View(db.Customers.ToList());
        }

        // GET: Customer/Details/5
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

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Mobile,GroupName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customer/Edit/5
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

        // POST: Customer/Edit/5
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

        // GET: Customer/Delete/5
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

        // POST: Customer/Delete/5
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Send(string MessageText, FormCollection collection, bool nullableValue)
        {
            var CompanyList = db_Company.Companies.ToList();
            var CustomerList = db.Customers.ToList();

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


            //for (int j2 = 0; j2 < CustomerList.Count(); j2++)
            //{
            //    for (int j = 0; j < collection.Count; j++)
            //    {
            //        if (CustomerList[j2].Id.ToString() == collection.Keys[j].ToString())
            //        {
            //            MessageFromList.Add(CustomerList[j2].Mobile);
            //            //MessageFromList.Add(CustomerList[j].Mobile);
            //        }
            //    }
            //}

            //for (int j2 = 0; j2 < CustomerList.Count(); j2++)
            //{
              
            //        if (CustomerList[j2].Id.ToString() == )
            //        {
            //            MessageFromList.Add(CustomerList[j2].Mobile);
            //            //MessageFromList.Add(CustomerList[j].Mobile);
            //        }
             
            //}

            if (ModelState.IsValid)
            {
                db_DBModels.Open();
                //foreach (string mobile in MessageFromList)
                //{
                    string queryInsert = "INSERT INTO MessageOut(MessageTo, MessageText,MessageFrom) " +
                    "VALUES('" + "2222"+ "','" + nullableValue + "','" + ci.getNumber() + "')";
                    int i = db_DBModels.DataInsert(queryInsert);
                 //}
               
                db_DBModels.Close();

            }


            return RedirectToAction("Index", "Customer");
        }
    }
    
}
