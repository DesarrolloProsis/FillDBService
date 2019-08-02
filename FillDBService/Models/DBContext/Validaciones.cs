namespace FillDBService.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Validaciones
    {
        [Key]
        [Column(Order = 0)]
        public DateTime Fecha { get; set; }

        [Key]
        [Column(Order = 1)]
        public string NumCarril { get; set; }

        [Key]
        [Column(Order = 2)]
        public string IdGare { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(2)]
        public string IdTurno { get; set; }

        [StringLength(10)]
        public string Estatus { get; set; }

        [StringLength(10)]
        public string NumBolsa { get; set; }

        [StringLength(50)]
        public string Comentarios { get; set; }

        [StringLength(20)]
        public string Carril_NumeroCarril { get; set; }

        [StringLength(3)]
        public string Carril_IdGare { get; set; }

        public virtual Carriles Carriles { get; set; }

        public virtual Turnos Turnos { get; set; }
    }
}
