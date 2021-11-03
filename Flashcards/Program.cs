using System;
using Microsoft.Data.SqlClient;

namespace Flashcards
{
    class Program
    {
        static void Main(string[] args)
        {
            QueryDb(); 
        }

        static internal void QueryDb()
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
                        $@"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'MyTestDataBase')
                           BEGIN
                             CREATE DATABASE MyTestDataBase;
                           END;
                         ";
                    tableCmd.ExecuteNonQuery();
                    conn.Close();

                    Console.WriteLine("Table Created");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
           
        }
    }
}
