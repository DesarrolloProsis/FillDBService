namespace FillDBService.Models.DBContext
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Administradores
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Administradores()
        {
            Bitacora = new HashSet<Bitacora>();
        }

        [Key]
        [StringLength(8)]
        public string NumeroCapufeAdministrador { get; set; }

        [StringLength(8)]
        public string NumeroGeaAdministracion { get; set; }

        [StringLength(30)]
        public string Nombre { get; set; }

        [StringLength(20)]
        public string ApellidoPaterno { get; set; }

        [StringLength(20)]
        public string ApellidoMaterno { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bitacora> Bitacora { get; set; }
    }
}
