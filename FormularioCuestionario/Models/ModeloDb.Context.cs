﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CuestionariosEntities : DbContext
    {
        public CuestionariosEntities()
            : base("name=CuestionariosEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Capacitaciones> Capacitaciones { get; set; }
        public virtual DbSet<Ciudades> Ciudades { get; set; }
        public virtual DbSet<Cuestionarios> Cuestionarios { get; set; }
        public virtual DbSet<Referidos> Referidos { get; set; }
        public virtual DbSet<Instructores> Instructores { get; set; }
    }
}
