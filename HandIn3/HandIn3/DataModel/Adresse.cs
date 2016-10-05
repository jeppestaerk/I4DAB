using System.Collections.Generic;

namespace HandIn3.DataModel
{
    public class Adresse
    {
        public long AdresseId { get; set; }
        public string Vejnavn { get; set; }
        public string Husnummer { get; set; }
        public string Postnummer { get; set; }
        public string Bynavn { get; set; }
    }
}