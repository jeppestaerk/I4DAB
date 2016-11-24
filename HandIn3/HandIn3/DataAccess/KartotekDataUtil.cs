using HandIn3.DataModel;
using System;
using System.Data.SqlClient;

namespace HandIn3.DataAccess
{
    public class KartotekDataUtil
    {
        SqlConnection _conn;

        public KartotekDataUtil()
        {
            _conn = new SqlConnection(@"Data Source=i4dab.ase.au.dk;Initial Catalog=E16I4DABH2Gr6;User ID=E16I4DABH2Gr6;Password=E16I4DABH2Gr6");
        }

        public void InsertPersonMedFolkeregisteradresse(Person person)
        {
            try
            {
                _conn.Open();

                if (person.FolkeregisterAdresse != null)
                {
                    string insertString = @"INSERT INTO Adresse(Vejnavn,Husnummer,Postnummer,Bynavn)
                                                    OUTPUT INSERTED.AdresseID  
                                                    VALUES (@Vejnavn, @Husnummer, @Postnummer, @Bynavn)";

                    using (SqlCommand cmd = new SqlCommand(insertString, _conn))
                    {
                        cmd.Parameters.AddWithValue("@Vejnavn", person.FolkeregisterAdresse.Vejnavn);
                        cmd.Parameters.AddWithValue("@Husnummer", person.FolkeregisterAdresse.Husnummer);
                        cmd.Parameters.AddWithValue("@Postnummer", person.FolkeregisterAdresse.Postnummer);
                        cmd.Parameters.AddWithValue("@Bynavn", person.FolkeregisterAdresse.Bynavn);

                        person.FolkeregisterAdresse.AdresseId = (long)cmd.ExecuteScalar();
                    }
                }

                if (person != null)
                {
                    string insertString = @"INSERT INTO Person(Fornavn, Mellemnavn, Efternavn, PersonType, AdresseID)
                                                    OUTPUT INSERTED.PersonID  
                                                    VALUES (@Fornavn, @Mellemnavn, @Efternavn, @PersonType, @AdresseID)";

                    using (SqlCommand cmd = new SqlCommand(insertString, _conn))
                    {
                        cmd.Parameters.AddWithValue("@Fornavn", person.Fornavn);
                        cmd.Parameters.AddWithValue("@Mellemnavn", person.Mellemnavn);
                        cmd.Parameters.AddWithValue("@Efternavn", person.Efternavn);
                        cmd.Parameters.AddWithValue("@PersonType", person.PersonType);
                        if (person.FolkeregisterAdresse != null)
                            cmd.Parameters.AddWithValue("@AdresseID", person.FolkeregisterAdresse.AdresseId);

                        person.PersonId = (long)cmd.ExecuteScalar();
                    }
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

        public void InsertTelefonTilPerson(Telefon telefon)
        {
            try
            {
                _conn.Open();

                string insertString = @"INSERT INTO Telefon(Telefonnummer, TelefonType, PersonID)
                                                    OUTPUT INSERTED.TelefonID  
                                                    VALUES (@Telefonnummer, @TelefonType, @PersonID)";

                using (SqlCommand cmd = new SqlCommand(insertString, _conn))
                {
                    cmd.Parameters.AddWithValue("@Telefonnummer", telefon.Telefonnummer);
                    cmd.Parameters.AddWithValue("@TelefonType", telefon.TelefonType);
                    cmd.Parameters.AddWithValue("@PersonID", telefon.Person.PersonId);

                    telefon.TelefonId = (long)cmd.ExecuteScalar();

                    telefon.Person.Telefon.Add(telefon);
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

        public void UpdatePerson(Person person)
        {
            try
            {
                _conn.Open();

                string updateString = @"UPDATE Person
                                    SET Fornavn = @Fornavn, Mellemnavn = @Mellemnavn, Efternavn = @Efternavn, PersonType = @PersonType, AdresseID = @AdresseID
                                    WHERE PersonID = @PersonID";

                using (SqlCommand cmd = new SqlCommand(updateString, _conn))
                {
                    cmd.Parameters.AddWithValue("@Fornavn", person.Fornavn);
                    cmd.Parameters.AddWithValue("@Mellemnavn", person.Mellemnavn);
                    cmd.Parameters.AddWithValue("@Efternavn", person.Efternavn);
                    cmd.Parameters.AddWithValue("@PersonType", person.PersonType);
                    if (person.FolkeregisterAdresse != null)
                        cmd.Parameters.AddWithValue("@AdresseID", person.FolkeregisterAdresse.AdresseId);
                    cmd.Parameters.AddWithValue("@PersonID", person.PersonId);

                    cmd.ExecuteNonQuery();
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

        public void UpdateTelefon(Telefon telefon)
        {
            try
            {
                _conn.Open();

                string updateString = @"UPDATE Telefon
                                    SET Telefonnummer = @Telefonnummer, TelefonType = @TelefonType, PersonID = @PersonID
                                    WHERE TelefonID = @TelefonID";

                using (SqlCommand cmd = new SqlCommand(updateString, _conn))
                {
                    cmd.Parameters.AddWithValue("@Telefonnummer", telefon.Telefonnummer);
                    cmd.Parameters.AddWithValue("@TelefonType", telefon.TelefonType);
                    cmd.Parameters.AddWithValue("@PersonID", telefon.Person.PersonId);
                    cmd.Parameters.AddWithValue("@TelefonID", telefon.TelefonId);

                    cmd.ExecuteNonQuery();
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
                                    WHERE Fornavn = @Fornavn AND Efternavn = @Efternavn";

                using (SqlCommand cmd = new SqlCommand(deleteString, _conn))
                {
                    cmd.Parameters.AddWithValue("@Fornavn", fornavn);
                    cmd.Parameters.AddWithValue("@Efternavn", efternavn);

                    cmd.ExecuteNonQuery();
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

        public void DeleteAdresse(string vejnavn, string husnummer)
        {
            try
            {
                _conn.Open();

                string deleteString = @"DELETE FROM Adresse
                                    WHERE Vejnavn = @Vejnavn AND Husnummer = @Husnummer";

                using (SqlCommand cmd = new SqlCommand(deleteString, _conn))
                {
                    cmd.Parameters.AddWithValue("@Vejnavn", vejnavn);
                    cmd.Parameters.AddWithValue("@Husnummer", husnummer);

                    cmd.ExecuteNonQuery();
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

        public void DeleteTelefon(string telefonnummer)
        {
            try
            {
                _conn.Open();

                string deleteString = @"DELETE FROM Telefon
                                    WHERE Telefonnummer = @Telefonnummer";

                using (SqlCommand cmd = new SqlCommand(deleteString, _conn))
                {
                    cmd.Parameters.AddWithValue("@Telefonnummer", telefonnummer);

                    cmd.ExecuteNonQuery();
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
