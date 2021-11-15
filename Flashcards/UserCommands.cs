using flashcards.Models;
using Flashcards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace flashcards
{
    public class UserCommands
    {
        internal static void MainMenu()
        {
            bool closeApp = false;
            while (closeApp == false)
            {
                Console.WriteLine("\n\nMAIN MENU");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\nType 0 to Close Application.");
                Console.WriteLine("Type 1 to Manage Flashcards.");
                Console.WriteLine("Type 2 to Study.");

                string commandInput = Console.ReadLine();
                if (string.IsNullOrEmpty(commandInput) || !int.TryParse(commandInput, out _))
                {
                    Console.WriteLine("\nInvalid Command. Please type a number from 0 to 2.\n");
                    continue;
                }

                int command = Convert.ToInt32(commandInput);

                switch (command)
                {
                    case 0:
                        closeApp = true;
                        break;
                    case 1:
                        StacksMenu();
                        break;
                    case 2:
                        StudyController.GetUsercommand();
                        break;
                    default:
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 2.\n");
                        break;
                }
            }
        }

        internal static string GetIdForManageStack()
        {
            string stackIdString = Console.ReadLine();

            while (string.IsNullOrEmpty(stackIdString) || !int.TryParse(stackIdString, out _))
            {
                Console.WriteLine("\nInvalid Command. Please type a numeric value");
                stackIdString = Console.ReadLine();

                if (!string.IsNullOrEmpty(stackIdString) || int.TryParse(stackIdString, out _))
                {
                    return stackIdString;
                }
            }

            return stackIdString;
        }

        internal static void StacksMenu()
        {
            Console.WriteLine("\n\nFlashcard Stacks Area\n");
            StacksController.GetStacks();

            bool closeArea = false;
            while (closeArea == false)
            {
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\nType 0 to Close Application.");
                Console.WriteLine("Type 1 to Return to Main Menu");
                Console.WriteLine("Type 2 to Create New Stack");
                Console.WriteLine("Type 3 to Manage a Stack");

                string commandInput = Console.ReadLine();

                while (string.IsNullOrEmpty(commandInput) || !int.TryParse(commandInput, out _))
                {
                    Console.WriteLine("\nInvalid Command. Please type a number from 0 to 3.\n");
                    commandInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(commandInput) || int.TryParse(commandInput, out _))
                    {
                        continue;
                    }
                }

                int command = Convert.ToInt32(commandInput);

                switch (command)
                {
                    case 0:
                        closeArea = true;
                        break;
                    case 1:
                        MainMenu();
                        break;
                    case 2:
                        StacksController.CreateStack();
                        break;
                    case 3:
                        StacksController.ManageStack();
                        break;
                    default:
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 3.\n");
                        break;
                }
            }
        }

        internal static void ManageStackMenu(int id, List<FlashcardsWithStack> stack)
        {
            int stackId = (int)id;

            bool closeArea = false;
            while (closeArea == false)
            {
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\nType 0 to close application.");
                Console.WriteLine("Type 1 to return to Main Menu");
                Console.WriteLine("Type 2 to change stack's name");
                Console.WriteLine("Type 3 to delete stack");
                Console.WriteLine("Type 4 to add a flashcard");
                Console.WriteLine("Type 5 to delete a flashcard");
                Console.WriteLine("Type 6 to update a flashcard");

                string commandInput = Console.ReadLine();

                while (string.IsNullOrEmpty(commandInput) || !int.TryParse(commandInput, out _))
                {
                    Console.WriteLine("\nInvalid Command. Please type a number from 0 to 4.\n");
                    commandInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(commandInput) || int.TryParse(commandInput, out _))
                    {
                        continue;
                    }
                }

                int command = Convert.ToInt32(commandInput);

                switch (command)
                {
                    case 0:
                        closeArea = true;
                        break;
                    case 1:
                        MainMenu();
                        break;
                    case 2:
                        StacksController.UpdateStackName(stackId);
                        break;
                    case 3:
                        StacksController.DeleteStack(stackId);
                        StacksController.GetStacks();
                        break;
                    case 4:
                        FlashcardsController.CreateFlashcard(stackId, null);
                        StacksController.GetStackWithCards(stackId);
                        break;
                    case 5:
                        FlashcardsController.DeleteFlashcard(stack);
                        StacksController.GetStackWithCards(stackId);
                        break;
                    case 6:
                        FlashcardsController.UpdateFlashcard(stack);
                        StacksController.GetStackWithCards(stackId);
                        break;
                    default:
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 6.\n");
                        break;
                }
            }
        }

        internal static string GetStringInput(string message)
        {
            Console.WriteLine(message);
            string name = Console.ReadLine();

            while (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("\nInvalid name");
                name = Console.ReadLine();

                if (!string.IsNullOrEmpty(name))
                {
                    return name;
                }
            }

            return name;
        }

        internal static string GetBinaryInput(string message)
        {
            Console.WriteLine(message);
            string option = Console.ReadLine();

            while (string.IsNullOrEmpty(option) && !option.Equals("Y") && !option.Equals("N"))
            {
                Console.WriteLine("\nInvalid option");
                option = Console.ReadLine();

                if (!string.IsNullOrEmpty(option))
                {
                    return option;
                }
            }

            return option;
        }

        internal static int GetIdForUpdateFlashcard(string message)
        {
            Console.WriteLine(message);
            string flashcardIdstring = Console.ReadLine();

            while (string.IsNullOrEmpty(flashcardIdstring) || !int.TryParse(flashcardIdstring, out _))
            {
                Console.WriteLine("\nInvalid Command. Please type a numeric value");
                flashcardIdstring = Console.ReadLine();

                if (!string.IsNullOrEmpty(flashcardIdstring) || int.TryParse(flashcardIdstring, out _))
                {
                    return Int32.Parse(flashcardIdstring);
                }
            }

            return Int32.Parse(flashcardIdstring);
        }
    }
}
