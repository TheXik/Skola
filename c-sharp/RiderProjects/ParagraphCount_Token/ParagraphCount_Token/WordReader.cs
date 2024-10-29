#nullable enable

using System;
using System.IO;
using System.Linq;
using System.Text;
using ParagraphCount_Token;

namespace WordReader
{
    public interface ITokenReader
    {
        Token GetNextToken();
    }


    public class WordReaderByWords : ITokenReader
    {
        private readonly TextReader _reader;
        private bool _isEof = false;
        private bool _lineEnded = false;
        private readonly StringBuilder _wordBuilder = new StringBuilder();

       
        private static readonly char[] _delimiters = { '\t', ' ' };

        public WordReaderByWords(TextReader reader)
        {
            _reader = reader;
        }

        public Token GetNextToken()
        {
            if (_isEof) // end of file flag  --> create a EoF token
            {
                return new Token { Type = TokenType.EoI};
            }

            if (_lineEnded) // end of line flag  --> create a EoL token
            {
                _lineEnded = false;
                return new Token { Type = TokenType.EoL};
            }

            _wordBuilder.Clear(); // clear after each word is created

            while (true)
            {
                int result = _reader.Read(); 

                if (result == -1)
                {
                    return HandleEndOfInput();
                }

                char currChar = (char)result;

                if (currChar == '\n')
                {
                    return HandleNewline();
                }

                if (_delimiters.Contains(currChar)) // if there is a delimiter the token is returned with current builded word
                {
                    if (_wordBuilder.Length > 0)
                    {
                        return  new Token { Type = TokenType.Word, Word = _wordBuilder.ToString()};
                    }
                }
                else
                {
                    _wordBuilder.Append(currChar);
                }
            }
        }
        
        private Token HandleEndOfInput()
        {
            
            if (_wordBuilder.Length > 0) 
            {
                _isEof = true; // sets EoF flag to true
                
                // if there was a word right next to EndOFInput then return this word and in the next call return the EoF token
                return  new Token { Type = TokenType.Word, Word = _wordBuilder.ToString()};
            }
            return new Token { Type = TokenType.EoI};
        }

        private Token HandleNewline()
        {
            if (_wordBuilder.Length > 0)
            {
                // if there was a word right next to \n char then create token of the word without \n
                // and set flag for newline so the token of EoL will be returned next time
                
                _lineEnded = true;
                return  new Token { Type = TokenType.Word, Word = _wordBuilder.ToString()};
            }

            return new Token { Type = TokenType.EoL};
        }

        
    }

}