using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using flashcards.Models;
using Flashcards;

namespace flashcards
{
    public class FlashcardsController
    {
        internal static void GetUsercommand()
        {
            Console.WriteLine("\n\nFlashcards Area");
            Console.WriteLine("\nWhat would you like to do?");
            Console.WriteLine("\nType 0 to Close Application.");
            Console.WriteLine("Type 1 to Return to Main Menu");
            Console.WriteLine("Type 2 to Create New Flashcard Stack");


            string commandInput = Console.ReadLine();

            if (string.IsNullOrEmpty(commandInput))
            {
                Console.WriteLine("\nInvalid Command. Please choose an option\n");
                GetUsercommand();
            }

            int command = Convert.ToInt32(commandInput);

            switch (command)
            {
                case 0:
                    Environment.Exit(0);
                    break;
                case 1:
                    Program.StartApp();
                    break;
                case 2:
                    CreateFlashcardStack();
                    break;
                default:
                    Console.WriteLine("\nInvalid Command. Please type a number from 0 to 2.\n");
                    GetUsercommand();
                    break;
            }
        }
        internal static void CreateFlashcardStack()
        {
            Stack stack = new Stack();

            Console.WriteLine("\n\nPlease Enter Stack Name\n\n");
            stack.Name = Console.ReadLine();
        }

        internal static void CreateFlashcard()
        {
            Console.WriteLine("\n\nHi there! I'm CreateFlashcard()\n\n");
        }
    }
}
