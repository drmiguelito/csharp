using System;

namespace ConsoleVeterinaryDoseCalculator
{
	/*
	 Práctica #8. Año 2021 
	 Programa Monoclase - multimétodo return - monolibrería
	 */
	internal class Program
	{
		//Atributos: generales y descriptivos
		public bool calculator = true;
		public string menu = "MENU: " +
			"\n 1. Cálculo de dosis" +
			"\n 2. Cálculo de dosis diaria" +
			"\n 3. Cálculo de dosis líquida" +
			"\n 4. Salir" +
			"\n Seleccione: ";
		public string option, previousCalc, stepText;

		//Atributos: cálculos
		public double num, dose, conc, weight, dosage, result;
		public int frequency;

		//Atributos: validaciones
		public string stringInput, dot = ".";
		public bool isNumeric, containsDot;
		static void Main(string[] args)
		{
			Program vetNurse = new Program();

			//Console screen set
			Console.Title = "VETERINARY DOSE CALCULATOR";
			Console.BackgroundColor = ConsoleColor.DarkMagenta;
			Console.Clear();

			while (vetNurse.calculator)
			{
				//Título
				Console.WriteLine("\n ***Veterinaria: Cálculo de dosis: mg/ml ***");
				Console.WriteLine(vetNurse.previousCalc);

				//Menu
				Console.Write(vetNurse.menu);
				vetNurse.MenuValidation();
				vetNurse.MenuDerivation();
				vetNurse.ExitProgram();
			}
            Console.WriteLine("\n ¡Nos vemos en la próxima!");
        }
		//----- Métodos: generales y resolución -----
		public void MenuDerivation()
		{
			if (option == "1") Dose();
			else if (option == "2") DailyDose();
			else if (option == "3") LiquidDose();
			else if (option == "4") ExitProgram();
		}
		public void Dose()
		{
            Console.WriteLine("\n ---> DOSIS <---");
			Console.Write(stepText = " DOSAJE (mg/ml): ");
            NumberValidation();
			dosage = num;
			Console.Write(stepText = " PESO del animal (kg): ");
			NumberValidation();
			weight = num;
			result = Math.Round(DoseFormula(dosage, weight));
			Console.WriteLine(" DOSIS (mg) = {0} mg/kg * {1} kg = {2} mg",
				dosage, weight, result);
			previousCalc = "\n Cálculo previo: dosis: " + result.ToString() + " mg \n";
		}
		public void DailyDose()
		{
			Console.WriteLine("\n ---> DOSIS DIARIA <---");
			Console.Write(stepText = " DOSAJE (mg/kg): ");
			NumberValidation();
			dosage = num;
			Console.Write(stepText = " PESO del animal (kg): ");
			NumberValidation();
			weight = num;
			Console.Write(stepText = " FRECUENCIA (veces/día): ");
			NumberValidation();
			frequency = (int)num;
			result = Math.Round(DailyDoseFormula(dosage, weight, frequency));
			Console.WriteLine(" DOSIS DIARIA (mg/día) = {0} mg/kg * {1} kg * {2} veces/día = {3} mg",
				dosage, weight, frequency, result);
			previousCalc = "\n Cálculo previo: dosis diaria: " + result.ToString() + " mg/día \n";
		}
		public void LiquidDose()
		{
			Console.WriteLine("\n ---> DOSIS LIQUIDA <---");
			Console.Write(stepText = " DOSIS (mg): ");
			NumberValidation();
			dose = num;
			Console.Write(stepText = " CONCENTRACION de la droga (mg/ml): ");
			NumberValidation();
			conc = num;
			result = Math.Round(LiquidDoseFormula(dose, conc), 2);
			Console.WriteLine(" DOSIS LIQUIDA (ml) = {0} mg / {1} mg/ml = {2} ml",
				dose, conc, result);
			previousCalc = "\n Cálculo previo: dosis líquida: " + result.ToString() + " ml \n";
		}
		//-----Metodos: fórmulas -----
		public double DoseFormula(double dosage, double weight)
		{
			return dosage * weight;
		}
		public double DailyDoseFormula (double dosage, double weight, int frequency) 
		{
			return dosage * weight * frequency;
		}
		public double LiquidDoseFormula(double dose, double concentration)
		{
			return dose / concentration;
		}
		//----- métodos de validación -----
		public void MenuValidation()
		{
			option = Console.ReadLine();
			while (option != "1" && option != "2" && option != "3" && option != "4")
			{
				Console.WriteLine(" ERROR: ingrese un número válido");
				Console.WriteLine(menu);
				option = Console.ReadLine();
			}
		}
		public void NumberValidation()
		{
			stringInput = Console.ReadLine();
			//Evaluación de input
			isNumeric = Double.TryParse(stringInput, out num);
			while (!isNumeric)
			{
				Console.Write(" - ERROR - \n" +
					" Por favor, solamente ingrese valores numéricos: ");
				stringInput = Console.ReadLine();
				isNumeric = Double.TryParse(stringInput, out num);
			}
			//Estandarización del punto a coma
			containsDot = stringInput.Contains(dot);
			if (containsDot) stringInput = stringInput.Replace(".", ",");
            //Parseo
            num = Double.Parse(stringInput);
			//Rebote si es cero
			while (num == 0)
			{
				Console.Write(" ERROR. Escribiste un cero. Por favor, utiliza otro número para{0}", stepText);
				
				//Recursividad
				NumberValidation();
			}
		}
		public void ExitProgram()
		{
			if (option != "4")
			{
				Console.WriteLine("\n ¿Desea realizar otro cálculo? S/N");
				string confirmation = Console.ReadLine().ToUpper();
				if (confirmation != "S") calculator = false;
				else Console.Clear();
			}
			else calculator = false;
        }
	}
}