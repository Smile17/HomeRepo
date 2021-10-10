using MathHelper.Algebra;
using System;

namespace MathHelper.Excel
{
    public class ExcelMatrix
    {
        public static void Table(Matrix A, string name)
        {
            var xlApp = new Microsoft.Office.Interop.Excel.Application();
            var xlWorkBook = xlApp.Workbooks.Add(Type.Missing);
            xlApp.SheetsInNewWorkbook = 3;
            var ws = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            OneTable(A, ws, 1, 1);


            xlWorkBook.SaveAs(name + ".xls");
            xlWorkBook.Close();
            xlApp.Quit();
        }
        public static void Table(string name, params Matrix[] M)
        {
            var xlApp = new Microsoft.Office.Interop.Excel.Application();
            var xlWorkBook = xlApp.Workbooks.Add(Type.Missing);
            xlApp.SheetsInNewWorkbook = 3;
            var ws = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            int m = 1;
            for (int i = 0; i < M.Length; i++)
            {
                OneTable(M[i], ws, m, 1);
                m += M[i].N + 2;
            }

            xlWorkBook.SaveAs(name + ".xls");
            xlWorkBook.Close();
            xlApp.Quit();
        }
        public static void OneTable(Matrix A, Microsoft.Office.Interop.Excel.Worksheet ws, int k, int m)
        {
            m--; k--;
            for (int i = 2; i <= A.M + 1; i++)
            {
                ws.Cells[1 + k, i + m] = "X" + (i - 1).ToString();
            }
            for (int i = 2; i <= A.N + 1; i++)
            {
                ws.Cells[i + k, 1 + m] = "X" + (i - 1).ToString();
            }
            for (int i = 0; i < A.N; i++)
                for (int j = 0; j < A.M; j++)
                {
                    ws.Cells[i + 2 + k, j + 2 + m] = A[i, j];
                }
        }
    }
}
