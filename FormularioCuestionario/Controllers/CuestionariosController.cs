using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FormularioCuestionario.Models;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Text.RegularExpressions;

namespace FormularioCuestionario.Controllers
{
    public class CuestionariosController : Controller
    {
        private CuestionariosEntities db = new CuestionariosEntities();

        // GET: Cuestionarios
        public ActionResult Fin()
        {
            return View();
        }

        // GET: Cuestionarios
        public ActionResult Index()
        {
            var cuestionarios = db.Cuestionarios.Include(c => c.Capacitaciones).Include(c => c.Ciudades);
            return View(cuestionarios.ToList());
        }

        // GET: Cuestionarios/Edit/5
        public ActionResult CuestionarioActual(int? dni)
        {
            Regex regEx = new Regex("^[0-9]");
            if (dni == 0 || dni == null || !regEx.IsMatch(dni.ToString()))
            {
                return View("Error");
            }
            Cuestionarios cuestionarios = db.Cuestionarios.FirstOrDefault(e => e.DNI == dni);
            if (cuestionarios == null)
            {
                return View("Error");
            }
            //Si ya fue realizado tiro error
            if (cuestionarios.Finalizado)
            {
                return View("Finalizado");
            }
            ViewBag.Capacitación = new SelectList(db.Capacitaciones, "IdCapacitación", "Descripción", cuestionarios.Capacitación);
            ViewBag.Ciudad = new SelectList(db.Ciudades, "IdCiudad", "NombreCiudad", cuestionarios.Ciudad);
            return View(cuestionarios);
        }

        // POST: Cuestionarios/Edit/5
        [HttpPost]
        public ActionResult CuestionarioActual([Bind(Include = "IdCuestionario,Nombre,Apellido,DNI,Ciudad,Capacitación,EMail,Teléfono,AsesoramientoInscripcion,SalónEquipamiento,Contenidos,IstructorConocimiento,InstructorClaridad,InstructorTrato,ConocimientoAdquirido,Utilidad,SatisfacciónGral,HariaOtro,Cual,Metodologia,Material,Duración,Predisposicion,Ejercicios,TratoAdministrativo,Sugerencias")] Cuestionarios cuestionarios)
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

                    string cual = String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}",chk_3, chk_4, chk_5, chk_6, chk_7, chk_8, chk_9, chk_10 );
                    if (String.IsNullOrEmpty(cuestionarios.EMail))
                        cuestionarios.EMail = "no_puso_mail@nada.com";
                    if (String.IsNullOrEmpty(cuestionarios.Sugerencias))
                        cuestionarios.Sugerencias = "sin comentarios";

                    cuestionarios.Finalizado = true;
                    cuestionarios.FechaHora = DateTime.Now;
                    cuestionarios.Cual = cual;
                    db.Entry(cuestionarios).State = EntityState.Modified;
                    db.SaveChanges();
                    //return View("Fin");
                    return RedirectToAction("Create", "Referidos",new { IdCuestionario = cuestionarios.IdCuestionario});
                }
                ViewBag.Capacitación = new SelectList(db.Capacitaciones, "IdCapacitación", "Descripción", cuestionarios.Capacitación);
                ViewBag.Ciudad = new SelectList(db.Ciudades, "IdCiudad", "NombreCiudad", cuestionarios.Ciudad);
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
                        err+=String.Format("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw new HttpException((int)HttpStatusCode.BadRequest, "Error; "+ err);
            }
            return View("Fin");
        }


        // GET: Cuestionarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuestionarios cuestionarios = db.Cuestionarios.Find(id);
            if (cuestionarios == null)
            {
                return HttpNotFound();
            }
            return View(cuestionarios);
        }

        // GET: Cuestionarios/Create
        public ActionResult Create()
        {
            ViewBag.Capacitación = new SelectList(db.Capacitaciones, "IdCapacitación", "Descripción");
            ViewBag.Ciudad = new SelectList(db.Ciudades, "IdCiudad", "NombreCiudad");
            return View();
        }

        // POST: Cuestionarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "IdCuestionario,Nombre,Apellido,DNI,Ciudad,Capacitación,EMail,Teléfono,AsesoramientoInscripcion,SalónEquipamiento,Contenidos,IstructorConocimiento,InstructorClaridad,InstructorTrato,ConocimientoAdquirido,Utilidad,SatisfacciónGral,HariaOtro,Cual,Metodologia,Material,Duración,Predisposicion,Ejercicios,TratoAdministrativo,Sugerencias")] Cuestionarios cuestionarios)
        {
            if (ModelState.IsValid)
            {
                cuestionarios.FechaHora = DateTime.Now;
                db.Cuestionarios.Add(cuestionarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Capacitación = new SelectList(db.Capacitaciones, "IdCapacitación", "Descripción", cuestionarios.Capacitación);
            ViewBag.Ciudad = new SelectList(db.Ciudades, "IdCiudad", "NombreCiudad", cuestionarios.Ciudad);
            return View(cuestionarios);
        }

        // GET: Cuestionarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuestionarios cuestionarios = db.Cuestionarios.Find(id);
            if (cuestionarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.Capacitación = new SelectList(db.Capacitaciones, "IdCapacitación", "Descripción", cuestionarios.Capacitación);
            ViewBag.Ciudad = new SelectList(db.Ciudades, "IdCiudad", "NombreCiudad", cuestionarios.Ciudad);
            return View(cuestionarios);
        }

        // POST: Cuestionarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCuestionario,Nombre,Apellido,DNI,Ciudad,Capacitación,EMail,Teléfono,AsesoramientoInscripcion,SalónEquipamiento,Contenidos,IstructorConocimiento,InstructorClaridad,InstructorTrato,ConocimientoAdquirido,Utilidad,SatisfacciónGral,HariaOtro,Cual,Metodologia,Material,Duración,Predisposicion,Ejercicios,TratoAdministrativo,Sugerencias")] Cuestionarios cuestionarios)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cuestionarios.FechaHora = DateTime.Now;
                    db.Entry(cuestionarios).State = EntityState.Modified;
                    
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    //return View("Fin");
                    return RedirectToAction("Create","Referidos");
                }
                ViewBag.Capacitación = new SelectList(db.Capacitaciones, "IdCapacitación", "Descripción", cuestionarios.Capacitación);
                ViewBag.Ciudad = new SelectList(db.Ciudades, "IdCiudad", "NombreCiudad", cuestionarios.Ciudad);
                return View("Index");
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
            return View(cuestionarios);
        }

        // GET: Cuestionarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuestionarios cuestionarios = db.Cuestionarios.Find(id);
            if (cuestionarios == null)
            {
                return HttpNotFound();
            }
            return View(cuestionarios);
        }

        // POST: Cuestionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cuestionarios cuestionarios = db.Cuestionarios.Find(id);
            db.Cuestionarios.Remove(cuestionarios);
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
