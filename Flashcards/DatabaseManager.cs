using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace flashcards
{
    internal class DatabaseManager
    {

        internal static void CheckDatabase()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;";
            try
            {

                SqlConnection conn = new SqlConnection(connectionString);
                using (conn)
                {
                    conn.Open();
                    var tableCmd = conn.CreateCommand();
                    tableCmd.CommandText =
                        $@"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Quiz')
                           BEGIN
                             CREATE DATABASE Quiz;
                           END;
                         ";
                    tableCmd.ExecuteNonQuery();
                    conn.Close();

                    Console.WriteLine("Good to go");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
