using System;
using System.IO;
#nullable enable

namespace InputOutputParser {
    
    public class ProgramInputOutputState : IDisposable {
        public const string ArgumentErrorMessage = "Argument Error";
        public const string FileErrorMessage = "File Error";

        public TextReader? Reader { get; private set; }
        public TextWriter? Writer { get; private set; }

        public bool InitializeFromCommandLineArgs(string[] args) {
            if (args.Length != 3) {
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

            try {
                Writer = new StreamWriter(args[1]);
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
            Writer?.Dispose();
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