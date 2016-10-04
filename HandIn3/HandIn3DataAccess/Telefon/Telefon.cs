using System.Collections.Generic;

namespace HandIn3DataAccess.Telefon
{
    class Telefon
    {
        public string Telefonnummer { get; set; }
        public string TelefonType { get; set; }
        public List<Person.Person> PersonId { get; set; }
    }
}