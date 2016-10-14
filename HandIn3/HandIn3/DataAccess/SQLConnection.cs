using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HandIn3.DataModel;

namespace HandIn3.DataAccess
{
    public class PersonkartotekDataUtil
    {
        private Person _locPerson;
        SqlConnection _conn;
        private Adresse _locadresse = null;
       
        public Person CurrentPerson
        {
            get { return _locPerson; }
            set { _locPerson = value; }
        }

        public PersonkartotekDataUtil()
        {
            _conn = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Handin3;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }


        public void InsertNewPerson(Person person)
        {

            this.SetCurrentAdress(person.FolkeregisterAdresse.AdresseId);

            if (_locadresse == null)
            {
                    InsertNewAdresse(person.FolkeregisterAdresse);
            }

            if (person.FolkeregisterAdresse.AdresseId != _locadresse.AdresseId)
            {
                InsertNewAdresse(person.FolkeregisterAdresse);
            }

            try
            {
                _conn.Open();

                string insertString = @"INSERT INTO Person(Fornavn, Mellemnavn, Efternavn, PersonType, AdresseID)
                                                    OUTPUT INSERTED.PersonID  
                                                    VALUES (@Data1, @Data2, @Data3, @Data4, @Data5)";

                using (SqlCommand cmd = new SqlCommand(insertString, _conn))
                {
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data2";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data3";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data4";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data5";
                    cmd.Parameters["@Data1"].Value = person.Fornavn;
                    cmd.Parameters["@Data2"].Value = person.Mellemnavn;
                    cmd.Parameters["@Data3"].Value = person.Efternavn;
                    cmd.Parameters["@Data4"].Value = person.PersonType;
                    cmd.Parameters["@Data5"].Value = _locadresse.AdresseId;


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

        public void SetCurrentAdress(long id)
        {
            SqlDataReader rdr = null;

            try
            {
                _conn.Open();

                SqlCommand cmd = new SqlCommand($"SELECT * FROM Adresse WHERE AdresseID = '{id}'", _conn);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine($"Adresse: {rdr["Vejnavn"]} {rdr["Husnummer"]} {rdr["Bynavn"]}");
                    _locadresse = new Adresse();
                    _locadresse.AdresseId = (long)rdr["AdresseID"];
                    _locadresse.Bynavn = (string)rdr["Bynavn"];
                    _locadresse.Husnummer = (string)rdr["Husnummer"];
                    _locadresse.Postnummer = (string)rdr["Postnummer"];
                    _locadresse.Vejnavn = (string)rdr["Vejnavn"];
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
                    _locPerson.PersonId = (long)rdr["PersonID"];
                    _locPerson.Fornavn = (string)rdr["Fornavn"];
                    _locPerson.Mellemnavn = (string)rdr["Mellemnavn"];
                    _locPerson.Efternavn = (string)rdr["Efternavn"];
                    _locPerson.PersonType = (string)rdr["PersonType"];
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
                    _locPerson.PersonId = (long)rdr["PersonID"];
                    _locPerson.Fornavn = (string)rdr["Fornavn"];
                    _locPerson.Mellemnavn = (string)rdr["Mellemnavn"];
                    _locPerson.Efternavn = (string)rdr["Efternavn"];
                    _locPerson.PersonType = (string)rdr["PersonType"];
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

        //public void GetCurrentTelefon()
        //{
        //    SqlDataReader rdr = null;
        //    string selectTelefonString = @"SELECT Telefon.* 
        //                                FROM Person INNER JOIN 
        //                                Telefon ON Person.PersonID = Telefon.PersonID
        //                                WHERE (Person.PersonID = @Data1)";

        //    try
        //    {
        //        _conn.Open();

        //        using (SqlCommand cmd = new SqlCommand(selectTelefonString, _conn))
        //        {
        //            cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";
        //            cmd.Parameters["@Data1"].Value = _locPerson.PersonId;

        //            rdr = cmd.ExecuteReader();

        //            _locPerson.Telefon = new List<Telefon>();
        //            Telefon locTelefon;

        //            while (rdr.Read())
        //            {
        //                Console.WriteLine($"Telefonnummer: {rdr["Telefonnummer"]}");
        //                locTelefon = new Telefon();
        //                locTelefon.TelefonId = (long) rdr["TelefonID"];
        //                locTelefon.Telefonnummer = (string) rdr["Telefonnummer"];
        //                locTelefon.TelefonType = (string) rdr["TelefonType"];
        //                _locPerson.Telefon.Add(locTelefon);
        //            }
        //        }

        //    }
        //    finally
        //    {
        //        if (rdr != null)
        //        {
        //            rdr.Close();
        //        }

        //        if (_conn != null)
        //        {
        //            _conn.Close();
        //        }
        //    }
        //}

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
                        locAdresse.AdresseId = (long)rdr["AdresseID"];
                        locAdresse.Vejnavn = (string)rdr["Vejnavn"];
                        locAdresse.Husnummer = (string)rdr["Husnummer"];
                        locAdresse.Postnummer = (string)rdr["Postnummer"];
                        locAdresse.Bynavn = (string)rdr["Bynavn"];
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

        //public void InsertNewPerson(Person person)
        //{
        //    try
        //    {
        //        _conn.Open();

        //        string insertString = @"INSERT INTO Person(Fornavn, Mellemnavn, Efternavn, PersonType, AdresseID)
        //                                            OUTPUT INSERTED.PersonID  
        //                                            VALUES (@Data1, @Data2, @Data3, @Data4, @Data5)";

        //        using (SqlCommand cmd = new SqlCommand(insertString, _conn))
        //        {
        //            cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";
        //            cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data2";
        //            cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data3";
        //            cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data4";
        //            cmd.Parameters["@Data1"].Value = person.Fornavn;
        //            cmd.Parameters["@Data2"].Value = person.Mellemnavn;
        //            cmd.Parameters["@Data3"].Value = person.Efternavn;
        //            cmd.Parameters["@Data4"].Value = person.PersonType;
        //            cmd.Parameters["@Data5"].Value = 1;

        //            person.PersonId = (long)cmd.ExecuteScalar();

        //            _locPerson = person;
        //        }
        //    }
        //    finally
        //    {
        //        if (_conn != null)
        //        {
        //            _conn.Close();
        //        }
        //    }
        //}

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
                    cmd.Parameters["@Data3"].Value = telefon.Person.PersonId;

                    telefon.TelefonId = (long)cmd.ExecuteScalar();

                    _locPerson.Telefon.Add(telefon);
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
                //_locPerson.FolkeregisterAdresse = new Adresse();

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

                    adresse.AdresseId = (long)cmd.ExecuteScalar();

                }

                _locadresse = adresse;
                //string updateString = @"UPDATE Person
                //                    SET AdresseID = @Data1
                //                    WHERE Person.PersonID = @Data2";

                //using (SqlCommand cmd = new SqlCommand(updateString, _conn))
                //{
                //    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";
                //    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data2";

                //    cmd.Parameters["@Data1"].Value = adresse.AdresseId;
                //    cmd.Parameters["@Data2"].Value = _locPerson.PersonId;

                //    var id = cmd.ExecuteNonQuery();

                //    _locPerson.FolkeregisterAdresse.AdresseId = adresse.AdresseId;
                //}
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

        public void UpdateCurrentAdresse()
        {
            try
            {
                _conn.Open();

                string updateString = @"UPDATE Adresse
                                    SET Vejnavn = @Data1, Husnummer = @Data2, Postnummer = @Data3, Bynavn = @Data4
                                    WHERE Adresse.AdresseID = @Data5";

                using (SqlCommand cmd = new SqlCommand(updateString, _conn))
                {
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data2";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data3";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data4";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data5";

                    cmd.Parameters["@Data1"].Value = _locPerson.FolkeregisterAdresse.Vejnavn;
                    cmd.Parameters["@Data2"].Value = _locPerson.FolkeregisterAdresse.Husnummer;
                    cmd.Parameters["@Data3"].Value = _locPerson.FolkeregisterAdresse.Postnummer;
                    cmd.Parameters["@Data4"].Value = _locPerson.FolkeregisterAdresse.Bynavn;
                    cmd.Parameters["@Data5"].Value = _locPerson.FolkeregisterAdresse.AdresseId;

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

        public void DeletePerson(string fornavn, string efternavn)
        {
            try
            {
                _conn.Open();

                string deleteString = @"DELETE FROM Person
                                    WHERE Person.Fornavn = @Data1 AND Person.Efternavn = @Data2";

                using (SqlCommand cmd = new SqlCommand(deleteString, _conn))
                {
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data2";

                    cmd.Parameters["@Data1"].Value = fornavn;
                    cmd.Parameters["@Data2"].Value = efternavn;

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

        //public void DeletePerson(long personId)
        //{
        //    try
        //    {
        //        _conn.Open();

        //        string deleteString = @"DELETE FROM Person
        //                            WHERE Person.PersonID = @Data1";

        //        using (SqlCommand cmd = new SqlCommand(deleteString, _conn))
        //        {
        //            cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";

        //            cmd.Parameters["@Data1"].Value = personId;

        //            var id = cmd.ExecuteNonQuery();
        //        }
        //    }
        //    finally
        //    {
        //        if (_conn != null)
        //        {
        //            _conn.Close();
        //        }
        //    }
        //}
        //public void DeleteCurrentAdresse()
        //{
        //    try
        //    {
        //        _conn.Open();

        //        string deleteString = @"DELETE FROM Adresse
        //                            WHERE Adresse.AdresseID = @Data1";

        //        using (SqlCommand cmd = new SqlCommand(deleteString, _conn))
        //        {
        //            cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";

        //            cmd.Parameters["@Data1"].Value = _locPerson.FolkeregisterAdresse.AdresseId;

        //            var id = cmd.ExecuteNonQuery();

        //            _locPerson.FolkeregisterAdresse = null;
        //        }
        //    }
        //    finally
        //    {
        //        if (_conn != null)
        //        {
        //            _conn.Close();
        //        }
        //    }
        //}

        public void DeleteAdresse(string vejnavn, string husnummer)
        {
            try
            {
                _conn.Open();

                string deleteString = @"DELETE FROM Adresse
                                    WHERE Adresse.Vejnavn = @Data1 AND Adresse.Husnummer = @Data2";

                using (SqlCommand cmd = new SqlCommand(deleteString, _conn))
                {
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data2";

                    cmd.Parameters["@Data1"].Value = vejnavn;
                    cmd.Parameters["@Data2"].Value = husnummer;

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

        //public void DeleteAdresse(long adresseId)
        //{
        //    try
        //    {
        //        _conn.Open();

        //        string deleteString = @"DELETE FROM Adresse
        //                            WHERE Adresse.AdresseID = @Data1";

        //        using (SqlCommand cmd = new SqlCommand(deleteString, _conn))
        //        {
        //            cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";

        //            cmd.Parameters["@Data1"].Value = adresseId;

        //            var id = cmd.ExecuteNonQuery();
        //        }
        //    }
        //    finally
        //    {
        //        if (_conn != null)
        //        {
        //            _conn.Close();
        //        }
        //    }
        //}
        //public void DeleteCurrentTelefon()
        //{
        //    try
        //    {
        //        _conn.Open();

        //        string deleteString = @"DELETE FROM Telefon
        //                            WHERE Telefon.PersonID = @Data1";

        //        using (SqlCommand cmd = new SqlCommand(deleteString, _conn))
        //        {
        //            cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";

        //            cmd.Parameters["@Data1"].Value = _locPerson.PersonId;

        //            var id = cmd.ExecuteNonQuery();
        //        }
        //    }
        //    finally
        //    {
        //        if (_conn != null)
        //        {
        //            _conn.Close();
        //        }
        //    }
        //}

        //public void DeleteTelefon(string telefonnummer)
        //{
        //    try
        //    {
        //        _conn.Open();

        //        string deleteString = @"DELETE FROM Telefon
        //                            WHERE Telefon.Telefonnummer = @Data1";

        //        using (SqlCommand cmd = new SqlCommand(deleteString, _conn))
        //        {
        //            cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";

        //            cmd.Parameters["@Data1"].Value = telefonnummer;

        //            var id = cmd.ExecuteNonQuery();
        //        }
        //    }
        //    finally
        //    {
        //        if (_conn != null)
        //        {
        //            _conn.Close();
        //        }
        //    }
        //}

        public void DeleteTelefon(long telefonId)
        {
            try
            {
                _conn.Open();

                string deleteString = @"DELETE FROM Telefon
                                    WHERE Telefon.TelefonID = @Data1";

                using (SqlCommand cmd = new SqlCommand(deleteString, _conn))
                {
                    cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";

                    cmd.Parameters["@Data1"].Value = telefonId;

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
                    Console.WriteLine($"{rdr["PersonID"],-5:D3}{rdr["Fornavn"],-15}{rdr["Mellemnavn"],-15}{rdr["Efternavn"],-15}{rdr["AdresseID"],-10:D3}");
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
                Console.Write(String.Format("{0,-" + ((10 / 2) + (title.Length / 2)) + "}", title));
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