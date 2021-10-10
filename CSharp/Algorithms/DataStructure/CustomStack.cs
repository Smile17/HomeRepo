using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructure
{
    class CustomStack<T> where T : IComparable
    {
        CustomList<T> list;
        public T[] Arr{get {return list.Arr;}}
        public int Capacity {get {return list.Capacity;}}
        public int Count { get{return list.Count;} }
        public CustomStack(int _capacity){
            list = new CustomList<T>(_capacity);
        }
        public CustomStack():this(0){}
        
        # region Add elements
        public void Push(T item)
        {
            list.Add(item);
        }
        public void PushRange(T[] items)
        {
            list.AddRange(items);
        }
        # endregion
        # region Peek & Pop elements
        public T Peek()
        {
            return list[list.Count - 1];
        }
        public T Pop()
        {
            var item = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            return item;
        }
        
        # endregion
    }

    class CustomStackTest{
        public static void TestPush()
        {
            var list = new CustomStack<int>();
            for(var i = 0; i < 20; i++)
            {
                list.Push(i);
                Console.WriteLine("capacity: {0}; count: {1}; elements: {2}", list.Capacity, list.Count, 
                String.Join(' ', list.Arr));
            }
        }

        public static void TestPushRange()
        {
            var list = new CustomStack<int>();
            for(var i = 0; i < 20; i++)
            {
                var arr = new int[]{i, i * 2};
                list.PushRange(arr);
                Console.WriteLine("capacity: {0}; count: {1}; elements: {2}", list.Capacity, list.Count, 
                String.Join(' ', list.Arr));
            }
        }

        public static void TestPop()
        {
            var list = new CustomStack<int>();
            for(var i = 0; i < 20; i++)
            {
                list.Push(i);
            }
            Console.WriteLine("capacity: {0}; count: {1}; elements: {2}", list.Capacity, list.Count, 
                String.Join(' ', list.Arr));
            for(var i = 0; i < 15; i++)
            {
                var item = list.Pop();
                Console.WriteLine("capacity: {0}; count: {1}; item: {2}; elements: {3}", list.Capacity, 
                list.Count, item,
                String.Join(' ', list.Arr));
            }
        }
        public static void TestPeek()
        {
            var list = new CustomStack<int>();
            for(var i = 0; i < 20; i++)
            {
                list.Push(i);
            }
            Console.WriteLine("capacity: {0}; count: {1}; elements: {2}", list.Capacity, list.Count, 
                String.Join(' ', list.Arr));
            for(var i = 0; i < 5; i++)
            {
                var item = list.Peek();
                Console.WriteLine("capacity: {0}; count: {1}; item: {2}; elements: {3}", list.Capacity, 
                list.Count, item,
                String.Join(' ', list.Arr));
            }
        }
        public static void RunAllTests()
        {
            //TestPush();
            //TestPushRange();
            //TestPop();
            TestPeek();
        }

    }
}