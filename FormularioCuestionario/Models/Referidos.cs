//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FormularioCuestionario.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Referidos
    {
        public int IdReferido { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Teléfono { get; set; }
        public int IdCuestionario { get; set; }
    
        public virtual Cuestionarios Cuestionarios { get; set; }
    }
}
