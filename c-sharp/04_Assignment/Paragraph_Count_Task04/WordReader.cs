#nullable enable
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace WordReader
{
    public class WordReaderByLines 
    // implementation of the WordReader that reads whole line and then splits it into words and returns it one by one
    {
        private TextReader Reader { get; set; }
        private string[] _wordsInLine = Array.Empty<string>();
        private int _currentWordIndex;

        public WordReaderByLines(TextReader textreader)
        {
            Reader = textreader;
        }
        private string[] SplitWords(string lineToSplit)
        {
            char[] delimiters = {'\n', '\t', ' ' }; // can add more delimiters if wanted to extend/change the definition of the word
            return lineToSplit.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
        }

        public string[]? GetNextSplittedLine()
        {
            var line = Reader.ReadLine();
            if (line == null)
            {
                return null;
            }
            return SplitWords(line);
        }

        public string? GetNextWordFromLine()
        // reads whole line and then splits it into words and returns it one by one
        {
            while (_currentWordIndex >= _wordsInLine.Length)
            {
                var line = Reader.ReadLine();
                if (line == null)
                {
                    return null;
                }

                _wordsInLine = SplitWords(line);
                _currentWordIndex = 0;
            }

            return _wordsInLine[_currentWordIndex++];
        }
    }

    public class WordReaderByWords
    // implemenation of the WordReader that reads character by character and returns word when it finds a delimiter
    {
        private TextReader Reader { get; set; }
        private readonly char[] _delimiters = { '\n', '\t', ' ' };

        public WordReaderByWords(TextReader textreader)
        {
            Reader = textreader;
        }

       
        public string? GetNextWord()
            // reads character by character and returns word when it finds a delimiter
        {
            var word = new StringBuilder();
            int result;

            do
            {
                result = Reader.Read();
                if (result == -1)
                {
                    return word.Length > 0 ? word.ToString() : null;
                }
                char nextChar = (char)result;
                if (!_delimiters.Contains(nextChar))
                {
                    word.Append(nextChar);
                }
            } while (!_delimiters.Contains((char)result) || word.Length == 0);
            return word.ToString();
        }
    }
}
