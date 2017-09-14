using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AperturaEncuesta.Models;

namespace AperturaEncuesta.Controllers
{
    public class CapacitacionController : Controller
    {
        private ModeloEncuestaAperturaContainer db = new ModeloEncuestaAperturaContainer();

        // GET: Capacitacion
        public ActionResult Index()
        {
            return View(db.Capacitacion.ToList());
        }

        // GET: Capacitacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capacitacion capacitacion = db.Capacitacion.Find(id);
            if (capacitacion == null)
            {
                return HttpNotFound();
            }
            return View(capacitacion);
        }

        // GET: Capacitacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Capacitacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCapacitacion,Descripcion,Contenido")] Capacitacion capacitacion)
        {
            if (ModelState.IsValid)
            {
                db.Capacitacion.Add(capacitacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(capacitacion);
        }

        // GET: Capacitacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capacitacion capacitacion = db.Capacitacion.Find(id);
            if (capacitacion == null)
            {
                return HttpNotFound();
            }
            return View(capacitacion);
        }

        // POST: Capacitacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCapacitacion,Descripcion,Contenido")] Capacitacion capacitacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(capacitacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(capacitacion);
        }

        // GET: Capacitacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capacitacion capacitacion = db.Capacitacion.Find(id);
            if (capacitacion == null)
            {
                return HttpNotFound();
            }
            return View(capacitacion);
        }

        // POST: Capacitacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Capacitacion capacitacion = db.Capacitacion.Find(id);
            db.Capacitacion.Remove(capacitacion);
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
