#nullable enable
using InputOutputParser;
using WordProcessor;
using WordReader;
using System;

namespace sum_or_not
{
    public enum TokenType : byte {Word, EoI,EoL,EoP}

    public struct Token
    {
        public TokenType Type;
        public string? Word;
    }
    public class Program
    {
        public static void Main(string[] args)
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

            string columnNameToCount = args[2]; // Takes the column name from args

            var processor = new ColumnCounter(columnNameToCount, state.Writer); // Initialize ColumnCounter

            var reader = new WordReaderByWords(state.Reader); // Initialize reader
            
            processAllWords(reader, processor);

            state.Dispose();
        }

        public static void processAllWords(ITokenReader reader, ITokenProcessor processor)
        {
            Token token;

            while (true)
            {
                token = reader.GetNextToken();
                processor.ProcessToken(token);

                if (token.Type == TokenType.EoI)
                {
                    break;
                }
            }
            processor.Finish();
        }
    }
}