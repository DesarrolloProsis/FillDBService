namespace FillDBService.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Discrepancias
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string NumeroCarril { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string IdGare { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(2)]
        public string IdTurno { get; set; }

        [Key]
        [Column(Order = 3)]
        public DateTime Fecha { get; set; }

        [Key]
        [Column(Order = 4)]
        public string HoraEvento { get; set; }

        [StringLength(10)]
        public string NumEvento { get; set; }

        [StringLength(10)]
        public string FolioECT { get; set; }

        [StringLength(8)]
        public string CodigoVehiculoDetectadoECT { get; set; }

        public int ImporteDetectadoECT { get; set; }

        public int ImporteEjeExcedenteECT { get; set; }

        [StringLength(8)]
        public string CodigoVehiculoMarcadoCR { get; set; }

        [StringLength(2)]
        public string TipoVehiculoMarcado { get; set; }

        [Required]
        [StringLength(3)]
        public string IdTipoPago { get; set; }

        public int ImporteMarcadoCR { get; set; }

        public int ImporteEjeExcedenteCR { get; set; }

        [StringLength(8)]
        public string CodigoVehiculoDetectadoEAP { get; set; }

        public int ImporteVehiculoDetectadoEAP { get; set; }

        public int ImporteEjeExcedenteEAP { get; set; }

        public virtual Carriles Carriles { get; set; }

        public virtual TipoPago TipoPago { get; set; }

        public virtual Turnos Turnos { get; set; }
    }
}
