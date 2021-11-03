namespace DataStructures.Tests;

public class ListPureMethodTests
{
    private readonly List<string> _list;
    private readonly List<string> _smallList;
    public ListPureMethodTests()
    {
        _list = new List<string>("one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten");
        _smallList = new List<string>("one", "two", "three");
    }

    [Fact]
    public void ListEqualsNullReference()
    {
        Assert.False(_list.Equals(null));
    }

    [Fact]
    public void ListEqualsEqualReference()
    {
        Assert.True(_list.Equals(_list));
    }
    
    [Fact]
    public void ListEqualsDifferentCount()
    {
        Assert.False(_list.Equals(_smallList));
    }
    
    [Fact]
    public void ListEqualsDifferentItem()
    {
        List<string> copyList = new(_list.ToArray())
        {
            [3] = "notFour"
        };
        Assert.False(_list.Equals(copyList));
    }
    
    [Fact]
    public void ListEquals()
    {
        List<string> copyList = new(_list.ToArray());
        Assert.True(_list.Equals(copyList));
    }

    [Fact]
    public void ListEnumerate()
    {
        var i = 0;
        foreach (var item in _list)
        {
            Assert.Equal(_list[i], item);
            i++;
        }
    }
    
    [Fact]
    public void ListEqualsObjectNullReference()
    {
        Assert.False(_list.Equals(obj: null));
    }

    [Fact]
    public void ListEqualsObjectEqualReference()
    {
        // ReSharper disable once EqualExpressionComparison
        Assert.True(_list.Equals(obj: _list));
    }
    
    [Fact]
    public void ListEqualsObjectDifferentType()
    {
        Assert.False(_list.Equals(new object()));
    }
    
    [Fact]
    public void ListEqualsObjectDifferentItem()
    {
        List<string> copyList = new(_list.ToArray())
        {
            [3] = "notFour"
        };
        Assert.False(_list.Equals(obj: copyList));
    }
    
    [Fact]
    public void ListObjectEquals()
    {
        List<string> copyList = new(_list.ToArray());
        Assert.True(_list.Equals(obj: copyList));
    }
    
    [Fact]
    public void ListGetHashCode()
    {
        Assert.Throws<InvalidOperationException>(() => _list.GetHashCode());
    }
}