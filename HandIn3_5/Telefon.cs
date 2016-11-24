namespace HandIn3_5
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Telefon")]
    public partial class Telefon
    {
        public long TelefonID { get; set; }

        [Required]
        [StringLength(300)]
        public string Telefonnummer { get; set; }

        [Required]
        [StringLength(300)]
        public string TelefonType { get; set; }

        public long PersonID { get; set; }

        public virtual Person Person { get; set; }
    }
}
