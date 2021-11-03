using System;
using flashcards;
using Microsoft.Data.SqlClient;

namespace Flashcards
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseManager.CheckDatabase();

            Console.WriteLine("\n\nMAIN MENU");
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("\nType 0 to Close Application.");
            Console.WriteLine("Type 1 to Manage Flashcards.");
            Console.WriteLine("Type 2 to Study.");
        }
    }
}
