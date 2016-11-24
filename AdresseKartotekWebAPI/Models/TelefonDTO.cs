using System.Web.Razor.Generator;

namespace AdresseKartotekWebAPI.Models
{
    public class TelefonDTO
    {        
        public string Telefonnummer { get; set; }
        public string TelefonType { get; set; }
    }

    public class ExtendedTelefonDTO
    {
        public string Telefonnummer { get; set; }
        public string TelefonType { get; set; }
        public PersonDTO PersonDto { get; set; }
    }
}