using KnowledgeTester.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KnowledgeTester.Service
{
    class TestChecker
    {

        public static int CheckTest(Test Test, List<GroupBox> Questions)
        {
            int res = 0;
            for (int i = 0; i < Test.Questions.Count; i++)
            {
                var q = Test.Questions[i];
                var gb = Questions[i];
                var checkedItems = ((CheckedListBox)gb.Controls[1]).CheckedItems;
                var pb = ((PictureBox)gb.Controls[0]);
                bool correct = false;
                if (q.CorrectAnswersIds.Count == checkedItems.Count)
                {
                    correct = true;
                    for (int j = 0; j < q.CorrectAnswersIds.Count; j++)
                    {
                        var ans = q.Answers[q.CorrectAnswersIds[j]];
                        if (!checkedItems.Contains(ans))
                        {
                            correct = false;
                            break;
                        }
                    }
                }
                if (correct)
                {
                    pb.Image = Image.FromFile("..\\..\\..\\img\\true.png");
                    res++;
                }
                else
                    pb.Image = Image.FromFile("..\\..\\..\\img\\false.png");

            }
            return res;
        }
    }
}
