namespace FillDBService.Models.DBContext
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AppDBContext : DbContext
    {
        public AppDBContext()
            : base("name=AppDBContext")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Administradores> Administradores { get; set; }
        public virtual DbSet<Bitacora> Bitacora { get; set; }
        public virtual DbSet<Cajeros> Cajeros { get; set; }
        public virtual DbSet<Carriles> Carriles { get; set; }
        public virtual DbSet<Discrepancias> Discrepancias { get; set; }
        public virtual DbSet<Encargados> Encargados { get; set; }
        public virtual DbSet<Eventos> Eventos { get; set; }
        public virtual DbSet<HistoricoServicios> HistoricoServicios { get; set; }
        public virtual DbSet<IdentificadorOperacion> IdentificadorOperacion { get; set; }
        public virtual DbSet<LL> LL { get; set; }
        public virtual DbSet<pn_importacion_wsIndra> pn_importacion_wsIndra { get; set; }
        public virtual DbSet<PreliquidacionCajero> PreliquidacionCajero { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TipoCarril> TipoCarril { get; set; }
        public virtual DbSet<TipoPago> TipoPago { get; set; }
        public virtual DbSet<Tramos> Tramos { get; set; }
        public virtual DbSet<Turnos> Turnos { get; set; }
        public virtual DbSet<Validaciones> Validaciones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carriles>()
                .HasMany(e => e.Bitacora)
                .WithRequired(e => e.Carriles)
                .HasForeignKey(e => new { e.NumeroCarril, e.IdGare });

            modelBuilder.Entity<Carriles>()
                .HasMany(e => e.Discrepancias)
                .WithRequired(e => e.Carriles)
                .HasForeignKey(e => new { e.NumeroCarril, e.IdGare });

            modelBuilder.Entity<Carriles>()
                .HasMany(e => e.Eventos)
                .WithRequired(e => e.Carriles)
                .HasForeignKey(e => new { e.NumeroCarril, e.IdGare });

            modelBuilder.Entity<Carriles>()
                .HasMany(e => e.LL)
                .WithRequired(e => e.Carriles)
                .HasForeignKey(e => new { e.NumeroCarril, e.IdGare });

            modelBuilder.Entity<Carriles>()
                .HasMany(e => e.PreliquidacionCajero)
                .WithRequired(e => e.Carriles)
                .HasForeignKey(e => new { e.NumeroCarril, e.IdGare });

            modelBuilder.Entity<Carriles>()
                .HasMany(e => e.Validaciones)
                .WithOptional(e => e.Carriles)
                .HasForeignKey(e => new { e.Carril_NumeroCarril, e.Carril_IdGare });
        }
    }
}
