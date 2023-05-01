using System;
using System.Collections;
using System.Collections.Generic;


static class RandomUtils
{
    public static Item Choice(this Random rnd, IEnumerable<Item> choices, IEnumerable<int> weights)
    {
        var cumulativeWeight = new List<int>();
        int last = 0;
        foreach (var cur in weights)
        {
            last += cur;
            cumulativeWeight.Add(last);
        }
        int choice = rnd.Next(last);
        int i = 0;
        foreach (var cur in choices)
        {
            if (choice < cumulativeWeight[i])
            {
                return cur;
            }
            i++;
        }
        return null;
    }
}
