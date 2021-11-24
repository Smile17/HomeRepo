using KnowledgeTester.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace KnowledgeTester.Service
{
    class GenerateControl
    {
        public static System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tester));

        public static GroupBox GenerateGroupBoxControl() 
        {
            var gbQuestion = new GroupBox();
            var lblQuestion = new Label();
            var clbAnswers = new CheckedListBox();
            var pbResult = new PictureBox();

            // 
            // gbQuestion
            // 
            gbQuestion.Controls.Add(pbResult);
            gbQuestion.Controls.Add(clbAnswers);
            gbQuestion.Controls.Add(lblQuestion);
            gbQuestion.Location = new System.Drawing.Point(3, 3);
            gbQuestion.Name = "gbQuestion";
            gbQuestion.Size = new System.Drawing.Size(489, 131);
            gbQuestion.TabIndex = 3;
            gbQuestion.TabStop = false;
            gbQuestion.Text = "gbQuestion";
            // 
            // pbResult
            // 
            //pbResult.Image = ((System.Drawing.Image)(resources.GetObject("pbResult.Image")));
            pbResult.Location = new System.Drawing.Point(409, 55);
            pbResult.Name = "pbResult";
            pbResult.Size = new System.Drawing.Size(51, 50);
            pbResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pbResult.TabIndex = 2;
            pbResult.TabStop = false;
            // 
            // clbAnswers
            // 
            clbAnswers.FormattingEnabled = true;
            clbAnswers.Location = new System.Drawing.Point(15, 46);
            clbAnswers.Name = "clbAnswers";
            clbAnswers.Size = new System.Drawing.Size(375, 76);
            clbAnswers.TabIndex = 1;
            // 
            // lblQuestion
            // 
            lblQuestion.AutoSize = true;
            lblQuestion.Location = new System.Drawing.Point(15, 28);
            lblQuestion.Name = "lblQuestion";
            lblQuestion.Size = new System.Drawing.Size(12, 15);
            lblQuestion.TabIndex = 0;
            lblQuestion.Text = "-";

            return gbQuestion;
        }
        public static void SetGroupBoxControl(GroupBox gb, TestQuestion q, int i)
        {
            int height = 135;
            gb.Text = "# " + (i+1).ToString();
            gb.Location = new System.Drawing.Point(3, 3 + height * (i));
            ((Label)gb.Controls[2]).Text = q.Question;
            ((CheckedListBox)gb.Controls[1]).Items.AddRange(q.Answers.ToArray());
        }
    }
}
