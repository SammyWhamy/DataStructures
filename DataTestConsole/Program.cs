using DataStructures;

List<int> list = new();
for (var i = 0; i < 10000; i++)
    list.Add(i);
for (var i = 0; i < 10000; i++)
    list.InsertAt(i, i);
for (var i = 0; i < 10000; i++)
    list.RemoveAt(i);