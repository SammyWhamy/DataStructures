using System.Collections;

namespace DataStructures;

public class DynamicArray<T> : IEnumerable<T>
{
    public T[] InternalArray;
    public int Capacity;
    public int Count;
    private const double GrowthFactor = 1.618f;
    
    public DynamicArray()
    {
        Count = 0;
        Capacity = 16;
        InternalArray = new T[Capacity];
    }

    public DynamicArray(int capacity)
    {
        if(capacity < 0)
            throw new ArgumentOutOfRangeException(nameof(capacity),"Capacity cannot be negative");
        Count = 0;
        Capacity = capacity;
        InternalArray = new T[Capacity];
    }
    
    public DynamicArray(params T[] values)
    {
        Count = Capacity = values.Length;
        InternalArray = new T[values.Length];
        values.CopyTo(InternalArray, 0);
    }

    public T this[int index]
    {
        get => InternalArray[index];
        set => InternalArray[index] = value;
    }
    
    private void Resize(int space = 0)
    {
        var neededSpace = Count + space;
        if (neededSpace <= Capacity && !(Capacity / GrowthFactor > neededSpace)) return;
        if (neededSpace > Capacity) Capacity = (int) (neededSpace * GrowthFactor);
        else Capacity = (int) Math.Ceiling(Capacity/GrowthFactor);
        var newArray = new T[Capacity];
        var upperBound = Math.Min(Count, InternalArray.Length);
        for(var i = 0; i < upperBound; i++)
            newArray[i] = InternalArray[i];
        InternalArray = newArray;
    }
    
    public void Add(params T[] values)
    {
        Resize(values.Length);
        foreach(var t in values)
            InternalArray[Count++] = t;
    }

    public void RemoveAt(params int[] inputIndexes)
    {
        foreach (var t in inputIndexes)
            if(t < 0 || t >= Count)
                throw new IndexOutOfRangeException();
        var indexes = inputIndexes.OrderBy(x => x).ToArray();
        for (var (i, j, k) = (0, 0, 0); k + j < Count; i++)
            if (j < indexes.Length && i == indexes[j]) j++;
            else InternalArray[k] = InternalArray[k++ + j];
        Count -= indexes.Length;
        Resize();
    }
    
    public int IndexOf(T value)
    {
        for(var i = 0; i < Count; i++)
            if(Comparer<T>.Default.Compare(InternalArray[i], value) == 0)
                return i;
        return -1;
    }
    
    public T[] ToArray()
    {
        var newArray = new T[Count];
        for(var i = 0; i < Count; i++)
            newArray[i] = InternalArray[i];
        return newArray;
    }
    
    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
    
    public IEnumerator<T> GetEnumerator()
        => new DynamicArrayEnumerator<T>(this);
    
    private class DynamicArrayEnumerator<T1> : IEnumerator<T1>
    {
        private int _position = -1;
        private readonly DynamicArray<T1> _list;
        
        public DynamicArrayEnumerator(DynamicArray<T1> dynArray) => _list = dynArray;
        public bool MoveNext() => ++_position < _list.Count;
        public void Reset() => _position = -1;
        public T1 Current => _list[_position];
        object? IEnumerator.Current => Current;
        public void Dispose() {}
    }
}