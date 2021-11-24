using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        List<string> list = new List<string>();
        public Form1()
        {
            InitializeComponent();
            list.Add("sdf");
            list.Add("sdfc");
            dataGridView1.Columns.Add("Column", "Column");
            for (int i = 0; i < list.Count; i++)
            {
                dataGridView1.Rows.Add(list[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
