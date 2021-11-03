namespace DataStructures.Tests;

public class ListLinqLikeTests
{
    private readonly List<string> _list;
    private readonly List<string> _duplicateList;
    public ListLinqLikeTests()
    {
        _list = new List<string>("one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten");
        _duplicateList = new List<string>("one", "two", "three", "one", "three", "four", "four", "two", "one");
    }

    [Fact]
    public void ListWhere()
    {
        List<string> expected = new("three", "four", "five", "seven", "eight", "nine");
        Assert.Equal(expected, _list.Where(x => x.Length > 3));
    }
    
    [Fact]
    public void ListDistinct()
    {
        List<string> expected = new("one", "two", "three", "four");
        Assert.Equal(expected, _duplicateList.Distinct());
    }
}