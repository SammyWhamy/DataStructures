namespace DataStructures;

public class IntList
{
    private int[] _array;
    private int _capacity;
    public int Count { get; private set; }

    public IntList()
    {
        Count = 0;
        _capacity = 16;
        _array = new int[_capacity];
    }

    public IntList(int capacity)
    {
        Count = 0;
        _capacity = capacity;
        _array = new int[_capacity];
    }
    
    public IntList(int[] array)
    {
        Count = array.Length;
        _capacity = array.Length + array.Length / 4;
        _array = new int[_capacity];
        array.CopyTo(_array, 0);
    }
    
    public int this[int index]
    {
        get
        {
            if(index >= Count)
                throw new IndexOutOfRangeException();
            return _array[index];
        }
        set
        {
            if(index >= Count)
                throw new IndexOutOfRangeException("Use IntList#Add() to add items to the list.");
            _array[index] = value;
        }
    }

    public int GetAt(int index)
        => this[index];

    public void SetAt(int index, int value)
        => this[index] = value;

    private void Resize(int space = 0)
    {
        _capacity = Count + space + (Count+space) / 4;
        var newArray = new int[_capacity];
        for(var i = 0; i < Count; i++)
            newArray[i] = _array[i];
        _array = newArray;
    }

    public void Add(int value)
    {
        Resize(1);
        _array[Count] = value;
        Count++;
    }

    public void RemoveAt(int index)
    {
        var newArray = new int[_capacity - 1];
        for (var (c, i) = (0, 0); c < Count; c++, i++)
            if (c == index) i--;
            else newArray[i] = _array[c];
        _array = newArray;
        Count--;
        Resize();
    }
    
    public void InsertAt(int index, int value)
    {
        var newArray = new int[_capacity + 1];
        for (var (c, i) = (0, 0); c < Count; c++, i++)
        {
            if (c == index) newArray[i++] = value;
            newArray[i] = _array[c];
        }
        _array = newArray;
        Count++;
        Resize();
    }
    
    public int IndexAt(int value)
    {
        for(var i = 0; i < Count; i++)
            if(_array[i] == value)
                return i;
        return -1;
    }

    public void Clear()
    {
        Count = 0;
        _capacity = 1;
        _array = new int[_capacity];
    }
    
    public bool Contains(int value)
    {
        for(var i = 0; i < Count; i++)
            if(_array[i] == value)
                return true;
        return false;
    }

    public IntList Where(Func<int, bool> predicate)
    {
        IntList newIntList = new();
        for (var i = 0; i < Count; i++)
            if (predicate(_array[i]))
                newIntList.Add(_array[i]);
        return newIntList;
    }
    
    public int[] ToArray()
    {
        var newArray = new int[Count];
        for(int i = 0; i < Count; i++)
            newArray[i] = _array[i];
        return newArray;
    }
    
    public override string ToString()
        => string.Join(", ", _array);
    
    public void Push(int value)
        => Add(value);
    
    public int Pop()
    {
        var toReturn = _array[Count-1];
        RemoveAt(Count - 1);
        return toReturn;
    }

    public int Shift()
    {
        var toReturn = _array[0];
        RemoveAt(0);
        return toReturn;
    }

    public void Unshift(int value)
        => InsertAt(0, value);
}