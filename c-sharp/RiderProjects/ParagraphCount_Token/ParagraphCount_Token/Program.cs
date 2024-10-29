#nullable enable
using InputOutputParser;
using WordProcessor;
using WordReader;

namespace ParagraphCount_Token
{
    public enum TokenType : byte
    {
        Word,
        EoI,
        EoL
    }

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

            if (state.Reader == null )
            {
                return;
            }
            
            

            var reader = new WordReaderByWords(state.Reader); // Initialize reader
            var processor = new ParagraphCounter();
            processAllWords(reader,processor);

            state.Dispose();
        }

        public static void processAllWords(ITokenReader reader, ITokenProcessor processor)
        {
            while (true)
            {
                Token token = reader.GetNextToken();
                processor.ProcessToken(token);
                if (token.Type == TokenType.EoI)
                {
                    break;
                }
            }
        }
    }
}