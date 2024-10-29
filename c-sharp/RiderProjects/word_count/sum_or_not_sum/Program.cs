#nullable enable
using InputOutputParser;
using WordProcessor;
using WordReader;
using WordWriter;
using System;

namespace sum_or_not_sum
{
    internal class Program
    {
        // constants
        public const string InvalidFileFormatMessage = "Invalid File Format";
        

        static void Main(string[] args)
        {
            var state = new ProgramInputOutputState();

            if (!state.InitializeFromCommandLineArgs(args))
            {
                return;
            }

            if (state.Reader == null || state.Writer == null)
            {
                return;
            }
            var reader = new WordReaderByLines(state.Reader); // initialize reader

            string columnNameToCount = args[2]; // takes the word that we should count by from args

            var processor = new ColumnCounter(columnNameToCount); // initialize columncounter that takes as a parameter name of the column

            string[]? header = reader.GetNextSplittedLine(); // reads the header/(the first line) of the file and checks whether it exists
            if (header == null || header.Length == 0)
            {
                Console.WriteLine(InvalidFileFormatMessage);
                return;
            }

            int wordCountInLine = header.Length; // stores word count of the header and pass it to column processor to get the index of the column that it should be counting

            if (!processor.GetIndexOfColumn(header)) // Finds the first occurrence of the word if there are more of them prints nonexistent column name 
            {
                return;
            }

            string[]? line;
            while ((line = reader.GetNextSplittedLine()) != null)
            {
                // this loop will read new lines until it reaches an invalid line or end of the file 
                if (line.Length == 0 || wordCountInLine != line.Length) 
                {
                    Console.WriteLine(InvalidFileFormatMessage);
                    return;
                }
                // this will process the line and count the correct word(word that is in the selected name of the column) from the line
                if (processor.CountSelectedRow(line)) 
                {
                    return;
                }
            }

            var writer = new Writer(state.Writer); // initializes writer 
            writer.WriteLineToFile(columnNameToCount); // write the name of the column to file

            for (int i = 0; i < columnNameToCount.Length; i++)
            {
                writer.WriteCharToLine('-'); // write separator line
            }

            writer.WriteLineToFile(""); // write a new line
            writer.WriteLineToFile(processor.columnCount.ToString()); // write the column count
            state.Dispose();
        }
    }
}
