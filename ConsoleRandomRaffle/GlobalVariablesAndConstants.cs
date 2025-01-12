using System;

namespace Globals
{
	public static class GlobalVariablesAndConstants
	{
		/*
		 * Atributos de uso general
		 */
		public static int counter = 0, round = 0;

		// *** SCREEN SEETINGS *** 

		/*
		 * ScreenSet() configura título, color de fondo de pantalla, y color de fuente
		 */
		public static void ScreenSet(string programTitle, Enum @background, Enum @foreground)
		{
			Console.Title = programTitle;
			Console.BackgroundColor = (ConsoleColor)background;
			Console.ForegroundColor = (ConsoleColor)foreground;
			Console.Clear();
		}
		// *** VALIDATIONS: INTEGER ***;
		private static string stringNumber;
		public static string StringNumber
		{
			get => stringNumber;
			set
			{
				stringNumber = value;
				int intNumber = 0;
				while (stringNumber == string.Empty)
				{
					Console.Write(" Campo vacío. Ingrese un valor númérico: ");
					stringNumber = Console.ReadLine();
				}
				bool isNumeric = Int32.TryParse(stringNumber, out intNumber);
				while (!isNumeric)
				{
					Console.Write(" - ERROR. Por favor, solamente ingrese valores numéricos: ");
					stringNumber = Console.ReadLine();
				    isNumeric = Int32.TryParse(stringNumber, out intNumber);
				}
			}
		}
		public static int ValidateInteger(string readLine)
		{
			int intNumber;
			StringNumber = readLine;
			intNumber = Int32.Parse(StringNumber);

			return intNumber;
		}

		// *** FORCE TO EXIT METHOD ***
		/*
		 ** HandleMethodByInput lanza una excepción ante el string "exit"
		 ** y torna falso el valor de la variable pública showError
		 ** Se utiliza en la visualización mediante if del catch en un método
		 */
		public static bool showError; //variable "global" para el método HandleMethodByInput()
		public static void HandleMethodByInput(string keyWord)
		{
			if (keyWord == "exit")
			{
				showError = false; // Trabaja como variable global
				throw new Exception();
			}
		}
	}
}
