using System;
using System.Collections.Generic;
using System.Dynamic;
using HandIn3.DataAccess;
using HandIn3.DataModel;

namespace HandIn3TestApplication
{
    class HandIn3TestApplication
    {
        static void Main()
        {
            PersonkartotekDataUtil personkartotek = new PersonkartotekDataUtil();

            Console.WriteLine(@"/// Pre indhold af database \\\");
            personkartotek.PrintAllPerson();
            personkartotek.PrintAllTelefon();
            personkartotek.PrintAllPAdresser();

            Adresse n1Adresse = new Adresse()
            {
                Vejnavn = "Strandvejen",
                Husnummer = "30B",
                Postnummer = "8000",
                Bynavn = "Aarhus C"
            };

            Adresse n2Adresse = new Adresse()
            {
                Vejnavn = "Park Alle",
                Husnummer = "1",
                Postnummer = "8000",
                Bynavn = "Aarhus C"
            };

            Person n1Person = new Person()
            {
                Fornavn = "Jeppe",
                Mellemnavn = "",
                Efternavn = "Stærk",
                PersonType = "Chef",
                FolkeregisterAdresse = n1Adresse,
                AlternativAdresse = new List<Adresse>(),
                Telefon = new List<Telefon>()

            };

            Person n2Person = new Person()
            {
                Fornavn = "Lars",
                Mellemnavn = "",
                Efternavn = "Hjerrild",
                PersonType = "CEO",
                FolkeregisterAdresse = n2Adresse,
                AlternativAdresse = new List<Adresse>(),
                Telefon = new List<Telefon>()
            };

            Telefon n1Telefon2 = new Telefon()
            {
                Person = n1Person,
                Telefonnummer = "61656585",
                TelefonType = "work"
            };

            Telefon n1Telefon1 = new Telefon()
            {
                Person = n1Person,
                Telefonnummer = "50403000",
                TelefonType = "mobil"
            };
       

            Telefon n2Telefon1 = new Telefon()
            {
                Person = n1Person,
                Telefonnummer = "88888888",
                TelefonType = "mobil"
            };
            Telefon n2Telefon2 = new Telefon()
            {
                Person = n1Person,
                Telefonnummer = "77777777",
                TelefonType = "work"
            };
            Telefon n2Telefon3 = new Telefon()
            {
                Person = n1Person,
                Telefonnummer = "55555555",
                TelefonType = "home"
            };



            personkartotek.InsertNewPerson(n1Person);

            personkartotek.SetCurrentPerson("Jeppe","Stærk");
            personkartotek.InsertNewTelefon(n1Telefon1);





            //personkartotek.DeleteTelefon(6);
            //personkartotek.DeletePerson("Jeppe", "Stærk");

            //personkartotek.InsertNewPerson(n1Person);


            // personkartotek.SetCurrentPerson("Lars","Hjerrild");

            //personkartotek.InsertNewTelefon(n2Telefon1);

            //personkartotek.DeleteAdresse("Strandvejen","30B");
            //personkartotek.InsertNewAdresse(n1Adresse);         


            //personkartotek.InsertNewAdresse(n1Adresse);
            //personkartotek.InsertNewTelefon(n1Telefon1);
            //personkartotek.InsertNewTelefon(n1Telefon2);

            //Console.WriteLine(@"/// Person n1 indsat \\\");
            //personkartotek.PrintAllPerson();
            //personkartotek.PrintAllTelefon();
            //personkartotek.PrintAllPAdresser();

            //n1Person.FolkeregisterAdresse.Vejnavn = "Finlandsgade";
            //n1Person.FolkeregisterAdresse.Husnummer = "22";
            //n1Person.FolkeregisterAdresse.Postnummer = "8200";
            //n1Person.FolkeregisterAdresse.Bynavn = "Aarhus N";
            //personkartotek.UpdateCurrentAdresse();

            //Console.WriteLine(@"/// Person n1 adresse opdateret \\\");
            //personkartotek.PrintAllPAdresser();

            //personkartotek.DeletePerson("Jeppe", "Stærk");
            //personkartotek.DeleteAdresse("Finlandsgade", "22");
            //personkartotek.DeleteAdresse("Strandvejen", "30B");
            //personkartotek.DeleteTelefon("61656585");

            //personkartotek.InsertNewPerson(n2Person);
            //personkartotek.InsertNewAdresse(n2Adresse);
            //personkartotek.InsertNewTelefon(n2Telefon1);
            //personkartotek.InsertNewTelefon(n2Telefon2);
            //personkartotek.InsertNewTelefon(n2Telefon3);

            //Console.WriteLine(@"/// Person n1 slettet \\\");
            //Console.WriteLine(@"/// Person n2 indsat \\\");

            //personkartotek.PrintAllPerson();
            //personkartotek.PrintAllTelefon();
            //personkartotek.PrintAllPAdresser();

            //personkartotek.DeleteCurrentPerson();

            //Console.WriteLine(@"/// Person n2 slettet \\\");
            //personkartotek.PrintAllPerson();
            //personkartotek.PrintAllTelefon();
            //personkartotek.PrintAllPAdresser();

            //personkartotek.DeletePerson("Lars", "Hjerrild");
            //personkartotek.DeleteAdresse("Park Alle", "1");

            //personkartotek.PrintAllPerson();
            //personkartotek.PrintAllTelefon();
            //personkartotek.PrintAllPAdresser();
        }
    }
}
