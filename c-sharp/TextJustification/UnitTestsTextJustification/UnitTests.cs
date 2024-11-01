using InputOutputParser;
using TextJustification;
using WordReader;
using WordProcessor;
namespace UnitTestsTextJustification;

public class UnitTests
{
    // ------------------------------------- ARGUMENTS UNIT TESTS -------------------------------------
    [Fact]
    public void NotEnoughArgumentsTest()
    {
        // Arrange
        var args = new string[] { "input.txt", "output.txt" }; // Only two arguments instead of three
        var programState = new ProgramInputOutputState();
        StringWriter output = new StringWriter();
        Console.SetOut(output);
        
        // Act
        bool result = programState.InitializeFromCommandLineArgs(args);
        
        // Assert
        Assert.False(result);
        Assert.Equal("Argument Error\n", output.ToString());
    }
    
    [Fact]
    public void MoreArgumentsThenNeededTest()
    {
        // Arrange
        var args = new string[] { "input.txt", "output.txt" ,"20", "more.txt"}; // four arguments instead of three
        var programState = new ProgramInputOutputState();
        StringWriter output = new StringWriter();
        Console.SetOut(output);
        
        // Act
        bool result = programState.InitializeFromCommandLineArgs(args);
        
        // Assert
        Assert.False(result);
        Assert.Equal("Argument Error\n", output.ToString());
    }
    
    [Fact]
    public void LineWidthArgumentIsNotANumberTest()
    {
        // Arrange
        var args = new string[] { "input.txt", "output.txt" ,"NOTANUMBER"}; //correct num of arguments but line width is not a number 
        var programState = new ProgramInputOutputState();
        StringWriter output = new StringWriter();
        Console.SetOut(output);
        
        // Act
        bool result = programState.InitializeFromCommandLineArgs(args);
        
        // Assert
        Assert.False(result);
        Assert.Equal("Argument Error\n", output.ToString());
    }

    [Fact]
    public void LineWidthArgumentIsInvalidNumberTest()
    {
        // Arrange
        var args = new string[]
            { "input.txt", "output.txt", "-20" }; //correct num of arguments but line width is not a number 
        var programState = new ProgramInputOutputState();
        StringWriter output = new StringWriter();
        Console.SetOut(output);

        // Act
        bool result = programState.InitializeFromCommandLineArgs(args);

        // Assert
        Assert.False(result);
        Assert.Equal("Argument Error\n", output.ToString());


    }
    
    [Fact]
    public void LineWidthArgumentIsZeroTest()
    {
        // Arrange
        var args = new string[] { "input.txt", "output.txt" ,"0"}; //correct num of arguments but line width is not a number 
        var programState = new ProgramInputOutputState();
        StringWriter output = new StringWriter();
        Console.SetOut(output);
        
        // Act
        bool result = programState.InitializeFromCommandLineArgs(args);
        
        // Assert
        Assert.False(result);
        Assert.Equal("Argument Error\n", output.ToString());
    }
    
    // ------------------------------------- FIlE UNIT TESTS -------------------------------------
    [Fact]
    public void FileDosntExistsTest()
    {
        // Arrange 
        var args = new string[] { "NONEXISTENTSFILE.txt", "OUTPUTNOTEXISTS.txt" ,"10"}; //correct num of arguments but line width is not a number 
        var programState = new ProgramInputOutputState();
        StringWriter output = new StringWriter();
        Console.SetOut(output);
        
        // Act
        bool result = programState.InitializeFromCommandLineArgs(args);
        
        // Assert
        Assert.False(result);
        Assert.Equal("File Error\n", output.ToString());
    }
    
    // ------------------------------------- PROGRAM UNIT TESTS -------------------------------------
   // DONT CHANGE THE WORDS IN THE FILE
   // EACH PARAGRAPH FORMATED SEPARATELY output paragraphs are always delimited by a single empty line
   //THE LAST LINE MUST END WITH \n
   // LINE AS MANY WORDS AS THERE CAN BE WITHOUT CHANGING ORDER
   //  All words must be separated by at least one space.
   // IMPORTANT HOW TO MANAGE SPACE 
   // If some there is still some empty space remaining to achieve the maximum text width,
    // this space is uniformly distributed among the word gaps by adding space characters.
    //If the extra spaces cannot be distributed uniformly, they should be added from the left, 
    
   //The final line of each paragraph should be aligned to the left (all words should be separated by exactly one)
   
