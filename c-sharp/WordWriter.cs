using System;
using System.IO;
#nullable enable

namespace WordWriter
{
    public class Writer
    {
        private  TextWriter _writer { get; set; }

        public Writer(TextWriter writer)
        {
            _writer = writer;
        }

        public void WriteLineToFile(string lineToWrite)
        {
            _writer.WriteLine(lineToWrite);
        }

        public void WriteCharToLine(char charToWrite)
        {
            _writer.Write(charToWrite);
        }
        
        
        
    }
  
}