using System;

/*
 Nota: Práctica Nro 11. Año 2021. 
 Objetivo: Aplicación de consola monolibrería (sólo librería System)
 */

namespace ConsoleDiceGameChicago
{
	internal class Program
	{
		static void Main(string[] args)
		{
			ChicagoGame chicago = new ChicagoGame();
			Game.ScreenSet("CHICADO DICE GAME", ConsoleColor.Black, ConsoleColor.DarkYellow);
			while (Game.start)
			{
				chicago.Menu();
			}
			Console.WriteLine("\n ¡Nos vemos en la próxima!");
		}
	}
	public class ChicagoGame
	{
		//Atributos: objetos
		Random random = new Random();

		//Atributos: variables
		string title = "\n *** Chicago Dice Game / Juego de dados Chicago *** \n";
		string menu = " MENU:" +
					"\n 1. Nueva partida" +
					"\n 2. Reglas" +
					"\n 3. Salir" +
					"\n Seleccione: ";
		public string rules = " *** Chicago Dice Game / Juego de dados Chicago ***\n" +
				"\n --- Reglas del Chicago ---" +
				"\n JUGADORES: 2 o más" +
				"\n MATERIALES: 2 dados" +
				"\n OBJETIVO: Gana quien suma más puntos tras 11 rondas" +
				"\n PARTIDA: " +
				"\n 1. Preparación: Cada jugador tira dos dados. El tiro que sume mayor puntuación, abre la partida." +
				"\n 2. Valor de Chicago: En cada ronda se establece un valor de Chicago, equivalente al N° de ronda + 1" +
				"\n Es decir, en la ronda 1, el valor será 2; en la ronda 2 el valor será 3; y así sucesivamente " +
				"\n hasta la ronda 11, en donde el valor del chicago será de 12" +
				"\n 2.2. Puntos: Si la sumatoria de 2 dados equivale al valor de Chicago para la ronda, " +
				"el jugador gana tantos puntos como valga el Chicago" +
				"\n\n Reglamento tomado de: 'Enciclopedia de los Juegos de Mesa', " +
				"\n de Niké Arts. " +
				"\n Editorial Victor, año 2000 " +
				"\n\n Presione cualquier tecla para volver al menú";

		public string option, message;
		public int pIndex, chicago, itsHigher, itsLower;
		public bool itsEqual;

		//Atributos: arrays
		string[] defaultPlayersNames =
		{
			"Carolina", "Martina", "Cecilia","Valeria",
			"Charly", "Robert", "Jack", "John"
		};
		string[] defaultPlayersNamesClone;
		string[] playerName = new string[3];
		int[] playerPoints = new int[3];
		int[] dice = new int[2];
		int[] diceValue = new int[3];
		int[] chicagoScoring = new int[3];

