using System.Collections;

namespace DataStructures;

public class List<T> : IEquatable<List<T>>, IEnumerable<T>
{
    private T[] _array;
    private int _capacity;
    private int _count;
    public int Count => _count;
    private const double GrowthFactor = 1.618f;

    public List()
    {
        _count = 0;
        _capacity = 16;
        _array = new T[_capacity];
    }

    public List(int capacity)
    {
        if(capacity < 0)
            throw new ArgumentOutOfRangeException(nameof(capacity),"Capacity cannot be negative");
        _count = 0;
        _capacity = capacity;
        _array = new T[_capacity];
    }
    
    public List(params T[] values)
    {
        _count = _capacity = values.Length;
        _array = new T[values.Length];
        values.CopyTo(_array, 0);
    }

    public List(IEnumerable<T> values)
    {
        _array = values.ToArray();
        _count = _capacity = _array.Length;
    }

    public T this[int index]
    {
        get
        {
            if(index >= _count)
                throw new IndexOutOfRangeException();
            return _array[index];
        }
        set
        {
            if(index >= _count)
                throw new IndexOutOfRangeException("Use List#Add() to add items to the list.");
            _array[index] = value;
        }
    }
    
    public static List<T> operator +(List<T> a, List<T> b)
    {
        var newArray = new T[a._count + b._count];
        for(var i = 0; i < a._count; i++)
            newArray[i] = a._array[i];
        for(var i = 0; i < b._count; i++)
            newArray[i + a._count] = b._array[i];
        return new List<T>(newArray);
    }
    
    public static List<T> operator -(List<T> baseList, List<T> removeList)
    {
        List<T> newList = new(baseList);
        newList.Remove(removeList.ToArray());
        return newList;
    }

    public static bool operator ==(List<T> a, List<T> b)
    {
        if (ReferenceEquals(a, b)) return true;
        if(a._count != b._count)
            return false;
        for (var i = 0; i < a._count; i++)
            if(Comparer<T>.Default.Compare(a[i], b[i]) != 0)
                return false;
        return true;
    }
    
    public static bool operator !=(List<T> a, List<T> b)
    {
        if (ReferenceEquals(a, b)) return false;
        if(a._count != b._count)
            return true;
        for (var i = 0; i < a._count; i++)
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
        var neededSpace = _count + space;
        if (neededSpace <= _capacity && !(_capacity / GrowthFactor > neededSpace)) return;
        if (neededSpace > _capacity) _capacity = (int) (neededSpace * GrowthFactor);
        else _capacity = (int) Math.Ceiling(_capacity/GrowthFactor);
        var newArray = new T[_capacity];
        var upperBound = Math.Min(_count, _array.Length);
        for(var i = 0; i < upperBound; i++)
            newArray[i] = _array[i];
        _array = newArray;
    }

    public void Add(params T[] values)
    {
        Resize(values.Length);
        foreach(var t in values)
            _array[_count++] = t;
    }
    
    public int Remove(params T[] values)
    {
        var indexes = new int[values.Length];
        var count = 0;
        for (var (i, v) = (0, 0); i < values.Length; i++)
        {
            var index = IndexOf(values[i]);
            if (index == -1) indexes[^++v] = index;
            else indexes[count++] = index;
        }
        var toRemove = indexes[..count];
        RemoveAt(toRemove);
        return toRemove.Length;
    }
    
    public int RemoveAll(Func<T, bool> predicate)
    {
        var count = 0;
        for(var i = 0; i < _count; i++)
        {
            if (!predicate(_array[i])) continue;
            RemoveAt(i);
            i--;
            count++;
        }
        return count;
    }

    public void RemoveAt(params int[] inputIndexes)
    {
        foreach (var t in inputIndexes)
            if(t < 0 || t >= _count)
                throw new IndexOutOfRangeException();
        var indexes = inputIndexes.OrderBy(x => x).ToArray();
        for (var (i, j, k) = (0, 0, 0); k + j < _count; i++)
            if (j < indexes.Length && i == indexes[j]) j++;
            else _array[k] = _array[k++ + j];
        _count -= indexes.Length;
        Resize();
    }
    
    public void InsertAt(int index, params T[] values)
    {
        Resize(values.Length);
        _count += values.Length;
        for (var i = _count - 1; i >= index + values.Length; i--)
            _array[i] = _array[i - values.Length];
        for (var i = 0; i < values.Length; i++)
            _array[index + i] = values[i];
    }
    
    public int IndexOf(T value)
    {
        for(var i = 0; i < _count; i++)
            if(Comparer<T>.Default.Compare(_array[i], value) == 0)
                return i;
        return -1;
    }
    
    public int LastIndexOf(T value)
    {
        for(var i = _count - 1; i >= 0; i--)
            if(Comparer<T>.Default.Compare(_array[i], value) == 0)
                return i;
        return -1;
    }

    public void Clear()
    {
        _count = 0;
        _capacity = 1;
        _array = new T[_capacity];
    }
    
    public bool Contains(T value)
    {
        for(var i = 0; i < _count; i++)
            if(Comparer<T>.Default.Compare(_array[i], value) == 0)
                return true;
        return false;
    }

    public List<T> Where(Func<T, bool> predicate)
    {
        List<T> newIntList = new();
        for (var i = 0; i < _count; i++)
            if (predicate(_array[i]))
                newIntList.Add(_array[i]);
        return newIntList;
    }

    public T Find(Func<T, bool> predicate)
    {
        for (var i = 0; i < _count; i++)
            if (predicate(_array[i]))
                return _array[i];
        return default!;
    }
    
    public T FindLast(Func<T, bool> predicate)
    {
        for (var i = _count - 1; i >= 0; i--)
            if (predicate(_array[i]))
                return _array[i];
        return default!;
    }
    
    public void ForEach(Action<T> action)
    {
        for (var i = 0; i < _count; i++)
            action(_array[i]);
    }

    public List<T> Distinct()
    {
        List<T> distinctList = new(_capacity);
        for (var i = 0; i < _count; i++)
            if(!distinctList.Contains(this[i]))
                distinctList.Add(this[i]);
        return distinctList;
    }
    
    public List<T> Reverse()
    {
        var i1 = 0;
        var i2 = _count - 1;
        while (i1 < i2)
        {
            (_array[i1], _array[i2]) = (_array[i2], _array[i1]);
            i1++;
            i2--;
        }
        return this;
    }
    
    public T[] ToArray()
    {
        var newArray = new T[_count];
        for(var i = 0; i < _count; i++)
            newArray[i] = _array[i];
        return newArray;
    }
    
    public override string ToString()
        => string.Join(", ", ToArray());

    public void Push(T value)
        => Add(value);
    
    public T Pop()
    {
        var toReturn = _array[_count-1];
        RemoveAt(_count - 1);
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
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if(this._count != other._count)
            return false;
        for (var i = 0; i < this._count; i++)
            if(Comparer<T>.Default.Compare(this[i], other[i]) < 0)
                return false;
        return true;
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
    
    public IEnumerator<T> GetEnumerator()
        => new ListEnumerator<T>(this);

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((List<T>)obj);
    }

    public override int GetHashCode() 
        => throw new InvalidOperationException("GetHashCode should not be used on List<T>");

    private class ListEnumerator<T1> : IEnumerator<T1>
    {
        private int _position = -1;
        private readonly List<T1> _list;
        
        public ListEnumerator(List<T1> list) => _list = list;
        public bool MoveNext() => ++_position < _list.Count;
        public void Reset() => _position = -1;
        public T1 Current => _list[_position];
        object? IEnumerator.Current => Current;
        public void Dispose() {}
    }
}