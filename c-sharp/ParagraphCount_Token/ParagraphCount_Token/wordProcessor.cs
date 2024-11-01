using System;
using System.Collections.Generic;
using System.IO;
using ParagraphCount_Token;

#nullable enable

namespace WordProcessor
{
    public interface ITokenProcessor
    {
        void ProcessToken(Token token);
        void Finish();

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