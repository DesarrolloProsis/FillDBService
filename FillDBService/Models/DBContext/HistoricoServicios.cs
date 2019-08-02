namespace FillDBService.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HistoricoServicios
    {
        [Key]
        [Column(Order = 0, TypeName = "date")]
        public DateTime Fecha { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(2)]
        public string Idturno { get; set; }

        public virtual Turnos Turnos { get; set; }
    }
}
