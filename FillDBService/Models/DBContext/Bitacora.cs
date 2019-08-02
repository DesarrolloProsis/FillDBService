namespace FillDBService.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bitacora")]
    public partial class Bitacora
    {
        [Key]
        [Column(Order = 0, TypeName = "date")]
        public DateTime Fecha { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string NumeroCarril { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(3)]
        public string IdGare { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(2)]
        public string IdTurno { get; set; }

        [Required]
        [StringLength(5)]
        public string IdIdentificadorOperacion { get; set; }

        [StringLength(12)]
        public string HoraInicial { get; set; }

        [StringLength(12)]
        public string HoraFinal { get; set; }

        [Required]
        [StringLength(8)]
        public string NumeroCapufeCajero { get; set; }

        [Required]
        [StringLength(8)]
        public string NumeroCapufeEncargado { get; set; }

        [Required]
        [StringLength(8)]
        public string NumeroCapufeAdministrador { get; set; }

        [StringLength(10)]
        public string Bolsa { get; set; }

        public virtual Administradores Administradores { get; set; }

        public virtual Cajeros Cajeros { get; set; }

        public virtual Carriles Carriles { get; set; }

        public virtual Encargados Encargados { get; set; }

        public virtual IdentificadorOperacion IdentificadorOperacion { get; set; }

        public virtual Turnos Turnos { get; set; }
    }
}
