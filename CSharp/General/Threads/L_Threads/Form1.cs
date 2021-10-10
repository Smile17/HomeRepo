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

namespace L_Threads
{
    public partial class Form1 : Form
    {
        public ClockTask[] c;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var c1 = new ClockTask("time", 2);
            var c2 = new ClockTask("is", 4);
            var c3 = new ClockTask("up", 6);
            c = new ClockTask[] { c1, c2, c3 };
            foreach(var ci in c)
            {
                ci.Callback += CallbackChangeRichTextBox;
                ci.Start();
            }

        }
        private void CallbackChangeRichTextBox(object sender, ClockTaskResponse response)
        {
            rtbMain.AppendText(response.Message);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (c != null)
            foreach (var ci in c)
            {
                ci.Stop();
            }
        }
    }
}
