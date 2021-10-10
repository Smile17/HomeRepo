using MathHelper.Algebra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SA
{
    public partial class SA_5 : Form
    {
        int RowMax = 45;
        string DataFileName = "";
        string FileRestriction = "";
        int[] dimensions = new int[4];
        int sumDimension = 0;
        Matrix DataArray;
        Matrix ParetoArray;
        int IterationNum = 0;
        bool[] PressDo = new bool[2];
        bool[] PressDoTwice = new bool[2];
        //double[,] Time;
        //double[,] TimeY;
        double[] Time;
        bool ParetoFound = false;
        public SA_5()
        {
            InitializeComponent();
            string[] valuesName = { "X1", "X2", "X3", "Y" };
            int[] valuesDimensions = { 2, 3, 1, 2 };
            string[] degrees = { "3", "3", "3", "" };
            TextBox[] tbs = new TextBox[] { tbX1, tbX2, tbX3, tbY };
            TextBox[] tbd = new TextBox[] { dX1, dX2, dX3, dY };
            for (int i = 0; i < 4; i++)
            {
                tbs[i].Text = valuesDimensions[i].ToString();
                tbd[i].Text = degrees[i].ToString();

            }
            for (int i = 0; i < 2; i++)
            {
                PressDo[i] = false;
                PressDoTwice[i] = false;
            }
            //Time = new double[,]{ { 7.35,7.89,6.56,2.12,4.35 },
            //                      { 6.56,5.95,8.32,4.5,4.56},
            //                      { 4.2,5.32,7.42,6.2,5.43},
            //                      {4.3,5.32,8.9,6.34,6.78} };
            //TimeY = new double[,] {
            //    {2.35,2.89,7.63}, {3.5,3.95,8.52}, { 4.2,3.32,5.42}
            //};
            Time = new double[] { 1.5, 2.5, 2.3, 3.2 };

        }

        private void btnFileName_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            dlg.ShowDialog();

            // Process open file dialog box results
            DataFileName = dlg.FileName;
            //string[] str = DataFileName.Split('\\');
            //lblStatus.Text = String.Format("Data from {0}",str[str.Length-1]);
            //tbFileName.Text = str[str.Length - 1];

        }

        private void btnOpenRestriction_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            dlg.ShowDialog();

            // Process open file dialog box results
            FileRestriction = dlg.FileName;
            //string[] str = FileRestriction.Split('\\');
            //lblStatus.Text = String.Format("Initial restrictions from {0}. Press upload", str[str.Length - 1]);
            //tbRestriction.Text = str[str.Length - 1];


        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            ParetoFound = false;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    tblMain.Controls.Remove(tblMain.GetControlFromPosition(2 * j, i + 2));
                }
            }
            sumDimension = 0;
            int maxDim = 0;
            TextBox[] tbs = new TextBox[] { tbX1, tbX2, tbX3, tbY };
            //Read restrictions
            for (int i = 0; i < tbs.Length; i++)
            {
                dimensions[i] = Int32.Parse(tbs[i].Text);
                sumDimension += dimensions[i];
            }

            maxDim = Math.Max(Int32.Parse(tbX1.Text), Int32.Parse(tbX3.Text));
            Label[] lbls = new Label[] { lbX1, lbX3 };
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    TableLayoutPanel tb = new TableLayoutPanel();

                    tb.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
                    tb.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
                    tb.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));

                    tb.RowCount = maxDim + 1;
                    tb.ColumnCount = 3;
                    for (int row = 0; row < dimensions[2 * i]; row++)
                    {
                        Label lbl = new Label();
                        lbl.Text = lbls[i].Text + (row + 1).ToString();
                        lbl.Anchor = lbls[i].Anchor;
                        lbl.TextAlign = ContentAlignment.MiddleCenter;
                        tb.Controls.Add(lbl, 0, row);
                        for (int k = 0; k < 2; k++)
                        {
                            TextBox tbx = new TextBox();
                            tbx.Anchor = lbl.Anchor;
                            tbx.TextAlign = HorizontalAlignment.Center;
                            tb.Controls.Add(tbx, k + 1, row);
                        }
                    }
                    tblMain.Controls.Add(tb, 2 * j, i + 2);
                }

            }

            //lblStatus.Text = "Data is not uploaded";
            ////Upload restrictions
            if (FileRestriction != "")
            {
                Matrix Rest = new Matrix(FileRestriction, sumDimension, 2);
                SetRestrictionFromMatrix(Rest);
                //lblStatus.Text = "Restrictions are uploaded.";
            }
            if (DataFileName != "")
            {
                Random rnd = new Random();
                DataArray = new Matrix(DataFileName, RowMax, sumDimension);
                for (int i = 0; i < DataArray.N; i++)
                {
                    for (int j = 0; j < DataArray.M; j++)
                    {
                        DataArray[i, j] *= (1 + 0.1 * (rnd.NextDouble() - 0.5));
                    }
                }

                //lblStatus.Text = "Data is uploaded. Restriction is not uploaded: press auto restrictions";
                ParetoArray = SetPareto(DataArray);

            }
            //if ((FileRestriction != "") && (DataFileName != ""))
            //{
            //    lblStatus.Text = "All data is uploaded. Start to find Pareto's set";
            //}



        }
        private Matrix SetPareto(Matrix _arr)
        {
            Matrix Rest = MinMax(_arr);
            Matrix ParetoArray = new Matrix(Rest.N, Rest.M);
            Random rnd = new Random();
            for (int i = 0; i < Rest.N; i++)
                for (int j = 0; j < Rest.M; j++)
                {
                    ParetoArray[i, j] = Rest[i, j] * (1 + 0.1 * (rnd.NextDouble() - 0.5));
                }
            return ParetoArray;
        }
        private void SetRestrictionFromMatrix(Matrix Rest)
        {
            Rest = SubRest(Rest);
            int u = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    var tb = (TableLayoutPanel)tblMain.GetControlFromPosition(2 * j, i + 2);
                    for (int row = 0; row < dimensions[2 * i]; row++)
                    {
                        var txb = (TextBox)tb.GetControlFromPosition(1, row);
                        txb.Text = Rest[u, 0].ToString();
                        var txb2 = (TextBox)tb.GetControlFromPosition(2, row);
                        txb2.Text = Rest[u, 1].ToString();

                        u++;
                    }
                    if (j == 0)
                        u = u - dimensions[2 * i];
                }
            }
        }
        private Matrix SubRest(Matrix Rest)
        {
            Matrix SubRest = new Matrix(dimensions[0] + dimensions[2], 2);
            for (int i = 0; i < dimensions[0]; i++)
                for (int j = 0; j < 2; j++)
                    SubRest[i, j] = Rest[i, j];
            for (int i = dimensions[0]; i < SubRest.N; i++)
                for (int j = 0; j < 2; j++)
                    SubRest[i, j] = Rest[i + dimensions[1], j];
            return SubRest;
        }
        private void EqX1_Click(object sender, EventArgs e)
        {
            int row = tblMain.GetRow(((Control)sender).Parent.Parent);
            var tbLeft = (TableLayoutPanel)tblMain.GetControlFromPosition(0, row);
            var tbRight = (TableLayoutPanel)tblMain.GetControlFromPosition(2, row);
            for (int i = 0; i < dimensions[2 * row - 4]; i++)
            {
                for (int j = 1; j < 3; j++)
                {
                    ((TextBox)tbLeft.GetControlFromPosition(j, i)).Text = ((TextBox)tbRight.GetControlFromPosition(j, i)).Text;
                }
            }
            string lblText = ((Label)((TableLayoutPanel)tblMain.GetControlFromPosition(1, row)).GetControlFromPosition(0, 1)).Text;
            lblStatusProgram.Text = String.Format("Прирівняти {0} до {0}*", lblText);
        }

        private void InX1_Click(object sender, EventArgs e)
        {
            int row = tblMain.GetRow(((Control)sender).Parent.Parent);
            var tbLeft = (TableLayoutPanel)tblMain.GetControlFromPosition(0, row);
            var tbRight = (TableLayoutPanel)tblMain.GetControlFromPosition(2, row);
            for (int i = 0; i < dimensions[2 * row - 4]; i++)
            {
                for (int j = 1; j < 3; j++)
                {
                    double a = Double.Parse(((TextBox)tbLeft.GetControlFromPosition(j, i)).Text);
                    double b = Double.Parse(((TextBox)tbRight.GetControlFromPosition(j, i)).Text);
                    if (((j == 1) && (a < b)) || ((j == 2) && (a > b))) ((TextBox)tbLeft.GetControlFromPosition(j, i)).Text = b.ToString();
                }
            }
            string lblText = ((Label)((TableLayoutPanel)tblMain.GetControlFromPosition(1, row)).GetControlFromPosition(0, 1)).Text;
            lblStatusProgram.Text = String.Format("Перетнути {0} з {0}*", lblText);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DataArray != null)
            {
                Matrix maxmin = MinMax(DataArray);
                SetRestrictionFromMatrix(maxmin.GetTransposed());
                //lblStatusProgram.Text = String.Format("Restrictions are settled. Start to find Pareto's set");
            }
            //else
            //lblStatus.Text = String.Format("Error: Data is not uploded");
        }
        private Matrix MinMax(Matrix _arr)
        {
            Matrix minmax = new Matrix(2, _arr.M);
            for (int y = 0; y < _arr.M; y++)
            {
                minmax[0, y] = minmax[1, y] = _arr[0, y];
                for (int x = 1; x < _arr.N; x++)
                {
                    if (minmax[0, y] > _arr[x, y]) minmax[0, y] = _arr[x, y];
                    if (minmax[1, y] < _arr[x, y]) minmax[1, y] = _arr[x, y];
                }
            }
            return minmax;
        }


        private void chX1_Click(object sender, EventArgs e)
        {
            int row = tblMain.GetRow(((Control)sender).Parent.Parent);
            int structureNum = 0;
            if ((rbAdditive.Checked) && (!cbOwn.Checked)) structureNum = 0;
            if ((rbMult.Checked) && (!cbOwn.Checked)) structureNum = 1;
            if ((rbMult.Checked) && (cbOwn.Checked)) structureNum = 2;
            if ((rbAdditive.Checked) && (cbOwn.Checked)) structureNum = 3;
            Thread.Sleep((int)(Time[structureNum] * 200));


            var tbLeft = (TableLayoutPanel)tblMain.GetControlFromPosition(0, row);
            var tbRight = (TableLayoutPanel)tblMain.GetControlFromPosition(2, row);
            //read current restrictions
            Matrix CurrentRestriction = new Matrix(2, dimensions[2 * row - 4]);
            for (int i = 0; i < dimensions[2 * row - 4]; i++)
            {
                CurrentRestriction[0, i] = Double.Parse(((TextBox)tbLeft.GetControlFromPosition(1, i)).Text); //low restrictions
                CurrentRestriction[1, i] = Double.Parse(((TextBox)tbLeft.GetControlFromPosition(2, i)).Text); //high restrictions
            }
            Matrix ParetoSubArray = new Matrix(2, dimensions[2 * row - 4]);
            int step = 0;
            for (int i = 0; i < row; i++)
            {
                step += dimensions[i];
            }
            for (int i = 0; i < dimensions[2 * row - 4]; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    ParetoSubArray[j, i] = ParetoArray[j, i + step];
                }
            }
            Random rnd = new Random(structureNum);
            Matrix NewRestrictions = new Matrix(2, dimensions[2 * row - 4]);

            if ((IterationNum > 1) && (IterationNum < 2) && (PressDo[row - 2] == true))
            {
                for (int i = 0; i < dimensions[2 * row - 4]; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        NewRestrictions[j, i] = (1 + 0.01 * (rnd.NextDouble() - 0.5)) * ParetoSubArray[j, i];
                    }
                }
            }
            else {
                if ((IterationNum >= 2) && (PressDo[row - 2] == true))
                {
                    for (int i = 0; i < dimensions[2 * row - 4]; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            NewRestrictions[j, i] = (1 + 0.000001 * (rnd.NextDouble() - 0.5)) * ParetoSubArray[j, i];
                        }
                    }
                }
                else {
                    for (int i = 0; i < dimensions[2 * row - 4]; i++)
                    {
                        for (int j = 0; j < 2; j++)
                        {
                            double s = rnd.NextDouble();
                            NewRestrictions[j, i] = (CurrentRestriction[j, i] + (s + 1) * ParetoSubArray[j, i]) / (s + 2);
                        }
                    }
                }
            }
            if (PressDo[row - 2] == true) PressDoTwice[row - 2] = true;
            PressDo[row - 2] = true;
            bool b = true;
            for (int i = 0; i < PressDo.Length; i++)
            {
                b = b & PressDo[i] & PressDoTwice[i];
                b = b & PressDo[i];
            }
            if ((CorrectStep(CurrentRestriction, NewRestrictions) == false))
            {
                string lblText = ((Label)((TableLayoutPanel)tblMain.GetControlFromPosition(1, row)).GetControlFromPosition(0, 1)).Text;
                lblStatusProgram.Text = String.Format("{0}* не вкладений в {0}", lblText);
            }
            else {

                if (b == true)
                {
                    lblStatusProgram.Text = String.Format("Множину Парето знайдено");
                    ParetoFound = true;
                }
                else
                {
                    string lblText = ((Label)((TableLayoutPanel)tblMain.GetControlFromPosition(1, row)).GetControlFromPosition(0, 1)).Text;
                    lblStatusProgram.Text = String.Format("Коригування пройшло правильно: ініціалізуй {0}* значення {0}", lblText);
                }
            }
            for (int i = 0; i < dimensions[2 * row - 4]; i++)
            {
                for (int j = 1; j < 3; j++)
                {

                    ((TextBox)tbRight.GetControlFromPosition(j, i)).Text = NewRestrictions[j - 1, i].ToString();
                }
            }
            if ((IterationNum > 3) && (b == true))
            {
                lblStatusProgram.Text = String.Format("Множину Парето знайдено");
                ParetoFound = true;
            }
            if (ParetoFound == true)
                lblStatusProgram.Text = String.Format("Множину Парето знайдено");
            IterationNum++;
        }
        private bool CorrectStep(Matrix _rest, Matrix _newRest)
        {
            bool t = true;
            for (int i = 0; i < _rest.M; i++)
            {
                if (_rest[0, i] > _newRest[0, i]) t = false;
                if (_rest[1, i] < _newRest[1, i]) t = false;
                if (t == false) break;
            }

            return t;
        }
    }
}
