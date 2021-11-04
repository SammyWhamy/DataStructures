using System.Collections;

namespace DataStructures;

public class List<T> : IEquatable<List<T>>, IEnumerable<T>
{
    private T[] _array;
    private int _capacity;
    public int Count { get; private set; }
    private const double GrowthFactor = 1.618f;

    public List()
    {
        Count = 0;
        _capacity = 16;
        _array = new T[_capacity];
    }

    public List(int capacity)
    {
        if(capacity < 0)
            throw new ArgumentOutOfRangeException(nameof(capacity),"Capacity cannot be negative");
        Count = 0;
        _capacity = capacity;
        _array = new T[_capacity];
    }
    
    public List(params T[] values)
    {
        Count = values.Length;
        _capacity = values.Length + values.Length / 4;
        _array = new T[_capacity];
        values.CopyTo(_array, 0);
    }

    public List(IEnumerable<T> values)
    {
        var arr = values.ToArray();
        Count = arr.Length;
        _capacity = arr.Length + arr.Length / 4;
        _array = new T[_capacity];
        arr.CopyTo(_array, 0);
    }

    public T this[int index]
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
                throw new IndexOutOfRangeException("Use List#Add() to add items to the list.");
            _array[index] = value;
        }
    }
    
    public static List<T> operator +(List<T> a, List<T> b)
    {
        var newCount = a.Count + b.Count;
        var newArray = new T[newCount];
        a.ToArray().CopyTo(newArray, 0);
        b.ToArray().CopyTo(newArray, a.Count);
        return new List<T>(newArray);
    }
    
    public static List<T> operator -(List<T> baseList, List<T> removeList)
    {
        List<T> newList = new(baseList.ToArray());
        foreach (var t in removeList)
            if(newList.Contains(t))
                newList.RemoveAt(newList.IndexAt(t));
        return newList;
    }

    public static bool operator ==(List<T> a, List<T> b)
    {
        if (ReferenceEquals(a, b)) return true;
        if(a.Count != b.Count)
            return false;
        for (var i = 0; i < a.Count; i++)
            if(Comparer<T>.Default.Compare(a[i], b[i]) != 0)
                return false;
        return true;
    }
    
    public static bool operator !=(List<T> a, List<T> b)
    {
        if (ReferenceEquals(a, b)) return false;
        if(a.Count != b.Count)
            return true;
        for (var i = 0; i < a.Count; i++)
            if(Comparer<T>.Default.Compare(a[i], b[i]) != 0)
                return true;
        return false;
    }

    public T GetAt(int index)
        => this[index];

    public void SetAt(int index, T value)
        => this[index] = value;

    private void Resize(int space = 0)
    {
        var neededSpace = Count + space;
        if (neededSpace <= _capacity && !(_capacity / GrowthFactor > neededSpace)) return;
        if (neededSpace > _capacity) _capacity = (int) (neededSpace * GrowthFactor);
        else _capacity = (int) Math.Ceiling(_capacity/GrowthFactor);
        var newArray = new T[_capacity];
        for(var i = 0; i < Count; i++)
            newArray[i] = _array[i];
        _array = newArray;
    }

    public void Add(params T[] values)
    {
        Resize(values.Length);
        foreach(var t in values)
            _array[Count++] = t;
    }

    public void RemoveAt(int index)
    {
        if (index == Count-1) _array[^1] = default!;
        else {
            var newArray = new T[_capacity - 1];
            for (var (c, i) = (0, 0); c < Count; c++, i++)
                if (c == index) i--;
                else newArray[i] = _array[c];
            _array = newArray;
        }
        Count--;
        Resize();
    }
    
    public void InsertAt(int index, params T[] values)
    {
        var newArray = new T[_capacity + values.Length];
        for (var (c, i) = (0, 0); c < Count + 1; c++, i++)
        {
            if (c == index)
                foreach (var t in values)
                    newArray[i++] = t;
            if (c < Count)
                newArray[i] = _array[c];
        }
        _array = newArray;
        Count+=values.Length;
        Resize();
    }
    
    public int IndexAt(T value)
    {
        for(var i = 0; i < Count; i++)
            if(Comparer<T>.Default.Compare(_array[i], value) == 0)
                return i;
        return -1;
    }

    public void Clear()
    {
        Count = 0;
        _capacity = 1;
        _array = new T[_capacity];
    }
    
    public bool Contains(T value)
    {
        for(var i = 0; i < Count; i++)
            if(Comparer<T>.Default.Compare(_array[i], value) == 0)
                return true;
        return false;
    }

    public List<T> Where(Func<T, bool> predicate)
    {
        List<T> newIntList = new();
        for (var i = 0; i < Count; i++)
            if (predicate(_array[i]))
                newIntList.Add(_array[i]);
        return newIntList;
    }

    public List<T> Distinct()
    {
        List<T> distinctList = new(_capacity);
        for (var i = 0; i < Count; i++)
            if(!distinctList.Contains(this[i]))
                distinctList.Add(this[i]);
        return distinctList;
    }
    
    public T[] ToArray()
    {
        var newArray = new T[Count];
        for(var i = 0; i < Count; i++)
            newArray[i] = _array[i];
        return newArray;
    }
    
    public override string ToString()
        => string.Join(", ", ToArray());

    public void Push(T value)
        => Add(value);
    
    public T Pop()
    {
        var toReturn = _array[Count-1];
        RemoveAt(Count - 1);
        return toReturn;
    }

    public T Shift()
    {
        var toReturn = _array[0];
        RemoveAt(0);
        return toReturn;
    }

    public void Unshift(T value)
        => InsertAt(0, value);
    
    public bool Equals(List<T>? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        if(this.Count != other.Count)
            return false;
        for (var i = 0; i < this.Count; i++)
            if(Comparer<T>.Default.Compare(this[i], other[i]) < 0)
                return false;
        return true;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public IEnumerator<T> GetEnumerator()
    {
        for(var i = 0; i < Count; i++)
            yield return this[i];
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((List<T>)obj);
    }

    public override int GetHashCode() 
        => throw new InvalidOperationException("GetHashCode should not be used on List<T>");
}