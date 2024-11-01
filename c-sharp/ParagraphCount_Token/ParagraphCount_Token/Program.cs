#nullable enable
using InputOutputParser;
using WordProcessor;
using WordReader;

namespace ParagraphCount_Token
{
    public enum TokenType : int
    {
        Word,
        EndOfInput,
        EndOfLine,
        EndOfParagraph
        
    }

    public readonly record struct Token(TokenType Type, string? Value =null)
    {
        public Token(string word) : this(TokenType.Word, word){}
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
            var paragraphDetectingReader = new ParagraphDetectingTokenReader(reader);
            var debugTokenReader = new DebugPrinterTokenReaderDecorator(paragraphDetectingReader);
            
            
            var processor = new ParagraphCounter();
            ProcessAllWords(debugTokenReader,processor);

            state.Dispose();
        }

        private static void ProcessAllWords(ITokenReader reader, ITokenProcessor processor)
        {
            while (true)
            {
                Token token = reader.GetNextToken();
                processor.ProcessToken(token);
                if (token.Type == TokenType.EndOfInput)
                {
                    break;
                }
            }
        }
    }
}