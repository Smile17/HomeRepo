using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.IO;
using System.Linq;

namespace Recursion
{
    class Tasks
    {
        // We can use BigInteger instead of int for bigger values

        # region Task 1
        // https://tproger.ru/articles/dynprog-starters/
        // Вычислить n-й член последовательности, заданной формулами:
        //    a2n = an ­+ an-1,
        //    a2n+1 = an – an-1,
        //    a0 = a1 = 1.

        // Just a simple recursion
        public static BigInteger Task1_0(int k)
        {
            if (k <= 1) return 1;
            if (k % 2 == 0) return Task1_0(k / 2) + Task1_0((k / 2) - 1);
            else return Task1_0(k / 2) - Task1_0((k / 2) - 1);
        }

        // Save all previous data to an array
        public static BigInteger Task1_1(int k)
        {
            if (k <= 1) return 1;
            var arr = new BigInteger[k + 1];
            arr[0] = arr[1] = 1;
            for (int i = 2; i < k + 1; i++)
            {
                if (i % 2 == 0)
                {
                    arr[i] = arr[i / 2] + arr[(i/2) - 1];
                }
                else{
                    arr[i] = arr[i / 2] - arr[(i/2) - 1];
                }
            }
            return arr[k];
        }

        // Calculate only needed values for calculating k-th value
        public static BigInteger Task1_2(int k)
        {
            if (k <= 1) return 1;
            // Find out needed indexes
            var stack = new List<int>();
            var queue = new Queue<int>();
            stack.Add(k);
            queue.Enqueue(k);
            while(queue.Count > 0)
            {
                var num = queue.Dequeue();
                
                if (num >= 2)
                {
                    queue.Enqueue(num / 2);
                    stack.Add(num / 2);
                    queue.Enqueue((num / 2) - 1);
                    stack.Add((num / 2) - 1); 
                }
            }
            var map = new Dictionary<int, BigInteger>();
            for (int j = stack.Count - 1; j >= 0; j--)
            {
                var i = stack[j];
                if (!map.ContainsKey(i))
                {
                    if (i == 0 || i == 1)
                        map.Add(i, 1);
                    else
                        if (i % 2 == 0)
                            map.Add(i, map[i / 2] + map[(i / 2) - 1]);
                        else
                            map.Add(i, map[i / 2] - map[(i / 2) - 1]);
                }
            }
            return map[k];

        }
        
        public static void TestTimeMemoryTask1(){
            // Run Jupyter notebook for plots
            var outp = new StreamWriter("time.txt");
            outp.WriteLine("k;Time Recursive;Memory Recursive;Time With Array;Memory With Array;Time With Stack;Memory With Stack;");
            for(int i = 0; i <= 100000; i += 1000){
                outp.Write(i + ";");
                Timer.RunExperimentForOneRecord(outp, 
                    () => Console.WriteLine(Tasks.Task1_0(i)),
                    () => Console.WriteLine(Tasks.Task1_1(i)),
                    () => Console.WriteLine(Tasks.Task1_2(i))
                );
                outp.WriteLine();
            }
            outp.Close();
        }
        #endregion
    
        #region Task 2
        // an = an-1 + an-2 + an-3
        public static BigInteger Task2_1(int k)
        {
            // We need only 3 last values to save
            var arr = new BigInteger[3];
            arr[0] = arr[1] = arr[2] = 1;
            for (int i = 3; i < k + 1; i++)
            {
                arr[i % 3] = arr[(i-1) % 3] + arr[(i-2) % 3] + arr[(i-3) % 3];
                //Console.Write(arr[i % 3] + " ");
            }
            return arr[k % 3];
        }

        public static BigInteger Task2_2(int k)
        {
            if (k < 3) return 1;
            // We need only 3 last values to save
            BigInteger x, y, z;
            x = y = z = 1;
            for (int i = 3; i < k + 1; i++)
            {
                z = x + y + z; // last element
                y = z - x - y;
                x = z - x - y;
                //Console.Write(z + " ");
            }
            return z;
        }

