using System;
using System.Collections.Generic;

#nullable enable

namespace WordProcessor
{
    public class WordCounter
    {
        public int count { get; set; }

        public WordCounter()
        {
            count = 0;
        }

        public void IncrementCount()
        {
            count++;
        }


    }

    public class ColumnCounter
    // this class will count the correct table column by the name of the header 
    {
        public long columnCount { get; set; }
        public string columnName{ get; set; }
        
        private int _columnIndex = 0;
        public const string NonExistentColumnMessage = "Non-existent Column Name";
        public const string InvalidIntegerMessage = ("Invalid Integer Value");
        

        public ColumnCounter(string ColumnNameInputed)
        {
            columnName = ColumnNameInputed;
        }

        public bool CountSelectedRow(string[] currentRow)
        {
            try
            {
                string currentRowCount = currentRow[_columnIndex];
                int convertedCount = Convert.ToInt32(currentRowCount); // try to convert to int
                
                columnCount += convertedCount;
                return false; // no error
            }
            catch (FormatException)
            {
                Console.WriteLine(InvalidIntegerMessage);
                return true; 
            }
            catch (OverflowException)
            {
                Console.WriteLine(InvalidIntegerMessage);
                return true;
            }
        }

        public bool GetIndexOfColumn(string[] line)
        // gets index of the column to count
        {
            foreach (var column in line)
            {   
                if (column == columnName)
                {
                    return true;
                }
                
                _columnIndex++;
            }


            Console.WriteLine(NonExistentColumnMessage);
            return false;
        }
            
        }

    public class WordFreqCounter
    {
        private Dictionary<string, int> _wordFreq = new Dictionary<string, int>();

        public void processWord(string word)
        {
            if (_wordFreq.ContainsKey(word))
            {
                _wordFreq[word]++;
            }
            else
            {
                _wordFreq.Add(word, 1);
            }
        }

        public void PrintSortedWordFrequency()
        {
            List<string> keys = new List<string>(_wordFreq.Keys);
            keys.Sort();
            foreach (string key in keys)
            {
                Console.WriteLine(key + ": " + _wordFreq[key]);
            }
        }
    }
}