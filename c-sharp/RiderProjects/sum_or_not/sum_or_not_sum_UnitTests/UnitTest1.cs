using System;
using System.IO;
using sum_or_not;
namespace sum_or_not_sum_UnitTests;
/*
public class SumOrNotSumUnitTests
{
    
    [Fact]
    public void NoArgumentsTest()
    {
        //arrange
        var args = new[] { "" };
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        Program.Main(args);

        // Assert
        Assert.Equal("Argument Error\n", output.ToString());
    }
    
    [Fact]
    public void WrongNumberOfArgumentsTest()
    { 
        //arrange
        var args = new[] { "input.txt", "output.txt" };
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
        var args = new[] { "nonExistent.txt", "output.txt","columnName" };
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        Program.Main(args);

        // Assert
        Assert.Equal("File Error\n", output.ToString());
    }
    
    [Fact]
    public void NonExistentColumnNameTest()
    {
        //arrange
        var existentText = """
                           column1 column2 column3 column4 column5
                           """;
        
        
        var args = new[] { "tempFileTest.txt", "output.txt", "NonExistentColumnName" };
        
        File.WriteAllText("tempFileTest.txt", existentText);
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        Program.Main(args);

        // Assert
        Assert.Equal("Non-existent Column Name\n", output.ToString());
    }
    
    [Fact]
    public void EmptyFileTest()
    {
        //arrange
        var existentText = """ """;
        
        
        var args = new[] { "tempFileTest.txt", "output.txt", "NonExistentColumnName" };
        
        File.WriteAllText("tempFileTest.txt", existentText);
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        Program.Main(args);

        // Assert
        Assert.Equal("Invalid File Format\n", output.ToString());
    }
    
    [Fact]
    public void emptyLineTest()
    {
        //arrange
        var existentText = """
                           mesic   zbozi       typ         prodejce    mnozstvi    cena    trzba
                           leden   brambory    tuzemske    Bartak      10895       12      130740
                           
                           leden   brambory    vlastni     Celestyn    15478       10      154780
                           leden   jablka      dovoz       Adamec      1321        30      39630
                           """;
        
        
        var args = new[] { "tempFileTest.txt", "output.txt", "mnozstvi" };
        
        File.WriteAllText("tempFileTest.txt", existentText);
        var output = new StringWriter();
        Console.SetOut(output);
    
        // Act
        Program.Main(args);
    
        // Assert
        Assert.Equal("Invalid File Format\n", output.ToString());
        
    }
    
    [Fact]
    public void WrongNumberOfWordsOnEachLineTest ()
    {
        //arrange
        var existentText = """
                           mesic   zbozi       typ         prodejce    mnozstvi    cena    trzba
                           leden   brambory    tuzemske    Bartak      10895       12      130740
                           leden   brambory    vlastni  SLOVO_NAVIC   Celestyn    15478       10      154780
                           leden   jablka      dovoz       Adamec      1321        30      39630
                           """;
        
        
        var args = new[] { "tempFileTest.txt", "output.txt", "mnozstvi" };
        
        File.WriteAllText("tempFileTest.txt", existentText);
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        Program.Main(args);

        // Assert
        Assert.Equal("Invalid File Format\n", output.ToString());
        
    }
    
    
    [Fact]
    public void WrongNumberOfWordsOnEachLineTest2 ()
    {
        //arrange
        var existentText = """
                           mesic   zbozi       typ         prodejce    mnozstvi    cena    trzba
                           leden   brambory    tuzemske    Bartak      10895       12      130740   SLOVO_NAVIC
                           leden   brambory    vlastni     Celestyn    15478       10      154780
                           leden   jablka      dovoz       Adamec      1321        30      39630
                           """;
        
        
        var args = new[] { "tempFileTest.txt", "output.txt", "mnozstvi" };
        
        File.WriteAllText("tempFileTest.txt", existentText);
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        Program.Main(args);

        // Assert
        Assert.Equal("Invalid File Format\n", output.ToString());
        
    }
    [Fact]
    public void WrongNumberOfWordsOnEachLineTest3 ()
    {
        //arrange
        var existentText = """
                           mesic   zbozi       typ         prodejce    mnozstvi    cena    trzba
                           leden   brambory    tuzemske    Bartak      10895       12      130740
                           leden   brambory    vlastni     Celestyn    15478       10      154780
                           leden   jablka      dovoz       Adamec      1321        30      39630 SLOVO_NAVIC
                           """;
        
        
        var args = new[] { "tempFileTest.txt", "output.txt", "mnozstvi" };
        
        File.WriteAllText("tempFileTest.txt", existentText);
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        Program.Main(args);

        // Assert
        Assert.Equal("Invalid File Format\n", output.ToString());
        
    }
    
    
    [Fact]
    public void InvalidIntegerValueTest ()
    {
        //arrange
        var existentText = """
                           mesic   zbozi       typ         prodejce    mnozstvi    cena    trzba
                           leden   brambory    tuzemske    Bartak      10895       12      130740
                           leden   brambory    vlastni     Celestyn    15478       10      154780
                           leden   jablka      dovoz       Adamec      1321        30      39630
                           """;
        
        
        var args = new[] { "tempFileTest.txt", "output.txt", "prodejce" };
        
        File.WriteAllText("tempFileTest.txt", existentText);
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        Program.Main(args);

        // Assert
        Assert.Equal("Invalid Integer Value\n", output.ToString());
    }
    
    [Fact]
    public void InvalidIntegerValueTest2 ()
    {
        //arrange
        var existentText = """
                           mesic   zbozi       typ         prodejce    mnozstvi    cena    trzba
                           leden   brambory    tuzemske    Bartak      10895       12      130740
                           leden   brambory    vlastni     Celestyn    BAD_VALUE       10      154780
                           leden   jablka      dovoz       Adamec      1321        30      39630
                           """;
        
        
        var args = new[] { "tempFileTest.txt", "output.txt", "mnozstvi" };
        
        File.WriteAllText("tempFileTest.txt", existentText);
        var output = new StringWriter();
        Console.SetOut(output);

        // Act
        Program.Main(args);

        // Assert
        Assert.Equal("Invalid Integer Value\n", output.ToString());
        
    }
    
    [Fact]
    public void CorrectOutputValueTest()
    {
        // Arrange
        var existentText = """
                           prodejce    mnozstvi    cena    
                           Bartak      10000       12      
                           Celestyn    20000       10      
                           Adamec      30000       30                                                             
                           """;
    
        var expectedOutputText = """
                                 cena
                                 ----
                                 52
                                 
                                 """;
        
        var args = new[] { "tempFileTest.txt", "output.txt", "cena" };

        File.WriteAllText("tempFileTest.txt", existentText);

        // Act
        Program.Main(args); 
        var actualOutputText = File.ReadAllText("output.txt");

        // Assert
        Assert.Equal(expectedOutputText, actualOutputText);
    }
    
    [Fact]
    public void CorrectOutputValueTest2()
    {
        // Arrange
        var existentText = """
                           mnozstvi    cena    trzba
                           10000       12      130740
                           20000       10      154780
                           -30000        30      -100
                           """;
    
        var expectedOutputText = """
                                 mnozstvi
                                 --------
                                 0
                                 
                                 """;
        
        var args = new[] { "tempFileTest.txt", "output.txt", "mnozstvi" };

        File.WriteAllText("tempFileTest.txt", existentText);

        // Act
        Program.Main(args); 
        var actualOutputText = File.ReadAllText("output.txt");

        // Assert
        Assert.Equal(expectedOutputText, actualOutputText);
    }
    
    [Fact]
    public void CorrectOutputValueTest3()
    {
        // Arrange
        var existentText = """
                           mnozstvi    cena    trzba    
                           10000       12      130740     
                           20000       10      154780     
                           30000        30      39630       
                           """;
    
        var expectedOutputText = """
                                 mnozstvi
                                 --------
                                 60000

                                 """;
        
        var args = new[] { "tempFileTest.txt", "output.txt", "mnozstvi" };

        File.WriteAllText("tempFileTest.txt", existentText);

        // Act
        Program.Main(args); 
        var actualOutputText = File.ReadAllText("output.txt");

        // Assert
        Assert.Equal(expectedOutputText, actualOutputText);
    }
    
    [Fact]
    public void MultipleEqualColumnsTest()
    {
        // Arrange
        var existentText = """
                           mnozstvi    cena    trzba   mnozstvi  
                           10000       12      130740  1   
                           20000       10      154780     2
                           30000        30      39630       3
                           """;
    
        var expectedOutputText = """
                                 mnozstvi
                                 --------
                                 60000

                                 """;
        
        var args = new[] { "tempFileTest.txt", "output.txt", "mnozstvi" };

        File.WriteAllText("tempFileTest.txt", existentText);

        // Act
        Program.Main(args); 
        var actualOutputText = File.ReadAllText("output.txt");

        // Assert
        Assert.Equal(expectedOutputText, actualOutputText);
    }

    
}

*/