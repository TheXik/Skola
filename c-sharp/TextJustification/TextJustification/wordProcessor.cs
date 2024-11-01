using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TextJustification;

#nullable enable

namespace WordProcessor
{
    public interface ITokenProcessor
    {
        void ProcessToken(Token token);
        void Finish();
    }

    public class WordFormater : ITokenProcessor
    {
        private readonly int _maxWidth;
        private readonly TextWriter _writer;

        private readonly List<string> _currentLineWords = new List<string>();
        private int _currentLineLength = 0;
        private bool _isEndOfParagraph = false;

        public WordFormater(int maxWidth, TextWriter writer)
        {
            _maxWidth = maxWidth;
            _writer = writer;
        }

        public void ProcessToken(Token token)
        {
            switch (token.Type)
            {
                case TokenType.Word:
                    ProcessWord(token.Value!);
                    break;

                case TokenType.EndOfLine:
                    // Ignore EndOfLine tokens, as paragraphs are detected by empty lines
                    break;

                case TokenType.EndOfParagraph:
                    _isEndOfParagraph = true;
                    WordExceededCurrentLine(finalLine: false);
                    _writer.WriteLine(); // Write an empty line to separate paragraphs
                    break;

                case TokenType.EndOfInput:
                    WordExceededCurrentLine(finalLine: true);
                    break;
            }
        }

        public void Finish()
        {
            WordExceededCurrentLine(finalLine: true);

        }

        private void ProcessWord(string word)
        {
            if (word.Length > _maxWidth)
            {
                WordExceededCurrentLine(finalLine: false);
                _writer.WriteLine(word);
                _isEndOfParagraph = false; 
                return;
            }

            if (_currentLineLength + _currentLineWords.Count + word.Length > _maxWidth)
            {
                WordExceededCurrentLine(finalLine: false);
            }

            _currentLineWords.Add(word);
            _currentLineLength += word.Length;
        }

        private void WordExceededCurrentLine(bool finalLine)
        {
            if (_currentLineWords.Count == 0)
            {
                return;
            }

            if (_currentLineWords.Count == 1 || finalLine || _isEndOfParagraph)
            {
                string line = string.Join(" ", _currentLineWords);
                _writer.WriteLine(line);
            }
            else
            {
                int totalSpaces = _maxWidth - _currentLineLength;
                int gaps = _currentLineWords.Count - 1;
                int spacesPerGap = totalSpaces / gaps;
                int extraSpaces = totalSpaces % gaps;

                var lineBuilder = new StringBuilder();
                for (int i = 0; i < _currentLineWords.Count; i++)
                {
                    lineBuilder.Append(_currentLineWords[i]);
                    if (i < gaps)
                    {
                        int spacesToAdd = spacesPerGap + (i < extraSpaces ? 1 : 0);
                        lineBuilder.Append(' ', spacesToAdd);
                    }
                }

                _writer.WriteLine(lineBuilder.ToString());
            }

            // Reset for the next line
            _currentLineWords.Clear();
            _currentLineLength = 0;
            _isEndOfParagraph = false;
        }


    }


    public class ParagraphCounter : ITokenProcessor
    {
        private int _wordCount = 0;
        

        public void ProcessToken(Token token)
        {
            switch (token.Type)
            {
                case TokenType.Word:
                    _wordCount++;
                    
                    break;

                case TokenType.EndOfLine:
                    break;
                
                case TokenType.EndOfParagraph:
                    Console.WriteLine(_wordCount);
                    _wordCount = 0;
                    break;

                case TokenType.EndOfInput:
                    Finish();
                    break;
            }
        }

        public void Finish()
        {
            if (_wordCount > 0)
            {
                Console.WriteLine(_wordCount);
            }
        }
    }
   
}