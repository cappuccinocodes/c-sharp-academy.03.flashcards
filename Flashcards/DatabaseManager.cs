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
                        $@"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'quizDb')
                           BEGIN
                             CREATE DATABASE quizDb;
                           END;
                         ";
                    tableCmd.ExecuteNonQuery();
                    conn.Close();
                }

                CreateTable();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        internal static void CreateTable()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB; Initial Catalog=quizDb; Integrated Security=true;";
            SqlConnection conn = new SqlConnection(connectionString);
            using (conn)
            {
                conn.Open();
                var tableCmd = conn.CreateCommand();

                tableCmd.CommandText =
                    $@" CREATE TABLE stack (
	                      Id int IDENTITY(1,1) NOT NULL,
	                      Name varchar(100) NOT NULL UNIQUE,
	                      PRIMARY KEY (Id)
                         );
                      ";
                tableCmd.ExecuteNonQuery();

                tableCmd.CommandText =
                    $@" CREATE TABLE flashcard (
                          Id int NOT NULL PRIMARY KEY,
                          Question varchar(30) NOT NULL,
                          Answer varchar(30) NOT NULL,
                          StackId int NOT NULL 
                            FOREIGN KEY 
                            REFERENCES stack(Id) 
                            ON DELETE CASCADE 
                            ON UPDATE CASCADE
                         );
                      ";
                tableCmd.ExecuteNonQuery();

                conn.Close();

                Console.WriteLine("Good to Go");
            }
        }
    }
}


