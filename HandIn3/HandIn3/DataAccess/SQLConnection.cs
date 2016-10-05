﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HandIn3.DataModel;

namespace HandIn3.DataAccess
{
    public class PersonkartotekDataUtil
    {
        private Person _locPerson;
        SqlConnection _conn;

        public Person CurrentPerson
        {
            get { return _locPerson; }
        }

        public PersonkartotekDataUtil()
        {
            _conn = new SqlConnection(@"Data Source=JEPPESTAERK\SQLEXPRESS;Initial Catalog='I4DAB HandIn2';Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True");
        }

        public void SetCurrentPerson(string fornavn, string efternavn)
        {
            SqlDataReader rdr = null;

            try
            {
                _conn.Open();

                SqlCommand cmd = new SqlCommand($"SELECT * FROM Person WHERE Fornavn = '{fornavn}' AND Efternavn = '{efternavn}'", _conn);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine($"Navn: {rdr["Fornavn"]} {rdr["Mellemnavn"]} {rdr["Efternavn"]}");
                    _locPerson = new Person();
                    _locPerson.PersonId = (long) rdr["PersonID"];
                    _locPerson.Fornavn = (string) rdr["Fornavn"];
                    _locPerson.Mellemnavn = (string) rdr["Mellemnavn"];
                    _locPerson.Efternavn = (string) rdr["Efternavn"];
                    _locPerson.PersonType = (string) rdr["PersonType"];
                    _locPerson.FolkeregisterAdresse.AdresseId = (long)rdr["AdresseID"];
                    break;

                }
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (_conn != null)
                {
                    _conn.Close();
                }
            }
        }