   //For every line of the output file there can be no white-space characters between the last character of the last word and the line break character.
   // IF WORD LENGTH > MAX_WIDTH  PRINTNI CELE SLOVO NA JEDEN RIADOK 
   //If a line contains only one word, this word should be aligned to the left.
   
   [Fact]
   public void EmptyFileTest()
   {
       // Arrange
       var input = """ """;
       var expectedOutput = """
                            
                            """;

       using (StringReader reader = new StringReader(input))
       using (StringWriter writer = new StringWriter())
       {
           
           int maxWidth = 17;

           //Arrange
           var wordReader = new WordReaderByWords(reader);
           var paragraphDetectingReader = new ParagraphDetectingTokenReader(wordReader);
           var processor = new WordFormater(maxWidth, writer);

           // Act
           Program.ProcessAllWords(paragraphDetectingReader, processor);
           string actualOutput = writer.ToString().TrimEnd();

           // Assert
           Assert.Equal(expectedOutput, actualOutput);
       }
   }
   
   [Fact]
   public void TextJustificationInitialTest()
   {
       // Arrange
       var input = """If a train station is where the train stops, what is a work station?""";
       var expectedOutput = """
                            If     a    train
                            station  is where
                            the  train stops,
                            what  is  a  work
                            station?
                            """;

       using (StringReader reader = new StringReader(input))
       using (StringWriter writer = new StringWriter())
       {
           
           int maxWidth = 17;

           //Arrange
           var wordReader = new WordReaderByWords(reader);
           var paragraphDetectingReader = new ParagraphDetectingTokenReader(wordReader);
           var processor = new WordFormater(maxWidth, writer);

           // Act
           Program.ProcessAllWords(paragraphDetectingReader, processor);
           string actualOutput = writer.ToString().TrimEnd();

           // Assert
           Assert.Equal(expectedOutput, actualOutput);
       }
   }
   
   [Fact]
   public void MultipleParagraphsTest()
   {
       // Arrange
       var input = """
                   If a train station is 
                   
                   the train stops, what is a work station?
                   
                   """;
       var expectedOutput = """
                            If     a    train
                            station is
                            
                            the  train stops,
                            what  is  a  work
                            station?
                            """;

       using (StringReader reader = new StringReader(input))
       using (StringWriter writer = new StringWriter())
       {
           
           int maxWidth = 17;

           // Arrange
           var wordReader = new WordReaderByWords(reader);
           var paragraphDetectingReader = new ParagraphDetectingTokenReader(wordReader);
           var processor = new WordFormater(maxWidth, writer);

           // Act
           Program.ProcessAllWords(paragraphDetectingReader, processor);
           string actualOutput = writer.ToString().TrimEnd();

           // Assert
           Assert.Equal(expectedOutput, actualOutput);
       }
   }
   
   [Fact]
   public void WordBiggerThanLineWidthTest()
   {
       // Arrange
       var input = """
                   LUKAS
                   LUKAS_CEZ_RIADOK_PRINTNE_SA_CELE_TOTO
                   
                   LUKAS
                   """;
       var expectedOutput = """
                            LUKAS
                            LUKAS_CEZ_RIADOK_PRINTNE_SA_CELE_TOTO
                            
                            LUKAS
                            
                            """;

       using (StringReader reader = new StringReader(input))
       using (StringWriter writer = new StringWriter())
       {
           
           int maxWidth = 5;

           //Arrange
           var wordReader = new WordReaderByWords(reader);
           var paragraphDetectingReader = new ParagraphDetectingTokenReader(wordReader);
           var processor = new WordFormater(maxWidth, writer);

           // Act
           Program.ProcessAllWords(paragraphDetectingReader, processor);
           string actualOutput = writer.ToString();

           // Assert
           Assert.Equal(expectedOutput, actualOutput);
       }
   }
   
   [Fact]
   public void WordBiggerThanLineWidthInNormalInputTest()
   {
       // Arrange
       var input = """
                   If a train station is where the train stops, what is a work TOTO_JE_VELMI_DLHE_SLOVO_ABY_PRESIAHLO_MAXIMALNU_DLZKU
                   station?
                   
                   """;
       var expectedOutput = """
                            If     a    train
                            station  is where
                            the  train stops,
                            what  is  a  work
                            TOTO_JE_VELMI_DLHE_SLOVO_ABY_PRESIAHLO_MAXIMALNU_DLZKU
                            station?
                            
                            """;

       using (StringReader reader = new StringReader(input))
       using (StringWriter writer = new StringWriter())
       {
           
           int maxWidth = 17;

           //Arrange
           var wordReader = new WordReaderByWords(reader);
           var paragraphDetectingReader = new ParagraphDetectingTokenReader(wordReader);
           var processor = new WordFormater(maxWidth, writer);

           // Act
           Program.ProcessAllWords(paragraphDetectingReader, processor);
           string actualOutput = writer.ToString();

           // Assert
           Assert.Equal(expectedOutput, actualOutput);
       }
   }
   
