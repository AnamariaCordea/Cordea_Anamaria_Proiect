namespace DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Animale")]
    public partial class Animale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Animale()
        {
            Appointments = new HashSet<Programari>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AnimalId { get; set; }

        [StringLength(50)]
        public string Tip { get; set; }

        [StringLength(50)]
        public string Rasa { get; set; }

        [StringLength(50)]
        public string Varsta { get; set; }

        [StringLength(50)]
        public string Problema { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Programari> Appointments { get; set; }
    }
}
