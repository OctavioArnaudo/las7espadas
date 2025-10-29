using System;

using System.Collections.Generic;

/// <summary>
/// 
/// A simple heap queue implementation that allows for efficient priority queue operations.
/// 
/// The queue is implemented as a binary heap, which allows for O(log n) time complexity for insertion and removal operations.
/// 
/// The queue supports generic types that implement the IComparable interface, allowing for custom sorting logic.
/// 
/// The queue provides methods to push items, pop the highest priority item, peek at the first item, check if the queue is empty,
/// 
/// check if an item is contained in the queue, remove an item, and clear the queue.
/// 
/// The queue maintains the order of items based on their priority, with the highest priority item at the front.
/// 
/// The queue is not thread-safe, so it should be used in a single-threaded context or with appropriate synchronization.
/// 
/// </summary>
public class HeapQueue<T> where T : IComparable<T>
{
    /// <summary>
    /// 
    /// The list that holds the items in the heap queue.
    /// 
    /// </summary>
    List<T> items;

    /// <summary>
    /// 
    /// Gets the number of items in the heap queue.
    /// 
    /// </summary>
    public int Count
    {
        get
        {
            return items.Count;
        }
    }

    /// <summary>
    /// 
    /// Gets a value indicating whether the heap queue is empty.
    /// 
    /// </summary>
    public bool IsEmpty
    {
        get
        {
            return items.Count == 0;
        }
    }

    /// <summary>
    /// 
    /// Gets the first item in the heap queue without removing it.
    /// 
    /// </summary>
    public T First
    {
        get
        {
            return items[0];
        }
    }

    /// <summary>
    /// 
    /// Clears all items from the heap queue.
    /// 
    /// </summary>
    public void Clear() => items.Clear();

    /// <summary>
    /// 
    /// Checks if the heap queue contains a specific item.
    /// 
    /// </summary>
    public bool Contains(T item) => items.Contains(item);

    /// <summary>
    /// 
    /// Removes a specific item from the heap queue.
    /// 
    /// </summary>
    public void Remove(T item) => items.Remove(item);

    /// <summary>
    /// 
    /// Peeks at the first item in the heap queue without removing it.
    /// 
    /// </summary>
    public T Peek() => items[0];

    /// <summary>
    ///
    /// Initializes a new instance of the HeapQueue class.
    /// 
    /// </summary>
    public HeapQueue()
    {
        //initialize the list to hold items in the heap queue.
        items = new List<T>();
    }

    /// <summary>
    /// 
    /// Adds an item to the heap queue and maintains the heap property.
    /// 
    /// </summary>
    public void Push(T item)
    {
        //add item to end of tree to extend the list
        items.Add(item);
        //find correct position for new item.
        SiftDown(0, items.Count - 1);
    }

    public T Pop()
    {

        //if there are more than 1 items, returned item will be first in tree.
        //then, add last item to front of tree, shrink the list
        //and find correct index in tree for first item.
        T item;
        //if there is only one item, return it and clear the list.
        var last = items[items.Count - 1];
        //if there is only one item, return it and clear the list.
        items.RemoveAt(items.Count - 1);
        //if there are more than one item, swap the first item with the last item
        if (items.Count > 0)
        {
            //swap the first item with the last item
            item = items[0];
            //replace the first item with the last item
            items[0] = last;
            //find correct position for the first item in the heap
            SiftUp();
        }
        else
        {
            //if there is only one item, return it and clear the list.
            item = last;
        }
        //return the item that was at the front of the heap
        return item;
    }

    /// <summary>
    /// 
    /// Compares two items in the heap queue.
    /// 
    /// </summary>
    int Compare(T A, T B) => A.CompareTo(B);

    /// <summary>
    /// 
    /// Moves an item down the heap to maintain the heap property.
    /// 
    /// </summary>
    void SiftDown(int startpos, int pos)
    {
        //preserve the newly added item.
        var newitem = items[pos];
        //find the position of the new item in the heap
        while (pos > startpos)
        {
            //find parent index in binary tree
            var parentpos = (pos - 1) >> 1;
            //if parent is less than or equal to new item, pos is new item position.
            var parent = items[parentpos];
            //if new item precedes or equal to parent, pos is new item position.
            if (Compare(parent, newitem) <= 0)
                break;
            //else move parent into pos, then repeat for grand parent.
            items[pos] = parent;
            //move up the tree
            pos = parentpos;
        }
        //set the new item into the position found.
        items[pos] = newitem;
    }

    /// <summary>
    /// 
    /// Moves an item up the heap to maintain the heap property.
    /// 
    /// </summary>
    void SiftUp()
    {
        //if there are no items, nothing to do.
        var endpos = items.Count;
        if (endpos <= 1)
            return;
        //start position is the first item in the heap
        var startpos = 0;
        //preserve the inserted item
        var newitem = items[0];
        //the position of the child in the binary tree
        var childpos = 1;
        //if there is only one item, no need to sift up.
        var pos = 0;
        //find child position to insert into binary tree
        while (childpos < endpos)
        {
            //get right branch
            var rightpos = childpos + 1;
            //if right branch should precede left branch, move right branch up the tree
            if (rightpos < endpos && Compare(items[rightpos], items[childpos]) <= 0)
                childpos = rightpos;
            //move child up the tree
            items[pos] = items[childpos];
            //if child precedes or equal to new item, pos is new item position.
            pos = childpos;
            //move down the tree and repeat.
            childpos = 2 * pos + 1;
        }
        //the child position for the new item.
        items[pos] = newitem;
        //now sift down the new item to find its correct position in the heap.
        SiftDown(startpos, pos);
    }
}