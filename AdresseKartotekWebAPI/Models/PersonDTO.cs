using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdresseKartotekWebAPI.Models
{
    public class PersonDTO
    {
        public long ID { get; set; }
        public string Fornavn { get; set; }
        public string Mellemnavn { get; set; }
        public string Efternavn { get; set; }
       
    }

    public class ExtendedPersonDTO
    {
        public long ID { get; set; }
        public string Fornavn { get; set; }
        public string Mellemnavn { get; set; }
        public string Efternavn { get; set; }
        public string PersonType { get; set; }
        public List<AdresseDTO> alternativer { get; set; }
        public List<TelefonDTO> Telefons { get; set; }
    }


}