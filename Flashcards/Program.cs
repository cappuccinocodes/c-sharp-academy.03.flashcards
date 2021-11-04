using System;
using System.Threading;
using flashcards;
using Microsoft.Data.SqlClient;

namespace Flashcards
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseManager.CheckDatabase();
            StartApp();
        }

        internal static void StartApp() {
            Console.WriteLine("\n\nMAIN MENU");
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("\nType 0 to Close Application.");
            Console.WriteLine("Type 1 to Manage Flashcards.");
            Console.WriteLine("Type 2 to Study.");

            string commandInput = Console.ReadLine();

            if (string.IsNullOrEmpty(commandInput))
            {
                Console.WriteLine("\nInvalid Command. Please choose an option\n");
                StartApp();
            }

            int command = Convert.ToInt32(commandInput);

            switch (command)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    FlashcardsController.GetUsercommand();
                    break;
                case 2:
                    Study.GetUsercommand();
                    break;
                default:
                    Console.WriteLine("\nInvalid Command. Please type a number from 0 to 2.\n");
                    StartApp();
                    break;
            }
        }
    }
}
