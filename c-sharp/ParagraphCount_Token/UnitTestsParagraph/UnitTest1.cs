using ParagraphCount_Token;
namespace UnitTestsParagraph;

public class UnitTest1
{
    
    [Fact]
    public void WrongNumberOfArgumentsTest()
    { 
        //arrange
        var args = new string[] {  };
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        Program.Main(args);

        // Assert
        Assert.Equal("Argument Error\n", output.ToString());
    }
    
    [Fact]
    public void MoreWrongNumberOfArgumentsTest()
    { 
        //arrange
        var args = new string[] {  "jeden.txt","dva.txt","tri.txt"};
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        Program.Main(args);

        // Assert
        Assert.Equal("Argument Error\n", output.ToString());
    }
    
    [Fact]
    public void FileNoneExistsTest()
    {
        //arrange
        var args = new[] { "nonExistent.txt"};
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        Program.Main(args);

        // Assert
        Assert.Equal("File Error\n", output.ToString());
    }
    

    [Fact]
    public void EmptyFileTest()
    {
        // Arrange
        var args = new[] { "empty.txt" };
        
        var input = """ """;
        File.WriteAllText("empty.txt", input);
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        Program.Main(args);

        // Assert
        Assert.Equal(string.Empty, output.ToString());
    }


    
    [Fact]
    public void twoParagraphs()
    {
        //arrange
        var args = new[] { "random.txt" };
        var input = """
                    If a train station is where the train stops, what is a work station?

                    A work station?
                    Yes!

                    """;
        File.WriteAllText("random.txt", input);
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        Program.Main(args);

        // Assert
        Assert.Equal("14\n4\n", output.ToString());
    }
    
    [Fact]
    public void oneParagraph()
    {
        //arrange
        var args = new[] { "random.txt" };
        var input = """
                    If a train station is where the train stops, what is a work station?

                    
                    
                    
                    
                    """;
        File.WriteAllText("random.txt", input);
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        Program.Main(args);

        // Assert
        Assert.Equal("14\n", output.ToString());
    }
    
    [Fact]
    public void oneParagraph1()
    {
        //arrange
        var args = new[] { "random.txt" };
        var input = """If a train station is where the train stops, what is a work station?""";
        File.WriteAllText("random.txt", input);
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        Program.Main(args);

        // Assert
        Assert.Equal("14\n", output.ToString());
    }
    
    [Fact]
    public void continuousOneParagraphTest()
    {
        //arrange
        var args = new[] { "random.txt" };
        var input = """
                    one
                    one
                    one
                    one
                    still one paragraph
                    """;
        File.WriteAllText("random.txt", input);
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        Program.Main(args);

        // Assert
        Assert.Equal("7\n", output.ToString());
    }
    
    [Fact]
    public void MultipleParagraphsWithMixedWhitespaceTest()
    {
        // Arrange
        var args = new[] { "random.txt" };
        var input = """
                    prvy paragraf 


                    druhy paragraf

                    treti paragraf
                    
                    
                    
                    
                    
                    stvrty paragraphg
                    """;
        File.WriteAllText("random.txt", input);
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        Program.Main(args);

        // Assert
        Assert.Equal("2\n2\n2\n2\n", output.ToString());
    }
    
    [Fact]
    public void FirstWhiteSpaceThenWordsTest()
    {
        // Arrange
        var args = new[] { "random.txt" };
        var input = """
                    
                    
                    
                    
                    prvy paragraf 
                    
                    druhy paragraf

                    treti paragraf
                    """;
        File.WriteAllText("random.txt", input);
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        Program.Main(args);

        // Assert
        Assert.Equal("2\n2\n2\n", output.ToString());
    }
    [Fact]
    public void EmptyParagraphsTest()
    {
        // Arrange
        var args = new[] { "random.txt" };
        var input = """

                    
                    
                    
                    
                    
                    
                    
                    """;
        File.WriteAllText("random.txt", input);
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        Program.Main(args);

        // Assert
        Assert.Equal("", output.ToString());
    }
    
    [Fact]
    public void WhiteSpace()
    {
        //arrange
        var args = new[] { "random.txt" };
        var input = """
                    If station? 
                     
                    A work station? 
                    Yes! 
                      
                    """;    
        File.WriteAllText("random.txt", input);
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        Program.Main(args);

        // Assert
        Assert.Equal("2\n4\n", output.ToString());
    }
    
}
