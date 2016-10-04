using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandIn3TestApplication
{
    class Person
    {
        public string Fornavn { get; set; }
        public string Mellemnavn { get; set; }
        public string Efternavn { get; set; }
        public string PersonType { get; set; }
        public Adresse AdresseID { get; set; }
    }

    class Telefon
    {
        public string Telefonnummer { get; set; }
        public string TelefonType { get; set; }
        public List<Person> PersonID { get; set; }
    }

    class Adresse
    {
        public string Vejnavn { get; set; }
        public string Husnummer { get; set; }
        public string Postnummer { get; set; }
        public string Bynavn { get; set; }
    }

    
}
