using System;
using System.IO;
using Paragraph_Count_Task04;

namespace TestProject1;

public class UnitTest1
{
    [Fact]
    public void oneParagraph()
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
}