   [Fact]
   public void SmallLineWidthTest()
   {
       // Arrange
       var input = "TOTO_JE_VELMI_DLHE_SLOVO_ABY_PRESIAHLO_MAXIMALNU_DLZKU\nstation?\n";
       var expectedOutput = "TOTO_JE_VELMI_DLHE_SLOVO_ABY_PRESIAHLO_MAXIMALNU_DLZKU\nstation?\n";

       using (StringReader reader = new StringReader(input))
       using (StringWriter writer = new StringWriter())
       {
           
           int maxWidth = 1;

           //Arrange
           var wordReader = new WordReaderByWords(reader);
           var paragraphDetectingReader = new ParagraphDetectingTokenReader(wordReader);
           var processor = new WordFormater(maxWidth, writer);

           // Act
           Program.ProcessAllWords(paragraphDetectingReader, processor);
           string actualOutput = writer.ToString();

           // Assert
           Assert.Equal(expectedOutput, actualOutput);
       }
   }
   
   [Fact]
   public void AligningUniformlyTextTest()
   {
       // Arrange
       var input = """aa bb cc d a a DLHSSIESLOVO""";
       var expectedOutput = """
                            aa bb cc d
                            a        a
                            DLHSSIESLOVO
                            
                            """;

       using (StringReader reader = new StringReader(input))
       using (StringWriter writer = new StringWriter())
       {
           
           int maxWidth = 10;

           //Arrange
           var wordReader = new WordReaderByWords(reader);
           var paragraphDetectingReader = new ParagraphDetectingTokenReader(wordReader);
           var processor = new WordFormater(maxWidth, writer);

           // Act
           Program.ProcessAllWords(paragraphDetectingReader, processor);
           string actualOutput = writer.ToString();

           // Assert
           Assert.Equal(expectedOutput, actualOutput);
       }
   }
   
   [Fact]
   public void AligningUniformlyTextTest2()
   {
       // Arrange
       var input = """a a a NEJAKEDLHESLOVO""";
       var expectedOutput = """
                            a   a   a
                            NEJAKEDLHESLOVO
                            
                            """;

       using (StringReader reader = new StringReader(input))
       using (StringWriter writer = new StringWriter())
       {
           
           int maxWidth = 9;

           //Arrange
           var wordReader = new WordReaderByWords(reader);
           var paragraphDetectingReader = new ParagraphDetectingTokenReader(wordReader);
           var processor = new WordFormater(maxWidth, writer);

           // Act
           Program.ProcessAllWords(paragraphDetectingReader, processor);
           string actualOutput = writer.ToString();

           // Assert
           Assert.Equal(expectedOutput, actualOutput);
       }
   }
   
   [Fact]
   public void AligningLeftTextTest()
   {
       // Arrange
       var input = """AAAAA BBBBB C NEJAKEDLHESLOVOVELMIDLHESLOVODLHSIEAKO20ZNAKOV""";
       var expectedOutput = """
                            AAAAA     BBBBB    C
                            NEJAKEDLHESLOVOVELMIDLHESLOVODLHSIEAKO20ZNAKOV
                            
                            """;
       // IT DOESNT SEEM LIKE IT BUT IN    (AAAAA_____BBBBB____C)   there is one more space 

       using (StringReader reader = new StringReader(input))
       using (StringWriter writer = new StringWriter())
       {
           int maxWidth = 20;

           // Initialize readers and processor for left alignment
           var wordReader = new WordReaderByWords(reader);
           var paragraphDetectingReader = new ParagraphDetectingTokenReader(wordReader);
           var processor = new WordFormater(maxWidth, writer);

           // Act
           Program.ProcessAllWords(paragraphDetectingReader, processor);
           string actualOutput = writer.ToString();

           // Assert
           Assert.Equal(expectedOutput, actualOutput);
       }
   }

