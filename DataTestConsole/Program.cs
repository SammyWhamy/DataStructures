using System;
using DataStructures;

var _listA = new List<string>("one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten");
var _listB = new List<string>("six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen");

foreach(var item in _listB) {
    _listA.Add(item);
}

var count = _listA.Count;
for(var i = 0; i < count; i++)
{
    _listA.Pop();
}

return 0;