namespace DatabaseModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Programari")]
    public partial class Programari
    {
        [Key]
        public int ProgramareId { get; set; }

        public int? AnimalId { get; set; }

        public int? MedicId { get; set; }

        public virtual Animale Animale { get; set; }

        public virtual Medici Medici { get; set; }
    }
}
