#nullable enable
namespace ConsoleApp1
{
    public class WordCounter
    {
        private bool _paragraphStartDetected;
        public int Count { get; private set; }

        public WordCounter()
        {
            Count = 0;
        }
        public void CountLine(string[] line) // count number of word in a single
        {
            for (int i = 0; i < line.Length; i++)
            {
                Count++;
                _paragraphStartDetected = true;
            }
        }

        public bool CheckIfParagraphEnd(string[] line) //this method checks whether paragraph has ended or not
        {
            if (line.Length == 0 && _paragraphStartDetected)
            {
                _paragraphStartDetected = false; // Reset for the next paragraph
                return true;
            }

            return false; 
        }
        public void ResetCount()
        {
            Count = 0;
        }
    }
}