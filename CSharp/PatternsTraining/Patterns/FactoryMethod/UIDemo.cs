using System;

namespace Patterns
{
    public class UIDemo
    {
        public static void MainDemo(string[] args)
        {
            var dialog = new WinDialog();
            var button = dialog.CreateButton();
            Console.WriteLine(button.Name);
        }
    }
}