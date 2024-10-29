#nullable enable

namespace ConsoleApp1
{
    public  class Program
    {
        public static void Main(string[] args)
        {
            var state = new ProgramInputOutputState();
          
            if (!state.InitializeFromCommandLineArgs(args)) // check for invalid input
            {
                return;
            }

            if (state.Reader == null)
            {
                return;
            }
            
            // initialization
            var reader = new WordReaderByLines(state.Reader); 
            var processor = new WordCounter();
                
            string[]? line;
            while ((line = reader.GetNextSplittedLine()) != null)
            {
                processor.CountLine(line);

                    
                if (processor.CheckIfParagraphEnd(line)) // chceck if paragraph has ended 
                {
                    Console.WriteLine(processor.Count); // print word count if paragraph is at end
                    processor.ResetCount(); // reset count
                }
            }
            if (processor.Count != 0) Console.WriteLine(processor.Count); // print last paragraph if it exists
        }
    }
}
