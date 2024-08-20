using System;

namespace ConsoleHorseRacingSpeedAndOddsCalculator
{
	//Programa de práctica año 2021
	//Monolibrería - monoclase - sólo métodos static void
	/*
	 Los métodos static no requieren instanciación, pero limitan el uso de la POO
	 Este es un ejemplo claro en donde se podría refactorizar a un sólo método de 
	 validación para cada caso y así reutilizar código. 
	 */
	internal class Program
	{
		static void Main(string[] args)
		{
			bool calculator = true;

			while (calculator)
			{
				//Títulos
				Console.Clear();
				Console.Title = "HORSE RACING SPEED CALCULATOR";
				Console.WriteLine("***Cálculo de velocidad y dividendos para SPC y QH***\n");

				//Menú
				Console.WriteLine("*Seleccione:" +
					"\n1. Cálculo de velocidades" +
					"\n2. Cálculo de dividendos" +
					"\n3. Salir");
				Console.Write("Selección: ");
				string menu = Console.ReadLine();

				//Validación de menú
				while (menu != "1" && menu != "2" && menu != "3")
				{
					Console.WriteLine("Por favor, seleccione una opción válida " +
						"(1 para cálculo de velocidades, 2 para cálculo de dividendos)");
					menu = Console.ReadLine();
				}
				//Ejecución de opción seleccionada
				if (menu == "1") SpeedCalc();
				else if (menu == "2") OddsCalc();
				else if (menu == "3")
				{
					Console.WriteLine("¡Nos vemos en la próxima!");
					calculator = false;
				}
				//Final
				Console.WriteLine("\n¿Desea salir del programa? S/N");
				string option = Console.ReadLine().ToUpper();
				if (option != "N")
				{
					Console.WriteLine("¡Gracias, nos vemos la próxima!");
					calculator = false;
				}
			}
		}

