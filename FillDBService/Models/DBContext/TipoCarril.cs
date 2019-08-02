namespace FillDBService.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipoCarril")]
    public partial class TipoCarril
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoCarril()
        {
            Carriles = new HashSet<Carriles>();
        }

        [Key]
        [StringLength(3)]
        public string IdTipoCarril { get; set; }

        [StringLength(40)]
        public string Descripcion { get; set; }

        [StringLength(4)]
        public string Tipo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Carriles> Carriles { get; set; }
    }
}
