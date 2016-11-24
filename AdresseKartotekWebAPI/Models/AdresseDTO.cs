using System.Collections.Generic;

namespace AdresseKartotekWebAPI.Models
{
    public class AdresseDTO
    {
        public string Vejnavn { get; set; }
        public string Husnummer { get; set; }
        public string Postnummer { get; set; }
        public string Bynavn { get; set; }       
    }

    public class ExtendedAdressDTO
    {
        public string Vejnavn { get; set; }
        public string Husnummer { get; set; }
        public string Postnummer { get; set; }
        public string Bynavn { get; set; }      
        public List<PersonDTO> Person { get; set; }

        public List<AdresseDTO> Adresses { get; set; }
    } 
}