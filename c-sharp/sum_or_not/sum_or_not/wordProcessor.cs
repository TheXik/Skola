using System;
using System.Collections.Generic;
using System.IO;
using sum_or_not;

#nullable enable

namespace WordProcessor
{
    public interface ITokenProcessor
    {
        void ProcessToken(Token token);
        void Finish();
        
    }
    
    

    public class ColumnCounter : ITokenProcessor
    {
        private readonly string _columnName;
        private readonly TextWriter _writer;
        private int _columnIndex = -1;
        private int _lineWordCount = 0;
        private int _columnCount = 0;
        private bool _isHeaderProcessed = false;
        private readonly string _invalidFileFormatMessage = "Invalid File Format";
        private readonly string _nonExistentColumnMessage = "Non-existent Column Name";
        private readonly string _invalidIntegerMessage = "Invalid Integer Value";
        private bool _hasErrorOccurred = false;

        public ColumnCounter(string columnName, TextWriter writer)
        {
            _columnName = columnName;
            _writer = writer;
        }

        public void ProcessToken(Token token)
        {
            if (_hasErrorOccurred)
                return;

            if (!_isHeaderProcessed)
            {
                ProcessHeader(token);
            }
            else
            {
                ProcessRow(token);
            }
        }

        private void ProcessHeader(Token token)
        {
            switch (token.Type)
            {
                case TokenType.Word:
                    if (token.Word == _columnName && _columnIndex == -1)
                    {
                        _columnIndex = _lineWordCount;
                    }
                    _lineWordCount++;
                    break;

                case TokenType.EoL:
                case TokenType.EoI:
                    if (_lineWordCount == 0)
                    {
                        Console.WriteLine(_invalidFileFormatMessage);
                        _hasErrorOccurred = true;
                    }
                    else if (_columnIndex == -1)
                    {
                        Console.WriteLine(_nonExistentColumnMessage);
                        _hasErrorOccurred = true;
                    }
                    else
                    {
                        _isHeaderProcessed = true;
                    }
                    break;
            }
        }

        private int _currentWordCount = 0;
        private string? _currentTargetWord = null;

        private void ProcessRow(Token token)
        {
            switch (token.Type)
            {
                case TokenType.Word:
                    if (_currentWordCount == _columnIndex)
                    {
                        _currentTargetWord = token.Word;
                    }
                    _currentWordCount++;
                    break;

                case TokenType.EoL:
                case TokenType.EoI:
                    if (_currentWordCount == 0 && token.Type == TokenType.EoI)
                    {
                        // End of file reached with no more data
                        return;
                    }

                    if (_currentWordCount != _lineWordCount)
                    {
                        Console.WriteLine(_invalidFileFormatMessage);
                        _hasErrorOccurred = true;
                        return;
                    }

                    if (_currentTargetWord != null && ProcessValue(_currentTargetWord))
                    {
                        _hasErrorOccurred = true;
                        return;
                    }

                    _currentWordCount = 0;
                    _currentTargetWord = null;

                    if (token.Type == TokenType.EoI)
                    {
                        // End of input
                        return;
                    }
                    break;
            }
        }

        private bool ProcessValue(string value)
        {
            try
            {
                int convertedCount = Convert.ToInt32(value);
                _columnCount += convertedCount;
                return false; // No error
            }
            catch (FormatException)
            {
                Console.WriteLine(_invalidIntegerMessage);
                return true;
            }
            catch (OverflowException)
            {
                Console.WriteLine(_invalidIntegerMessage);
                return true;
            }
        }

        public void Finish()
        {
            if (!_hasErrorOccurred)
            {
                _writer.WriteLine(_columnName); // Write column name

                // Write separator line
                for (int i = 0; i < _columnName.Length; i++)
                {
                    _writer.Write('-');
                }

                _writer.WriteLine(); // New line after separator
                _writer.WriteLine(_columnCount.ToString()); // Write the final count
            }
            _writer.Flush();
        }
    }
   
}