using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructure
{
    class StackAlgorithms
    {
        # region Check is string s has right order of brackets
        public static bool IsBalanced(string s)
        {
            if (s.Length % 2 != 0) return false;
            var stack = new Stack<char>();
            for(int i = 0; i < s.Length; i++)
            {
                if(stack.Count == 0) stack.Push(s[i]);
                else{
                    var e = stack.Peek();
                    if (IsCloser(s[i]))
                    {
                        if (IsPair(e, s[i])) stack.Pop();
                        else return false;
                    }
                    else stack.Push(s[i]);
                }
            }
            return stack.Count == 0;
        }
        private static bool IsCloser(char b)
        {
            return b == ')' || b == '}' || b == ']';
        }
        private static bool IsPair(char a, char b)
        {
            if (a == '(' && b == ')') return true;
            if (a == '{' && b == '}') return true;
            if (a == '[' && b == ']') return true;
            return false;
        }
        public static void IsBalancedTest()
        {
            var strs = new string[]{"{{}}", "{({})}", "{{}", "{(})", "{}(}"};
            var res = new bool[]{true, true, false, false, false};
            for(int i = 0; i < strs.Length; i++)
            {
                Console.WriteLine("{0}\tIsBalanced {1}\tRes: {2}", strs[i], IsBalanced(strs[i]), res[i]);
            }
        }
        #endregion
    
        # region Make Prefix / Postfix notation from Infix one
        private static List<char> PrefixPostfixNotation(string s, char open = '(', char close = ')')
        {
            var res = new List<char>(s.Length);
            var stack = new Stack<char>();
            char e;
            for(int i = 0; i < s.Length; i++)
            {
                if (s[i] == open)
                {
                    stack.Push(s[i]);
                    continue;
                }
                if (s[i] == close)
                {
                    while((e = stack.Pop()) != open)
                    {
                        res.Add(e);
                    }
                    continue;
                }
                if (Char.IsLetterOrDigit(s[i]))
                {
                    res.Add(s[i]);
                    continue;
                }
                if(stack.Count == 0) 
                {
                    stack.Push(s[i]);
                    continue;
                }
                e = stack.Peek();
                if (Priority(s[i]) > Priority(e)) // s[i] has higher priority
                {
                    stack.Push(s[i]);
                    continue;
                }
                res.Add(stack.Pop());
                stack.Push(s[i]);
            }
            while (stack.Count != 0)
            {
                res.Add(stack.Pop());
            }
            return res;
        }
        public static string PostfixNotation(string s)
        {
            var res = PrefixPostfixNotation(s, '(', ')');
            return String.Join(" ", res);
        }
        public static string PrefixNotation(string s)
        {
            var input = String.Join("", s.Reverse());
            var res = PrefixPostfixNotation(input, ')', '(');
            res.Reverse();
            return String.Join(" ", res);
        }
        
        private static int Priority(char c)
        {
            if (c == '|') return 0;
            if (c == '^') return 1;
            if (c == '&') return 2;
            if (c == '+' || c == '-') return 3;
            if (c == '*' || c == '/') return 4;
            if (c == '%') return 5;
            return -1;
        }
        private static bool IsOperator(char c)
        {
            return (c == '|') || (c == '^') || (c == '&') || (c == '+' || c == '-') || (c == '*' || c == '/') || (c == '%');
        }
        
        public delegate Tuple<T, T> PopNotation<T>(Stack<T> stack);
        public static double CalculatePrefixPostfixNotation(string s, Dictionary<char, string> map, PopNotation<double> pop)
        {
            foreach (var key in map.Keys)
            {
                s = s.Replace(key.ToString(), map[key]);
            }
            Console.WriteLine(s);
            var chars = s.Split(' ');
            var stack = new Stack<double>();
            for(int i = 0; i < chars.Length; i++)
            {
                var e = chars[i];
                if (!IsOperator(e[0]))
                {
                    stack.Push(Double.Parse(e));
                    continue;
                }
                // e is an operator
                var t = pop(stack);
                var item1 = t.Item1;
                var item2 = t.Item2;
                double res = 0;
                switch (e)
                {
                    case("+"): res = item1 + item2; break;
                    case("-"): res = item1 - item2; break;
                    case("*"): res = item1 * item2; break;
                    case("/"): res = item1 / item2; break;
                }
                stack.Push(res);
            }
            return stack.Pop();
        }
        
        private static Tuple<T, T> PostPopNotation<T>(Stack<T> stack)
        {
            var item2 = stack.Pop();
            var t = new Tuple<T, T>(stack.Pop(), item2);
            return t;
        }
        public static double CalculatePostfixNotation(string s, Dictionary<char, string> map)
        {
            var pop = new PopNotation<double>(PostPopNotation<double>);
            return CalculatePrefixPostfixNotation(s, map, pop);
        }
        private static Tuple<T, T> PrePopNotation<T>(Stack<T> stack)
        {
            return new Tuple<T, T>(stack.Pop(), stack.Pop());
        }
        public static double CalculatePrefixNotation(string s, Dictionary<char, string> map)
        {
            var pop = new PopNotation<double>(PrePopNotation<double>);
            var strs = s.Split(' ');
            s = String.Join(' ', strs.Reverse());
            return CalculatePrefixPostfixNotation(s, map, pop);
        }

        public static void PostfixNotationTest()
        {
            var strs = new string[]{"(a+b)/4", "5+(a)*(b+c*d)", "a+b", "a+2-5", "a+b*c", "c+a*b/c", "a+b&c"};
            for(int i = 0; i < strs.Length; i++)
            {
                Console.WriteLine("Infix: {0}; Postfix: {1}", strs[i], PostfixNotation(strs[i]));
            }
        }
        public static void PrefixNotationTest()
        {
            var strs = new string[]{"(a+b)/4", "5+(a)*(b+c*d)", "a+b", "a+2-5", "a+b*c", "c+a*b/c", "a+b&c"};
            for(int i = 0; i < strs.Length; i++)
            {
                Console.WriteLine("Infix: {0}; Prefix: {1}", strs[i], PrefixNotation(strs[i]));
            }
        }
        public static void CalculatePostfixNotationTest()
        {
            var strs = new string[]{"5+(a)*(b+c*a)", "a+b", "a+2-5", "a+b*c", "c+a*b/c", "a+b&c"};
            var map = new Dictionary<char, string>()
            {
                { 'a', "3.5" }, 
                { 'b', "3" },
                { 'c', "4" },
            };
            for(int i = 0; i < strs.Length; i++)
            {
                Console.WriteLine("Infix: {0}; Postfix: {1}", strs[i], CalculatePostfixNotation(PostfixNotation(strs[i]), map));
            }
        }
        public static void CalculatePrefixNotationTest()
        {
            var strs = new string[]{"5+(a)*(b+c*a)", "a+b", "a+2-5", "a+b*c", "c+a*b/c", "a+b&c"};
            var map = new Dictionary<char, string>()
            {
                { 'a', "3.5" }, 
                { 'b', "3" },
                { 'c', "4" },
            };
            for(int i = 0; i < strs.Length; i++)
            {
                Console.WriteLine("Infix: {0}; Prefix: {1}", strs[i], CalculatePrefixNotation(PrefixNotation(strs[i]), map));
            }
        }
        # endregion
    
        # region Make Infix notation from Prefix / Postfix one

        public static string InfixNotationFromPrefixPostfix(string s, PopNotation<string> pop)
        {
            var strs = s.Split(' ');
            var stack = new Stack<string>();
            for(int i = 0; i < strs.Length; i++)
            {
                if(!IsOperator(strs[i][0]))
                {
                    stack.Push(strs[i]);
                    continue;
                }
                var t = pop(stack);
                var item1 = t.Item1;
                var item2 = t.Item2;
                stack.Push(String.Format("({0}{1}{2})", item1, strs[i], item2));
            }
            return stack.Pop();
        }

        public static string InfixNotationFromPostfix(string s)
        {
            var pop = new PopNotation<string>(PostPopNotation<string>);
            return InfixNotationFromPrefixPostfix(s, pop);
        }
        public static string InfixNotationFromPrefix(string s)
        {
            var pop = new PopNotation<string>(PrePopNotation<string>);
            var strs = s.Split(' ');
            s = String.Join(' ', strs.Reverse());
            return InfixNotationFromPrefixPostfix(s, pop);
        }
        public static void InfixNotationFromPostfixTest()
        {
            var strs = new string[]{"(a+b)/4", "5+(a)*(b+c*d)", "a+b", "a+2-5", "a+b*c", "c+a*b/c", "a+b&c"};
            for(int i = 0; i < strs.Length; i++)
            {
                var not = PostfixNotation(strs[i]);
                Console.WriteLine("Infix Initial: {0}; Postfix: {1}; Infix:{2}", strs[i], not, InfixNotationFromPostfix(not));
            }
        }
        public static void InfixNotationFromPrefixTest()
        {
            var strs = new string[]{"(a+b)/4", "5+(a)*(b+c*d)", "a+b", "a+2-5", "a+b*c", "c+a*b/c", "a+b&c"};
            for(int i = 0; i < strs.Length; i++)
            {
                var not = PrefixNotation(strs[i]);
                Console.WriteLine("Infix Initial: {0}; Prefix: {1}; Infix:{2}", strs[i], not, InfixNotationFromPrefix(not));
            }
        }

        # endregion

        # region Make Prefix notation from Postfix one and Postfix from Prefix
        public delegate string PushNotation(string op, string op1, string op2);
        public static string PrefixPostfixNotationFromPostfixPrefix(string s, PopNotation<string> pop, PushNotation push)
        {
            var strs = s.Split(' ');
            var stack = new Stack<string>();
            for(int i = 0; i < strs.Length; i++)
            {
                if(!IsOperator(strs[i][0]))
                {
                    stack.Push(strs[i]);
                    continue;
                }
                var t = pop(stack);
                var item1 = t.Item1;
                var item2 = t.Item2;
                //stack.Push(String.Format("{0} {1} {2}", item1, item2, strs[i]));
                stack.Push(push(strs[i], item1, item2));
            }
            return stack.Pop();
        }

        public static string PrefixNotationFromPostfix(string s)
        {
            var pop = new PopNotation<string>(PostPopNotation<string>);
            var push = new PushNotation((op, op1, op2) =>{
                return String.Format("{0} {1} {2}", op, op1, op2);
            });
            return PrefixPostfixNotationFromPostfixPrefix(s, pop, push);
        }
        public static string PostfixNotationFromPrefix(string s)
        {
            var pop = new PopNotation<string>(PrePopNotation<string>);
            var strs = s.Split(' ');
            s = String.Join(' ', strs.Reverse());
            var push = new PushNotation((op, op1, op2) =>{
                return String.Format("{0} {1} {2}", op1, op2, op);
            });
            return PrefixPostfixNotationFromPostfixPrefix(s, pop, push);
        }

        public static void PrefixNotationFromPostfixTest()
        {
            var strs = new string[]{"(a+b)/4", "5+(a)*(b+c*d)", "a+b", "a+2-5", "a+b*c", "c+a*b/c", "a+b&c"};
            for(int i = 0; i < strs.Length; i++)
            {
                var post = PostfixNotation(strs[i]);
                var pre = PrefixNotation(strs[i]);
                Console.WriteLine("Infix: {0}; Postfix: {1}; Prefix:{2}; PrefixFromPostfix: {3}", 
                strs[i], post, pre, PrefixNotationFromPostfix(post));
            }
        }
        public static void PostfixNotationFromPrefixTest()
        {
            var strs = new string[]{"(a+b)/4", "5+(a)*(b+c*d)", "a+b", "a+2-5", "a+b*c", "c+a*b/c", "a+b&c"};
            for(int i = 0; i < strs.Length; i++)
            {
                var post = PostfixNotation(strs[i]);
                var pre = PrefixNotation(strs[i]);
                Console.WriteLine("Infix: {0}; Postfix: {1}; Prefix:{2}; PostfixFromPrefix: {3}", 
                strs[i], post, pre, PostfixNotationFromPrefix(pre));
            }
        }
        # endregion
    
        # region Stock Span Problem
        // https://www.geeksforgeeks.org/the-stock-span-problem/
        public static int[] StockSpanProblem(int[] arr)
        {
            var res = new int[arr.Length];
            res[0] = 1;
            for(int i = 1; i < arr.Length; i++)
            {
                res[i] = 1;
                int j = i - 1;
                while (arr[i] > arr[j])
                {
                    res[i] += res[j];
                    j -= res[j];
                    if (j < 0) break;
                }
            }
            return res;
        }
        public static int[] StockSpanProblemStack(int[] arr)
        {
            var res = new int[arr.Length]; res[0] = 1;
            var stack = new Stack<int>(); stack.Push(0);
            for(int i = 1; i < arr.Length; i++)
            {
                while (stack.Count != 0 && arr[i] >= arr[stack.Peek()])
                    stack.Pop();
                res[i] = (stack.Count == 0) ? (i + 1) : (i - stack.Peek()); 
                stack.Push(i);
            }
            return res;
        }
        public static void StockSpanProblemTest()
        {
            //var arr = Helper.GenerateArray(20);
            //var arr = new int[]{100, 80, 60, 70, 60, 75, 85}; // res = {1, 1, 1, 2, 1, 4, 6}
            var arr = new int[]{74, 665, 742, 512, 254, 469, 748, 445, 663, 758, 38, 60, 724, 142, 330, 779, 317, 636, 591, 243, 289, 507, 241, 143, 65, 249, 247, 606, 691, 330, 371, 151, 607, 702, 394, 349, 430, 624, 85, 755, 357, 641, 167, 177, 332, 709, 145, 440, 627, 124, 738, 739, 119, 483, 530, 542, 34, 716, 640, 59, 305, 331, 378, 707, 474, 787, 222, 746, 525, 673, 671, 230, 378, 374, 298, 513, 787, 491, 362, 237, 756, 768, 456, 375, 32, 53, 151, 351, 142, 125, 367, 231, 708, 592, 408, 138, 258, 288, 554, 784, 546, 110, 210, 159, 222, 189, 23, 147, 307, 231, 414, 369, 101, 592, 363, 56, 611, 760, 425, 538, 749, 84, 396, 42, 403, 351, 692, 437, 575, 621, 597, 22, 149, 800};
            Console.WriteLine(arr.ToStringExtended());
            var res1 = StockSpanProblem(arr);
            Console.WriteLine(res1.ToStringExtended());
            var res2 = StockSpanProblemStack(arr);
            Console.WriteLine(res2.ToStringExtended());
        }

        # endregion
    
        # region Next Greater Element
        // 
        public static int[] NextGreaterElement(int[] arr)
        {
            var res = new int[arr.Length];
            res[arr.Length - 1] = -1;
            for(int i = arr.Length - 2; i >= 0; i--)
            {
                int j = i + 1;
                while (j != -1 && arr[i] >= arr[j])
                {
                    j = res[j];
                }
                res[i] = j;
            }
            return res;
        }
        
        public static int[] NextGreaterElementStack(int[] arr)
        {
            var res = new int[arr.Length]; res[arr.Length - 1] = -1;
            var stack = new Stack<int>(); stack.Push(arr.Length - 1);
            for(int i = arr.Length - 2; i >= 0; i--)
            {
                while (stack.Count != 0 && arr[i] >= arr[stack.Peek()])
                    stack.Pop();
                res[i] = (stack.Count == 0) ? -1 : stack.Peek(); 
                stack.Push(i);
            }
            return res;
        }
        public static int[] NextGreaterElementStack2(int[] arr)
        {
            var res = new int[arr.Length];
            var stack = new Stack<int>(); stack.Push(0);
            for(int i = 1; i < arr.Length; i++)
            {
                while (stack.Count != 0 && arr[i] > arr[stack.Peek()])
                {
                    var j = stack.Pop();
                    res[j] = i;
                }
                stack.Push(i);
            }
            while(stack.Count != 0)
            {
                res[stack.Pop()] = -1;
            }
            return res;
        }
        public static void NextGreaterElementTest()
        {
            var arr = Helper.GenerateArray(10);
            //var arr = new int[]{4, 5, 2, 25}; // 5 25 25 -1
            Console.WriteLine(arr.ToStringExtended());
            var res = NextGreaterElement(arr);
            Console.WriteLine(res.ToStringExtended());
            for(int i = 0; i < res.Length; i++)
            {
                if (res[i] == -1)
                     Console.Write("{0}\t", -1);
                else
                    Console.Write("{0}\t", arr[res[i]]);
            }
            Console.WriteLine();
            Console.WriteLine("===========================");
            var res2 = NextGreaterElementStack(arr);
            Console.WriteLine(res2.ToStringExtended());
            for(int i = 0; i < res2.Length; i++)
            {
                if (res2[i] == -1)
                     Console.Write("{0}\t", -1);
                else
                    Console.Write("{0}\t", arr[res2[i]]);
            }
            Console.WriteLine();
            Console.WriteLine("===========================");
            var res3 = NextGreaterElementStack2(arr);
            Console.WriteLine(res3.ToStringExtended());
            for(int i = 0; i < res3.Length; i++)
            {
                if (res3[i] == -1)
                     Console.Write("{0}\t", -1);
                else
                    Console.Write("{0}\t", arr[res3[i]]);
            }
            Console.WriteLine();
        }
        #endregion

        # region Itertive Hanoi Tower
        public static string HanoiTower(int n)
        {
            // s - source tower, d - destination tower, a - auxillary tower
            // n = 1
            var s1 = "s -> d;"; // from source to destination
            var s2 = "s -> a;"; // from source to auxillary
            var s3 = "a -> d;"; // from auxillary to destination
            var s4 = "d -> a;"; // from destination to auxillary
            var s5 = "d -> s;"; // from destination to source
            var s6 = "a -> s;"; // from auxillary to source
            for(int i = 2; i <= n; i++)
            {
                var new_s1 = s2 + "s -> d;" + s3;
                var new_s2 = s1 + "s -> a;" + s4;
                var new_s3 = s6 + "a -> d;" + s1;
                var new_s4 = s5 + "d -> a;" + s2;
                var new_s5 = s4 + "d -> s;" + s6;
                var new_s6 = s3 + "a -> s;" + s5;
                s1 = new_s1;
                s2 = new_s2;
                s3 = new_s3;
                s4 = new_s4;
                s5 = new_s5;
                s6 = new_s6;
            }
            // Just to check
            var count = s1.Split(';').Count() - 1;
            Console.WriteLine(count);
            return s1;
        }
        public static void HanoiTowerTest()
        {
            var res = HanoiTower(10);
            Console.WriteLine(res);
        }
        #endregion

        # region Next Greater Element
        // 
        public static int[] Method(int[] arr)
        {
            return arr;
        }
        public static void MethodTest()
        {
            var arr = Helper.GenerateArray(20);
            Console.WriteLine(arr.ToStringExtended());
            var res = Method(arr);
            Console.WriteLine(res);
        }
        #endregion
    }


}