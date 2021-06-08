using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CustomArray
{
    public class CustomArray<T> : IEnumerable<T>
    {
        public T[] customArray;
        private int first;
        private int length;

        public int First 
        {
            get => first;
            private set => first = value;
        }

        public int Last 
        {
            get => length-1 + first;
        }

        public int Length 
        {
            get => length;
            private set => length = value;
        }

        public T[] Array
        {
            get => customArray;
        }
       
        public CustomArray(int first, int length)
        {

            if(length <= 0)
            {
                throw new ArgumentException("ArgumentException");
            }
            this.first = first;
            customArray = new T[length];
            this.length = length;
        }

        public CustomArray(int first, IEnumerable<T> list)
        {
            if (list == null)
                throw new NullReferenceException();
            if (!list.Any())
                throw new ArgumentException("ArgumentException");
            this.first = first;
            customArray = list.ToArray();
            this.length = customArray.Length;
        }

        public CustomArray(int first, params T[] list)
        {
            if (!list.Any())
                throw new ArgumentException("ArgumentException");
            if (list == null)
                throw new ArgumentNullException("list");
            this.first = first;
            customArray = list;
            this.length = customArray.Length;
        }

        public T this[int item]
        {
            get
            {
                if (item > customArray.Length || item < first)
                    throw new ArgumentException("ArgumentException");

                return customArray[item - first];
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("item");
                if (item >= customArray.Length || item < first || first + customArray.Length == item)
                    throw new ArgumentException("ArgumentException");

                customArray[item - first] = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach(var x in customArray)
            {
                yield return x;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return customArray.GetEnumerator();
        }
    }
}
