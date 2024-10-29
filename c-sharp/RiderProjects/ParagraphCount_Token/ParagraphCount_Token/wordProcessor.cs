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
        private bool _hasParagraph = false;
        private int _repeatedEoL = 0;

        public void ProcessToken(Token token)
        {
            switch (token.Type)
            {
                case TokenType.Word:
                    _wordCount++;
                    _hasParagraph = true;
                    _repeatedEoL = 0; 
                    break;

                case TokenType.EoL:
                    _repeatedEoL++;
                    if (_repeatedEoL > 1 && _hasParagraph) 
                    {
                        Console.WriteLine(_wordCount);
                        _wordCount = 0;
                        _hasParagraph = false;
                        _repeatedEoL = 0;
                    }
                    break;

                case TokenType.EoI:
                    Finish();
                    break;
            }
        }

        public void Finish()
        {
            if (_hasParagraph)
            {
                Console.WriteLine(_wordCount);
            }
        }
    }
   
}