using System;
using HandIn3.DataAccess;
using HandIn3.DataModel;
using System.Collections.Generic;

namespace HandIn3TestApplication
{
    class HandIn3TestApplication
    {
        static void Main()
        {
            KartotekDataUtil personkartotek = new KartotekDataUtil();

            Console.WriteLine("PRE DATABASE INDHOLD");
            personkartotek.PrintAllPerson();
            personkartotek.PrintAllPAdresser();
            personkartotek.PrintAllTelefon();
            Console.WriteLine();

            Adresse n1Adresse = new Adresse()
            {
                Vejnavn = "Strandvejen",
                Husnummer = "30B",
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

            Telefon n1Telefon = new Telefon()
            {
                Person = n1Person,
                Telefonnummer = "61656585",
                TelefonType = "work"
            };

            personkartotek.InsertPersonMedFolkeregisteradresse(n1Person);
            personkartotek.InsertTelefonTilPerson(n1Telefon);

            Console.WriteLine("PERSON, ADRESSE OG TELEFON INDSAT");
            personkartotek.PrintAllPerson();
            personkartotek.PrintAllPAdresser();
            personkartotek.PrintAllTelefon();
            Console.WriteLine();

            n1Person.Mellemnavn = "Stærk";
            n1Person.Efternavn = "Antonsen";

            n1Telefon.Telefonnummer = "50403000";
            n1Telefon.TelefonType = "privat";

            personkartotek.UpdatePerson(n1Person);
            personkartotek.UpdateTelefon(n1Telefon);

            Console.WriteLine("PERSON OG TELEFON OPDATERET");
            personkartotek.PrintAllPerson();
            personkartotek.PrintAllTelefon();
            Console.WriteLine();

            personkartotek.DeletePerson("Jeppe", "Antonsen");
            personkartotek.DeleteAdresse("Strandvejen", "30B");
            personkartotek.DeleteTelefon("50403000");

            Console.WriteLine("PERSON, ADRESSE OG TELEFON SLETTET");
            personkartotek.PrintAllPerson();
            personkartotek.PrintAllPAdresser();
            personkartotek.PrintAllTelefon();
            Console.WriteLine();
        }
    }
}
