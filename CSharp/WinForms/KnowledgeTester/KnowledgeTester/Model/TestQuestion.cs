using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeTester.Model
{
    class TestQuestion
    {
        public string Question { get; set; }
        public List<string> Answers { get; set; }
        public List<int> CorrectAnswersIds { get; set; }
    }
}