		//Propiedades
		public string Option
		{
			get { return option; }
			set
			{
				option = value;
				while (option != "1" && option != "2" && option != "3")
				{
					Console.WriteLine(" ERROR. Por favor, ingrese opción correcta: ");
					Console.Write(menu);
					option = Console.ReadLine();
				}
			}
		}
		public string PlayerName
		{
			get { return playerName[0]; }
			set
			{
				playerName[0] = value;
				while (playerName[0] == String.Empty)
				{
					Console.Write(" ERROR ¡No escribiste ningún nombre! " +
						"Ingresa tu nombre o nickname: ");
					playerName[0] = Console.ReadLine();
				}
			}
		}
		//#0. Menu
		public void Menu()
		{
			Console.WriteLine(title);
			Console.Write(menu);
			Option = Console.ReadLine();
			if (Option == "1") GamePlay();
			else if (Option == "2") Game.Rules(rules);
			else if (Option == "3") Game.Exit(true);
		}
		//#1. GamePlay
		public void GamePlay()
		{
			Console.Clear();
			Console.WriteLine(title);
			NameAssignation();
			WhoStarts();
			Rounds();
			EndGame();
			Game.Exit(false);
		}
		//#1.1. Asignación de nombre
		public void NameAssignation()
		{
			//Asignación de nombre de usuario
			Console.WriteLine(" a. --> JUGADORES <--");
			Console.Write(" Introducí tu nombre o nickname: ");
			PlayerName = Console.ReadLine();

			//Versión clonada para preservar el array original
			defaultPlayersNamesClone = (string[])defaultPlayersNames.Clone();

			//Sorteo de nombres:
			for (int i = 1; i < playerName.Length; i++) //i = 0 es el nickname del usuario
			{
				pIndex = random.Next(0, defaultPlayersNamesClone.Length);
				playerName[i] = defaultPlayersNamesClone[pIndex];
				while (playerName[i] == "")
				{
					pIndex = random.Next(0, defaultPlayersNamesClone.Length);
					playerName[i] = defaultPlayersNamesClone[pIndex];
				}
				playerName[i] = defaultPlayersNamesClone[pIndex];
				defaultPlayersNamesClone[pIndex] = "";
			}
			/*El namespace System no tiene listas, que permiten añadir o quitar elementos de forma dinámica
				 * La consigna en este ejercicio es una aplicación monolibrería. 
				 * Una forma de evitar repetición con dicha condición es borrar elementos del array ya utilizados
				 * y cotejarlos a cadenas vacías para reiniciar el sorteo, hasta que salgan nombres utilizables.
			*/
			Console.WriteLine(" Jugadores: {0} (tú) - {1} - {2}",
				playerName[0], playerName[1], playerName[2]);
		}
		//#1.2. Quien saca el valor más chico en el dado
		public void WhoStarts()
		{
			Console.WriteLine("\n b. --> APERTURA <-- " +
				"\n Quien obtenga mayor número iniciará la partida:");
			Console.ReadKey();
			DiceRoll();
			itsHigher = ItsHigher(diceValue[0], diceValue[1], diceValue[2]);
			for(int i = 0; i < diceValue.Length; i++)
				if (diceValue[i] == itsHigher) pIndex = i;
			Console.ReadKey();
			WhoStartsReorder(pIndex);
            Console.ReadKey();
        }
		//#1.2.1. Reordenamiento de jugadores
		public void WhoStartsReorder(int pIndex)
		{
			int whoStarts1st, whoStarts2nd, whoStartsLast = 0; 			string name1, name2, name3;

			//Quien empieza primero (mayor valor)
			whoStarts1st = pIndex;
			
			//Quien empieza último (menor valor)
			itsLower = ItsLower(diceValue[0], diceValue[1], diceValue[2]);
			for(int i = 0; i < diceValue.Length; i++)
				if (itsLower == diceValue[i]) whoStartsLast = i;
			
			//Quien queda en el medio (valor intermedio)
			whoStarts2nd = 3 - whoStarts1st - whoStartsLast;
			
			//Reasignación
			name1 = playerName[whoStarts1st];
			name2 = playerName[whoStarts2nd];
			name3 = playerName[whoStartsLast];
			playerName[0] = name1; 			playerName[1] = name2; 			playerName[2] = name3;
            Game.SetLines("-", 1);
			Console.WriteLine(" Abre la partida {0} ({1} pts). " +
				"Lo siguen {2} y {3}",
				name1, diceValue[whoStarts1st],
				name2, name3);
			Game.SetLines("-", 1);
		}
		public void Rounds()
		{
			const int roundsToShow = 3; //bloque de rondas que aparecen por consola
			const int roundsLimit = 11; //11 rondas es el máximo
			int round = 1;
			chicago = round + 1; //El valor del Chicago siempre 1 más la ronda
			Console.Clear();
			Game.SetLines("-", 1);

			while (round < roundsLimit)
			{
				Console.Clear();
				Game.SetLines("~", 1);
				Console.WriteLine(" d. --> RONDAS <--");
				Game.SetLines("~", 1);

				for (int roundNumber = 0; roundNumber < roundsToShow; roundNumber++)
				{
                    Console.WriteLine();
					Console.WriteLine(" RONDA {0} (Chicago = {1}) ", round, chicago);
					Console.ReadKey();
					DiceRoll();
					round++;
					chicago = round + 1;

					if (round > 11) 
					{
                        Console.WriteLine();
                        Game.SetLines("-", 1);
                        Console.WriteLine(" Resumen de partida: {0} = {1} pts, {2} = {3} pts, {4} = {5} pts",
							playerName[0], playerPoints[0],
							playerName[1], playerPoints[1],
							playerName[2], playerPoints[2]);
						Game.SetLines("-", 1);
						break;
					}
				}
				Console.ReadKey();
			}
		}
		public void EndGame()
		{
			ItsEqual(playerPoints[0], playerPoints[1], playerPoints[2]);
			for (int i = 0; i < playerPoints.Length; i++)
				if (itsHigher == playerPoints[i]) pIndex = i;

			//Pantalla final
			Console.Clear();
			Console.WriteLine();

			string endText0 = " *- Juego de dados Chicago -* ";
			Console.SetCursorPosition((Console.WindowWidth - endText0.Length) / 2,
				Console.CursorTop);
			Console.WriteLine(endText0);

			Console.WriteLine();
			Game.SetLines("*", 2);

			string endText1 = " ***--> FINAL <--***";
			string endText2 = " ***** ¡¡¡HA GANADO " + playerName[pIndex].ToUpper() + "!!!***** ";
			string endText3 = " Puntuación final: " + playerPoints[pIndex] + " pts.";

			//Alineamiento
			Console.SetCursorPosition((Console.WindowWidth - endText1.Length) / 2,
				Console.CursorTop);
			Console.WriteLine(endText1);
			Console.SetCursorPosition((Console.WindowWidth - endText2.Length) / 2,
				Console.CursorTop);
			Console.WriteLine(endText2);
			Console.SetCursorPosition((Console.WindowWidth - endText3.Length) / 2,
				Console.CursorTop);
			Console.WriteLine(endText3);
			Game.SetLines("*", 2);
		}
		//---Métodos auxiliares---
		//#1.5.1. Dados
		public void DiceRoll()
		{
			for (int i = 0; i < playerName.Length; i++)
			{
				diceValue[i] = 0;

				for (int j = 0; j < dice.Length; j++)
				{
					dice[j] = random.Next(1, 7);
					diceValue[i] += dice[j];
				}
				if (diceValue[i] == chicago)
				{
					playerPoints[i] += chicago;
					chicagoScoring[i] += 1;
					message = "¡Chicago de " + playerName[i] + "! Scoring: " +
						chicagoScoring[i] + " chicago. Puntos: " + playerPoints[i] + " pts";
				}
				else if (chicago != 0) message = playerPoints[i] + " pts"; //Si arrancó la ronda, nunca es cero
				Console.WriteLine(" {0}: [{1}][{2}] {3}",
					playerName[i], dice[0], dice[1], message);
				Console.ReadKey();
			}
		}
		public int ItsHigher(int num1, int num2, int num3)
		{
			Tiebreaker(itsHigher = Math.Max(num1, Math.Max(num2, num3))); //evalua empate o no
			return itsHigher;
		}
		public int ItsLower(int num1, int num2, int num3)
		{
			itsLower = Math.Min(num1, Math.Min(num2, num3));
			return itsLower;
		}
		public int Tiebreaker(int itsHigher)
		{
			while (itsHigher == diceValue[0] && itsHigher == diceValue[1] ||
				itsHigher == diceValue[1] && itsHigher == diceValue[2] ||
				itsHigher == diceValue[0] && itsHigher == diceValue[2])
			{
				Console.WriteLine("\n EMPATE: {0} ({1}) - {2} ({3}) - {4} ({5}). Se realiza nueva ronda: ",
					playerName[0], diceValue[0],
					playerName[1], diceValue[1],
					playerName[2], diceValue[2]);
				DiceRoll();
				itsHigher = ItsHigher(diceValue[0], diceValue[1], diceValue[2]);
			}
			return itsHigher;
		}
		public int Tiebreaker(int playerA, int playerB)
		{
			int[] players = { playerA, playerB };
			while (diceValue[playerA] == diceValue[playerB])
			{
				diceValue[playerA] = random.Next(1, 7);
				diceValue[playerB] = random.Next(1, 7);
			}
			itsHigher = Math.Max(diceValue[playerA], diceValue[playerB]);
			for (int i = 0; i < players.Length; i++)
				if (diceValue[players[i]] == itsHigher) pIndex = players[i];
			return pIndex;
		}
		public void ItsEqual(int points1, int points2, int points3)
		{
			bool itsEqual;
			chicago = 0;
			itsHigher = ItsHigher(playerPoints[0], playerPoints[1], playerPoints[2]);

			//Triple empate...
			if (itsEqual = Math.Equals(points1, Math.Equals(points2, points3)))
			{
				Console.WriteLine(" Triple empate de {0}, {1}, y {2}." +
					"\n Se realizará una ronda desempate (gana tiro más alto):",
									 playerName[0], playerName[1], playerName[2]);
				for (int i = 0; i < diceValue.Length; i++)
					diceValue[i] = random.Next(1, 7);
				itsHigher = Tiebreaker(ItsHigher(
					diceValue[0], diceValue[1], diceValue[2])
					);
				for (int i = 0; i < diceValue.Length; i++)
					playerPoints[i] += diceValue[i];
				Console.ReadKey();
			}
			//Los 2 iguales...(jugador 1 y 2)
			else if (itsEqual = Math.Equals(playerPoints[0], playerPoints[1])
				&& itsHigher == playerPoints[1]) 
			{
				Console.WriteLine(" Empate de {0} y {1}", playerName[0], playerName[1]);
				pIndex = Tiebreaker(0, 1);
				for (int i = 0; i < diceValue.Length; i++)
					playerPoints[i] += diceValue[i];
				Console.ReadKey();
			}
			// Los 2 iguales...(jugador 2 y 3)
			else if (itsEqual = Math.Equals(playerPoints[1], playerPoints[2])
				&& itsHigher == playerPoints[2])
			{
				Console.WriteLine("Empate de {0} y {1}", playerName[1], playerName[2]);
				pIndex = Tiebreaker(1, 2);
				for (int i = 0; i < diceValue.Length; i++)
					playerPoints[i] += diceValue[i];
				Console.ReadKey();
			}
			// Los 2 iguales... (jugador 1 y 3)	
			else if (itsEqual = Math.Equals(playerPoints[0], playerPoints[2])
				&& itsHigher == playerPoints[2]) 
			{
				Console.WriteLine("Empate de {0} y {2}", playerName[0], playerName[1]);
				pIndex = Tiebreaker(0, 2);
			}
		}
		//#1.6. Final
		
	}
	public static class Game
	{
		//Atributos: variables
		public static bool start = true;
		
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
		//#2. Rules
		public static void Rules(string rules)
		{
			Console.Clear();
			Console.WriteLine(rules);
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
				else Console.Clear();
			}
		}
	}
}