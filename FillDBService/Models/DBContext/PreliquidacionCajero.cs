namespace FillDBService.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PreliquidacionCajero")]
    public partial class PreliquidacionCajero
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
        public string Bolsa { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime FechaOperacion { get; set; }

        [StringLength(12)]
        public string HoraInicial { get; set; }

        [StringLength(12)]
        public string HoraFinal { get; set; }

        [StringLength(10)]
        public string EventoInicial { get; set; }

        [StringLength(10)]
        public string EventoFinal { get; set; }

        public int EventosCancelados { get; set; }

        [StringLength(10)]
        public string FolioECTInicial { get; set; }

        [StringLength(10)]
        public string FolioECTFinal { get; set; }

        public int FoliosECTCancelados { get; set; }

        public int EfectivoEntregado { get; set; }

        public int SMCPMarcado { get; set; }

        public int SMCPEntregado { get; set; }

        public int SMCPNumMarcado { get; set; }

        public int SMCPNumEntregado { get; set; }

        public int EfectivoEntregadoDLL { get; set; }

        public int SEEMN { get; set; }

        public int EfectivoEnContraEntregado { get; set; }

        public int EfectivoEnContraPorEntregar { get; set; }

        public int NumBoletosCancelados { get; set; }

        public int ImporteBoletosCancelados { get; set; }

        public int NumResidentesEntregados { get; set; }

        public int ImporteResidentesEntregados { get; set; }

        public int NumResidentesMarcados { get; set; }

        public int ImporteResidentesMarcados { get; set; }

        public int NumVehiculoTelepeajeEntregados { get; set; }

        public int ImporteVehiculoTelepeajeEntregados { get; set; }

        public int NumVehiculosTelepeajeMarcados { get; set; }

        public int ImporteVehiculosTelepeajeMarcados { get; set; }

        public int NumVscEntregados { get; set; }

        public int ImporteVscEntregados { get; set; }

        public int NumVscMarcados { get; set; }

        public int ImporteVscMarcados { get; set; }

        public int NumResidentesSPEntregados { get; set; }

        public int ImporteResidentesSPEntregados { get; set; }

        public int NumResidentesSPMarcados { get; set; }

        public int ImporteResidentesSPMarcados { get; set; }

        public int NumEludidosEntregados { get; set; }

        public int ImporteEludidosEntregados { get; set; }

        public int NumEludidosMarcados { get; set; }

        public int ImporteEludidosMarcados { get; set; }

        public int NumReclasificacionesMarcadas { get; set; }

        public int ImporteReclasificacionesMarcadas { get; set; }

        public int DeteccionesErroneas { get; set; }

        public int ImporteDeteccionesErroneas { get; set; }

        public int DepositoMN { get; set; }

        public int DepositoDLL { get; set; }

        [StringLength(10)]
        public string TipoCambio { get; set; }

        [StringLength(10)]
        public string FolioInicialSecc1 { get; set; }

        [StringLength(10)]
        public string FolioFinalSecc1 { get; set; }

        public int FoliosCanceladosSecc1 { get; set; }

        [StringLength(10)]
        public string FolioInicialSecc2 { get; set; }

        [StringLength(10)]
        public string FolioFinalSecc2 { get; set; }

        public int FoliosCanceladosSecc2 { get; set; }

        [StringLength(10)]
        public string FolioInicialSecc3 { get; set; }

        [StringLength(10)]
        public string FolioFinalSecc3 { get; set; }

        public int FoliosCanceladosSecc3 { get; set; }

        public int NumBoletosGeneradoPErrorDLLS { get; set; }

        public int ImporteBoletosGeneradosPErrorDLLS { get; set; }

        public int SubtotalMarcadoComoPagadoDLLS { get; set; }

        public int NumResidentesPagoInmediatoEntregados { get; set; }

        public int ImporteResidentesPagoInmediatoEntregados { get; set; }

        public int NumResidentesPagoInmediatoMarcados { get; set; }

        public int ImporteResidentesPagoInmediatoMarcados { get; set; }

        public int NumTarjetasDeCreditoEntregados { get; set; }

        public int ImporteTarjetasDeCreditoEntregados { get; set; }

        public int NumTarjetasDeCreditoMarcados { get; set; }

        public int ImporteTarjetasDeCreditoMarcados { get; set; }

        public int NumTarjetasDeDebitoEntregados { get; set; }

        public int ImporteTarjetasDeDebitoEntregados { get; set; }

        public int NumTarjetasDeDebitoMarcados { get; set; }

        public int ImporteTarjetasDeDebitoMarcados { get; set; }

        public virtual Carriles Carriles { get; set; }

        public virtual Turnos Turnos { get; set; }
    }
}