        public static void TestTimeMemoryTask2(){
            // Run Jupyter notebook for plots
            var outp = new StreamWriter("time_task2.txt");
            outp.WriteLine("k;Time With Array;Memory With Array;Time With Variable;Memory With Variable;");
            for(int i = 100; i <= 1000; i += 100){
                outp.Write(i + ";");
                Timer.RunExperimentForOneRecord(outp, 
                    () => Console.WriteLine(Tasks.Task2_1(i)),
                    () => Console.WriteLine(Tasks.Task2_2(i))
                );
                outp.WriteLine();
            }
            outp.Close();
        }
        #endregion

        # region 2 dimension
        public static BigInteger Task3_1(int k, int m)
        {
            if (k == 1 || m == 1) return 1;
            var arr = new BigInteger[k, m];
            for(int i = 0; i < k; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    if (i == 0 || j == 0)
                        arr[i, j] = 1;
                    else
                        arr[i, j] = arr[i - 1, j] + arr[i, j - 1];
                    //Console.Write(arr[i, j] + " ");
                }
                //Console.WriteLine();
            }
            return arr[k - 1, m - 1];

        }

        public static BigInteger Task3_2(int k, int m)
        {
            if (k == 1 || m == 1) return 1;
            if (k > m){
                var tmp = k;
                k = m;
                m = tmp;
            } // k <= m
            var arr = Enumerable.Repeat<BigInteger>(1, k - 1).ToArray();
            //for (int i = 0; i < k; i++)
            //{
            //    Console.Write("1 ");
            //}
            //Console.WriteLine();
            
            for(int i = 1; i < m; i++)
            {
                //Console.Write("1 "); 
                arr[0] += 1;
                //Console.Write(arr[0] + " ");
                for (int j = 1; j < k - 2; j++)
                {
                    arr[j] += arr[j - 1];
                    //Console.Write(arr[j] + " ");
                }
                //Console.WriteLine();
            }
            return arr[k - 2];

        }
        public static void TestTimeMemoryTask3(){
            // Run Jupyter notebook for plots
            var outp = new StreamWriter("time_task3.txt");
            outp.WriteLine("k;Time With Array;Memory With Array;Time With Smaller Array;Memory With Smaller Array;");
            for(int i = 1; i <= 100; i += 10){
                outp.Write(i + ";");
                Timer.RunExperimentForOneRecord(outp, 
                    () => Console.WriteLine(Tasks.Task3_1(i, 10)),
                    () => Console.WriteLine(Tasks.Task3_2(i, 10))
                );
                outp.WriteLine();
            }
            outp.Close();
        }
        #endregion
    
