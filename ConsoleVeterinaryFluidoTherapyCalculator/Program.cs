using System;
/*
 Práctica #7 año 2021
Monoclase - multimétodo void - monolibrería
 */

namespace ConsoleVeterinaryFluidoTherapyCalculator
{
	internal class Program
	{
		//Attributes: menu:
		public string option, mainMenu = "\n MENU: " +
					"\n 1. Déficit previo" +
					"\n 2. Mantenimiento" +
					"\n 3. Pérdidas contemporáneas" +
					"\n 4. Salir";
		public string animalType, maintenanceMenu = " " +
			"\n 1. Perro adulto talla grande" +
			"\n 2. Perro adulto talla pequeña" +
			"\n 3. Gato" +
			"\n 4. Cachorro (canino o felino)";
		//Attributes: validation:
		public string stringInput, dot = ".";
		public bool calculator = true;
		public bool isNumeric, containsDot;
		public double num;
		//Attributes: calculations:
		public double weight, percent, result;
		static void Main(string[] args)
		{
			//Initial set
			Console.Title = "VETERINARY FT CALCULATOR";
			Console.BackgroundColor = ConsoleColor.DarkMagenta;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Clear();
			Program ft = new Program();
			//Calculator
			while (ft.calculator)
			{
				//Título
				Console.WriteLine("\n ***Fluidoterapia veterinaria: volumen a administrar***");
				//Menú y derivación a métodos
				ft.MenuAndProcedures();
				//Final
				ft.ExitCalculator();
			}
			Console.WriteLine("\n ¡Nos vemos en la próxima!");
		}
		//Método general
		public void MenuAndProcedures()
		{
			//Menú
			Console.WriteLine(mainMenu);
			Console.Write(" Seleccione: ");
			option = Console.ReadLine();
			//Validación de opción de menú
			while (option != "1" && option != "2" && option != "3" && option != "4")
			{
				Console.WriteLine("ERROR. Por favor, ingrese del 1 al 5 la opción deseada");
				Console.WriteLine(mainMenu);
				option = Console.ReadLine();
			}
			//Derivación a método de opción de menú
			switch (option)
			{
				case "1":
					PreviousDeficit();
					break;
				case "2":
					Maintenance();
					break;
				case "3":
					ContemporaryLosses();
					break;
				case "4":
					ExitCalculator();
					break;
			}
		}
		//Métodos derivados
		public void PreviousDeficit()
		{
			Console.WriteLine("\n ---> DEFICIT PREVIO <---");
			//Peso
			Console.Write(" PESO del animal (kg): ");
			NumberValidation();
			weight = num;
			//% deshidratacion
			Console.Write(" % de deshidratación (escriba números, o tipee i para más info). ");
			NumberValidation();
			percent = num;
			//Resolución
			result = weight * percent * 10;
			Console.WriteLine(" RESULTADO: {0} kg * {1} % * 10 = {2} ml",
				weight, percent, result);
			//Enraizar a cero el peso:
			weight = 0;
		}
		public void Maintenance()
		{
			double animalTypeIndex;
			Console.WriteLine("\n ---> MANTENIMIENTO <---");
			Console.Write(" TIPO de animal: {0} \n Seleccione: ", maintenanceMenu);
			AnimalTypeValidation();
			animalTypeIndex = num;
			Console.Write("\n PESO del animal (kg): ");
			NumberValidation();
			weight = num;
			//Resolución
			result = animalTypeIndex * weight;
			Console.WriteLine(" RESULTADO ({0}): {1} ml * {2} kg = {3} ml/día"
				, animalType, animalTypeIndex, weight, result);
			Console.WriteLine();
			//Enraizar a cero el peso:
			weight = 0;
		}
		public void ContemporaryLosses()
		{
			Console.WriteLine("\n ---> PERDIDAS CONTEMPORANEAS <--- ");
			Console.WriteLine(" * Las pérdidas contemporáeas deben añadirse a la fluidoterapia de mantenimiento " +
				"en las primeras 24 hs *");
			Console.Write(" PESO del animal (kg): ");
			NumberValidation();
			weight = num;
			result = weight * 0.04;
			Console.WriteLine(" RESULTADO: 4% de {0} kg (peso corporal) = {1} ml/día", weight, result);
			//Enraizar a cero el peso:
			weight = 0;
		}
		//Métodos de validación
		public void NumberValidation()
		{
			stringInput = Console.ReadLine();
			if (option == "1" && weight != 0)
			{
				while (stringInput == "i" || stringInput == "I")
				{
					PreviousDeficitInfo();
					Console.Write("\n Escriba % de deshidratación del animal para continuar: ");
					stringInput = Console.ReadLine();
				}
				Console.WriteLine(); //salto de línea
			}
			isNumeric = Double.TryParse(stringInput, out num);
			//Reingreso para validación 
			while (!isNumeric)
			{
				Console.Write(" - ERROR - \n" +
					" Por favor, solamente ingrese valores numéricos: ");
				stringInput = Console.ReadLine();
				isNumeric = Double.TryParse(stringInput, out num);
			}
			//Estandarización del punto a coma
			containsDot = stringInput.Contains(dot);
			if (containsDot)
			{
				stringInput = stringInput.Replace(".", ",");
			}
			//Parseo
			num = Double.Parse(stringInput);
		}
		public void AnimalTypeValidation()
		{
			stringInput = Console.ReadLine();
			while (stringInput != "1" && stringInput != "2"
				&& stringInput != "3" && stringInput != "4")
			{
				Console.WriteLine(" Por favor, ingrese número válido: ");
				Console.WriteLine("{0}: ", maintenanceMenu);
				stringInput = Console.ReadLine();
			}
			switch (stringInput)
			{
				case "1":
					animalType = "perros adultos talla grande";
					num = 40; // ml/kg
					break;
				case "2":
				case "3":
					animalType = "perros adultos talla chica y gatos";
					num = 60; // ml/kg
					break;
				case "4":
					animalType = "cachorros";
					num = 130; // ml/kg
					break;
			}
		}
		public void ExitCalculator()
		{
			if (option != "4")
			{
				Console.WriteLine("\n ¿Desea continuar con el programa? S/N");
				string exitProg = Console.ReadLine().ToUpper();
				if (exitProg != "S") calculator = false;
				else Console.Clear();
			}
			else calculator = false;
			
		}
		//Métodos auxiliares
		public void PreviousDeficitInfo()
		{
			Console.Clear();
			Console.WriteLine(" ---> DEFICIT PREVIO <---");
			Console.WriteLine(" PESO del animal: {0}", weight);
			Console.WriteLine("\n *ESTIMACION DEL % DE DESHIDRATACION DEL PACIENTE*: ");
			Console.WriteLine(" ** < 5 % de deshidratación **:" +
				"\n . No detectable.");
			Console.WriteLine(" ** 5 - 6 % de deshidratación **: " +
				"\n . Pliegue cutáneo +.");
			Console.WriteLine(" ** 6 - 10 % de deshidratación **: " +
				"\n . Pliegue cutáneo++." +
				"\n . Leve aumento del TLLC." +
				"\n . Posible hundimiento de ojos. " +
				"\n . Posible sequedad de mucosas.");
			Console.WriteLine(" **10 - 12 % de deshidratación **: " +
				"\n . Pliegue cutáneo++++." +
				"\n . Aumento marcado del TLLC. " +
				"\n . Hundimiento de ojos. " +
				"\n . Sequedad de mucosas." +
				"\n . Posibles signos de shock (taquicardia, pulso rápido y débil, frío en extremidades).");
			Console.WriteLine(" ** 12 - 15 % de deshidratación ** " +
				"\n . Shock, muerte inminente.");
			Console.WriteLine("\n Fuente: wikibooks.org/wiki/Medicina_Veterinaria \n");
		}
	}
}