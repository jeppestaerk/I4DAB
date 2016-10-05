using System.Collections.Generic;

namespace HandIn3.DataModel
{
    public class Telefon
    {
        public long TelefonId { get; set; }
        public string Telefonnummer { get; set; }
        public string TelefonType { get; set; }
        public Person Person { get; set; }
    }
}