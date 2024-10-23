using System;

namespace ConsoleCalculator
{
	// Practica #12 - año 2022 - Clases separadas 
	internal class Program
	{
		static void Main(string[] args)
		{
			//Set
			Console.Title = " CONSOLE CALCULATOR";
			Console.BackgroundColor = ConsoleColor.DarkGray;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear();
			Calculator.CalculatorProgram();
        }
	}
}
