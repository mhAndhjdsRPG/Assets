using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class ListUtils
{
    public static void AddIfNotContains<T>(this List<T> list, T value)
    {
        if (!list.Contains(value))
        {
            list.Add(value);
        }
    }


    public static void RemoveIfContains<T>(this List<T> list, T value)
    {
        if (list.Contains(value))
        {
            list.Remove(value);
        }
    }

}

