using System;
using System.Threading;
using flashcards;
using Microsoft.Data.SqlClient;

namespace Flashcards
{
    class Program
    {
        static void Main()
        {
            DatabaseManager.CheckDatabase();
            UserCommands.MainMenu();
        }
    }
}
