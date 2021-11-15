using flashcards.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashcards
{
    public class FlashcardsController
    {
        public static readonly string connectionString =
            "Server=(localdb)\\MSSQLLocalDB; Initial Catalog=quizDb; Integrated Security=true;";
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

        internal static void DeleteFlashcard(List<FlashcardsWithStack> list)
        {
            int flashcardIdOnView = UserCommands.GetIdForDeleteFlashcard();
            int flashcardId = list.Select(x => x.Id).ElementAt(flashcardIdOnView - 1);

            SqlConnection conn = new(connectionString);

            using (conn)
            {
                conn.Open();
                var tableCmd = conn.CreateCommand();
                tableCmd.CommandText =
                    $"DELETE FROM flashcard WHERE Id = {flashcardId}";
                tableCmd.ExecuteNonQuery();
                conn.Close();
            }

            Console.WriteLine("\n\nYour flashcard was successfully deleted.\n\n");

        }
    }
}
