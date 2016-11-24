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

<<<<<<< HEAD
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
=======
            personkartotek.InsertPersonMedFolkeregisteradresse(n1Person);
            personkartotek.InsertTelefonTilPerson(n1Telefon);

            Console.WriteLine("PERSON, ADRESSE OG TELEFON INDSAT");
            personkartotek.PrintAllPerson();
            personkartotek.PrintAllPAdresser();
            personkartotek.PrintAllTelefon();
            Console.WriteLine();

            n1Person.Mellemnavn = "Stærk";
            n1Person.Efternavn = "Antonsen";
>>>>>>> 3e9428e857897f93b658b7a5e5a3c3f15dd3a711

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
