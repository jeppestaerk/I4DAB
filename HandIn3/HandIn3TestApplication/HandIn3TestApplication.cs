using HandIn3.DataAccess;
using HandIn3.DataModel;

namespace HandIn3TestApplication
{
    class HandIn3TestApplication
    {
        static void Main()
        {
            PersonkartotekDataUtil personkartotek = new PersonkartotekDataUtil();

            personkartotek.PrintAllPerson();
            personkartotek.PrintAllTelefon();
            personkartotek.PrintAllPAdresser();

            Adresse newAdresse = new Adresse()
            {
                Vejnavn = "Strandvejen",
                Husnummer = "30B",
                Postnummer = "8000",
                Bynavn = "Aarhus C"
            };

            Person newPerson = new Person()
            {
                Fornavn = "Jeppe",
                Mellemnavn = "",
                Efternavn = "Stærk",
                PersonType = "Chef",
                FolkeregisterAdresse = newAdresse
            };
            Telefon newTelefon1 = new Telefon()
            {
                Person = newPerson,
                Telefonnummer = "50403000",
                TelefonType = "mobil"
            };
            Telefon newTelefon2 = new Telefon()
            {
                Person = newPerson,
                Telefonnummer = "61656585",
                TelefonType = "work"
            };

            personkartotek.InsertNewPerson(newPerson);
            personkartotek.InsertNewAdresse(newAdresse);
            personkartotek.InsertNewTelefon(newTelefon1);
            personkartotek.InsertNewTelefon(newTelefon2);

            personkartotek.PrintAllPerson();
            personkartotek.PrintAllTelefon();
            personkartotek.PrintAllPAdresser();

            personkartotek.DeleteCurrentPerson();

            personkartotek.PrintAllPerson();
            personkartotek.PrintAllTelefon();
            personkartotek.PrintAllPAdresser();
        }
    }
}
