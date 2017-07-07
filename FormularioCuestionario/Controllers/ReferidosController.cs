using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FormularioCuestionario.Models;
using System.Data.Entity.Validation;

namespace FormularioCuestionario.Controllers
{
    public class ReferidosController : Controller
    {
        private CuestionariosEntities db = new CuestionariosEntities();

        // GET: Referidos
        public ActionResult Index(int? IdCuestionario)
        {
            ICollection<Referidos> referidos;
            if (IdCuestionario != 0 && IdCuestionario != null)
                referidos = db.Referidos.Include(r => r.Cuestionarios).Where(e => e.IdCuestionario == IdCuestionario).ToList();
            else
                referidos = db.Referidos.Include(r => r.Cuestionarios).ToList();
            
                ViewBag.IdCuestionarioActual = IdCuestionario;

            return View(referidos);
        }

        public PartialViewResult List()
        {
            var referidos = db.Referidos.Include(r => r.Cuestionarios);
            return PartialView(referidos.ToList());
        }

        // GET: Referidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referidos referidos = db.Referidos.Find(id);
            if (referidos == null)
            {
                return HttpNotFound();
            }
            return View(referidos);
        }

        // GET: Referidos/Create
        //public ActionResult Create([Bind(Include = "IdCuestionario,Nombre,Apellido,DNI,Ciudad,Capacitación,EMail,Teléfono,AsesoramientoInscripcion,SalónEquipamiento,Contenidos,IstructorConocimiento,InstructorClaridad,InstructorTrato,ConocimientoAdquirido,Utilidad,SatisfacciónGral,HariaOtro,Cual,Metodologia,Material,Duración,Predisposicion,Ejercicios,TratoAdministrativo,Sugerencias")] Cuestionarios cuestionario)
        //{
        //    ModelState
        //    ViewBag.IdCuestionario = new SelectList(db.Cuestionarios, "IdCuestionario", "Nombre");
        //    return View();
        //}

        public ActionResult Create(int? IdCuestionario)
        {
            ViewBag.IdCuestionario = new SelectList(db.Cuestionarios, "IdCuestionario", "Nombre");
            ViewData["IdCuestionarioActual"] = IdCuestionario;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdReferido,Nombre,Apellido,Teléfono,IdCuestionario")] Referidos referidos)
        {
            try
            {
                Cuestionarios cuestionarioActual;
                if (ModelState.IsValid)
                {
                    int idCuestionario = Convert.ToInt32(Request.Form["IdCuestionarioActual"]);
                    db.Referidos.Add(referidos);
                    db.SaveChanges();
                    //Traigo el num del dni del cuestionario actual  //Cuestionario --http://localhost:52101/Cuestionarios/CuestionarioActual?dni=555
                    cuestionarioActual = db.Cuestionarios.FirstOrDefault(e => e.IdCuestionario == referidos.IdCuestionario);
                    ViewBag.Capacitación = new SelectList(db.Capacitaciones, "IdCapacitación", "Descripción", cuestionarioActual.Capacitación);
                    ViewBag.IdCuestionario = cuestionarioActual.IdCuestionario;// = new SelectList(db.Cuestionarios, "IdCuestionario", "Nombre");
                    ViewBag.Ciudad = new SelectList(db.Ciudades, "IdCiudad", "NombreCiudad", cuestionarioActual.Ciudad);
                    ViewData["IdCuestionarioActual"] = cuestionarioActual.IdCuestionario;
                    return RedirectToAction("Index", new { IdCuestionario = idCuestionario });
                }

            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }

            ViewBag.IdCuestionario = new SelectList(db.Cuestionarios, "IdCuestionario", "Nombre", referidos.IdCuestionario);
            return View(referidos);
        }

        // GET: Referidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referidos referidos = db.Referidos.Find(id);
            if (referidos == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCuestionario = new SelectList(db.Cuestionarios, "IdCuestionario", "Nombre", referidos.IdCuestionario);
            return View(referidos);
        }

        // POST: Referidos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdReferido,Nombre,Apellido,Teléfono,IdCuestionario")] Referidos referidos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(referidos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCuestionario = new SelectList(db.Cuestionarios, "IdCuestionario", "Nombre", referidos.IdCuestionario);
            return View(referidos);
        }

        //// GET: Referidos/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Referidos referidos = db.Referidos.Find(id);
        //    if (referidos == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(referidos);
        //}

        // Cambio este metodo para eliminar sin confirmación
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referidos referidos = db.Referidos.Find(id);
            if (referidos == null)
            {
                return HttpNotFound();
            }
            db.Referidos.Remove(referidos);
            db.SaveChanges();
            //return RedirectToAction("Index");
            Cuestionarios cuestionarioActual = db.Cuestionarios.FirstOrDefault(e => e.IdCuestionario == referidos.IdCuestionario);
            ViewBag.Capacitación = new SelectList(db.Capacitaciones, "IdCapacitación", "Descripción", cuestionarioActual.Capacitación);
            ViewBag.Ciudad = new SelectList(db.Ciudades, "IdCiudad", "NombreCiudad", cuestionarioActual.Ciudad);
            ViewBag.IdCuestionario = cuestionarioActual.IdCuestionario;
            //return View("../Cuestionarios/CuestionarioActual", cuestionarioActual);
            return RedirectToAction("Index",new { IdCuestionario = cuestionarioActual.IdCuestionario});
        }

        // POST: Referidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Referidos referidos = db.Referidos.Find(id);
            db.Referidos.Remove(referidos);
            db.SaveChanges();
            //return RedirectToAction("Index");
            Cuestionarios cuestionarioActual = db.Cuestionarios.FirstOrDefault(e => e.IdCuestionario == referidos.IdCuestionario);
            ViewBag.Capacitación = new SelectList(db.Capacitaciones, "IdCapacitación", "Descripción", cuestionarioActual.Capacitación);
            ViewBag.Ciudad = new SelectList(db.Ciudades, "IdCiudad", "NombreCiudad", cuestionarioActual.Ciudad);
            //return View("../Cuestionarios/CuestionarioActual", cuestionarioActual);
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
