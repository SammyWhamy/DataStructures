namespace DataStructures.Tests;

public class ListMethodsTests
{
    private readonly List<string> _list;
    private readonly List<string> _smallList;
    private readonly List<string> _dupeList;
    private readonly List<int> _intList;
    public ListMethodsTests()
    {
        _list = new List<string>("one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten");
        _smallList = new List<string>("one", "two", "three");
        _dupeList = new List<string>("one", "two", "three", "one", "two", "three", "one", "two", "three");
        _intList = new List<int>(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
    }
    
    [Theory]
    [InlineData("one", 0)]
    [InlineData("two", 1)]
    [InlineData("three", 2)]
    [InlineData("four", 3)]
    [InlineData("five", 4)]
    [InlineData("six", 5)]
    [InlineData("seven", 6)]
    [InlineData("eight", 7)]
    [InlineData("nine", 8)]
    [InlineData("ten", 9)]
    [InlineData("zero", -1)]
    [InlineData("doesntExist", -1)]
    public void ListIndexAt(string value, int index)
    {
        Assert.Equal(index, _list.IndexOf(value));
    }

    [Theory]
    [InlineData(0, "pre", new [] { "pre", "one", "two", "three" })]
    [InlineData(1, "mid", new [] { "one", "mid", "two", "three" })]
    [InlineData(2, "mid", new [] { "one", "two", "mid", "three" })]
    [InlineData(3, "end", new [] { "one", "two", "three", "end" })]
    public void ListInsertAt(int index, string value, string[] expected)
    {
        var expectedCount = _smallList.Count+1;
        _smallList.InsertAt(index, value);
        Assert.Equal(expectedCount, _smallList.Count);
        Assert.Equal(expected, _smallList.ToArray());
    }

    [Fact]
    public void ListInsertManyAt()
    {
        var expectedCount = _smallList.Count+3;
        _smallList.InsertAt(3, "end", "end2", "end3");
        Assert.Equal(expectedCount, _smallList.Count);
        Assert.Equal(new [] {"one", "two", "three", "end", "end2", "end3"}, _smallList.ToArray());
    }

    [Fact]
    public void ListClear()
    {
        _list.Clear();
        Assert.Equal(0, _list.Count);
        Assert.Equal(Array.Empty<string>(), _list.ToArray());
    }
    
    [Theory]
    [InlineData(0, new [] { "two", "three" })]
    [InlineData(1, new [] { "one", "three" })]
    [InlineData(2, new [] { "one", "two" })]
    public void ListRemoveAt(int index, string[] expected)
    {
        var expectedCount = _smallList.Count-1;
        _smallList.RemoveAt(index);
        Assert.Equal(expectedCount, _smallList.Count);
        Assert.Equal(expected, _smallList.ToArray());
    }
    
    [Theory]
    [InlineData(-312)]
    [InlineData(-1)]
    [InlineData(321312)]
    public void ListRemoveAtInvalidIndex(int index)
    {
        Assert.Throws<IndexOutOfRangeException>(() => _smallList.RemoveAt(index));
    }

    [Theory]
    [InlineData("one", true)]
    [InlineData("two", true)]
    [InlineData("three", true)]
    [InlineData("four", true)]
    [InlineData("five", true)]
    [InlineData("six", true)]
    [InlineData("seven", true)]
    [InlineData("eight", true)]
    [InlineData("nine", true)]
    [InlineData("ten", true)]
    [InlineData("zero", false)]
    [InlineData("doesntExist", false)]
    public void ListContains(string values, bool expected)
    {
        Assert.Equal(expected, _list.Contains(values));
    }

    [Fact]
    public void ListPop()
    {
        var initialCount = _list.Count;
        for(var i = 0; i < 10; i++)
        {
            Assert.Equal(_list[^1], _list.Pop());
            Assert.Equal(initialCount-i-1, _list.Count);
        }
    }
    
    [Fact]
    public void ListPush()
    {
        var initialCount = _list.Count;
        for(var i = 0; i < 10; i++)
        {
            _list.Push(i.ToString());
            Assert.Equal(i.ToString(), _list[^1]);
            Assert.Equal(initialCount+i+1, _list.Count);
        }
    }
    
    [Fact]
    public void ListShift()
    {
        var initialCount = _list.Count;
        for(var i = 0; i < 10; i++)
        {
            Assert.Equal(_list[0], _list.Shift());
            Assert.Equal(initialCount-i-1, _list.Count);
        }
    }
    
    [Fact]
    public void ListUnshift()
    {
        var initialCount = _list.Count;
        for(var i = 0; i < 10; i++)
        {
            _list.Unshift(i.ToString());
            Assert.Equal(i.ToString(), _list[0]);
            Assert.Equal(initialCount+i+1, _list.Count);
        }
    }

    [Fact]
    public void ListToString()
    {
        Assert.Equal("one, two, three, four, five, six, seven, eight, nine, ten", _list.ToString());
        Assert.Equal("one, two, three", _smallList.ToString());
    }
    
    [Theory]
    [InlineData(new [] {2}, new [] {1, 3, 4, 5, 6, 7, 8, 9, 10})]
    [InlineData(new [] {2, 3, 4}, new [] {1, 5, 6, 7, 8, 9, 10})]
    [InlineData(new [] {3, 4, 7, 8, 10}, new [] {1, 2, 5, 6, 9})]
    [InlineData(new [] {-1, 391, -391, int.MaxValue, int.MinValue}, new [] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}, 5)]
    public void ListRemove(int[] toRemove, int[] expected, int minus = 0)
    {
        Assert.Equal(toRemove.Length-minus, _intList.Remove(toRemove));
        Assert.Equal(expected.Length, _intList.Count);
        Assert.Equal(expected, _intList.ToArray());
    }
    
    [Theory]
    [InlineData("hello", new [] { "one", "two", "three" })]
    [InlineData("nope", new [] { "one", "two", "three" })]
    [InlineData("10", new [] { "one", "two", "three" })]
    public void ListRemoveNonExistentValue(string value, string[] expected)
    {
        Assert.Equal(0, _smallList.Remove(value));
        Assert.Equal(expected.Length, _smallList.Count);
        Assert.Equal(expected, _smallList.ToArray());
    }

    [Theory]
    [InlineData("one", 6)]
    [InlineData("two", 7)]
    [InlineData("three", 8)]
    [InlineData("nope", -1)]
    [InlineData("hello", -1)]
    public void ListLastIndexOf(string value, int expected)
    {
        Assert.Equal(expected, _dupeList.LastIndexOf(value));
    }
}