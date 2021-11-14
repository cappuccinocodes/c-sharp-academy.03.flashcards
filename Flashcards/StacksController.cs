using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ConsoleTableExt;
using flashcards.Models;
using flashcards.Models.DTOs;
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
            GetStacks();
     
            Console.WriteLine("\nType the id of the stack you'd like to manage.\n");

            string stackIdString = UserCommands.GetIdForManageStack();

            int stackId = Int32.Parse(stackIdString);

            List<FlashcardsWithStack> stack = GetStackWithCards(stackId);

            UserCommands.ManageStackMenu(stackId, stack);
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

            string[] columns = { "Id", "Name" };

            TableVisualisationEngine.ShowTable(stacks, null);

            return stacks;
        }
        internal static List<FlashcardsWithStack> GetStackWithCards(int id)
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
                       WHERE s.Id={id}";

            List<FlashcardsWithStack> cards = new();

            SqlDataReader reader = tableCmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cards.Add(
                        new FlashcardsWithStack
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

            reader.Close();

            Console.WriteLine("\n\n");
            
            string stackName = cards.FirstOrDefault()?.StackName;
            string tableTitle = $"{id} - {stackName}";

            List<FlashcardsWithStackToView> stackToView = new List<FlashcardsWithStackToView>();

            int cardIndex = 1; 
            cards.ForEach(x =>
            {
                stackToView.Add(new FlashcardsWithStackToView
                {
                    Id = cardIndex,
                    Answer = x.Answer,
                    Question = x.Question
                });

                cardIndex++;
            }); 

            TableVisualisationEngine.ShowTable(stackToView, tableTitle);

            return cards;

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
            FlashcardsController.CreateFlashcard(stackId, stack.Name);
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
    }
}

