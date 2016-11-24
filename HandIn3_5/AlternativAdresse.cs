namespace HandIn3_5
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AlternativAdresse")]
    public partial class AlternativAdresse
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long PersonID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AdresseID { get; set; }

        [Required]
        [StringLength(300)]
        public string AdresseType { get; set; }

        public virtual Adresse Adresse { get; set; }

        public virtual Person Person { get; set; }
    }
}
