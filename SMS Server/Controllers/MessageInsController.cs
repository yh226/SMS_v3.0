using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SMS_Server.Models;

namespace SMS_Server.Controllers
{
    public class MessageInsController : Controller
    {
        private MessageInContext db = new MessageInContext();

        // GET: MessageIns
        public ActionResult Index()
        {
            return View(db.MessageIns.ToList());
        }

        // GET: MessageIns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageIn messageIn = db.MessageIns.Find(id);
            if (messageIn == null)
            {
                return HttpNotFound();
            }
            return View(messageIn);
        }

        // GET: MessageIns/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MessageIns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SendTime,ReceiveTime,MessageFrom,MessageTo,SMSC,MessageText,MessageType,MessageParts,MessagePDU,Gateway,UserId")] MessageIn messageIn)
        {
            if (ModelState.IsValid)
            {
                db.MessageIns.Add(messageIn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(messageIn);
        }

        // GET: MessageIns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageIn messageIn = db.MessageIns.Find(id);
            if (messageIn == null)
            {
                return HttpNotFound();
            }
            return View(messageIn);
        }

        // POST: MessageIns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SendTime,ReceiveTime,MessageFrom,MessageTo,SMSC,MessageText,MessageType,MessageParts,MessagePDU,Gateway,UserId")] MessageIn messageIn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(messageIn).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(messageIn);
        }

        // GET: MessageIns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageIn messageIn = db.MessageIns.Find(id);
            if (messageIn == null)
            {
                return HttpNotFound();
            }
            return View(messageIn);
        }

        // POST: MessageIns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MessageIn messageIn = db.MessageIns.Find(id);
            db.MessageIns.Remove(messageIn);
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
    }
}
