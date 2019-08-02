namespace FillDBService.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Eventos
    {
        [Key]
        public int IdEvento { get; set; }

        [Required]
        [StringLength(20)]
        public string NumeroCarril { get; set; }

        [StringLength(10)]
        public string NumEvento { get; set; }

        [Required]
        [StringLength(2)]
        public string IdTurno { get; set; }

        [Required]
        [StringLength(3)]
        public string IdGare { get; set; }

        [Required]
        [StringLength(3)]
        public string IdTipoPago { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }

        [StringLength(12)]
        public string Hora { get; set; }

        [StringLength(10)]
        public string Folio { get; set; }

        [StringLength(8)]
        public string CodigoVehiculoDetectado { get; set; }

        [StringLength(8)]
        public string CodigoVehiculoMarcado { get; set; }

        [StringLength(2)]
        public string TipoVehiculo { get; set; }

        public int ImporteVehiculoDetectado { get; set; }

        public int ImporteEjeExcedente { get; set; }

        public int ImporteMarcadoCajero { get; set; }

        public int EjeExcedenteMarcadoCajero { get; set; }

        [StringLength(60)]
        public string NumTarjetaIAVE { get; set; }

        public bool SituacionTarjetaIAVE { get; set; }

        [StringLength(10)]
        public string AutorizacionBancaria { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaContable { get; set; }

        [StringLength(10)]
        public string CodigoAlfaNumerico { get; set; }

        public virtual Carriles Carriles { get; set; }

        public virtual TipoPago TipoPago { get; set; }

        public virtual Turnos Turnos { get; set; }
    }
}