        public void SetCurrentPerson(long id)
        {
            SqlDataReader rdr = null;

            try
            {
                _conn.Open();

                SqlCommand cmd = new SqlCommand($"SELECT * FROM Person WHERE PersonID = '{id}'", _conn);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine($"Navn: {rdr["Fornavn"]} {rdr["Mellemnavn"]} {rdr["Efternavn"]}");
                    _locPerson = new Person();
                    _locPerson.PersonId = (long) rdr["PersonID"];
                    _locPerson.Fornavn = (string) rdr["Fornavn"];
                    _locPerson.Mellemnavn = (string) rdr["Mellemnavn"];
                    _locPerson.Efternavn = (string) rdr["Efternavn"];
                    _locPerson.PersonType = (string) rdr["PersonType"];
                    _locPerson.FolkeregisterAdresse.AdresseId = (long) rdr["AdresseID"];
                    break;

                }
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (_conn != null)
                {
                    _conn.Close();
                }
            }
        }

        public void GetCurrentTelefon()
        {
            SqlDataReader rdr = null;
            string selectTelefonString = @"SELECT Telefon.* 
                                        FROM Person INNER JOIN 
                                        Telefon ON Person.PersonID = Telefon.PersonID
                                        WHERE (Person.PersonID = @Data1)";

            try
            {
                _conn.Open();

                using (SqlCommand cmd = new SqlCommand(selectTelefonString, _conn))
                {
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";
                    cmd.Parameters["@Data1"].Value = _locPerson.PersonId;

                    rdr = cmd.ExecuteReader();

                    _locPerson.Telefon = new List<Telefon>();
                    Telefon locTelefon;

                    while (rdr.Read())
                    {
                        Console.WriteLine($"Telefonnummer: {rdr["Telefonnummer"]}");
                        locTelefon = new Telefon();
                        locTelefon.TelefonId = (long) rdr["TelefonID"];
                        locTelefon.Telefonnummer = (string) rdr["Telefonnummer"];
                        locTelefon.TelefonType = (string) rdr["TelefonType"];
                        _locPerson.Telefon.Add(locTelefon);
                    }
                }

            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (_conn != null)
                {
                    _conn.Close();
                }
            }
        }

        public void GetCurrentAdresse()
        {
            SqlDataReader rdr = null;
            string selectAdresseString = @"SELECT Adresse.* 
                                        FROM Person INNER JOIN 
                                        Adresse ON Person.AdresseID = Adresse.AdresseID
                                        WHERE (Person.AdresseID = @Data1)";

            try
            {
                _conn.Open();

                using (SqlCommand cmd = new SqlCommand(selectAdresseString, _conn))
                {
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";
                    cmd.Parameters["@Data1"].Value = _locPerson.FolkeregisterAdresse.AdresseId;

                    rdr = cmd.ExecuteReader();

                    _locPerson.FolkeregisterAdresse = new Adresse();
                    Adresse locAdresse;

                    while (rdr.Read())
                    {
                        Console.WriteLine($"Adresse: {rdr["Vejnavn"]} {rdr["Husnummer"]}, {rdr["Postnummer"]} {rdr["Bynavn"]}");
                        locAdresse = new Adresse();
                        locAdresse.AdresseId = (long) rdr["AdresseID"];
                        locAdresse.Vejnavn = (string) rdr["Vejnavn"];
                        locAdresse.Husnummer = (string) rdr["Husnummer"];
                        locAdresse.Postnummer = (string) rdr["Postnummer"];
                        locAdresse.Bynavn = (string) rdr["Bynavn"];
                        _locPerson.FolkeregisterAdresse = locAdresse;
                        break;
                    }
                }

            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (_conn != null)
                {
                    _conn.Close();
                }
            }
        }

        public void InsertNewPerson(Person person)
        {
            try
            {
                _conn.Open();

                string insertString = @"INSERT INTO Person(Fornavn, Mellemnavn, Efternavn, PersonType)
                                                    OUTPUT INSERTED.PersonID  
                                                    VALUES (@Data1, @Data2, @Data3, @Data4)";

                using (SqlCommand cmd = new SqlCommand(insertString, _conn))
                {
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data2";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data3";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data4";
                    cmd.Parameters["@Data1"].Value = person.Fornavn;
                    cmd.Parameters["@Data2"].Value = person.Mellemnavn;
                    cmd.Parameters["@Data3"].Value = person.Efternavn;
                    cmd.Parameters["@Data4"].Value = person.PersonType;

                    person.PersonId = (long)cmd.ExecuteScalar();

                    _locPerson = person;
                }
            }
            finally
            {
                if (_conn != null)
                {
                    _conn.Close();
                }
            }
        }

        public void InsertNewTelefon(Telefon telefon)
        {
            try
            {
                _conn.Open();

                string insertString = @"INSERT INTO Telefon(Telefonnummer, TelefonType, PersonID)
                                                    OUTPUT INSERTED.TelefonID  
                                                    VALUES (@Data1, @Data2, @Data3)";

                using (SqlCommand cmd = new SqlCommand(insertString, _conn))
                {
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data2";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data3";
                    cmd.Parameters["@Data1"].Value = telefon.Telefonnummer;
                    cmd.Parameters["@Data2"].Value = telefon.TelefonType;
                    cmd.Parameters["@Data3"].Value = _locPerson.PersonId;

                    telefon.TelefonId = (long)cmd.ExecuteScalar();
                }
            }
            finally
            {
                if (_conn != null)
                {
                    _conn.Close();
                }
            }
        }

        public void InsertNewAdresse(Adresse adresse)
        {
            try
            {
                _conn.Open();

                string insertString = @"INSERT INTO Adresse(Vejnavn,Husnummer,Postnummer,Bynavn)
                                                    OUTPUT INSERTED.AdresseID  
                                                    VALUES (@Data1, @Data2, @Data3, @Data4)";
                _locPerson.FolkeregisterAdresse = new Adresse();

                using (SqlCommand cmd = new SqlCommand(insertString, _conn))
                {
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data2";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data3";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data4";
                    cmd.Parameters["@Data1"].Value = adresse.Vejnavn;
                    cmd.Parameters["@Data2"].Value = adresse.Husnummer;
                    cmd.Parameters["@Data3"].Value = adresse.Postnummer;
                    cmd.Parameters["@Data4"].Value = adresse.Bynavn;

                    adresse.AdresseId = (long) cmd.ExecuteScalar();
                }

                string updateString = @"UPDATE Person
                                    SET AdresseID = @Data1
                                    WHERE Person.PersonID = @Data2";

                using (SqlCommand cmd = new SqlCommand(updateString, _conn))
                {
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data2";

                    cmd.Parameters["@Data1"].Value = adresse.AdresseId;
                    cmd.Parameters["@Data2"].Value = _locPerson.PersonId;

                    var id = cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (_conn != null)
                {
                    _conn.Close();
                }
            }
        }

        public void UpdateCurrentPerson()
        {
            try
            {
                _conn.Open();

                string updateString = @"UPDATE Person
                                    SET Fornavn = @Data1, Mellemnavn = @Data2, Efternavn = @Data3, PersonType = @Data4, AdresseID = @Data5
                                    WHERE Person.PersonID = @Data6";

                using (SqlCommand cmd = new SqlCommand(updateString, _conn))
                {
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data2";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data3";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data4";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data5";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data6";

                    cmd.Parameters["@Data1"].Value = _locPerson.Fornavn;
                    cmd.Parameters["@Data2"].Value = _locPerson.Mellemnavn;
                    cmd.Parameters["@Data3"].Value = _locPerson.Efternavn;
                    cmd.Parameters["@Data4"].Value = _locPerson.PersonType;
                    cmd.Parameters["@Data5"].Value = _locPerson.FolkeregisterAdresse.AdresseId;
                    cmd.Parameters["@Data6"].Value = _locPerson.PersonId;

                    var id = cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                if (_conn != null)
                {
                    _conn.Close();
                }
            }
        }

        public void DeleteCurrentPerson()
        {
            try
            {
                _conn.Open();

                string deleteString = @"DELETE FROM Person
                                    WHERE Person.PersonID = @Data1";

                using (SqlCommand cmd = new SqlCommand(deleteString, _conn))
                {
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";

                    cmd.Parameters["@Data1"].Value = _locPerson.PersonId;

                    var id = cmd.ExecuteNonQuery();
                    _locPerson = null;
                }
            }
            finally
            {
                if (_conn != null)
                {
                    _conn.Close();
                }
            }
        }

        public int GetNumberOfRecords()
        {
            int count = -1;

            try
            {
                _conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT count(*) FROM Person", _conn);

                count = (int)cmd.ExecuteScalar();
            }
            finally
            {
                if (_conn != null)
                {
                    _conn.Close();
                }
            }
            return count;
        }

        public void PrintAllPerson()
        {
            SqlDataReader rdr = null;

            try
            {
                _conn.Open();

                SqlCommand cmd = new SqlCommand($"SELECT * FROM Person", _conn);

                rdr = cmd.ExecuteReader();

                string title = "PERSONER";
                Console.Write("".PadLeft(24, '*'));
                Console.Write("".PadLeft(1));
                Console.Write(String.Format("{0,-" + ((10 / 2) + (title.Length / 2)) + "}", title));
                Console.Write("".PadRight(1));
                Console.WriteLine("".PadRight(24, '*'));

                Console.WriteLine("{0,-5}{1,-15}{2,-15}{3,-15}{4,-10}", "ID", "Fornavn", "Mellemnavn", "Efternavn", "AdresseID");
                Console.WriteLine(("").PadLeft(60, '-'));

                while (rdr.Read())
                {
                    Console.WriteLine($"{rdr["PersonID"],-5:D3}{rdr["Fornavn"], -15}{rdr["Mellemnavn"], -15}{rdr["Efternavn"], -15}{rdr["AdresseID"],-10:D3}");
                }
                Console.WriteLine();
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (_conn != null)
                {
                    _conn.Close();
                }
            }
        }

        public void PrintAllTelefon()
        {
            SqlDataReader rdr = null;

            try
            {
                _conn.Open();

                SqlCommand cmd = new SqlCommand($"SELECT * FROM Telefon", _conn);

                rdr = cmd.ExecuteReader();

                string title = "TELEFONER";
                Console.Write("".PadLeft(24, '*'));
                Console.Write("".PadLeft(1));
                Console.Write(String.Format("{0,-" + ((10 / 2) + (title.Length / 2)) + "}", title));
                Console.Write("".PadRight(1));
                Console.WriteLine("".PadRight(24, '*'));

                Console.WriteLine("{0,-5}{1,-15}{2,-15}{3,-10}", "ID", "Telefonr", "Type", "PersonID");
                Console.WriteLine(("").PadLeft(60, '-'));
                while (rdr.Read())
                {
                    Console.WriteLine($"{rdr["TelefonID"],-5:D3}{rdr["Telefonnummer"],-15}{rdr["TelefonType"],-15}{rdr["PersonID"],-10:D3}");
                }
                Console.WriteLine();
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (_conn != null)
                {
                    _conn.Close();
                }
            }
        }

        public void PrintAllPAdresser()
        {
            SqlDataReader rdr = null;

            try
            {
                _conn.Open();

                SqlCommand cmd = new SqlCommand($"SELECT * FROM Adresse", _conn);

                rdr = cmd.ExecuteReader();

                string title = "ADRESSER";
                Console.Write("".PadLeft(24, '*'));
                Console.Write("".PadLeft(1));
                Console.Write(String.Format("{0,-" + ((10/2) + (title.Length / 2)) + "}", title));
                Console.Write("".PadRight(1));
                Console.WriteLine("".PadRight(24, '*'));

                Console.WriteLine("{0,-5}{1,-25}{2,-10}{3,-10}{4,-15}", "ID", "Vejnavn", "Husnr", "Postnr", "By");
                Console.WriteLine(("").PadLeft(60, '-'));
                while (rdr.Read())
                {
                    Console.WriteLine($"{rdr["AdresseID"],-5:D3}{rdr["Vejnavn"],-25}{rdr["Husnummer"],-10}{rdr["Postnummer"],-10}{rdr["Bynavn"],-15}");
                }
                Console.WriteLine();
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (_conn != null)
                {
                    _conn.Close();
                }
            }
        }
    }
}