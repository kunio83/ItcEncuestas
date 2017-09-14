using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConfirmacionApertura.Models;
using System.Text.RegularExpressions;
using System.Data.Entity.Validation;

namespace ConfirmacionApertura.Controllers
{
    public class PadronsController : Controller
    {
        private EncuestaAperturaEntities db = new EncuestaAperturaEntities();

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
            PlanCiudad planCiudad = db.PlanCiudad.FirstOrDefault(e => e.IdPlanCiudad == padronActual.IdPlanCiudad);
            Ciudad ciudad = db.Ciudad.FirstOrDefault(e => e.IdCiudad == planCiudad.IdCiudad);
            
            //por aca resuelvo la fecha de inicio del alumno
            DateTime fechaInicio = obtenerFechaDeInicioAlumno(ciudad.FechaInicio, padronActual.AlumnoDia);
            ViewBag.FechaInicioAlumno = this.obtenerDiaSemanaByNum(fechaInicio.Day) + " " + fechaInicio.ToString("dd/MM");
            
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

            ViewBag.AlumnoDatosPadron = datosPadronActual;

            if (encuestaApertura.Finalizado)
            {
                return View("DatosPadronFinalizado", datosPadronActual);
            }

            return View("DatosPadron",datosPadronActual);
        }

        // POST: Cuestionarios/Edit/5
        [HttpPost]
        public ActionResult DatosPadronActual(AlumnoDatosPadron alumnoDatosPadron)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Bindeo los datos
                    alumnoDatosPadron = bindearDatos(alumnoDatosPadron);


                    if (String.IsNullOrEmpty(alumnoDatosPadron.Padron.AlumnoMail))
                        alumnoDatosPadron.Padron.AlumnoMail = "no_puso_mail@nada.com";
                    if (String.IsNullOrEmpty(alumnoDatosPadron.EncuestaApertura.EncuestaSugerencia))
                        alumnoDatosPadron.EncuestaApertura.EncuestaSugerencia = "sin comentarios";

                    alumnoDatosPadron.EncuestaApertura.Finalizado = true;
                    alumnoDatosPadron.EncuestaApertura.FechaHora = DateTime.Now;
                    

                    db.Entry(alumnoDatosPadron.Padron).State = EntityState.Modified;
                    db.Entry(alumnoDatosPadron.EncuestaApertura).State = EntityState.Modified;

