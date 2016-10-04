using HandIn3DataAccess.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


public class PersonkartotekDataUtil
{
    private Person locPerson;
    SqlConnection conn;

    public Person currentPerson
    {
        get { return locPerson; }
    }

    public PersonkartotekDataUtil()
    {
        conn = new SqlConnection(@"Data Source=JEPPESTAERK\SQLEXPRESS;Initial Catalog='I4DAB HandIn2';Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True");
    }

    public void setCurrentPerson(string fornavn, string efternavn)
    {
        SqlDataReader rdr = null;

        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand($"SELECT * FROM Person WHERE Fornavn = '{fornavn}' AND Efternavn = '{efternavn}'", conn);

            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine($"Navn: {rdr["Fornavn"]} {rdr["Mellemnavn"]} {rdr["Efternavn"]}");
                locPerson = new Person();
                locPerson.PersonId = (long) rdr["PersonID"];
                locPerson.Fornavn = (string) rdr["Fornavn"];
                locPerson.Mellemnavn = (string) rdr["Mellemnavn"];
                locPerson.Efternavn = (string) rdr["Efternavn"];
                locPerson.PersonType = (string) rdr["PersonType"];
                locPerson.AdresseId = (long)rdr["AdresseID"];
                break;

            }
        }
        finally
        {
            if (rdr != null)
            {
                rdr.Close();
            }

            if (conn != null)
            {
                conn.Close();
            }
        }
    }

    public void setCurrentPerson(long id)
    {
        SqlDataReader rdr = null;

        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand($"SELECT * FROM Person WHERE PersonID = '{id}'", conn);

            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine($"Navn: {rdr["Fornavn"]} {rdr["Mellemnavn"]} {rdr["Efternavn"]}");
                locPerson = new Person();
                locPerson.PersonId = (long) rdr["PersonID"];
                locPerson.Fornavn = (string) rdr["Fornavn"];
                locPerson.Mellemnavn = (string) rdr["Mellemnavn"];
                locPerson.Efternavn = (string) rdr["Efternavn"];
                locPerson.PersonType = (string) rdr["PersonType"];
                locPerson.AdresseId = (long) rdr["AdresseID"];
                break;

            }
        }
        finally
        {
            if (rdr != null)
            {
                rdr.Close();
            }

            if (conn != null)
            {
                conn.Close();
            }
        }
    }

    public void getCurrentTelefon()
    {
        SqlDataReader rdr = null;
        string selectTelefonString = @"SELECT Telefon.* 
                                        FROM Person INNER JOIN 
                                        Telefon ON Person.PersonID = Telefon.PersonID
                                        WHERE (Person.PersonID = @Data1)";

        try
        {
            conn.Open();

            using (SqlCommand cmd = new SqlCommand(selectTelefonString, conn))
            {
                cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";
                cmd.Parameters["@Data1"].Value = this.locPerson.PersonId;

                rdr = cmd.ExecuteReader();

                locPerson.Ejer = new List<Telefon>();
                Telefon locTelefon = null;

                while (rdr.Read())
                {
                    Console.WriteLine($"Telefonnummer: {rdr["Telefonnummer"]}");
                    locTelefon = new Telefon();
                    locTelefon.TelefonId = (long) rdr["TelefonID"];
                    locTelefon.Telefonnummer = (string) rdr["Telefonnummer"];
                    locTelefon.TelefonType = (string) rdr["TelefonType"];
                    locPerson.Ejer.Add(locTelefon);
                }
            }

        }
        finally
        {
            if (rdr != null)
            {
                rdr.Close();
            }

            if (conn != null)
            {
                conn.Close();
            }
        }
    }

    public void getCurrentAdresse()
    {
        SqlDataReader rdr = null;
        string selectAdresseString = @"SELECT Adresse.* 
                                        FROM Person INNER JOIN 
                                        Adresse ON Person.AdresseID = Adresse.AdresseID
                                        WHERE (Person.AdresseID = @Data1)";

        try
        {
            conn.Open();

            using (SqlCommand cmd = new SqlCommand(selectAdresseString, conn))
            {
                cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";
                cmd.Parameters["@Data1"].Value = this.locPerson.AdresseId;

                rdr = cmd.ExecuteReader();

                locPerson.FolkeregisterAdresse = new Adresse();
                Adresse locAdresse = null;

                while (rdr.Read())
                {
                    Console.WriteLine($"Adresse: {rdr["Vejnavn"]} {rdr["Husnummer"]}, {rdr["Postnummer"]} {rdr["Bynavn"]}");
                    locAdresse = new Adresse();
                    locAdresse.AdresseId = (long) rdr["AdresseID"];
                    locAdresse.Vejnavn = (string) rdr["Vejnavn"];
                    locAdresse.Husnummer = (string) rdr["Husnummer"];
                    locAdresse.Postnummer = (string) rdr["Postnummer"];
                    locAdresse.Bynavn = (string) rdr["Bynavn"];
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

            if (conn != null)
            {
                conn.Close();
            }
        }
    }

    public void InsertNewPerson(Person person)
    {
        try
        {
            conn.Open();

            string insertString = @"INSERT INTO Person(Fornavn, Mellemnavn, Efternavn, PersonType, AdresseID)
                                                    OUTPUT INSERTED.PersonID  
                                                    VALUES (@Data1, @Data2, @Data3, @Data4, @Data5)";

            using (SqlCommand cmd = new SqlCommand(insertString, conn))
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
                cmd.Parameters["@Data5"].Value = person.AdresseId;

                person.PersonId = (long)cmd.ExecuteScalar();

                this.locPerson = person;
            }
        }
        finally
        {
            if (conn != null)
            {
                conn.Close();
            }
        }
    }

    public void UpdateCurrentPerson()
    {
        try
        {
            conn.Open();

            string updateString = @"UPDATE Person
                                    SET Fornavn = @Data1, Mellemnavn = @Data2, Efternavn = @Data3, PersonType = @Data4
                                    WHERE Person.PersonID = @Data5";

            using (SqlCommand cmd = new SqlCommand(updateString, conn))
            {
                cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";
                cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data2";
                cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data3";
                cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data4";
                cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data5";

                cmd.Parameters["@Data1"].Value = this.locPerson.Fornavn;
                cmd.Parameters["@Data2"].Value = this.locPerson.Mellemnavn;
                cmd.Parameters["@Data3"].Value = this.locPerson.Efternavn;
                cmd.Parameters["@Data4"].Value = this.locPerson.PersonType;
                cmd.Parameters["@Data5"].Value = this.locPerson.PersonId;

                var id = (int) cmd.ExecuteNonQuery();
            }
        }
        finally
        {
            if (conn != null)
            {
                conn.Close();
            }
        }
    }

    public void DeleteCurrentPerson()
    {
        try
        {
            conn.Open();

            string deleteString = @"DELETE FROM Person
                                    WHERE Person.PersonID = @Data1";

            using (SqlCommand cmd = new SqlCommand(deleteString, conn))
            {
                cmd.Parameters.Add(cmd.CreateParameter()).ParameterName = "@Data1";

                cmd.Parameters["@Data1"].Value = this.locPerson.PersonId;

                var id = (int)cmd.ExecuteNonQuery();
                locPerson = null;
            }
        }
        finally
        {
            if (conn != null)
            {
                conn.Close();
            }
        }
    }

    public int GetNumberOfRecords()
    {
        int count = -1;

        try
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT count(*) FROM Person", conn);

            count = (int)cmd.ExecuteScalar();
        }
        finally
        {
            if (conn != null)
            {
                conn.Close();
            }
        }
        return count;
    }
}