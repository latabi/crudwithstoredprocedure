﻿   using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrudUsingSpMVC5.Models;

namespace CrudUsingSpMVC5.Controllers
{
    public class CustomerVMsController : Controller
    {
        private CrudUsingSpMVC5Context db = new CrudUsingSpMVC5Context();
        # region ---Dropdowncascading---
        /// <summary>
        /// dropdown cacading method
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public ActionResult FillCity(int state)
        {
            var cities = db.Cities.Where(c => c.StateId == state);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region ---indexview---
        public ActionResult Index()
        {
            return View(db.CustomerVMs.ToList());
        }
        #endregion
        #region  --details---

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerVM customerVM = db.CustomerVMs.Find(id);
            if (customerVM == null)
            {
                return HttpNotFound();
            }
            return View(customerVM);
        }
        #endregion
        #region ---create---
        // GET: CustomerVMs/Create
        public ActionResult Create()
        {
            ViewBag.StateList = db.States;
            var model = new CustomerVM();
            return View(model);
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,CurrentAddress,State,City")] CustomerVM customerVM)
        {
            if (ModelState.IsValid)
            {
                db.CustomerVMs.Add(customerVM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StateList = db.States;
            return View(customerVM);
        }

        // GET: CustomerVMs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerVM customerVM = db.CustomerVMs.Find(id);
            if (customerVM == null)
            {
                return HttpNotFound();
            }
            return View(customerVM);
        }

        // POST: CustomerVMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,CurrentAddress,State,City")] CustomerVM customerVM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerVM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customerVM);
        }

        // GET: CustomerVMs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerVM customerVM = db.CustomerVMs.Find(id);
            if (customerVM == null)
            {
                return HttpNotFound();
            }
            return View(customerVM);
        }

        // POST: CustomerVMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomerVM customerVM = db.CustomerVMs.Find(id);
            db.CustomerVMs.Remove(customerVM);
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
