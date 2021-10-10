using System;
using System.Linq;


namespace MathHelper.Algebra
{
    class Polynom : Vector
    {
        const double eps = 0.01;
        public string Variable { get; set; } = "x";
        public string Pattern { get; set; } = "{0}^{1}";
        public Polynom(int N) : base(N) { }
        public Polynom(Vector v) : base(v) { }
        public Polynom(params double[] a):base(a){ }
        //Pattern is used for specific elements of sum
        //Pattern for Console: "{0}^{1}"
        //        for Latex:   "{{0}}^{{1}}"
        public override string ToString()
        {
            string s = "";
            bool flag = true;
            int j = this.Length - 1;
            while (j >= 0)
            {
                if (Vec[j] != 0)
                {
                    if ((flag == false) && (Vec[j] > 0)) s += "+";
                    if (flag == true) flag = false;

                    if (Vec[j] < 0) s += "-";
                    if (Math.Abs(Vec[j]) != 1)
                    {
                        if ((Vec[j] - (int)Vec[j]) < eps)
                            s += String.Format("{0:F0}", Math.Abs(Vec[j]));
                        else
                            s += String.Format("{0:F4}", Math.Abs(Vec[j]));
                    }
                    if (j >= 2)
                        s += String.Format(Pattern, Variable, j);
                    if (j == 1)
                        s += String.Format(Pattern.Split('^').First(), Variable);
                    if ((j == 0) && (Math.Abs(Vec[j]) == 1))
                    {
                        if ((Vec[j] - (int)Vec[j]) < eps)
                            s += String.Format("{0:F0}", Math.Abs(Vec[j]));
                        else
                            s += String.Format("{0:F4}", Math.Abs(Vec[j]));
                    }

                }
                j--;
            }
            return s;
        }
        public double Function(double x)//v[0]+v[1]x+v[2]*x*x+...
        {
            double s = 0; double a = 1;
            for (int i = 0; i < this.Length; i++)
            {
                s += this[i] * a;
                a *= x;
            }
            return s;
        }
        public Polynom MultipleByX()
        {
            Polynom res = new Polynom(this.Length);
            res[0] = 0;
            for (int i = 1; i < res.Length; i++)
                res[i] = this[i - 1];
            return res;
        }
        
    }
}
