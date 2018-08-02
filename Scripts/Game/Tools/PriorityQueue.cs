using System;
using System.Collections;
using System.Collections.Generic;


public class PriorityQueue<T> where T : class, IComparable
{

    private List<T> list = new List<T>();

    public int Count
    {
        get { return list.Count; }
    }

    public T Dequeue()
    {
        T retT = list[0];
        list.RemoveAt(0);
        return retT;
    }

    public void Enqueue(T item)
    {
        list.Add(item);
        list.Sort();
    }

    public void Clear()
    {
        list.Clear();
    }

    public T Peek()
    {
        if (list[0] != null)
        {
            return list[0];
        }
        else
        {
            return null;
        }
    }
}


