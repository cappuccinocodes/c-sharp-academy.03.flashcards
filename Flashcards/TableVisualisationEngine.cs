using ConsoleTableExt;
using System;
using System.Collections;
using System.Collections.Generic;

namespace flashcards
{
    public class TableVisualisationEngine
    {
        public static void ShowTable<T>(List<T> tableData, string? tableName) where T : class
        {
                Console.WriteLine("\n\n");

                ConsoleTableBuilder
                    .From(tableData)
                    .WithTitle(tableName)
                    .ExportAndWriteLine();
                Console.WriteLine("\n\n");
            }
        }
    }
}