using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TextJustification;

#nullable enable
//https://stackoverflow.com/questions/12961868/split-and-join-c-sharp-string
namespace WordProcessor
{
    public interface ITokenProcessor
    {
        void ProcessToken(Token token);
    }

    public class WordFormater(int maxWidth, TextWriter writer) : ITokenProcessor
    {
        private readonly List<string> _currentLineWords = new List<string>();
        private int _currentLineLength = 0;
        private bool _isEndOfParagraph = false;
        private bool _finalLine = false;
        private bool _pendingEmptyLine = false; // Add this flag

        public void ProcessToken(Token token)
        {
            switch (token.Type)
            {
                case TokenType.Word:
                    
                    if (_pendingEmptyLine)
                    {
                        writer.WriteLine(); // Write the empty line before the next paragraph
                        _pendingEmptyLine = false;
                    }
                    ProcessWord(token.Value!); // silence compiler warning  | We know the word.token will not be null
                    break;
                
                
                case TokenType.EndOfLine:
                    // ignore cuz line wrapping is handled by MaxWidth not line breaks
                    break; 

                case TokenType.EndOfParagraph:
                    //Marks the end of a paragraph writes the current line if needed
                    _isEndOfParagraph = true;
                    WordExceededCurrentLine();
                    _pendingEmptyLine = true;
                    break;

                case TokenType.EndOfInput:
                    _finalLine = true;
                    WordExceededCurrentLine();
                    _pendingEmptyLine = false;
                    break;
            }
        }
        
        private void ProcessWord(string word)
        {
            //If word is larger than the max width size write it on a separate line 
            if (word.Length > maxWidth)
            {
                WordExceededCurrentLine(); 
                writer.WriteLine(word);
                _isEndOfParagraph = false; 
                return;
            }
            
            //If adding the word would exceed the maxWidth
            if (_currentLineLength + _currentLineWords.Count + word.Length > maxWidth) // dont forget to count spaces also
            {
                WordExceededCurrentLine();
            }
            // else add word to the list of words on the current line in paragraph
            _currentLineWords.Add(word);
            _currentLineLength += word.Length;
        }

        private void WordExceededCurrentLine()
        {
            if (_currentLineWords.Count == 0) // if is no word in the list it wont do anything printing is done in process word
            {
                return;
            }

            if (_currentLineWords.Count == 1 || _finalLine || _isEndOfParagraph) // if there is one word and is final line  p
            {
                
                string line = string.Join(" ", _currentLineWords); // joins all words on line and prints them together with space
                writer.WriteLine(line);
            }
            else
            {
                int totalSpaces = maxWidth - _currentLineLength;
                int gaps = _currentLineWords.Count - 1; // one space is between two words therfore --> -1
                int spacesPerGap = totalSpaces / gaps; //uniformly
                int extraSpaces = totalSpaces % gaps; // left aligned

                var lineBuilder = new StringBuilder();
                for (int i = 0; i < _currentLineWords.Count; i++)
                {
                    lineBuilder.Append(_currentLineWords[i]);
                    if (i < gaps) //if the current word is not the last word in the line
                    {
                        //  spacesPerGap behaves uniformly and extraspaces accounts for left alignment
                        int spacesToAdd = spacesPerGap + (i < extraSpaces ? 1 : 0); 
                        lineBuilder.Append(' ', spacesToAdd);
                    }
                }
                writer.WriteLine(lineBuilder.ToString());
            }

            // Reset for the next line
            _currentLineWords.Clear();
            _currentLineLength = 0;
            _isEndOfParagraph = false;
        }


    }
}