#nullable enable
using System;
using InputOutputParser;
using WordProcessor;
using WordReader;

namespace TextJustification
{
    public enum TokenType 
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

            if (state.Reader == null || state.Writer == null)
            {
                return;
            }

             int maxwidth = state.TextWidth;
             

           
            var reader = new WordReaderByWords(state.Reader); // Initialize reader
            var paragraphDetectingReader = new ParagraphDetectingTokenReader(reader);
            var debugTokenReader = new DebugPrinterTokenReaderDecorator(paragraphDetectingReader);
            
            
            var processor = new WordFormater(maxwidth, state.Writer);
            ProcessAllWords(debugTokenReader,processor);
            state.Dispose();
            
        }

        public static void ProcessAllWords(ITokenReader reader, ITokenProcessor processor)
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
            processor.Finish();
        }
    }
}