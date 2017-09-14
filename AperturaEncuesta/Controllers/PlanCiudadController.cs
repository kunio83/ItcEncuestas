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
    public class PlanCiudadController : Controller
    {
        private ModeloEncuestaAperturaContainer db = new ModeloEncuestaAperturaContainer();

        // GET: PlanCiudad
        public ActionResult Index()
        {
            return View(db.PlanCiudad.ToList());
        }

        // GET: PlanCiudad/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanCiudad planCiudad = db.PlanCiudad.Find(id);
            if (planCiudad == null)
            {
                return HttpNotFound();
            }
            return View(planCiudad);
        }

        // GET: PlanCiudad/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlanCiudad/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPlanCiudad,IdCiudad,IdPlan,MatriculaValor,CuotaCantidad,CuotaValor,CuotaValorDescuento,CertificadoValor")] PlanCiudad planCiudad)
        {
            if (ModelState.IsValid)
            {
                db.PlanCiudad.Add(planCiudad);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(planCiudad);
        }

        // GET: PlanCiudad/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanCiudad planCiudad = db.PlanCiudad.Find(id);
            if (planCiudad == null)
            {
                return HttpNotFound();
            }
            return View(planCiudad);
        }

        // POST: PlanCiudad/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPlanCiudad,IdCiudad,IdPlan,MatriculaValor,CuotaCantidad,CuotaValor,CuotaValorDescuento,CertificadoValor")] PlanCiudad planCiudad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planCiudad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(planCiudad);
        }

        // GET: PlanCiudad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlanCiudad planCiudad = db.PlanCiudad.Find(id);
            if (planCiudad == null)
            {
                return HttpNotFound();
            }
            return View(planCiudad);
        }

        // POST: PlanCiudad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PlanCiudad planCiudad = db.PlanCiudad.Find(id);
            db.PlanCiudad.Remove(planCiudad);
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