   [Fact]
   public void OneWordAlignedToLeftOnOneLineTest()
   {
       // Arrange
       var input = """
                   TEST1


                   TESTCISLO2
                   
                   """;
       var expectedOutput = "TEST1\n\nTESTCISLO2\n";
       // IT DOESNT SEEM LIKE IT BUT IN    (AAAAA_____BBBBB____C)   there is one more space 

       using (StringReader reader = new StringReader(input))
       using (StringWriter writer = new StringWriter())
       {
           int maxWidth = 10;

           // Initialize readers and processor for left alignment
           var wordReader = new WordReaderByWords(reader);
           var paragraphDetectingReader = new ParagraphDetectingTokenReader(wordReader);
           var processor = new WordFormater(maxWidth, writer);

           // Act
           Program.ProcessAllWords(paragraphDetectingReader, processor);
           string actualOutput = writer.ToString();

           // Assert
           Assert.Equal(expectedOutput, actualOutput);
       }

   }
   
   
   [Fact]
   public void FinalLineCheckTest()
   {
       // Arrange
       var input = """
                   FINAL LINE CHECKING_WHETHER_IT_PRINTS_WHOLE_WORD
                   """;
       var expectedOutput = """
                            FINAL LINE
                            CHECKING_WHETHER_IT_PRINTS_WHOLE_WORD
                            
                            """;
       // IT DOESNT SEEM LIKE IT BUT IN    (AAAAA_____BBBBB____C)   there is one more space 

       using (StringReader reader = new StringReader(input))
       using (StringWriter writer = new StringWriter())
       {
           int maxWidth = 10;

           // Initialize readers and processor for left alignment
           var wordReader = new WordReaderByWords(reader);
           var paragraphDetectingReader = new ParagraphDetectingTokenReader(wordReader);
           var processor = new WordFormater(maxWidth, writer);

           // Act
           Program.ProcessAllWords(paragraphDetectingReader, processor);
           string actualOutput = writer.ToString();

           // Assert
           Assert.Equal(expectedOutput, actualOutput);
       }

   }
   
   [Fact]
   public void LastLineEndsWithLineBreakTest()
   {
       // Arrange
       var input = """TEST""";
       var expectedOutput = "TEST\n";
       // IT DOESNT SEEM LIKE IT BUT IN    (AAAAA_____BBBBB____C)   there is one more space 

       using (StringReader reader = new StringReader(input))
       using (StringWriter writer = new StringWriter())
       {
           int maxWidth = 10;

           // Initialize readers and processor for left alignment
           var wordReader = new WordReaderByWords(reader);
           var paragraphDetectingReader = new ParagraphDetectingTokenReader(wordReader);
           var processor = new WordFormater(maxWidth, writer);

           // Act
           Program.ProcessAllWords(paragraphDetectingReader, processor);
           string actualOutput = writer.ToString();

           // Assert
           Assert.Equal(expectedOutput, actualOutput);
       }

   }
   
   [Fact]
   public void LastLineEndsWithLineBreakTest2()
   {
       // Arrange
       var input = """TESTTESTTE ST""";
       var expectedOutput = "TESTTESTTE\nST\n";
       // IT DOESNT SEEM LIKE IT BUT IN    (AAAAA_____BBBBB____C)   there is one more space 

       using (StringReader reader = new StringReader(input))
       using (StringWriter writer = new StringWriter())
       {
           int maxWidth = 10;

           // Initialize readers and processor for left alignment
           var wordReader = new WordReaderByWords(reader);
           var paragraphDetectingReader = new ParagraphDetectingTokenReader(wordReader);
           var processor = new WordFormater(maxWidth, writer);

           // Act
           Program.ProcessAllWords(paragraphDetectingReader, processor);
           string actualOutput = writer.ToString();

           // Assert
           Assert.Equal(expectedOutput, actualOutput);
       }

   }
   
   [Fact]
   public void LastLineEndsWithLineBreakTest3()
   {
       // Arrange
       var input = "TESTTESTTE AAAAAAAAAAAAAAAAAAAAAAAAAAAA\n\n";
       var expectedOutput = "TESTTESTTE\nAAAAAAAAAAAAAAAAAAAAAAAAAAAA\n";
       // IT DOESNT SEEM LIKE IT BUT IN    (AAAAA_____BBBBB____C)   there is one more space 

       using (StringReader reader = new StringReader(input))
       using (StringWriter writer = new StringWriter())
       {
           int maxWidth = 10;

           // Initialize readers and processor for left alignment
           var wordReader = new WordReaderByWords(reader);
           var paragraphDetectingReader = new ParagraphDetectingTokenReader(wordReader);
           var processor = new WordFormater(maxWidth, writer);

           // Act
           Program.ProcessAllWords(paragraphDetectingReader, processor);
           string actualOutput = writer.ToString();

           // Assert
           Assert.Equal(expectedOutput, actualOutput);
       }

   }
   
   
   
}
