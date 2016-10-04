using System;
using System.Data;
using System.Data.SqlClient;


/// <summary>
/// Demonstrates how to work with SqlCommand objects
/// </summary>
public class SqlCommandDemo
{
    SqlConnection conn;

    public SqlCommandDemo()
    {
        // Instantiate the connection
        conn = new SqlConnection(
@"Data Source=JEPPESTAERK\SQLEXPRESS;Initial Catalog='I4DAB HandIn2';Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True");
    }

    /// <summary>
    /// use ExecuteReader method
    /// </summary>
    public void ReadData()
    {
        SqlDataReader rdr = null;

        try
        {
            // Open the connection
            conn.Open();

            // 1. Instantiate a new command with a query and connection
            SqlCommand cmd = new SqlCommand("SELECT Person.Fornavn FROM Person", conn);

            // 2. Call Execute reader to get query results
            rdr = cmd.ExecuteReader();

            // print the CategoryName of each record
            while (rdr.Read())
            {
                Console.WriteLine(rdr[0]);
            }
        }
        finally
        {
            // close the reader
            if (rdr != null)
            {
                rdr.Close();
            }

            // Close the connection
            if (conn != null)
            {
                conn.Close();
            }
        }
    }

    /// <summary>
    /// use ExecuteNonQuery method for Insert
    /// </summary>
    public void Insertdata()
    {
        try
        {
            // Open the connection
            conn.Open();

            // prepare command string
            //string insertString = @"
            //    INSERT INTO Adresse(Vejnavn,Husnummer,Postnummer,Bynavn) 
            //    VALUES('Strandvejen','30B','8000','Aarhus C') 
            //    INSERT INTO Person(Fornavn, Mellemnavn, Efternavn, PersonType, AdresseID) 
            //    VALUES('Jeppe', '', 'Stærk', 'homie', (SELECT Adresse.AdresseID FROM Adresse WHERE Vejnavn = 'Strandvejen' AND Husnummer = '30B'))
            //    INSERT INTO Telefon(Telefonnummer, TelefonType, PersonID) 
            //    VALUES('50403000', 'mobil', (SELECT Person.PersonID FROM Person WHERE Fornavn = 'Jeppe' AND Efternavn = 'Stærk'))";
            //string insertString = @"
            //    INSERT INTO Adresse(Vejnavn,Husnummer,Postnummer,Bynavn) 
            //    VALUES('Strandvejen','30B','8000','Aarhus C') ";
            string insertString = @"
                INSERT INTO Person(Fornavn, Mellemnavn, Efternavn, PersonType, AdresseID) 
                VALUES('Jeppe', '', 'Stærk', 'homie', (SELECT Adresse.AdresseID FROM Adresse WHERE Vejnavn = 'Paludan mullersvej' AND Husnummer = '18'))";
            //string insertString =
            //    @"INSERT INTO Telefon(Telefonnummer, TelefonType, PersonID) 
            //    VALUES('50403000', 'mobil', (SELECT Person.PersonID FROM Person WHERE Fornavn = 'Jeppe' AND Efternavn = 'Stærk'))";



            // 1. Instantiate a new command with a query and connection
            SqlCommand cmd = new SqlCommand(insertString, conn);

            // 2. Call ExecuteNonQuery to send command
            cmd.ExecuteNonQuery();
        }
        finally
        {
            // Close the connection
            if (conn != null)
            {
                conn.Close();
            }
        }
    }

    /// <summary>
    /// use ExecuteNonQuery method for Update
    /// </summary>
    public void UpdateData()
    {
        try
        {
            // Open the connection
            conn.Open();

            // prepare command string
            string updateString = @"
                UPDATE Person
                SET Person.Fornavn = 'Jebbe'
                WHERE Person.Fornavn = 'Jeppe'";

            // 1. Instantiate a new command with command text only
            SqlCommand cmd = new SqlCommand(updateString);

            // 2. Set the Connection property
            cmd.Connection = conn;

            // 3. Call ExecuteNonQuery to send command
            cmd.ExecuteNonQuery();
        }
        finally
        {
            // Close the connection
            if (conn != null)
            {
                conn.Close();
            }
        }
    }

    /// <summary>
    /// use ExecuteNonQuery method for Delete
    /// </summary>
    public void DeleteData()
    {
        try
        {
            // Open the connection
            conn.Open();

            // prepare command string
            string deleteString = @"
                 DELETE FROM Person
                 WHERE Person.Fornavn = 'Jebbe'
                 AND Person.Efternavn = 'Stærk'";

            // 1. Instantiate a new command
            SqlCommand cmd = new SqlCommand();

            // 2. Set the CommandText property
            cmd.CommandText = deleteString;

            // 3. Set the Connection property
            cmd.Connection = conn;

            // 4. Call ExecuteNonQuery to send command
            cmd.ExecuteNonQuery();
        }
        finally
        {
            // Close the connection
            if (conn != null)
            {
                conn.Close();
            }
        }
    }

    /// <summary>
    /// use ExecuteScalar method
    /// </summary>
    /// <returns>number of records</returns>
    public int GetNumberOfRecords()
    {
        int count = -1;

        try
        {
            // Open the connection
            conn.Open();

            // 1. Instantiate a new command
            SqlCommand cmd = new SqlCommand("SELECT count(*) FROM Person", conn);

            // 2. Call ExecuteScalar to send command
            count = (int)cmd.ExecuteScalar();
        }
        finally
        {
            // Close the connection
            if (conn != null)
            {
                conn.Close();
            }
        }
        return count;
    }
}