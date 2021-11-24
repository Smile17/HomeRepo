using KnowledgeTester.Model;
using KnowledgeTester.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KnowledgeTester
{
    public partial class Tester : Form
    {
        Test Test { get; set; }
        public List<GroupBox> Questions { get; set; }
        public Tester()
        {
            InitializeComponent();
            Questions = new List<GroupBox>();
            //Questions.Add(gbQuestion);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            GenerateTestService.Generate("test.json");
            MessageBox.Show("Test is saved in test.json");
        }

        private void btnUploadTest_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Title = "Browse Text Files",
                CheckFileExists = true,
                CheckPathExists = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var path = openFileDialog1.FileName;
                var json = File.ReadAllText(path);
                Test = JsonSerializer.Deserialize<Test>(json);

                lblTestName.Text = Test.Name;
                for(int i = 0; i < Test.Questions.Count; i++)
                {
                    var q = Test.Questions[i]; 
                    var cntrl = GenerateControl.GenerateGroupBoxControl();
                    pnlTest.Controls.Add(cntrl);
                    GenerateControl.SetGroupBoxControl(cntrl, q, i);
                    Questions.Add(cntrl);

                }
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            var res = TestChecker.CheckTest(Test, Questions);
            lblResult.Text = String.Format("{0}/{1}", res, Questions.Count);
        }
    }
}
