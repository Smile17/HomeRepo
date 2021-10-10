using System;
					
public class Program
{
	public static void Main()
	{
		Console.WriteLine("Start...");
		string expression = Console.ReadLine();
		//string expression = "9 sqrt 4 2 * 1 5 - 2 ^ / +";
       	string[] operators = expression.Split(' ');
		double[] numbers = new double[1];
		int length;
		for (int i = 0; i < operators.Length; i++)
		{
			length = numbers.Length;
			if(double.TryParse(operators[i], out numbers[length - 1]))
			{
				Array.Resize(ref numbers, length + 1);
			}
			else
			{
				switch (operators[i])
				{
					case "+":
						numbers[length - 3] += numbers[length - 2];
						Array.Resize(ref numbers, length - 1);
						break;
					case "-":
						numbers[length - 3] -= numbers[length - 2];
						Array.Resize(ref numbers, length - 1);
						break;
					case "*":
						numbers[length - 3] *= numbers[length - 2];
						Array.Resize(ref numbers, length - 1);
						break;
					case "/":
						numbers[length - 3] /= numbers[length - 2];
						Array.Resize(ref numbers, length - 1);
						break;
					case "^":
						numbers[length - 3] = Math.Pow(numbers[length - 3], numbers[length - 2]);
						Array.Resize(ref numbers, length - 1);
						break;
					case "sqrt":
						numbers[length - 2] = Math.Sqrt(numbers[length - 2]);
						break;
				}
			}
		}
		//for(int i =0; i < numbers.Length; i++)
		//	Console.WriteLine(numbers[i]);
		Console.WriteLine("Result: {0}", numbers[0]);
		Console.WriteLine("Done...");
	}
	
	
	
	
}
