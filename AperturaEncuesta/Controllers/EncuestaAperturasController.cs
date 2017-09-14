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
    public class EncuestaAperturasController : Controller
    {
        private ModeloEncuestaAperturaContainer db = new ModeloEncuestaAperturaContainer();

        // GET: EncuestaAperturas
        public ActionResult Index()
        {
            return View(db.EncuestaApertura.ToList());
        }

        // GET: EncuestaAperturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EncuestaApertura encuestaApertura = db.EncuestaApertura.Find(id);
            if (encuestaApertura == null)
            {
                return HttpNotFound();
            }
            return View(encuestaApertura);
        }

        // GET: EncuestaAperturas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EncuestaAperturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEncuestaApertura,EncuestaAsesoramiento,EncuestaSugerencia")] EncuestaApertura encuestaApertura)
        {
            if (ModelState.IsValid)
            {
                db.EncuestaApertura.Add(encuestaApertura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(encuestaApertura);
        }

        // GET: EncuestaAperturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EncuestaApertura encuestaApertura = db.EncuestaApertura.Find(id);
            if (encuestaApertura == null)
            {
                return HttpNotFound();
            }
            return View(encuestaApertura);
        }

        // POST: EncuestaAperturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEncuestaApertura,EncuestaAsesoramiento,EncuestaSugerencia")]EncuestaApertura encuestaApertura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(encuestaApertura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(encuestaApertura);
        }

        // GET: EncuestaAperturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EncuestaApertura encuestaApertura = db.EncuestaApertura.Find(id);
            if (encuestaApertura == null)
            {
                return HttpNotFound();
            }
            return View(encuestaApertura);
        }

        // POST: EncuestaAperturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EncuestaApertura encuestaApertura = db.EncuestaApertura.Find(id);
            db.EncuestaApertura.Remove(encuestaApertura);
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
