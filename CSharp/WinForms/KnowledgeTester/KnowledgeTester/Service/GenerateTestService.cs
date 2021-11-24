using KnowledgeTester.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KnowledgeTester.Model
{
    class GenerateTestService
    {
        public static void Generate(string path)
        {
            var test = new Test() { Name = "Test1", Questions = new List<TestQuestion>()};
            test.Questions.Add(new TestQuestion()
            {
                Question = "Translate: виверження (вулкана)",
                Answers = new List<string>() { "eruption", "corruption", "illusion", "damage" },
                CorrectAnswersIds = new List<int>() { 0 }
            });
            test.Questions.Add(new TestQuestion()
            {
                Question = "Translate: мама",
                Answers = new List<string>() { "father", "dad", "mother", "mom" },
                CorrectAnswersIds = new List<int>() { 2, 3 }
            });
            test.Questions.Add(new TestQuestion()
            {
                Question = "Translate: недолік",
                Answers = new List<string>() { "drawback", "disadvantage", "benefit", "flaw" },
                CorrectAnswersIds = new List<int>() { 0,1,3 }
            });
            string json = JsonSerializer.Serialize<Test>(test);
            File.WriteAllText(path, json);
        }

        public static Test ReadFile(string path) 
        {
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<Test>(json);
        }
    }
}
