using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AperturaEncuesta.Models;
using System.Text.RegularExpressions;
using System.Data.Entity.Validation;

namespace AperturaEncuesta.Controllers
{
    public class PadronController : Controller
    {
        private ModeloEncuestaAperturaContainer db = new ModeloEncuestaAperturaContainer();

        // GET: Cuestionarios/Edit/5
        public ActionResult DatosPadronActual(int? dni)
        {
            Regex regEx = new Regex("^[0-9]");
            if (dni == 0 || dni == null || !regEx.IsMatch(dni.ToString()))
            {
                return View("Error");
            }
            Padron padronActual = db.Padron.FirstOrDefault(e => e.AlumnoDNI == dni);
            if (padronActual == null)
            {
                return View("Error");
            }
            EncuestaApertura encuestaApertura = db.EncuestaApertura.FirstOrDefault(e => e.IdEncuestaApertura == padronActual.IdEncuestaApertura);
            //Si ya fue realizado tiro error
            if (encuestaApertura.Finalizado)//ahora
            {
                //Aca deberia  boquear algunos datos
                    //    return View("Finalizado");
            }

            PlanCiudad planCiudad = db.PlanCiudad.FirstOrDefault(e => e.IdPlanCiudad == padronActual.IdPlanCiudad);
            Ciudad ciudad = db.Ciudad.FirstOrDefault(e => e.IdCiudad == planCiudad.IdCiudad);
            Plan plan = db.Plan.FirstOrDefault(e => e.IdPlan == planCiudad.IdPlan);
            Capacitacion capacitacion = db.Capacitacion.FirstOrDefault(e => e.IdCapacitacion == padronActual.IdCapacitacion);
            
            AlumnoDatosPadron datosPadronActual = new AlumnoDatosPadron()
            {
                EncuestaApertura = encuestaApertura,
                PlanCiudad = planCiudad,
                Plan = plan,
                Ciudad = ciudad,
                Capacitacion = capacitacion,
                Padron = padronActual
            };

            ViewBag.AlumnODatosPadron = datosPadronActual;
            //ViewBag.Ciudad = new SelectList(db.Ciudades, "IdCiudad", "NombreCiudad", cuestionarios.Ciudad);
            return View("DatosPadron");
        }

        // POST: Cuestionarios/Edit/5
        [HttpPost]
        public ActionResult DatosPadronActual([Bind(Include = "IdCuestionario,Nombre,Apellido,DNI,Ciudad,Capacitación,EMail,Teléfono,AsesoramientoInscripcion,SalónEquipamiento,Contenidos,IstructorConocimiento,InstructorClaridad,InstructorTrato,ConocimientoAdquirido,Utilidad,SatisfacciónGral,HariaOtro,Cual,Metodologia,Material,Duración,Predisposicion,Ejercicios,TratoAdministrativo,Sugerencias,FechaHora,Finalizado,Edad,Ocupacion")] Padron cuestionarios)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Obtengo los datos para cual que es string
                    string chk_3 = (((string)Request.Form["chk_3"]).Contains("true")) ? "3" : "";
                    string chk_4 = (((string)Request.Form["chk_4"]).Contains("true")) ? "4" : "";
                    string chk_5 = (((string)Request.Form["chk_5"]).Contains("true")) ? "5" : "";
                    string chk_6 = (((string)Request.Form["chk_6"]).Contains("true")) ? "6" : "";
                    string chk_7 = (((string)Request.Form["chk_7"]).Contains("true")) ? "7" : "";
                    string chk_8 = (((string)Request.Form["chk_8"]).Contains("true")) ? "8" : "";
                    string chk_9 = (((string)Request.Form["chk_9"]).Contains("true")) ? "9" : "";
                    string chk_10 = (((string)Request.Form["chk_10"]).Contains("true")) ? "10" : "";

                    string cual = String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}", chk_3, chk_4, chk_5, chk_6, chk_7, chk_8, chk_9, chk_10);
                    //if (String.IsNullOrEmpty(cuestionarios.EMail))
                    //    cuestionarios.EMail = "no_puso_mail@nada.com";
                    //if (String.IsNullOrEmpty(cuestionarios.Sugerencias))
                    //    cuestionarios.Sugerencias = "sin comentarios";

                    //cuestionarios.Finalizado = true;
                    //cuestionarios.FechaHora = DateTime.Now;
                    //cuestionarios.Cual = cual;
                    db.Entry(cuestionarios).State = EntityState.Modified;
                    db.SaveChanges();
                    //return View("Fin");
                    //return RedirectToAction("Create", "Referidos", new { IdCuestionario = cuestionarios.IdCuestionario });
                }
                //ViewBag.Capacitación = new SelectList(db.Capacitaciones, "IdCapacitación", "Descripción", cuestionarios.Capacitación);
                //ViewBag.Ciudad = new SelectList(db.Ciudades, "IdCiudad", "NombreCiudad", cuestionarios.Ciudad);
                return View("Index");
            }
            catch (DbEntityValidationException e)
            {
                string err = "";
                foreach (var eve in e.EntityValidationErrors)
                {

                    err += String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        err += String.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw new HttpException((int)HttpStatusCode.BadRequest, "Error; " + err);
            }
            //return View("Fin");
        }



        // GET: Padron
        public ActionResult Index()
        {
            return View(db.Padron.ToList());
        }

        // GET: Padron/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Padron padron = db.Padron.Find(id);
            if (padron == null)
            {
                return HttpNotFound();
            }
            return View(padron);
        }

        // GET: Padron/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Padron/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPadron,AlumnoNombre,AlumnoDNI,AlumnoMail,AlumnoCel,AlumnoHora,AlumnoDia,IdCapacitacion,IdPlanCiudad,AlumnoEdad,AlumnoDomicilio,AlumnoTel,AlumnoBarrio,IdEncuestaApertura")] Padron padron)
        {
            if (ModelState.IsValid)
            {
                db.Padron.Add(padron);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(padron);
        }

        // GET: Padron/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Padron padron = db.Padron.Find(id);
            if (padron == null)
            {
                return HttpNotFound();
            }
            return View(padron);
        }

        // POST: Padron/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPadron,AlumnoNombre,AlumnoDNI,AlumnoMail,AlumnoCel,AlumnoHora,AlumnoDia,IdCapacitacion,IdPlanCiudad,AlumnoEdad,AlumnoDomicilio,AlumnoTel,AlumnoBarrio,IdEncuestaApertura")] Padron padron)
        {
            if (ModelState.IsValid)
            {
                db.Entry(padron).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(padron);
        }

        // GET: Padron/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Padron padron = db.Padron.Find(id);
            if (padron == null)
            {
                return HttpNotFound();
            }
            return View(padron);
        }

        // POST: Padron/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Padron padron = db.Padron.Find(id);
            db.Padron.Remove(padron);
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
