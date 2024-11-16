using System;

namespace ConsoleCardGameWar
{
	public static class Game
	{
		//Atributos: variables
		public static bool start = true;
		private static string option;
		private static int value;

		//Propiedades:
		public static int Value { get; set; }	
		public static string Option
		{
			get => option;
			set
			{
				option = value;
				while (option != "1" && option != "2" && option != "3")
				{
					Console.WriteLine(" ERROR. Por favor, ingrese opción correcta: ");
					Menu();
					option = Console.ReadLine();
				}
			}
		}
		//#0. Apariencia general
		public static void ScreenSet(string gameTitle, Enum @background, Enum @foreground)
		{
			Console.Title = gameTitle;
			Console.BackgroundColor = (ConsoleColor)background;
			Console.ForegroundColor = (ConsoleColor)foreground;
			Console.Clear();
		}
		//#0. Líneas divisorias
		public static void SetLines(string style, int number)
		{
			for (int i = 0; i < number; i++)
			{
				for (int j = 0; j < Console.WindowWidth; j++)
				{
					Console.SetCursorPosition(j, Console.CursorTop);
					Console.Write(style);
				}
				Console.SetCursorPosition(0, Console.CursorTop + 1);
			}
		}
		//#0. Asignación de nombres
		public static string NameAssignation(string[] namesArray)
		{
			Random random = new Random();
			return namesArray[random.Next(0, namesArray.Length)];
		}
		//#1. Menu
		public static void Menu()
		{
			Card card = new Card();
			Console.Clear();
			Console.WriteLine();
			Console.WriteLine(" *** War Card Game ***");
			Console.WriteLine(" 1. Jugar");
			Console.WriteLine(" 2. Reglas");
			Console.WriteLine(" 3. Salir");
			Console.Write(" Seleccione: ");
			Option = Console.ReadLine();
			Console.WriteLine();

			if (Option == "1") card.WarGamePlay();
			else if (Option == "2") WarGameRules();
			else if (Option == "3") Exit(true);
		}
		//#2. Reglas
		public static void WarGameRules()
		{
			Console.Clear();
			Console.WriteLine();
			Console.WriteLine(" ***War Card Game***");
			Console.WriteLine();
			Console.WriteLine(" ---> Reglas <---");
			Console.WriteLine(" * Jugadores: 2");
			Console.WriteLine(" #1. Elementos: 1 baraja francesa, en este caso, restringida al 9-10-J-Q-K-As");
			Console.WriteLine(" #2. Objetivo: El objetivo es ganar la mayor cantidad de bazas, dejando al " +
				" oponente sin cartas.");
			Console.WriteLine(" #3. Dinámica: Se reparten 12 cartas a cada jugador, ubicadas boca abajo.");
			Console.WriteLine(" #3.1. Por tunos, cada, jugador destapa la carta superior. La más alta gana y se lleva la otra carta.");
			Console.WriteLine(" #3.2. El juego termina cuando se acaban las cartas de un jugador.");
			Console.WriteLine();
			Console.WriteLine(" >> Presione cualquier tecla para retornar al menú principal");
			Console.ReadKey();
			Console.Clear();
		}
		//#3. Exit
		public static void Exit(bool quitProgram)
		{
			if (quitProgram) start = false;
			else
			{
				Console.Write("\n ¿Quiere salir del programa? S/N: ");
				string confirmation = Console.ReadLine().ToUpper();
				if (confirmation != "N") start = false;
			}
		}
		//#3.3. Establecer valor de la carta
		public static int CardValue(string card)
		{
			switch (card)
			{
				case "Joker": 
					Value = 15;
					break;
				case "cA":
				case "hA":
				case "dA":
				case "eA":
					Value = 14;
					break;
				case "cK":
				case "hK":
				case "dK":
				case "eK":
					Value = 13;
					break;
				case "cQ":
				case "hQ":
				case "dQ":
				case "eQ":
					Value = 12;
					break;
				case "cJ":
				case "hJ":
				case "dJ":
				case "eJ":
					Value = 11;
					break;
				case "c10":
				case "h10":
				case "d10":
				case "e10":
					Value = 10;
					break;
				case "c9":
				case "h9":
				case "d9":
				case "e9":
					Value = 9;
					break;
				case "c8":
				case "h8":
				case "d8":
				case "e8":
					Value = 8;
					break;
				case "c7":
				case "h7":
				case "d7":
				case "e7":
					Value = 7;
					break;
				case "c6":
				case "h6":
				case "d6":
				case "e6":
					Value = 6;
					break;
				case "c5":
				case "h5":
				case "d5":
				case "e5":
					Value = 5;
					break;
				case "c4":
				case "h4":
				case "d4":
				case "e4":
					Value = 4;
					break;
				case "c3":
				case "h3":
				case "d3":
				case "e3":
					Value = 3;
					break;
				case "c2":
				case "h2":
				case "d2":
				case "e2":
					Value = 2;
					break;
				default:
                    Console.WriteLine(" ERROR. Valor de carta no reconocido");
					break;
            }
			return Value;
		}
	}
}
