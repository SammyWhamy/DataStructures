namespace DataStructures.Tests;

public class ListLinqLikeTests
{
    private readonly List<string> _list;
    private readonly List<string> _duplicateList;
    private readonly List<int> _intList;
    public ListLinqLikeTests()
    {
        _list = new List<string>("one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten");
        _duplicateList = new List<string>("one", "two", "three", "one", "three", "four", "four", "two", "one");
        _intList = new List<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
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
    
    [Fact]
    public void ListRemoveAll()
    {
        Assert.Equal(5, _intList.RemoveAll(x => x > 5));
        Assert.Equal(5, _intList.Count);
        Assert.Equal(new [] {1, 2, 3, 4, 5}, _intList);
    }
    
    [Fact]
    public void ListFind()
    {
        Assert.Equal(1, _intList.Find(x => x < 3));
        Assert.Equal(6, _intList.Find(x => x > 5));
        Assert.Null(_list.Find(x => x == "woah"));
    }
    
    [Fact]
    public void ListFindLast()
    {
        Assert.Equal(2, _intList.FindLast(x => x < 3));
        Assert.Equal(10, _intList.FindLast(x => x > 5));
        Assert.Null(_list.FindLast(x => x == "woah"));
    }

    [Fact]
    public void ListForEach()
    {
        var array = new int[10];
        _intList.ForEach(x => array[x - 1] = x*2);
        Assert.Equal(new[] {2, 4, 6, 8, 10, 12, 14, 16, 18, 20}, array);
    }
    
    [Fact]
    public void ListReverse()
    {
        Assert.Equal(new[] {10, 9, 8, 7, 6, 5, 4, 3, 2, 1}, _intList.Reverse());
        Assert.Equal(new [] {"ten", "nine", "eight", "seven", "six", "five", "four", "three", "two", "one"}, _list.Reverse());
    }
}