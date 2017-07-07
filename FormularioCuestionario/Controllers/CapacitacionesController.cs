using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FormularioCuestionario.Models;

namespace FormularioCuestionario.Controllers
{
    public class CapacitacionesController : Controller
    {
        private CuestionariosEntities db = new CuestionariosEntities();

        // GET: Capacitaciones
        public ActionResult Index()
        {
            return View(db.Capacitaciones.ToList());
        }

        // GET: Capacitaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capacitaciones capacitaciones = db.Capacitaciones.Find(id);
            if (capacitaciones == null)
            {
                return HttpNotFound();
            }
            return View(capacitaciones);
        }

        // GET: Capacitaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Capacitaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCapacitación,Descripción")] Capacitaciones capacitaciones)
        {
            if (ModelState.IsValid)
            {
                db.Capacitaciones.Add(capacitaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(capacitaciones);
        }

        // GET: Capacitaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capacitaciones capacitaciones = db.Capacitaciones.Find(id);
            if (capacitaciones == null)
            {
                return HttpNotFound();
            }
            return View(capacitaciones);
        }

        // POST: Capacitaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCapacitación,Descripción")] Capacitaciones capacitaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(capacitaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(capacitaciones);
        }

        // GET: Capacitaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Capacitaciones capacitaciones = db.Capacitaciones.Find(id);
            if (capacitaciones == null)
            {
                return HttpNotFound();
            }
            return View(capacitaciones);
        }

        // POST: Capacitaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Capacitaciones capacitaciones = db.Capacitaciones.Find(id);
            db.Capacitaciones.Remove(capacitaciones);
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
