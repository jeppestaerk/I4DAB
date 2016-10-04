using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandIn3TestApplication
{
    class HandIn3TestApplication
    {
        private static string hest = "Helle";

        // call methods that demo SqlCommand capabilities
        static void Main()
        {
            SqlCommandDemo scd = new SqlCommandDemo();

            Console.WriteLine();
            Console.WriteLine("Persons Before Insert");
            Console.WriteLine("------------------------");

            // use ExecuteReader method
            scd.ReadData();

            // use ExecuteNonQuery method for Insert
            scd.Insertdata();
            Console.WriteLine();
            Console.WriteLine("Persons After Insert");
            Console.WriteLine("------------------------------");

            scd.ReadData();

            // use ExecuteNonQuery method for Update
            scd.UpdateData();

            Console.WriteLine();
            Console.WriteLine("Persons After Update");
            Console.WriteLine("------------------------------");

            scd.ReadData();

            // use ExecuteNonQuery method for Delete
            scd.DeleteData();

            Console.WriteLine();
            Console.WriteLine("Persons After Delete");
            Console.WriteLine("------------------------------");

            scd.ReadData();

            // use ExecuteScalar method
            int numberOfRecords = scd.GetNumberOfRecords();

            Console.WriteLine();
            Console.WriteLine("Number of Records: {0}", numberOfRecords);
        }
    }
}
