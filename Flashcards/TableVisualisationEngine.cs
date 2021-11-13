using ConsoleTableExt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace flashcards
{
    public class TableVisualisationEngine
    {
        public static void ShowTable<T>(List<T> tableData, [AllowNull]string tableName) where T : class
        {
            if (tableName == null)
                tableName = "";

            Console.WriteLine("\n\n");

            ConsoleTableBuilder
                .From(tableData)
                .WithTitle(tableName)
                .ExportAndWriteLine();
            Console.WriteLine("\n\n");
        }
    }
}
