﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AperturaEncuesta.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ModeloEncuestaAperturaContainer : DbContext
    {
        public ModeloEncuestaAperturaContainer()
            : base("name=ModeloEncuestaAperturaContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Capacitacion> Capacitacion { get; set; }
        public virtual DbSet<Ciudad> Ciudad { get; set; }
        public virtual DbSet<Plan> Plan { get; set; }
        public virtual DbSet<PlanCiudad> PlanCiudad { get; set; }
        public virtual DbSet<Padron> Padron { get; set; }
        public virtual DbSet<EncuestaApertura> EncuestaApertura { get; set; }
    }
}
