namespace FillDBService.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tramos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tramos()
        {
            Carriles = new HashSet<Carriles>();
        }

        [Key]
        [StringLength(3)]
        public string IdGare { get; set; }

        [StringLength(4)]
        public string NumeroPlaza { get; set; }

        [StringLength(40)]
        public string Nombre { get; set; }

        [StringLength(3)]
        public string Cuerpo { get; set; }

        [StringLength(30)]
        public string Delegacion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Carriles> Carriles { get; set; }
    }
}
