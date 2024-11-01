#nullable enable

namespace ConsoleApp1 {
    
    public class ProgramInputOutputState : IDisposable {
        public const string ArgumentErrorMessage = "Argument Error";
        public const string FileErrorMessage = "File Error";

        public TextReader? Reader { get; private set; }


        public bool InitializeFromCommandLineArgs(string[] args) {
            if (args.Length != 1) {
                Console.WriteLine(ArgumentErrorMessage);
                return false;
            }

            try {
                Reader = new StreamReader(args[0]);
            } catch (IOException) {
                Console.WriteLine(FileErrorMessage);
                return false;
            } catch (UnauthorizedAccessException) {
                Console.WriteLine(FileErrorMessage);
                return false;
            } catch (ArgumentException) {
                Console.WriteLine(FileErrorMessage);
                return false;
            }

            
            return true;
        }

        public void Dispose() {
            Reader?.Dispose();
        }
    }

    public class LineCounter {
        private TextReader _reader;
        private TextWriter _writer;

        public LineCounter(TextReader reader, TextWriter writer) {
            _reader = reader;
            _writer = writer;
        }

        public void Execute() {
            int lineCount = 0;
            while (_reader.ReadLine() is not null) {
                lineCount++;
            }

            _writer.WriteLine(lineCount);
        }
    }
}