using MathHelper.Algebra;
using System;


namespace MathHelper.Latex
{
    class ToLatex
    {
        //Import matrix to Latex as a table
        //This latex code needs to include the following package to Latex file
        //\usepackage{longtable}
        public static string MatrixToLatex(Matrix A, string[] Headers, string TableName)
        {
            string s = "";
            {
                s += @"\begin{longtable}{";
                for (int i = 0; i < A.M; i++)
                    s += @"|p{.10\textwidth}";
                s += "|}\n";
            }
            if (Headers != null)
            {
                for (int i = 0; i < Headers.Length - 1; i++)
                    s += "${" + Headers[i] + "}$ & ";
                s += "${" + Headers[Headers.Length - 1] + "}$";
                s += "\\\\\\hline\n";
            }
            for (int i = 0; i < A.N; i++)
            {
                for (int j = 0; j < A.M - 1; j++)
                    s += "{" + String.Format("{0:F4}", A[i, j]) + "} & ";
                s += "{" + String.Format("{0:F4}", A[i, A.M - 1]) + "} ";
                s += "\\\\\\hline\n";
            }

            if (TableName != "")
                s += "\\caption{" + TableName + "}\n";
            s += "\\end{longtable}";
            return s;
        }
        //Import Vector to Latex as a time line
        //This latex code needs to include the following package to Latex file
        //\usepackage{pgfplots}
        public static string VectorToLatex(string title, string xlabel, string ylabel, int min, int max, int step, string[] pName, params Vector[] p)
        {
            string s = "";
            s += "\\begin{tikzpicture}\n";
            s += "\\begin{axis}[\n";
            s += "title={" + title + "},\n";
            s += "xlabel={" + xlabel + "},\n";
            s += "ylabel={" + ylabel + "},\n";
            s += " xmin=" + min.ToString() + ", xmax=" + max.ToString() + ",\n";
            double Ymin = 0, Ymax = 0;
            int i;
            for (i = 0; i < p.Length; i++)
            {
                for (int j = 0; j < p[i].Length; j++)
                {
                    if (Ymin > p[i][j]) Ymin = p[i][j];
                    if (Ymax < p[i][j]) Ymax = p[i][j];
                }
            }
            int ymin = (int)Ymin - step;
            int ymax = (int)Ymax + step;
            s += "ymin=" + ymin + ", ymax=" + ymax + ",\n";
            string str = "";
            i = min;
            if ((max - min) / step < 10)
                for (i = min; i <= max - step; i = i + step)
                {
                    str += i.ToString() + ",";
                }
            else
                for (i = min; i <= max - step; i = i + (int)((max - min) / 10))
                {
                    str += i.ToString() + ",";
                }
            str += i.ToString();
            s += "xtick ={" + str + "},\n";
            str = "";
            i = ymin;
            if ((ymax - ymin) / step < 10)
                for (i = ymin; i <= ymax - step; i = i + step)
                {
                    str += i.ToString() + ",";
                }
            else
                for (i = ymin; i <= ymax - step; i = i + (int)((ymax - ymin) / 10))
                {
                    str += i.ToString() + ",";
                }
            str += i.ToString();
            s += "ytick ={" + str + "},\n";
            s += "legend pos=north west,\n";
            s += "ymajorgrids=true,\n";
            s += "grid style=dashed,\n]";
            string[] Colors = { "blue", "red", "brown" };
            for (int j = 0; j < p.Length; j++)
            {
                str = "";
                str += "\\addplot[\n";
                str += "color=" + Colors[j] + ",\n";
                str += " mark=square,\n]";
                str += "coordinates {\n";
                for (i = min; i <= max; i = i + step)
                {
                    str += String.Format("({0},{1})", i, p[j][i]);
                }
                str += "\n};\n";
                str += "\\addlegendentry{$" + pName[j] + "$}\n";
                s += str;
            }
            s += "\\end{axis}\n";
            s += "\\end{tikzpicture}";
            return s;
        }

    }
}
