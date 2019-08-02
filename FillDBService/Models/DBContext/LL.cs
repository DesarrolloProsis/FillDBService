namespace FillDBService.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LL")]
    public partial class LL
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
        [StringLength(12)]
        public string Hora { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(2)]
        public string IdTurno { get; set; }

        [StringLength(10)]
        public string NumEvento { get; set; }

        [StringLength(8)]
        public string ClaseVehiculo { get; set; }

        [StringLength(30)]
        public string CodigoIAVE { get; set; }

        public bool Validacion { get; set; }

        [StringLength(10)]
        public string ClaveTransportista { get; set; }

        public int Importe { get; set; }

        [StringLength(3)]
        public string NumeroEjes { get; set; }

        public virtual Carriles Carriles { get; set; }

        public virtual Turnos Turnos { get; set; }
    }
}
