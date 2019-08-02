namespace FillDBService.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IdentificadorOperacion")]
    public partial class IdentificadorOperacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IdentificadorOperacion()
        {
            Bitacora = new HashSet<Bitacora>();
        }

        [Key]
        [StringLength(5)]
        public string IdIdentificadorOperacion { get; set; }

        [StringLength(20)]
        public string Descripcion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bitacora> Bitacora { get; set; }
    }
}
