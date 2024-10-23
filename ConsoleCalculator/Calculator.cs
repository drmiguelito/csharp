using System;

namespace ConsoleCalculator
{
	public static class Calculator
	{
		//Atributos:
		public static string option, symbol, stringInput, dot = ".";
		public static double number, num1, num2, num3, result;
		public static bool isNumeric, containsDot = false, is2ndNumber = false, calculator = true;
		public static string menu =
				" --> Signos y caracteres de operadores <-- " +
				"\n >> Básicos: Suma (+), resta (-), multiplicación (*), división (/) " +
				"\n >> Exponentes y raíz: Raíz cuadrada (r), exponencial (e), exponente e (ex) " +
				"\n >> Ángulos: Seno (sen), Coseno (cos), Tangente (tan) ";

		//Campos (propiedades):
		public static string StringInput
		{
			get => stringInput;
			set
			{
				stringInput = value;

				//Estandarización a valor numérico
				isNumeric = Double.TryParse(StringInput, out number);
                while (!isNumeric)
				{
					Console.Write(" - ERROR - \n" +
						" Por favor, solamente ingrese valores numéricos: ");
					StringInput = Console.ReadLine();
					isNumeric = Double.TryParse(StringInput, out number);
				}
				//Estandarización del punto a coma
				containsDot = StringInput.Contains(dot);
				if (containsDot) StringInput = StringInput.Replace(".", ",");
				//Fin del proceso
				Number = Double.Parse(StringInput);
			}
		}
		public static string Symbol
		{
			get { return symbol; }
			set
			{
				symbol = value;
				while (Symbol != "+" && Symbol != "-"
					&& Symbol != "/" && Symbol != "*"
					&& Symbol != "SEN" && Symbol != "COS" && Symbol != "TAN"
					&& Symbol != "R" && Symbol != "E" && Symbol != "EX")
				{
                    Console.Write(" Ingrese el caracter que corresponda:\n " 
						+	menu + "\n ");
                    Symbol = Console.ReadLine().ToUpper();
				}
			}
		}
		public static double Number
		{
			get { return number; }
			set { number = value; }
		}
		//Métodos: Generales
		public static void CalculatorProgram()
		{
			while (calculator)
			{
				Console.Clear();
				Menu();
				ExitProgram();
			}
		}
		public static void Menu()
		{
            Console.WriteLine();
            Console.WriteLine(" *** Console Calculator ***");
            Console.WriteLine(menu);
            Console.WriteLine();
            Console.WriteLine(" Ingrese valores seguidos de enter: ");
			Console.Write(" Valor 1: ");

			//1er input
			StringInput = Console.ReadLine();
			num1 = Number; //pasaje a primer valor

			//2do input
			Console.Write(" Operador: ");
			Symbol = Console.ReadLine().ToUpper();

			//3er input
			if (Symbol == "+" || Symbol == "-" || Symbol == "E"
				|| Symbol == "*" || Symbol == "/") 
			{
				Console.Write(" Valor 2: ");
				StringInput = Console.ReadLine();
				num2 = Number; //pasaje a primer valor
			}
			//Resultado
			switch (Symbol)
			{
				case "R": result = SquareRoot(num1); break;
				case "EX": result = ENumber(num1); break;
				case "SEN": result = Sine(num1); break;
				case "COS": result = Cosine(num1); break;
				case "TAN": result = Tangent(num1); break;
				case "+": result = Add(num1, num2); break;
				case "-": result = Subtract(num1, num2); break;
				case "*": result = Multiplication(num1, num2); break;
				case "/": result = Division(num1, num2); break;
				case "E": result = Exponent(num1, num2); break;
			}
			//Final
			if(num2 != 0) 
				Console.WriteLine(" Resultado: " + num1 + " " + Symbol +
				" " + num2 + " = " + result);
			else 
				Console.WriteLine(" Resultado: " + num1 + " " + Symbol +
				" = " + result);
			
			//Restart values
			num1 = 0;
			num2 = 0;
		}
		//Conversor:
		public static double StringToNumber(string stringInput)
		{
			return number = Double.Parse(stringInput);
		} 
		//Métodos: Operaciones básicas:
		public static double Add(double num1, double num2)
		{
			return num1 + num2;
		}
		public static double Subtract(double num1, double num2)
		{
			return num1 - num2;
		}
		public static double Multiplication (double num1, double num2) 
		{
			return num1 * num2;
		}
		public static double Division (double num1, double num2)
		{
			return Math.Round(num1 / num2, 2);
		}
		//Métodos: otras operaciones
		public static double Exponent (double num1, double num2)
		{
			return Math.Pow(num1, num2); //Exponente
		}
		public static double ENumber (double number)
		{
			return Math.Exp(number);
		}
		public static double SquareRoot (double number) //Raíz cuadrada
		{
			return Math.Round(Math.Sqrt(number), 2);
		}
		public static double Sine (double number) //Seno
		{
			return Math.Round(Math.Sin(number));
		}
		public static double Cosine (double number) //Coseno
		{
			return Math.Round(Math.Cos(number), 2);
		}
		public static double Tangent (double number) //Tangente
		{
			return Math.Round(Math.Tan(number), 2);
		}
		public static void ExitProgram()
		{
			Console.WriteLine("\n ¿Desea realizar otro cálculo? S/N");
			option = Console.ReadLine().ToUpper();
			if (option != "S")
			{
				Console.WriteLine(" ¡Nos vemos en la próxima!");
				calculator = false;
			}
			else Console.Clear(); 
		}
	}
}