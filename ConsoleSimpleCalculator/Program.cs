using System;

namespace ConsoleSimpleCalculator
{
	class Program
	{
		static void Main(string[] args)
		{
			//Inicialización de variables
			bool calculadora = true;
			double num1, num2, resultado = 0;
			Console.WriteLine("+-*/^CONSOLE CALCULATOR^/*-+");
			Console.WriteLine("Ingrese valores: ");
			while (calculadora == true)
			{
				Console.Write("Valor 1: ");
				try
				{
					num1 = Double.Parse(Console.ReadLine());
				}
				catch (Exception ex)
				{
					Console.WriteLine("Por favor, ingrese sólo caracteres válidos: ");
					num1 = Double.Parse(Console.ReadLine());
				}
				Console.Write("Operador (+, -, %, x): ");
				string operador = Console.ReadLine();
				while (operador != "+"
						&& operador != "-"
						&& operador != "%"
						&& operador != "x")
				{
					Console.Write("Escriba un caracter válido (+, -, %, o x): ");
					operador = Console.ReadLine();
				}
				Console.Write("Valor 2: ");
				try
				{
					num2 = Double.Parse(Console.ReadLine());
				}
				catch (Exception e)
				{
					Console.WriteLine("Por favor, ingrese sólo números para valor 2: ");
					num2 = Double.Parse(Console.ReadLine());
				}
				if (operador == "+")
				{
					resultado = num1 + num2;
				}
				else if (operador == "-")
				{
					resultado = num1 - num2;
				}
				else if (operador == "x")
				{
					resultado = num1 * num2;
				}
				else if (operador == "%")
				{
					resultado = num1 / num2;
				}
				Console.WriteLine("Resultado: "
						+ num1 + " "
						+ operador + " "
						+ num2 + " = "
						+ resultado);
				Console.WriteLine("¿Seguir usando calculadora? S / N");
				string continuar = Console.ReadLine().ToUpper();
				if (continuar == "N")
				{
					calculadora = false;
				}
				else if (continuar != "N" && continuar != "S"){
					Console.WriteLine("Presione S para continuar, " +
							"y presione cualquier letra para salir del programa");
					continuar = Console.ReadLine().ToUpper();
					if (continuar != "S")
					{
						calculadora = false;
					}
				}

			}

		}
	}
}
