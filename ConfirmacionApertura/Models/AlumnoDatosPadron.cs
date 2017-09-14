using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConfirmacionApertura.Models
{
    public class AlumnoDatosPadron
    {
        public int IdAlumnoDatosPadron { get; set; }
        public Padron Padron { get; set; }
        public Capacitacion Capacitacion { get; set; }
        public Ciudad Ciudad { get; set; }
        public Plan Plan { get; set; }
        public PlanCiudad PlanCiudad { get; set; }
        public EncuestaApertura EncuestaApertura { get; set; }

    }
}