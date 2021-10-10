using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructure
{
    class CustomList<T> where T : IComparable
    {
        T[] arr;
        public T[] Arr{get {return arr;}}
        int capacity;
        public int Capacity {get {return capacity;}}
        int count;
        public int Count { get{return count;} }
        public CustomList(int _capacity){
            arr = new T[capacity];
            capacity = _capacity;
        }
        public CustomList():this(0){}
        
        # region Add elements
        public void Add(T item)
        {
            Relocate(1);
            arr[count] = item;
            this.count++;
        }
        public void AddRange(T[] items)
        {
            var countNew = items.Length;
            Relocate(countNew);
            for(var i = 0; i < countNew; i++)
            {
                arr[count + i] = items[i];
            }
            this.count += countNew;
        }
        public void Insert(int index, T item)
        {
            var newArr = Allocate(1);
            for(int i = 0; i < index; i++)
            {
                newArr[i] = arr[i];
            }
            newArr[index] = item;
            for(int i = index; i < count; i++)
            {
                newArr[i + 1] = arr[i];
            }
            arr = newArr;
            this.count++;
        }
        public void InsertRange(int index, T[] items)
        {
            var newArr = Allocate(items.Length);
            for(int i = 0; i < index; i++)
            {
                newArr[i] = arr[i];
            }
            for(int i = 0; i < items.Length; i++)
            {
                newArr[index + i] = items[i];
            }
            for(int i = index; i < count; i++)
            {
                newArr[i + items.Length] = arr[i];
            }
            arr = newArr;
            this.count+=items.Length;
        }
        private T[] Allocate(int countNew)
        {
            while(capacity < count + countNew)
            {
                capacity = (capacity == 0)? 8: capacity * 2;
            }
            var newArr = new T[capacity];
            return newArr;
            
        }
        private void Relocate(int countNew)
        {
            while(capacity < count + countNew)
            {
                capacity = (capacity == 0)? 8: capacity * 2;
            }
            var newArr = new T[capacity];
            for (var i = 0; i < count; i++)
            {
                newArr[i] = arr[i];
            }
            arr = newArr;
            
        }
        # endregion
        # region Remove elements
        public void RemoveAt(int index)
        {
            var newArr = Delocate(1);
            var i  = 0;
            for(i = 0; i < index; i++)
            {
                newArr[i] = arr[i];
            }
            for(i = index; i < count - 1; i++)
            {
                newArr[i] = arr[i + 1];
            }
            arr = newArr;
            count--;
        }
        public void RemoveBatch(int[] indeces)
        {
            var ids = new List<int>(indeces);
            ids.Sort();
            ids = ids.Distinct().ToList();
            var newArr = Delocate(ids.Count);
            var old_j = 0;
            var j = 0;
            for(var i = 0; i < ids.Count; i++)
            {
                for(; old_j < ids[i]; j++, old_j++)
                {
                    newArr[j] = arr[old_j];
                }
                old_j++;
            }
            for(; old_j < count; j++, old_j++)
            {
                newArr[j] = arr[old_j];
            }
            count -= ids.Count;
            arr = newArr;
            
        }
        
        public void RemoveRange(int index, int _count)
        {
            var newArr = Delocate(_count);
            var i  = 0;
            for(i = 0; i < index; i++)
            {
                newArr[i] = arr[i];
            }
            for(i = index; i < count - _count; i++)
            {
                newArr[i] = arr[i + _count];
            }
            arr = newArr;
            count -= _count;
        }
        private T[] Delocate(int countDel)
        {
            while(capacity >= 2 * (count - countDel))
            {
                capacity = capacity / 2;
            }
            var newArr = new T[capacity];
            return newArr;
        }
        # endregion
    
        public T this[int i]
        {
            get { return arr[i]; }
            set { arr[i] = value; }
        }
        #region Sort
        public void Sort()
        {
            var stack = new Stack<Tuple<int, int>>();
            stack.Push(new Tuple<int, int>(0, count - 1));
            while(stack.Count != 0)
            {
                var range = stack.Pop();
                var j = Partition(arr, range.Item1, range.Item2);
                if (j - 1 - range.Item1 > 0)
                    stack.Push(new Tuple<int, int>(range.Item1, j - 1));
                if (range.Item2 - (j + 1) > 0)
                    stack.Push(new Tuple<int, int>(j + 1, range.Item2));
                
            }
        }

        private int Partition(T[] arr, int low, int high)
        {
            var j = (new Random()).Next(high - low) + low;
            Swap(ref arr[low], ref arr[j]);
            var i = low; j = high + 1;
            while(true)
            {
                while(arr[++i].CompareTo(arr[low]) == -1) // arr[++i] < arr[low]
                {
                    if(i == high) break;
                }
                while(arr[low].CompareTo(arr[--j]) == -1) // arr[--j] >= arr[low]
                {
                    if (j <= low) break;
                }
                if (i >= j) break;
                Swap(ref arr[i], ref arr[j]);
            }
            Swap(ref arr[low], ref arr[j]);
            return j;
        }
        private void Swap(ref T a, ref T b)
        {
            var tmp = a; a = b; b = tmp;
        }
        #endregion
    }

    class CustomListTest{
        public static void TestAdd()
        {
            var list = new CustomList<int>();
            for(var i = 0; i < 20; i++)
            {
                list.Add(i);
                Console.WriteLine("capacity: {0}; count: {1}; elements: {2}", list.Capacity, list.Count, 
                String.Join(' ', list.Arr));
            }
        }

        public static void TestAddRange()
        {
            var list = new CustomList<int>();
            for(var i = 0; i < 20; i++)
            {
                var arr = new int[]{i, i * 2};
                list.AddRange(arr);
                Console.WriteLine("capacity: {0}; count: {1}; elements: {2}", list.Capacity, list.Count, 
                String.Join(' ', list.Arr));
            }
        }

        public static void TestInsertItem()
        {
            var list = new CustomList<int>();
            Random rnd = new Random(DateTime.Now.Second);
            for(var i = 0; i < 20; i++)
            {
                var id = rnd.Next(list.Count);
                list.Insert(id, i);
                Console.WriteLine("capacity: {0}; count: {1}; id: {2}; elements: {3}", list.Capacity, 
                list.Count, id,
                String.Join(' ', list.Arr));
            }
        }

        public static void TestInsertRange()
        {
            var list = new CustomList<int>();
            for(var i = 0; i < 20; i++)
            {
                Random rnd = new Random(DateTime.Now.Second);
                var id = rnd.Next(list.Count);
                var arr = new int[]{i, i * 2};
                list.InsertRange(id, arr);
                Console.WriteLine("capacity: {0}; count: {1}; id: {2}; elements: {3}", list.Capacity, 
                list.Count, id,
                String.Join(' ', list.Arr));
            }
        }
        public static void TestRemoveItem()
        {
            var list = new CustomList<int>();
            for(var i = 0; i < 20; i++)
            {
                list.Add(i);
            }
            Console.WriteLine("capacity: {0}; count: {1}; elements: {2}", list.Capacity, list.Count, 
                String.Join(' ', list.Arr));
            Random rnd = new Random(DateTime.Now.Second);
            for(var i = 0; i < 15; i++)
            {
                var id = rnd.Next(list.Count);
                list.RemoveAt(id);
                Console.WriteLine("capacity: {0}; count: {1}; index: {2}; elements: {3}", list.Capacity, 
                list.Count, id,
                String.Join(' ', list.Arr));
            }
        }
        public static void TestRemoveBatch()
        {
            var list = new CustomList<int>();
            for(var i = 0; i < 20; i++)
            {
                list.Add(i);
            }
            Console.WriteLine("capacity: {0}; count: {1}; elements: {2}", list.Capacity, list.Count, 
                String.Join(' ', list.Arr));
            Random rnd = new Random(DateTime.Now.Second);
            for(var i = 0; i < 5; i++)
            {
                var ids = new int[]{rnd.Next(list.Count), rnd.Next(list.Count), rnd.Next(list.Count)};
                list.RemoveBatch(ids);
                Console.WriteLine("capacity: {0}; count: {1}; index: {2}; elements: {3}", list.Capacity, 
                list.Count, String.Join(' ', ids),
                String.Join(' ', list.Arr));
            }
        }
        
        public static void TestRemoveRange()
        {
            var list = new CustomList<int>();
            for(var i = 0; i < 20; i++)
            {
                list.Add(i);
            }
            Console.WriteLine("capacity: {0}; count: {1}; elements: {2}", list.Capacity, list.Count, 
                String.Join(' ', list.Arr));
            Random rnd = new Random(DateTime.Now.Second);
            for(var i = 0; i < 7; i++)
            {
                var id = rnd.Next(list.Count - 1);
                list.RemoveRange(id, 2);
                Console.WriteLine("capacity: {0}; count: {1}; index: {2}; elements: {3}", list.Capacity, 
                list.Count, id,
                String.Join(' ', list.Arr));
            }
        }
        
        public static void Test()
        {
            Console.WriteLine(1.CompareTo(3));
            Console.WriteLine(2.CompareTo(2));
            Console.WriteLine(3.CompareTo(1));
        }
        public static void TestSort()
        {
            var clist = new CustomList<int>();
            var list = new List<int>();
            Random rnd = new Random(DateTime.Now.Second);
            for(var i = 0; i < 20; i++)
            {
                var e = rnd.Next(20);
                list.Add(e);
                clist.Add(e);
            }
            for(int i = 0; i < clist.Count; i++)
            {
                Console.Write(clist[i] + " ");
            }
            Console.WriteLine();
            list.Sort();
            clist.Sort();
            Console.WriteLine(String.Join(' ', clist.Arr));
            Console.WriteLine(String.Join(' ', list));
        }
        
        public static void RunAllTests()
        {
            //TestAdd();
            //TestAddRange();
            //TestInsertItem();
            //TestInsertRange();
            //TestRemoveItem();
            //TestRemoveBatch();
            //TestRemoveRange();
            //Test();
            //TestSort();
        }

    }
}