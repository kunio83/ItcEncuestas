//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConfirmacionApertura.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Padron
    {
        public int IdPadron { get; set; }
        [Required(ErrorMessage = "El Nombre es requerido")]
        public string AlumnoNombre { get; set; }
        [Required(ErrorMessage = "El DNI es requerido")]
        public int AlumnoDNI { get; set; }
        [Required(ErrorMessage = "El MAIL es requerido")]
        [DataType(DataType.EmailAddress)]
        public string AlumnoMail { get; set; }
        [Required(ErrorMessage = "El Celular es requerido")]
        public string AlumnoCel { get; set; }
        public string AlumnoHora { get; set; }
        public string AlumnoDia { get; set; }
        public int IdCapacitacion { get; set; }
        public int IdPlanCiudad { get; set; }
        [Required(ErrorMessage = "La Edad es requerido")]
        public int AlumnoEdad { get; set; }
        [Required(ErrorMessage = "El Domicilio es requerido")]
        public string AlumnoDomicilio { get; set; }
        public string AlumnoTel { get; set; }
        [Required(ErrorMessage = "El Barrio es requerido")]
        public string AlumnoBarrio { get; set; }
        public int IdEncuestaApertura { get; set; }
    
        public virtual Capacitacion Capacitacion { get; set; }
        public virtual EncuestaApertura EncuestaApertura { get; set; }
        public virtual PlanCiudad PlanCiudad { get; set; }
    }
}