        /*
        Имеется калькулятор, который выполняет три операции:
        Прибавить к числу X единицу;
        Умножить число X на 2;
        Умножить число X на 3.
        Определите, какое наименьшее число операций необходимо для того, чтобы получить из числа 1 заданное число N. Выведите это число, и, на следующей строке, набор исполненных операций вида «111231».
         */
         public static BigInteger Task4(int X)
         {
            var arr = new BigInteger[X + 1];
            arr[1] = 0;
            for(int i = 2; i <= X; i++)
            {
                var tmp = new List<BigInteger>();
                tmp.Add(arr[i - 1]);
                if (i % 2 == 0) tmp.Add(arr[i / 2]);
                if (i % 3 == 0) tmp.Add(arr[i / 3]);
                arr[i] = tmp.Min() + 1;
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
            // Get the path to X
            string ret = X + " ";
            int j = X;
            while (j > 1)
            {
                if (arr[j - 1] == arr[j] - 1) j = j - 1;
                else if (j % 2 == 0 && arr[j / 2] == arr[j] - 1) j = j / 2;
                else if (j % 3 == 0 && arr[j / 3] == arr[j] - 1) j = j / 3;
                ret += j + " "; 
            }
            Console.WriteLine(ret);
            return arr[X];
         }
    
        /*
        В каждой клетке прямоугольной таблицы N*M записано некоторое число. Изначально игрок находится в левой верхней клетке. За один ход ему разрешается перемещаться в соседнюю клетку либо вправо, либо вниз (влево и вверх перемещаться запрещено). При проходе через клетку с игрока берут столько килограммов еды, какое число записано в этой клетке (еду берут также за первую и последнюю клетки его пути).
        Требуется найти минимальный вес еды в килограммах, отдав которую игрок может попасть в правый нижний угол.
         */
        public static BigInteger Task5(int[,] arr_raw)
        {
            var arr = arr_raw.Clone() as int[,];
            int i, j;
            for (i = 1; i < arr.GetLength(0); i++)
            {
                arr[i, 0] += arr[i - 1, 0];
            }
            for (j = 1; j < arr.GetLength(1); j++)
            {
                arr[0, j] += arr[0, j - 1];
            }
            for (i = 1; i < arr.GetLength(0); i++)
            {
                for (j = 1; j < arr.GetLength(1); j++)
                {
                    arr[i, j] += Math.Min(arr[i - 1, j], arr[i, j - 1]);
                }
            }
            Console.WriteLine(arr.ToStringExtended());

            // Get the path to X
            string ret = (arr.GetLength(0) - 1) + ";" + (arr.GetLength(1) - 1) + "\n";
            i = (arr.GetLength(0) - 1);
            j = (arr.GetLength(1) - 1);
            while (i + j > 0)
            {
                if (i == 0)
                    j--;
                else if (j == 0)
                    i--;
                else{
                    if (arr[i - 1, j] == arr[i, j] - arr_raw[i, j])
                        i--;
                    else j--;
                }
                ret += i + ";" + j + "\n"; 
            }
            Console.WriteLine(ret);            
            return arr[arr.GetLength(0) - 1, arr.GetLength(1) - 1];
        }
        
        /*
        У вас есть неограниченное количество монет достоинством 25, 10, 5 и 1 цент. Напишите код, определяющий количество способов представления n центов
         */
         public static BigInteger Task6(int n, int denom)
         {
            if (n == 0) return 1;
            int next_denom = 1;
            switch (denom){
            case 25:
                next_denom = 10;
                break;
            case 10:
                next_denom = 5;
                break;
            case 5:
                next_denom = 1;
                break;
            case 1:
                return 1;
            }
            BigInteger ways = 0;
            for (int i = 0; i * denom <= n; i++)
                ways += Task6(n - i * denom, next_denom);
            return ways;
         }

         /*
         Представьте себе треугольник, составленный из чисел. Одно число расположено в вершине. Ниже размещено два числа, затем три, и так до нижней грани. Вы начинаете на вершине, и нужно спуститься к основанию треугольника. За каждый ход вы можете спуститься на один уровень и выбрать между двумя числами под текущей позицией. По ходу движения вы «собираете» и суммируете числа, которые проходите. Ваша цель – найти максимальную сумму, которую можно получить из различных маршрутов.
          */
          public static BigInteger Task7(List<List<int>> list)
          {
              var dyn_list = new List<List<int>>(list.Select(el => new List<int>(el)));
              for (int i = 1; i < dyn_list.Count; i++)
              {
                  dyn_list[i][0] += dyn_list[i-1][0]; 
                  for(int j = 1; j < i; j++)
                  {
                      dyn_list[i][j] += Math.Max(dyn_list[i-1][j], dyn_list[i-1][j-1]);
                  }
                  dyn_list[i][i] += dyn_list[i-1][i-1]; 
              }
              Console.WriteLine(dyn_list.ToStringExtended());
              return dyn_list[dyn_list.Count - 1].Max();
          }
    }
}