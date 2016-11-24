namespace AdresseKartotekWebAPI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Adresse")]
    public partial class Adresse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Adresse()
        {
            AlternativAdresses = new HashSet<AlternativAdresse>();
            People = new HashSet<Person>();
        }

        public long AdresseID { get; set; }

        [Required]
        [StringLength(300)]
        public string Vejnavn { get; set; }

        [Required]
        [StringLength(300)]
        public string Husnummer { get; set; }

        [Required]
        [StringLength(300)]
        public string Postnummer { get; set; }

        [Required]
        [StringLength(300)]
        public string Bynavn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlternativAdresse> AlternativAdresses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Person> People { get; set; }
    }
}
