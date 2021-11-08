namespace DataStructures.Tests;

public class ListCtorTests
{
    [Fact]
    public void ParameterLess()
    {
        List<int> list = new();
        Assert.Equal(0, list.Count);
    }

    [Theory]
    [InlineData(16)]
    [InlineData(3910)]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-381)]
    public void Capacity(int capacity)
    {
        if (capacity >= 0)
        {
            List<int> list = new(capacity);
            Assert.Equal(0, list.Count);
        }
        else
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new List<int>(capacity));
        }
    }

    [Fact]
    public void ArrayInitializedInt()
    {
        int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        List<int> list = new(array);
        Assert.Equal(array, list);
        Assert.Equal(array.Length, list.Count);
    }
    
    [Fact]
    public void ArrayInitializedString()
    {
        string[] array = { "hello", "hi", "world", "test", "test", "welcome", "bye" };
        List<string> list = new(array);
        Assert.Equal(array, list);
        Assert.Equal(array.Length, list.Count);
    }
    
    [Fact]
    public void ArrayInitializedBool()
    {
        bool[] array = { false, true, true, false, true, false, true, false, true, false };
        List<bool> list = new(array);
        Assert.Equal(array, list);
        Assert.Equal(array.Length, list.Count);
    }

    [Fact]
    public void EnumerableInitialized()
    {
        var enumerable = new System.Collections.Generic.List<int> { 1, 2, 3 };
        List<int> list = new(enumerable);
        Assert.Equal(enumerable, list);
        Assert.Equal(enumerable.Count, list.Count);
    }
}