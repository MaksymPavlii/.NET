using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CustomList
{
    public class CustomList<T> : ICollection<T>
    {
        private Item<T> firstNode;
        private Item<T> lastNode;
        private int count;

        public Item<T> Head
        {
            get { return firstNode; }
        }


        public Item<T> LastNode
        {
            get { return lastNode; }
        }

        public int Count
        {
            get => count;
            private set => count = value;
        }

        public bool IsReadOnly => false;

        public CustomList(params T[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            try
            {
                firstNode = new Item<T>(values[0]);
            }
            catch
            {
                firstNode = null;
            }

            var currentNode = firstNode;
            Count++;
            for (int i = 1; i < values.Count(); i++)
            {
                currentNode.Next = new Item<T>(values[i]);
                currentNode = currentNode.Next;
                Count++;
            }
            if (!values.Any())
                Count = 0;
            lastNode = currentNode;

        }

        public CustomList(IEnumerable<T> values)
        {
            var array = values.ToArray();
            firstNode = new Item<T>(array[0]);
            var currentNode = firstNode;
            Count++;
            for (int i = 1; i < values.Count(); i++)
            {
                currentNode.Next = new Item<T>(array[i]);
                currentNode = currentNode.Next;
                Count++;
            }
            if (!values.Any())
                Count = 0;
            lastNode = currentNode;

        }

        public T this[int index]
        {
            get
            {
                if (index < 0)
                {
                    IndexOutOfRangeException e = new IndexOutOfRangeException();
                    throw e;
                }
                Item<T> currentNode = firstNode;
                for (int i = 0; i < index; i++)
                {
                    if (currentNode.Next == null) { 
                    IndexOutOfRangeException e = new IndexOutOfRangeException();
                    throw e;
                        }
                    currentNode = currentNode.Next;
                }
                return currentNode.Data;
            }

            set
            {
                if (index < 0)
                {
                    IndexOutOfRangeException e = new IndexOutOfRangeException();
                    throw e;
                }
                Item<T> currentNode = firstNode;
                for (int i = 0; i < index; i++)
                {
                    if (currentNode.Next == null)
                        throw new ArgumentOutOfRangeException("index");
                    currentNode = currentNode.Next;
                }
                currentNode.Data = value;
            }
        }

        public void Add(T data)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (IsEmpty)
                firstNode = lastNode = new Item<T>(data);
            else
                lastNode = lastNode.Next = new Item<T>(data);
            count++;
        }

        public bool IsEmpty
        {
            get
            {
                return firstNode == null;
            }
        }

        public void Clear()
        {
            firstNode = lastNode = null;
            count = 0;
        }

        public bool Contains(T item)
        {

            Item<T> currentNode = firstNode;
            while (currentNode != null)
            {
                if (currentNode.Data.ToString().Equals(item.ToString()))
                {
                    return true;
                }
                currentNode = currentNode.Next;
            }
            return false;
        }

        public T RemoveFromFront()
        {
            T removedData = firstNode.Data;
            if (firstNode == lastNode)
                firstNode = lastNode = null;
            else
                firstNode = firstNode.Next;
            count--;
            return removedData;
        }
        public T RemoveFromBack()
        {
            var removedData = lastNode.Data;
            if (firstNode == lastNode)
                firstNode = lastNode = null;
            else
            {
                Item<T> currentNode = firstNode;
                while (currentNode.Next != lastNode)
                    currentNode = currentNode.Next;
                lastNode = currentNode;
                currentNode.Next = null;
            }
            count--;
            return removedData;

        }


        public bool Remove(T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            if (firstNode.Data.ToString().Equals(item.ToString()))
            {
                RemoveFromFront();
                return true;
            }
            else if (lastNode.Data.ToString().Equals(item.ToString()))
            {
                RemoveFromBack();
                return true;
            }
            else
            {
                var currentNode = firstNode;
                while (currentNode.Next != null)
                {
                    if (currentNode.Next.Data.ToString().Equals(item.ToString()))
                    {
                        currentNode.Next = currentNode.Next.Next;
                        count--;
                        if (currentNode.Next == null)
                            lastNode = currentNode;
                        return true;
                    }
                    currentNode = currentNode.Next;
                }
            }
            return false;
        }


        public int IndexOf(T item)
        {
            Item<T> node = firstNode;
            for (int i = 1; i < count; i++)
            {
                node = node.Next;
                if (node.Data.ToString().Equals(item.ToString()))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (item == null)
                throw new ArgumentNullException("item");
            if (index > count || index < 0)
                throw new ArgumentOutOfRangeException("index");
            if (index == 0)
                InsertAtFront(item);
            else if (index == (count - 1))
                InsertAtBack(item);
            else
            {
                var currentNode = firstNode;
                for (int i = 0; i < index - 1; i++)
                {
                    currentNode = currentNode.Next;
                }
                var newNode = new Item<T>(item, currentNode.Next);
                currentNode.Next = newNode;
                count++;
            }
        }
        public void InsertAtBack(T item)
        {
            lock (this)
            {
                if (IsEmpty)
                    firstNode = lastNode = new Item<T>(item);
                else
                    lastNode = lastNode.Next = new Item<T>(item);
                count++;
            }
        }

        public void InsertAtFront(T item)
        {

            if (IsEmpty)
                firstNode = lastNode = new Item<T>(item);
            else
                firstNode = new Item<T>(item, firstNode);
            count++;

        }

        public void RemoveAt(int index)
        {
            if (index > count || index < 0)
                throw new ArgumentOutOfRangeException("index");
            if (index == 0)
                RemoveFromFront();
            else if (index == (count - 1))
                RemoveFromBack();
            else
            {
                var currentNode = firstNode;
                for (int i = 1; i < index; i++)
                {
                    currentNode = currentNode.Next;
                }

                currentNode.Next = currentNode.Next.Next;
                count--;
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array is null)
                throw new ArgumentNullException("array");
            if ( count > array.Length)
                throw new ArgumentException("array");
            var currentNode = firstNode;
            for (int i = 2; i < arrayIndex; i++)
            {
                currentNode = currentNode.Next;
            }
            while (currentNode != null)
            {
                array[arrayIndex] = currentNode.Data;
                currentNode = currentNode.Next;
                arrayIndex++;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {

            Item<T> item = firstNode;
            while (item != null)
            {
                yield return item.Data;
                item = item.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