		public static void SpeedCalc()
		{
			//Subtítulo
			Console.WriteLine("\n--> VELOCIDAD <--");

			//Variables de cálculo
			double distance = 0, time, kmph, mps, mph, length, lengthResidual;
			string lengthText = "";

			//Variables de validación
			string stringInput, stringToNumber ="", dot;
			bool isNumeric, containsDot;

			//Campo distancia
			Console.Write("DISTANCIA (metros): ");
			stringInput = Console.ReadLine();
			isNumeric = Double.TryParse(stringInput, out distance);
            while (!isNumeric)
			{
				Console.WriteLine("<Error>. Por favor, ingrese sólo números:");
				stringInput = Console.ReadLine();
				isNumeric = Double.TryParse(stringInput, out distance);
			}
			//Estandarización a "," para parsearlo (en caso de que presente un ".")
			dot = ".";
			containsDot = stringInput.Contains(dot);
			if (containsDot)
			{
				stringToNumber = stringInput.Replace(".", ",");
				distance = Double.Parse(stringToNumber);
			}
			//Campo tiempo
			Console.Write("Dist: {0} m. TIEMPO (segundos): ", distance);

			//Validación del campo tiempo
			stringInput = Console.ReadLine();
			isNumeric = double.TryParse(stringInput, out time);
			while (!isNumeric)
			{
				Console.WriteLine("<Error>. Por favor, ingrese sólo números:");
				stringInput = Console.ReadLine();
				isNumeric = double.TryParse(stringInput, out time);
			}
			//Estandarización a "," para parsearlo (en caso de que presente un ".")
			dot = ".";
			containsDot = stringInput.Contains(dot);
			if (containsDot)
			{
				stringToNumber = stringInput.Replace(".", ",");
				time = Double.Parse(stringToNumber);
			}
			//Cálculos
			kmph = Math.Round((distance * 3.6) / time, 2);
			mps = Math.Round(distance / time, 2);
			mph = Math.Round((distance / 1.609) * 3.6 / time, 2);
			length = Math.Round((distance / time) / 2.7, 1);
			lengthResidual = length % 1;
			if (lengthResidual > 0 && lengthResidual < 0.2) lengthText = "y pescuezo";
			else if (lengthResidual > 0.1 && lengthResidual < 0.5) lengthText = "y un cuarto";
			else if (lengthResidual == 0.5) lengthText = " y medio";
			else if (lengthResidual > 0.5 && lengthResidual < 0.8) lengthText = "y tres cuartos";
			length = (int)length;

			//Resultado
			Console.WriteLine("\n--> Velocidad:\n{0} km/h \n{1} mi/h \n{2} m/s", kmph, mph, mps);
			Console.WriteLine("--> 1 segundo equivale a {0} cuerpos {1} a dicha velocidad.", length, lengthText);
		}
		public static void OddsCalc()
		{
			//Título y subtítulo
			Console.Title = "HR SPEED CONVERTER : ODDS";
			Console.WriteLine("\n--> CALCULO DE DIVIDENDOS <--");

			//Variables de validación
			string stringNumber, dot = ".";
			bool isNumeric, containsDot;

			//Variables de cálculo
			double commission, pool, totalWager = 0;
			int participants;

			//Campo comisión
			Console.Write("\nCOMISION (en %) del organizador: ");
			stringNumber = Console.ReadLine();
			isNumeric = Double.TryParse(stringNumber, out commission);

			//Validación 1: ¿Se escribió un número?
			while (!isNumeric) 
			{
				Console.WriteLine("<Error>. Por favor, ingrese sólo números:");
				stringNumber = Console.ReadLine();
				isNumeric = Double.TryParse(stringNumber, out commission);
			}
			//Validación 2: Estandarización a "," para parsearlo (en caso de que presente un ".")
			containsDot = stringNumber.Contains(dot);
			if (containsDot)
			{
			   	stringNumber = stringNumber.Replace(".", ",");
			}
			commission = Double.Parse(stringNumber);

			//Campo nro. de participantes
			Console.Write("CANTIDAD de participantes: ");
			try
			{
				participants = Int32.Parse(Console.ReadLine());
			}
			catch (Exception e)
			{
				Console.Write("<Error>. Por favor, escriba sólo números enteros: ");
				participants = Int32.Parse(Console.ReadLine());
			}
			//Arrays de validación
			string[] stringNumberArray = new string[participants];
			double[] wagerArray = new double[participants];
			double[] odds = new double[participants];
			int[] fractionalOdds = new int[participants];
            
            //Campo wagering por cada participante
            Console.WriteLine("SUMA destinada a cada participante: ");
			for (int i = 0; i < wagerArray.Length; i++)
			{
				Console.Write(".# {0}: $ ", i + 1);
				stringNumberArray[i] = Console.ReadLine();

				//Validación
				isNumeric = Double.TryParse(stringNumberArray[i], out wagerArray[i]);
                while (!isNumeric)
				{
					Console.WriteLine("<Error>. Por favor, ingrese sólo números:");
					stringNumberArray[i] = Console.ReadLine();
					isNumeric = Double.TryParse(stringNumberArray[i], out wagerArray[i]);
				}
				wagerArray[i] = Double.Parse(stringNumberArray[i]);

				//Estandarización a "," para parsearlo (en caso de que presente un ".")
				dot = ".";
				containsDot = stringNumberArray[i].Contains(dot);
				if (containsDot)
				{
					stringNumberArray[i] = stringNumberArray[i].Replace(".", ",");
					
				}
				wagerArray[i] = Double.Parse(stringNumberArray[i]);
				totalWager += wagerArray[i];
			}
			//Resolución
			pool = totalWager - (totalWager * (commission / 100));
			Console.WriteLine("\nPool total: $ " + totalWager);
			Console.WriteLine("Pool con comisión ({0}%): $ {1}", commission, pool);
			Console.WriteLine("\nNro | Pool | Div. decimal y fraccional:");
			for (int i = 0; i < wagerArray.Length; i++)
			{
				odds[i] = pool / wagerArray[i];
				if (odds[i] < 3)
				{
					if (odds[i] < 1) odds[i] = 1.10;
					fractionalOdds[i] = (int)odds[i];
				}
				else fractionalOdds[i] = (int)odds[i] - 1;
				
				Console.WriteLine(".# {0}: {1:C}. Div: {2:C} ({3} a 1). ",
				(i + 1), wagerArray[i], Math.Round(odds[i], 2), fractionalOdds[i]);
			}
		}
	}
}
