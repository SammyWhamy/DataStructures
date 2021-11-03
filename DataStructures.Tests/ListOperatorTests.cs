namespace DataStructures.Tests;

public class ListOperatorTests
{
    private readonly List<string> _listA;
    private readonly List<string> _listB;
    public ListOperatorTests()
    {
        _listA = new List<string>("one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten");
        _listB = new List<string>("six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen");
    }
    
    [Fact]
    public void ListPlusOperator()
    {
        var list = _listA + _listB;
        Assert.Equal(_listA.Count + _listB.Count, list.Count);
        var array = new string[_listA.Count + _listB.Count];
        _listA.ToArray().CopyTo(array, 0);
        _listB.ToArray().CopyTo(array, _listA.Count);
        Assert.Equal(list.ToArray(), array);
    }
    
    [Fact]
    public void ListMinusOperator()
    {
        var list = _listA - _listB;
        Assert.Equal(5, list.Count);
    }

    [Fact]
    public void ListCopyEqualOperator()
    {
        List<string> listCopy = new(_listA.ToArray());
        Assert.True(_listA == listCopy);
    }
    
    [Fact]
    public void ListReferenceEqualOperator()
    {
        // ReSharper disable once EqualExpressionComparison
        Assert.True(_listA == _listA);
    }
    
    [Fact]
    public void ListDifferentSizeEqualOperator()
    {
        List<string> listCopy = new(_listA.ToArray()) { "eleven" };
        Assert.False(_listA == listCopy);
    }
    
    [Fact]
    public void ListDifferentItemEqualOperator()
    {
        List<string> listCopy = new(_listA.ToArray())
        {
            [4] = "notFive"
        };
        Assert.False(_listA == listCopy);
    }
    
    [Fact]
    public void ListCopyUnequalOperator()
    {
        List<string> listCopy = new(_listA.ToArray());
        Assert.False(_listA != listCopy);
    }
    
    [Fact]
    public void ListReferenceUnequalOperator()
    {
        // ReSharper disable once EqualExpressionComparison
        Assert.False(_listA != _listA);
    }
    
    [Fact]
    public void ListDifferentSizeUnequalOperator()
    {
        List<string> listCopy = new(_listA.ToArray()) { "eleven" };
        Assert.True(_listA != listCopy);
    }
    
    [Fact]
    public void ListDifferentItemUnequalOperator()
    {
        List<string> listCopy = new(_listA.ToArray())
        {
            [4] = "notFive"
        };
        Assert.True(_listA != listCopy);
    }
}