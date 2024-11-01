using System;
using System.IO;
#nullable enable

namespace InputOutputParser {
    
    public class ProgramInputOutputState : IDisposable {
        public const string ArgumentErrorMessage = "Argument Error";
        public const string FileErrorMessage = "File Error";
        public int TextWidth;

        public TextReader? Reader { get; private set; }
        public TextWriter? Writer { get; private set; }
        
       
        public bool InitializeFromCommandLineArgs(string[] args) {
            if (args.Length != 3) {
                Console.WriteLine(ArgumentErrorMessage);
                return false;
            }
            
            try {
                // validating first because we want to check wheter line width is a valid number and only after that check if files exists
                TextWidth = Convert.ToInt32(args[2]); 
                if (TextWidth <= 0) {
                    Console.WriteLine(ArgumentErrorMessage);
                    return false;
                }
            } catch (FormatException) {
                Console.WriteLine(ArgumentErrorMessage);
                return false;
            } catch (OverflowException) {
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
    
}