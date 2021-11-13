using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ConsoleTableExt;
using flashcards.Models;
using Flashcards;
using Microsoft.Data.SqlClient;

namespace flashcards
{
    public class StacksController
    {
        public static readonly string connectionString =
            "Server=(localdb)\\MSSQLLocalDB; Initial Catalog=quizDb; Integrated Security=true;";

        public static void ManageStack()
        {
            var stacks = GetStacks();
            TableVisualisationEngine.ShowTable(stacks, null);
            Console.WriteLine("\nType the id of the stack you'd like to manage.\n");

            string stackIdString = UserCommands.GetIdForManageStack();

            int stackId = Int32.Parse(stackIdString);
            GetStackById(stackId);

            UserCommands.ManageStackMenu(stackId);
        }

        internal static List<Stack> GetStacks()
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = "SELECT * FROM stack";

            List<Stack> stacks = new();

            SqlDataReader reader = tableCmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    stacks.Add(
                        new Stack
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                }
            }
            else
            {
                Console.WriteLine("\n\nNo rows found.\n\n");
            }

            reader.Close();

            return stacks;
        }

        internal static void GetStackById(int id)
        {
            using var connection = new SqlConnection(connectionString);
            Stack stack = new();

            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                @$"SELECT f.Id, s.Name as stackname, f.Question, f.Answer
                       FROM flashcard f
                       LEFT JOIN stack s
                       ON s.Id = f.StackId
                       WHERE s.Id = 1; ";

            List<StackWithFlashcards> cards = new();

            SqlDataReader reader = tableCmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cards.Add(
                        new StackWithFlashcards
                        {
                            Id = reader.GetInt32(0),
                            StackName = reader.GetString(1),
                            Answer = reader.GetString(2),
                            Question = reader.GetString(3)
                        });
                }
            }
            else
            {
                Console.WriteLine("\n\nNo rows found.\n\n");
            }

            Console.WriteLine("\n\n");

            string stackName = cards.FirstOrDefault().StackName;
            string tableTitle = $"{id} - {stackName}";

            reader.Close();

            TableVisualisationEngine.ShowTable(cards, tableTitle);
        }

        internal static void CreateStack()
        {
            Stack stack = new();

            Console.WriteLine("\n\nPlease Enter Stack Name\n\n");
            stack.Name = Console.ReadLine();

            SqlConnection conn = new(connectionString);

            using (conn)
            {
                conn.Open();
                var tableCmd = conn.CreateCommand();
                tableCmd.CommandText =
                    $@"INSERT INTO stack (Name) VALUES ('{stack.Name}')";
                tableCmd.ExecuteNonQuery();
                conn.Close();
            }

            var stackId = GetStackId();

            Console.WriteLine("\n\nYour flashcards stack was successfully created.\n\n");
            CreateFlashcard(stackId, stack.Name);
        }

        internal static void DeleteStack(int stackId)
        {
            SqlConnection conn = new(connectionString);

            using (conn)
            {
                conn.Open();
                var tableCmd = conn.CreateCommand();
                tableCmd.CommandText =
                    $"DELETE FROM stack WHERE Id = ('{stackId}')";
                tableCmd.ExecuteNonQuery();
                conn.Close();
            }

            Console.WriteLine("\n\nYour flashcards stack was successfully deleted.\n\n");
        }
        private static int GetStackId()
        {
            SqlConnection conn = new(connectionString);

            conn.Open();
            SqlCommand comm = new("SELECT IDENT_CURRENT('stack')", conn);
            int id = Convert.ToInt32(comm.ExecuteScalar());
            conn.Close();
            return id;

        }

        internal static void CreateFlashcard(int stackId, string name)
        {
            Console.WriteLine($"\n\nHi there! I'm creating a flashcard for stack {stackId} - {name}\n\n");

            bool createFlashcard = true;

            while (createFlashcard)
            {
                Flashcard flashcard = new();

                Console.WriteLine("\n\nPlease Enter Question:\n\n");
                flashcard.Question = Console.ReadLine();

                Console.WriteLine("\n\nPlease Enter Answer:\n\n");
                flashcard.Answer = Console.ReadLine();

                SqlConnection conn = new(connectionString);

                using (conn)
                {
                    conn.Open();
                    var tableCmd = conn.CreateCommand();
                    tableCmd.CommandText =
                        $@"INSERT INTO flashcard (question, answer, stackId) VALUES ('{flashcard.Question}', '{flashcard.Answer}', '{stackId}')";
                    tableCmd.ExecuteNonQuery();

                    Console.WriteLine(
                        $"\n\nWould you like to create another flashcard for stack {stackId} - {name}\n\n? (Y/N)\n\n");
                    
                    string anotherFlashcard = Console.ReadLine();

                    while (anotherFlashcard != "Y" && anotherFlashcard != "N")
                    {
                        Console.WriteLine(
                            $"\n\nPlease choose Y/N\n\n?");
                        anotherFlashcard = Console.ReadLine();

                        if (anotherFlashcard == "Y" || anotherFlashcard == "N")
                            return;
                        
                    }

                    if (anotherFlashcard == "N")
                        createFlashcard = false;
                  
                }
            }
        }
    }
}

