using InputOutputParser;
using WordReader;

namespace read_file
{
    public class WordReaderByLinesTests
    {
        private ProgramInputOutputState _programState;
        private WordReaderByLines _wordReader;
        private string _inputFilePath;
        private string _outputFilePath;

        [SetUp]
        public void Setup()
        {
            _programState = new ProgramInputOutputState();
            _inputFilePath = Path.GetTempFileName();
            _outputFilePath = Path.GetTempFileName();
        }

        [TearDown]
        public void TearDown()
        {
            _programState.Dispose();
            File.Delete(_inputFilePath);
            File.Delete(_outputFilePath);
        }

        [Test]
        public void WordReaderByLines_ReadsWordsFromLines()
        {
            // Arrange: Write input data to the file
            var inputText = "Hello world\nThis is a test\nAnother line";
            File.WriteAllText(_inputFilePath, inputText);

            // Initialize the program with input and output files
            string[] args = { _inputFilePath, _outputFilePath };
            _programState.InitializeFromCommandLineArgs(args);

            // Pass the reader to WordReaderByLines
            _wordReader = new WordReaderByLines(_programState.Reader);

            // Act: Read words from the lines
            var words = new System.Collections.Generic.List<string>();
            string? word;
            while ((word = _wordReader.GetNextWordFromLine()) != null)
            {
                words.Add(word);
            }

            // Assert: Check that words were read correctly
            var expectedWords = new[]
            {
                "Hello", "world", "This", "is", "a", "test", "Another", "line"
            };

            Assert.AreEqual(expectedWords.Length, words.Count);
            Assert.AreEqual(expectedWords, words.ToArray());
        }

        [Test]
        public void WordReaderByLines_ReturnsNullAtEndOfFile()
        {
            // Arrange: Write input data to the file
            var inputText = "Only one line";
            File.WriteAllText(_inputFilePath, inputText);

            // Initialize the program with input and output files
            string[] args = { _inputFilePath, _outputFilePath };
            _programState.InitializeFromCommandLineArgs(args);

            // Pass the reader to WordReaderByLines
            _wordReader = new WordReaderByLines(_programState.Reader);

            // Act: Read all words, then check for end of file
            _wordReader.GetNextWordFromLine(); // "Only"
            _wordReader.GetNextWordFromLine(); // "one"
            _wordReader.GetNextWordFromLine(); // "line"
            var endOfFileWord = _wordReader.GetNextWordFromLine(); // Should be null

            // Assert: End of file should return null
            Assert.IsNull(endOfFileWord);
        }
        
        
    }
}
