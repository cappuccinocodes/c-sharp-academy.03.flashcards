using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using flashcards.Models;

namespace flashcards
{
    public class StudyController
    {
        internal static void CreateStudySession()
        {
            List<Stack> stacks = StacksController.GetStacks();
            var id = UserCommands.GetIntegerInput("Which stack would you like to study?");

            while (!stacks.Any(x => x.Id == id))
            {
                Console.WriteLine("\nThere's no stack wit this id.");
                id = UserCommands.GetIntegerInput("Which stack would you like to study?");
            }

            List<FlashcardsWithStack> stack = StacksController.GetStackWithCards(id);

            stack.ForEach(x =>
            {
                Console.WriteLine(x.Question);
            });

        }
    }
}
