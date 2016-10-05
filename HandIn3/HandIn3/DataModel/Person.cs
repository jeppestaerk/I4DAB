using System.Collections.Generic;

namespace HandIn3DataAccess.DataModel
{
    public class Person
    {
        public long PersonId { get; set; }
        public string Fornavn { get; set; }
        public string Mellemnavn { get; set; }
        public string Efternavn { get; set; }
        public string PersonType { get; set; }
        public long AdresseId { get; set; }
        public Adresse FolkeregisterAdresse { get; set; }
        public List<Telefon> Ejer { get; set; }
        
    }
}
