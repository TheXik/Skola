﻿#nullable enable

using System;
using System.IO;
using System.Text;
using TextJustification;

namespace WordReader
{
    public interface ITokenReader
    {
        Token GetNextToken();
    }

    public record class DebugPrinterTokenReaderDecorator(ITokenReader Reader) : ITokenReader
    {
        public Token GetNextToken()
        {
            var token = Reader.GetNextToken();
            Console.WriteLine(token);
            return token;
        }
    }
    


    public class WordReaderByWords : ITokenReader
    {
        private readonly TextReader _reader;
        private bool _isEof = false;
        private bool _lineEnded = false;
        private readonly StringBuilder _wordBuilder = new StringBuilder();
        public WordReaderByWords(TextReader reader)
        {
            _reader = reader;
        }

        private static bool IsDelimiter(char currentChar)
        {
            if (currentChar == '\t' || currentChar == ' ')
            {
                return true;
            }
            return false;
        }

        public Token GetNextToken()
        {
            if (_isEof) // end of file flag  --> create a EoF token
            {
                return new Token(TokenType.EndOfInput);
            }

            if (_lineEnded) // end of line flag  --> create a EoL token
            {
                _lineEnded = false;
                return new Token(TokenType.EndOfLine);
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

                if (IsDelimiter(currChar)) // if there is a delimiter the token is returned with current builded word
                {
                    if (_wordBuilder.Length > 0)
                    {
                        return new Token(TokenType.Word, _wordBuilder.ToString());
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
                return new Token(TokenType.Word, _wordBuilder.ToString());
            }
            return new Token(TokenType.EndOfInput);
        }

        private Token HandleNewline()
        {
            if (_wordBuilder.Length > 0)
            {
                // if there was a word right next to \n char then create token of the word without \n
                // and set flag for newline so the token of EoL will be returned next time
                
                _lineEnded = true;
                return new Token(TokenType.Word, _wordBuilder.ToString());
            }

            return new Token(TokenType.EndOfLine);
        }

        
    }


    public class ParagraphDetectingTokenReader : ITokenReader
    {
        private readonly ITokenReader _reader;
        private int _repeatedEoL = 0;
        private bool _hasParagraph = false;
        public ParagraphDetectingTokenReader(ITokenReader reader)
        {
            _reader = reader;
        }

        public Token GetNextToken()
        {
            var token = _reader.GetNextToken();
            switch (token.Type)
            {
                case TokenType.EndOfInput:
                    return token;

                case TokenType.Word:
                    _repeatedEoL = 0;
                    _hasParagraph = true;
                    return token;

                case TokenType.EndOfLine:
                    _repeatedEoL++;
                    if (_repeatedEoL > 1 && _hasParagraph)
                    {
                        _hasParagraph = false;
                        _repeatedEoL = 0;
                        return new Token(TokenType.EndOfParagraph);
                    }
                    return token;

                default:
                    return token;
            }
        }
    }
}