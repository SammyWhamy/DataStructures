namespace DataStructures.Tests;

public class ListAccessTests
{
    private readonly List<string> _list;
    public ListAccessTests()
    {
        _list = new List<string>("one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten");
    }

    [Theory]
    [InlineData(0, "one")]
    [InlineData(1, "two")]
    [InlineData(2, "three")]
    [InlineData(3, "four")]
    [InlineData(4, "five")]
    [InlineData(5, "six")]
    [InlineData(6, "seven")]
    [InlineData(7, "eight")]
    [InlineData(8, "nine")]
    [InlineData(9, "ten")]
    public void IndexerAccess(int index, string expected)
    {
        Assert.Equal(expected, _list[index]);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(11)]
    [InlineData(3219)]
    public void IndexerGetOutOfRange(int index)
    {
        Assert.Throws<IndexOutOfRangeException>(() => _list[index]);
    }
    
    [Theory]
    [InlineData(0, "ten")]
    [InlineData(1, "nine")]
    [InlineData(2, "eight")]
    [InlineData(3, "seven")]
    [InlineData(4, "six")]
    [InlineData(5, "five")]
    [InlineData(6, "four")]
    [InlineData(7, "three")]
    [InlineData(8, "two")]
    [InlineData(9, "one")]
    public void IndexerSet(int index, string value)
    {
        _list[index] = value;
        Assert.Equal(value, _list[index]);
    }
    
    [Theory]
    [InlineData(-1, "test")]
    [InlineData(11, "test")]
    [InlineData(3219, "test")]
    public void IndexerSetOutOfRange(int index, string value)
    {
        Assert.Throws<IndexOutOfRangeException>(() => _list[index] = value);
    }
    
    [Theory]
    [InlineData(0, "one")]
    [InlineData(1, "two")]
    [InlineData(2, "three")]
    [InlineData(3, "four")]
    [InlineData(4, "five")]
    [InlineData(5, "six")]
    [InlineData(6, "seven")]
    [InlineData(7, "eight")]
    [InlineData(8, "nine")]
    [InlineData(9, "ten")]
    public void IndexMethodAccess(int index, string expected)
    {
        Assert.Equal(expected, _list.GetAt(index));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(11)]
    [InlineData(3219)]
    public void IndexMethodGetOutOfRange(int index)
    {
        Assert.Throws<IndexOutOfRangeException>(() => _list.GetAt(index));
    }
    
    [Theory]
    [InlineData(0, "ten")]
    [InlineData(1, "nine")]
    [InlineData(2, "eight")]
    [InlineData(3, "seven")]
    [InlineData(4, "six")]
    [InlineData(5, "five")]
    [InlineData(6, "four")]
    [InlineData(7, "three")]
    [InlineData(8, "two")]
    [InlineData(9, "one")]
    public void IndexMethodSet(int index, string value)
    {
        _list.SetAt(index, value);
        Assert.Equal(value, _list.GetAt(index));
    }
    
    [Theory]
    [InlineData(-1, "test")]
    [InlineData(11, "test")]
    [InlineData(3219, "test")]
    public void IndexMethodSetOutOfRange(int index, string value)
    {
        Assert.Throws<IndexOutOfRangeException>(() => _list.SetAt(index, value));
    }
}