                    db.SaveChanges();
                    return View("Fin");
                }
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

        private AlumnoDatosPadron bindearDatos(AlumnoDatosPadron alumnoDatosPadron)
        {
            Padron padron = db.Padron.FirstOrDefault(e => e.IdPadron == alumnoDatosPadron.Padron.IdPadron);
            padron.AlumnoBarrio = alumnoDatosPadron.Padron.AlumnoBarrio;
            padron.AlumnoCel = alumnoDatosPadron.Padron.AlumnoCel;
            padron.AlumnoDomicilio = alumnoDatosPadron.Padron.AlumnoDomicilio;
            padron.AlumnoEdad = alumnoDatosPadron.Padron.AlumnoEdad;
            padron.AlumnoMail = alumnoDatosPadron.Padron.AlumnoMail;
            padron.AlumnoNombre = alumnoDatosPadron.Padron.AlumnoNombre;
            padron.AlumnoTel = alumnoDatosPadron.Padron.AlumnoTel;

            EncuestaApertura encuesta = db.EncuestaApertura.FirstOrDefault(e => e.IdEncuestaApertura == alumnoDatosPadron.Padron.IdEncuestaApertura);
            encuesta.FechaHora = alumnoDatosPadron.EncuestaApertura.FechaHora;
            encuesta.Finalizado = alumnoDatosPadron.EncuestaApertura.Finalizado;
            encuesta.EncuestaAsesoramiento = alumnoDatosPadron.EncuestaApertura.EncuestaAsesoramiento;
            encuesta.EncuestaSugerencia = alumnoDatosPadron.EncuestaApertura.EncuestaSugerencia;

            alumnoDatosPadron.Padron = padron;
            alumnoDatosPadron.EncuestaApertura = encuesta;

            return alumnoDatosPadron;

        }

        // GET: Padrons
        public ActionResult Index()
        {
            var padron = db.Padron.Include(p => p.Capacitacion).Include(p => p.EncuestaApertura).Include(p => p.PlanCiudad);
            return View(padron.ToList());
        }

        // GET: Padrons/Details/5
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

        // GET: Padrons/Create
        public ActionResult Create()
        {
            ViewBag.IdCapacitacion = new SelectList(db.Capacitacion, "IdCapacitacion", "Descripcion");
            ViewBag.IdEncuestaApertura = new SelectList(db.EncuestaApertura, "IdEncuestaApertura", "EncuestaSugerencia");
            ViewBag.IdPlanCiudad = new SelectList(db.PlanCiudad, "IdPlanCiudad", "MatriculaValor");
            return View();
        }

        // POST: Padrons/Create
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

            ViewBag.IdCapacitacion = new SelectList(db.Capacitacion, "IdCapacitacion", "Descripcion", padron.IdCapacitacion);
            ViewBag.IdEncuestaApertura = new SelectList(db.EncuestaApertura, "IdEncuestaApertura", "EncuestaSugerencia", padron.IdEncuestaApertura);
            ViewBag.IdPlanCiudad = new SelectList(db.PlanCiudad, "IdPlanCiudad", "MatriculaValor", padron.IdPlanCiudad);
            return View(padron);
        }

        // GET: Padrons/Edit/5
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
            ViewBag.IdCapacitacion = new SelectList(db.Capacitacion, "IdCapacitacion", "Descripcion", padron.IdCapacitacion);
            ViewBag.IdEncuestaApertura = new SelectList(db.EncuestaApertura, "IdEncuestaApertura", "EncuestaSugerencia", padron.IdEncuestaApertura);
            ViewBag.IdPlanCiudad = new SelectList(db.PlanCiudad, "IdPlanCiudad", "MatriculaValor", padron.IdPlanCiudad);
            return View(padron);
        }

        // POST: Padrons/Edit/5
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
            ViewBag.IdCapacitacion = new SelectList(db.Capacitacion, "IdCapacitacion", "Descripcion", padron.IdCapacitacion);
            ViewBag.IdEncuestaApertura = new SelectList(db.EncuestaApertura, "IdEncuestaApertura", "EncuestaSugerencia", padron.IdEncuestaApertura);
            ViewBag.IdPlanCiudad = new SelectList(db.PlanCiudad, "IdPlanCiudad", "MatriculaValor", padron.IdPlanCiudad);
            return View(padron);
        }

        // GET: Padrons/Delete/5
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

        // POST: Padrons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Padron padron = db.Padron.Find(id);
            db.Padron.Remove(padron);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public DateTime obtenerFechaDeInicioAlumno(DateTime fechaInicioCurso, String diaSemana)
        {
            int numDiaSemana = obtenerNumDiaSemana(diaSemana);

            while (fechaInicioCurso.Day != numDiaSemana)
            {
                fechaInicioCurso = fechaInicioCurso.AddDays(1);
            }

            return fechaInicioCurso;
        }

        private int obtenerNumDiaSemana(string diaSenana)
        {
            switch (diaSenana.Trim().ToUpper())
            {
                case "DOMINGO": return 1;
                case "LUNES": return 2;
                case "MARTES": return 3;
                case "MIÉRCOLES": return 4;
                case "JUEVES": return 5;
                case "VIERNES": return 6;
                case "SABADO": return 7;
                default: return 0;
            }
        }

        private string obtenerDiaSemanaByNum(int diaSenana)
        {
            switch (diaSenana)
            {
                case 1: return "Domingo";
                case 2: return "Lunes";
                case 3: return "Martes";
                case 4: return "Miércoles";
                case 5: return "Jueves";
                case 6: return "Viernes";
                case 7: return "Sabado";
                default: return "";
            }
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
