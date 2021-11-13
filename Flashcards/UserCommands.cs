using Flashcards;
using System;
using System.Collections.Generic;
using System.Linq;
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
                        Study.GetUsercommand();
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
            var stacks = StacksController.GetStacks();
            TableVisualisationEngine.ShowTable(stacks, null);

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
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 2.\n");
                        break;
                }
            }
        }

        internal static void ManageStackMenu(int? id)
        {
            int stackId = (int)id;
            var stacks = StacksController.GetStacks();
            TableVisualisationEngine.ShowTable(stacks, null);

            bool closeArea = false;
            while (closeArea == false)
            {
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\nType 0 to close application.");
                Console.WriteLine("Type 1 to return to Main Menu");
                Console.WriteLine("Type 2 to delete stack");
                Console.WriteLine("Type 3 to add a flashcard");
                Console.WriteLine("Type 4 to delete a flashcard");

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
                        StacksController.DeleteStack(stackId);
                        break;
                    case 3:
                        StacksController.ManageStack();
                        break;
                    default:
                        Console.WriteLine("\nInvalid Command. Please type a number from 0 to 2.\n");
                        break;
                }
            }
        }
    }
}
