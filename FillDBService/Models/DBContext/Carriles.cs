namespace FillDBService.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Carriles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Carriles()
        {
            Bitacora = new HashSet<Bitacora>();
            Discrepancias = new HashSet<Discrepancias>();
            Eventos = new HashSet<Eventos>();
            LL = new HashSet<LL>();
            PreliquidacionCajero = new HashSet<PreliquidacionCajero>();
            Validaciones = new HashSet<Validaciones>();
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string NumeroCarril { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(3)]
        public string IdGare { get; set; }

        [Required]
        [StringLength(3)]
        public string IdTipoCarril { get; set; }

        [StringLength(20)]
        public string Carril { get; set; }

        [StringLength(20)]
        public string NumeroTramo { get; set; }

        [StringLength(2)]
        public string Cuerpo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bitacora> Bitacora { get; set; }

        public virtual TipoCarril TipoCarril { get; set; }

        public virtual Tramos Tramos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Discrepancias> Discrepancias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Eventos> Eventos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LL> LL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PreliquidacionCajero> PreliquidacionCajero { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Validaciones> Validaciones { get; set; }
    }
}
