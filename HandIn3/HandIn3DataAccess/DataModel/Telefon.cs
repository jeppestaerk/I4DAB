using System.Collections.Generic;

namespace HandIn3DataAccess.DataModel
{
    public class Telefon
    {
        public long TelefonId { get; set; }
        public string Telefonnummer { get; set; }
        public string TelefonType { get; set; }
        public List<Person> PersonId { get; set; }
    }
}