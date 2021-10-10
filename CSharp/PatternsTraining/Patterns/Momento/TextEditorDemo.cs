using System;
using System.Collections.Generic;

namespace Patterns
{
    public class TextEditorDemo
    {
        public static void MainTextEditorDemo(string[] args)
        {
            TextEditor editor = new TextEditor(new Text("Times New Roman", 28, "Red"));
            Console.WriteLine(editor);
            editor.Undo();
            Console.WriteLine(editor);
            editor.Redo();
            Console.WriteLine(editor);
            editor.Execute(new List<ITextCommand>() { new CommandChangeColor() { Color = "Yellow" }, new CommandChangeSize() { Size = 24 } });
            Console.WriteLine(editor);
            editor.Execute(new List<ITextCommand>() { new CommandChangeColor() { Color = "Green" } });
            Console.WriteLine(editor);
            editor.Undo();
            Console.WriteLine(editor);
            editor.Redo();
            Console.WriteLine(editor);
            editor.Undo();
            editor.Undo();
            Console.WriteLine(editor);
            editor.Execute(new List<ITextCommand>() { new CommandChangeColor() { Color = "Black" } });
            Console.WriteLine(editor);
            editor.Redo();
            Console.WriteLine(editor);

        }
    }
}