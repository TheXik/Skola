#nullable enable
using InputOutputParser;
using WordProcessor;
using WordReader;

namespace word_count
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // var state = new ProgramInputOutputState();
            // if (!state.InitializeFromCommandLineArgs(args))  // INITIALIZE FROMCOMMAND LINE ARGS NEED TO CHANGE TO NOT PARSE WRITER AND NUMBER OF ARGS
            // {
            //     return;
            // }
            //
            // if (state.Reader == null)
            // {
            //     return;
            // }
            // TEST COUNT READLINES CLASS
            // var reader = new WordReaderByLines(state.Reader);
            // var processor = new WordCounter();
            // while (reader.GetNextWordFromLine() != null)
            // {
            //     processor.IncrementCount();
            // }
            // Console.WriteLine(processor.count);
            
            
            // TEST COUNT READ BY CHAR CLASS
            // var reader = new WordReaderByWords(state.Reader);
            // var processor = new WordCounter();
            // while ( reader.GetNextWord()  != null)
            // {
            //     processor.IncrementCount();
            // }
            // Console.WriteLine(processor.count);
            
            
            // TEST FREQUENCY READING BY CHARS
            // var reader = new  WordReaderByWords(state.Reader);
            // var processor = new WordFreqCounter();
            // string? word;
            // while ((word = reader.GetNextWord()) != null)
            // {
            //     processor.processWord(word);
            // }
            // processor.PrintSortedWordFrequency();
            
            
            // TEST FREQUENCY READING BY LINES
            // var reader = new  WordReaderByLines(state.Reader);
            // var processor = new WordFreqCounter();
            // string? word;
            // while ((word = reader.GetNextWordFromLine()) != null)
            // {
            //     processor.processWord(word);
            // }
            // processor.PrintSortedWordFrequency();
        }
    }
}
