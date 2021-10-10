using System;

namespace L2
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomString customString = new CustomString("asdkdfn435f");
            Console.WriteLine(customString);
            customString.ShiftLeft(3);
            Console.WriteLine(customString);
            customString.ShiftRight(3);
            Console.WriteLine(customString);

            Console.WriteLine("Digits: ");
            Digits digits = new Digits(343454);
            Console.WriteLine(digits);
            digits.Shift();
            Console.WriteLine(digits);

            Console.WriteLine("Small letters: ");
            SmallLetters smallLetters = new SmallLetters("sfTYUbnd");
            Console.WriteLine(smallLetters);
            smallLetters.Shift();
            Console.WriteLine(smallLetters);

        }
    }

    public class CustomString
    {
        public char[] Arr;

        # region Constructors
        public CustomString(string s)
        {
            Arr = s.ToCharArray();
        }

        # endregion

        // Methods
        public override string ToString()
        {
            return String.Join("", Arr);
        }
        public virtual int Length()
        {
            
            return Arr.Length;
        }
        public virtual void Shift(){}
        public void ShiftLeft(int step = 1)  
        {  
            char[] tmp = new char[step];
            for(int i = 0; i < tmp.Length; i++)
            {
                tmp[i] = Arr[i];
            }
            for(int i = 0; i < Arr.Length - step; i++)
            {
                Arr[i] = Arr[i + step];
            }
            for(int i = 0; i<tmp.Length; i++)
            {
                Arr[Arr.Length + i - step] = tmp[i];
            }
            
        }
        public void ShiftRight(int step = 1)  
        {  
            char[] tmp = new char[step];
            for(int i = 0; i < tmp.Length; i++)
            {
                tmp[i] = Arr[Arr.Length - step + i];
            }
            for(int i = Arr.Length - 1; i >= step; i--)
            {
                Arr[i] = Arr[i - step];
            }
            for(int i = 0; i<tmp.Length; i++)
            {
                Arr[i] = tmp[i];
            }
            
        }

    }

    public class Digits : CustomString
    {
        public Digits(int number) : base(number.ToString()){}
        public override int Length()
        {
            Console.WriteLine("It is overriden method for digits");
            return ((CustomString)this).Length();
        }
        public override void Shift()
        {
            Console.WriteLine("It is overriden method for digits");
            ((CustomString)this).ShiftRight(1);
        }
    }
    public class SmallLetters : CustomString
    {
        public SmallLetters(string s) : base(s.ToLower()){}
        public override int Length()
        {
            Console.WriteLine("It is overriden method for small letters");
            return ((CustomString)this).Length();
        }
        public override void Shift()
        {
            Console.WriteLine("It is overriden method for small letters");
            ((CustomString)this).ShiftLeft(1);
        }
    }
}
