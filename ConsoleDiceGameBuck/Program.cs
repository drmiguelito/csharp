using System;
/*
 * Práctica #10. Año 2021
 * Objetivo: aplicación monolibrería. Uso de propiedades y clases static.
 */

namespace ConsoleDiceGameBuck
{
	internal class Program
	{
		static void Main(string[] args)
		{
			BuckGame buck = new BuckGame();
			Game.ScreenSet();
			
			while (buck.start)
			{
				Console.WriteLine(buck.title);
				buck.Menu();
            }
			Console.WriteLine("\n ¡Nos vemos en la próxima!");
		}
	}
	internal class BuckGame
	{
		//Atributos: variables
		public bool start = true;
		public string title = "\n *** Buck Dice Game / Juego de dados Buck *** \n";
		public string menu = " MENU:" +
					"\n 1. Nueva partida" +
					"\n 2. Reglas" +
					"\n 3. Salir" +
					"\n Seleccione: ";
		public string option, message;
		public int pIndex, itsMinor, itsHigher, buck;

		//Atributos: arrays
		string[] defaultPlayersNames =
		{
			"Carolina", "Martina", "Cecilia","Valeria",
			"Charly", "Robert", "Jason", "John"
		};
		string[] defaultPlayersNamesClone;
		string[] playerName = new string[3];
		public int[] dice = new int[3];
		int[] playerPoints = new int[3];

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
		//#0. Menú
		public void Menu()
		{
			Console.Write(menu);
			Option = Console.ReadLine();
			if (Option == "1") GamePlay();
			else if (Option == "2") Rules();
			else if (Option == "3") Exit();
		}
		//#1. Juego del Buck
		public void GamePlay()
		{
			Console.Clear();
			Console.WriteLine(title);
			NameAssignation();
			WhoStarts();
			SetBuckValue();
			Rounds();
			EndGame();
			Exit();
		}
		//#1.1. Asignación de nombres
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
				pIndex = Dice.Number(0, defaultPlayersNamesClone.Length);
				playerName[i] = defaultPlayersNamesClone[pIndex];

				while (playerName[i] == "")
				{
					pIndex = Dice.Number(0, defaultPlayersNamesClone.Length);
					playerName[i] = defaultPlayersNamesClone[pIndex];
				}
				/*El namespace System no tiene listas, que permiten añadir o quitar elementos de forma dinámica
				 * La consigna en este ejercicio es una aplicación monolibrería. 
				 * Una forma de evitar repetición con dicha condición es borrar elementos del array ya utilizados
				 * y cotejarlos a cadenas vacías para reiniciar el sorteo, hasta que salgan nombres utilizables.
			*/
				playerName[i] = defaultPlayersNamesClone[pIndex];
				defaultPlayersNamesClone[pIndex] = "";
			}
			Console.WriteLine(" Jugadores: {0} (tú) - {1} - {2}",
				playerName[0], playerName[1], playerName[2]);
		}
		//#1.2. Quien saca el valor más chico en el dado
		public void WhoStarts()
		{
			Console.WriteLine("\n b. --> APERTURA <-- " +
				"\n Quien obtenga menor número iniciará la partida");
			for (int i = 0; i < dice.Length; i++) dice[i] = Dice.Number(1, 7);
			itsMinor = Math.Min(dice[0], Math.Min(dice[1], dice[2]));

			//Validación y repetición en caso de empate
			while (itsMinor == dice[0] && itsMinor == dice[1] ||
				itsMinor == dice[1] && itsMinor == dice[2] ||
				itsMinor == dice[0] && itsMinor == dice[2])
			{
				Console.WriteLine(" EMPATE: {0} [{1}] - {2} [{3}] - {4} [{5}]. Se realiza nueva ronda: ",
					playerName[0], dice[0], playerName[1], dice[1], playerName[2], dice[2]);

				for (int i = 0; i < dice.Length; i++) dice[i] = Dice.Number(1, 7);
				itsMinor = Math.Min(dice[0], Math.Min(dice[1], dice[2]));
			}
			if (itsMinor == dice[0]) pIndex = 0;
			else if (itsMinor == dice[1]) pIndex = 1;
			else if (itsMinor == dice[2]) pIndex = 2;
			Console.ReadKey();
			Console.WriteLine(
				" Tirada: {0} [{1}] - {2} [{3}] - {4} [{5}] ~ Abre {6} (valor: {7}) \n",
				playerName[0], dice[0], playerName[1], dice[1], playerName[2], dice[2],
				playerName[pIndex], dice[pIndex]
				);
			WhoStartsReorder();
		}
		public void WhoStartsReorder()
		{
			int whoStarts1st, whoStarts2nd, whoStartsLast = 0; 			string name1, name2, name3;
			
			//Quien empieza primero (menor valor)
			whoStarts1st = pIndex;

            //Quien empieza último (mayor valor)
            itsHigher = Math.Max(dice[0], Math.Max(dice[1], dice[2]));
			if (itsHigher == dice[0]) whoStartsLast = 0;
			else if (itsHigher == dice[1]) whoStartsLast = 1;
			else if (itsHigher == dice[2]) whoStartsLast = 2;

            //Quien queda en el medio (valor intermedio)
            whoStarts2nd = 3 - whoStarts1st - whoStartsLast;

			//Reasignación
			name1 = playerName[whoStarts1st];
			name2 = playerName[whoStarts2nd];
			name3 = playerName[whoStartsLast];
			playerName[0] = name1; 			playerName[1] = name2; 			playerName[2] = name3;
        }
		//#1.4. Determinar valor PUNTO (Buck point)
		public void SetBuckValue()
		{
            Console.WriteLine(" c. --> VALOR DEL BUCK <--");
			Console.ReadKey();
			buck = Dice.Number(1, 7);
			Console.WriteLine(" Tira {0}: [{1}]",
				playerName[0], buck);
            Console.WriteLine();
            Console.ReadKey();
			Game.SetLines("#", 1);
			Console.WriteLine(" El Buck se ha establecido en: " + buck);
			Game.SetLines("#", 1);
			Console.ReadKey();
		}
		//#1.5. Rondas
		public void Rounds()
		{
			const int roundsToShow = 3;
			Console.Clear();
            Game.SetLines("-", 1);
			int round = 1;

			while (playerPoints[0] < 15 && playerPoints[1] < 15 && playerPoints[2] < 15)
			{
				Console.Clear();
				Game.SetLines("~", 1);
				Console.WriteLine(" d. --> RONDAS <--");
				Game.SetLines("~", 1);
				Console.WriteLine(" >> Buck: {0} >> ", buck);
				Game.SetLines("~", 1);

				for (int roundNumber = 0; roundNumber < roundsToShow; roundNumber++)
				{
					Console.WriteLine("\n RONDA {0}: ", round);
					for (int i = 0; i < playerName.Length; i++) //# jugador
					{
						Console.ReadKey();
						//3 dados
						int singleRoundPointCounter = 0;
						for (int j = 0; j < dice.Length; j++) //# dado
						{
							dice[j] = Dice.Number(1,7);
							if (dice[j] == buck)
							{
								playerPoints[i]++;
								singleRoundPointCounter++;
							}
						}
						//Conclusiones de tirada
						if (singleRoundPointCounter == 1) message = "Punto para " + playerName[i] + ". ";
						else if (singleRoundPointCounter == 2) 
						{
							if (playerPoints[i] == 16)
							{
								message = "Tiro anulado: Según este relgamento del Buck, " +
									"cuando un jugador tiene 13 o 14 puntos, debe alcanzar" +
									"los 15 puntos exactos\n ¡Y " + playerName[i] + " hizo un doble!";
								playerPoints[i] -= 2;
							}
							message = "¡Doble punto de " + playerName[i] + "! "; 
						}
						else if (dice[0] == dice[1] && dice[1] == dice[2] && dice[2] != buck)
						{
							if (playerPoints[i] == 13 || playerPoints[i] == 14)
							{
								message = "Tiro anulado: Según este relgamento del Buck, " +
									"cuando un jugador tiene 13 o 14 puntos, debe alcanzar" +
									"los 15 puntos exactos\n ¡Y " + playerName[i] + 
									" hizo un 'small Buck!";
							}
							else
							{
								message = "¡" + playerName[i] + " hizo un 'small Buck'! ";
								playerPoints[i] += 4;
							}
						}
						else if (dice[0] == dice[1] && dice[1] == dice[2] && dice[2] == buck)
						{
							if (playerPoints[i] == 13 || playerPoints[i] == 14)
							{
								message = "Tiro anulado: Según este relgamento del Buck, " +
									"cuando un jugador tiene 13 o 14 puntos, debe alcanzar" +
									"los 15 puntos exactos\n ¡Y " + playerName[i] +
									" hizo un 'Buck!";
							}
							else
							{
								message = "¡" + playerName[i] + " hizo un 'BUCK'! ";
								playerPoints[i] += 15;
							}
						}
						else message = "";
						Console.WriteLine(" {0}: [{1}] [{2}] [{3}]\t {4}{5} pts.",
							playerName[i], dice[0], dice[1], dice[2], message, playerPoints[i]);
					}
					round++;
					Console.ReadKey();
					if (playerPoints[0] >= 15 
					 	|| playerPoints[1] >= 15 
						|| playerPoints[2] >= 15)
						break;
				}
				if (playerPoints[0] >= 15
						 || playerPoints[1] >= 15
						|| playerPoints[2] >= 15)
					break;
				Console.ReadKey();
			}
		}
		//#1.6. Final
		public void EndGame()
		{
			int itsHigher = Math.Max(playerPoints[0], Math.Max(playerPoints[1], playerPoints[2])); 			bool itsEqual; 			if (itsEqual = Math.Equals(playerPoints[0], 				Math.Equals(playerPoints[1], playerPoints[2])))
			{
				Console.WriteLine("Triple empate de {0}, {1}, y {2}",
									 playerName[0], playerName[1], playerName[2]);
				Console.ReadKey();
			} 			else if (itsEqual = Math.Equals(playerPoints[0], playerPoints[1]) &&
							 itsHigher == playerPoints[1])
				Console.WriteLine("Empate de {0} y {1}", playerName[0], playerName[1]);
			else if (itsEqual = Math.Equals(playerPoints[1], playerPoints[2]) &&
				 itsHigher == playerPoints[2])
				Console.WriteLine("Empate de {0} y {1}", playerName[1], playerName[2]);
			else if (itsEqual = Math.Equals(playerPoints[0], playerPoints[2]) &&
				 itsHigher == playerPoints[2])
				Console.WriteLine("Empate de {0} y {2}", playerName[0], playerName[1]);
			else
			{
				if (itsHigher == playerPoints[0]) pIndex = 0;
				else if (itsHigher == playerPoints[1]) pIndex = 1;
				else if (itsHigher == playerPoints[2]) pIndex = 2;
				Console.Clear();
                Console.WriteLine();
                Game.SetLines("*", 2);

				string endText1 = " ***--> FINAL <--***";
				string endText2 = " ***** ¡¡¡HA GANADO " + playerName[pIndex].ToUpper() + "!!!***** ";
				string endText3 = " Puntuación final: " + playerPoints[pIndex] + " pts.";
				Console.SetCursorPosition((Console.WindowWidth - endText1.Length) / 2, Console.CursorTop);
                Console.WriteLine(endText1);
				Console.SetCursorPosition((Console.WindowWidth - endText2.Length) / 2, Console.CursorTop);
				Console.WriteLine(endText2);
				Console.SetCursorPosition((Console.WindowWidth - endText3.Length) / 2, Console.CursorTop);
				Console.WriteLine(endText3);

				Game.SetLines("*", 2);
			}
		}
		//#2. Reglas
		public void Rules()
		{
			Console.Clear();
			Console.WriteLine("\n  *** Buck Dice Game / Juego de dados Buck ***");
			Console.WriteLine("\n --- Reglas del Buck ---" +
				"\n JUGADORES: 2 o más" +
				"\n MATERIALES: 3 dados" +
				"\n OBJETIVO: Gana quien alcanza primero 15 puntos" +
				"\n PARTIDA: " +
				"\n 1. Preparación: Cada jugador tira un dado: el de menor puntuación abre la partida." +
				"\n 2. Apertura: Quien abre la partida arroja un dado. Dicho valor será denominado 'PUNTO'." +
				"\n 3. Desarrollo: Por turnos, cada jugador tirará TRES DADOS." +
				"\n 3.1. Por cada dado cuyo valor individual coincida con 'PUNTO', el jugador ganará 1 punto." +
				"\n 3.2. Si en una tirada salen 3 números idénticos, pero DISTINTOS de 'PUNTO'," +
				"\n el jugador ganará 4 puntos." +
				"\n 3.3. Si en una tirada salen tres números IGUALES a 'PUNTO', " +
				"\n el jugador obtendrá 15 puntos (GANA LA PARTIDA)." +
				"\n 4. Excepciones (importante): Cuando un jugador tenga 13 o 14 puntos, deberá lograr " +
				"\n los 15 puntos de forma EXACTA para ganar la partida." +
				"\n Si sobrepasa dicho número, la tirada será inválida." +
				"\n\n Reglamento tomado de: 'Enciclopedia de los Juegos de Mesa', " +
				"\n de Niké Arts. " +
				"\n Editorial Victor, año 2000 "
				);
			Console.WriteLine("\n Presione cualquier tecla para volver al menú");
			Console.ReadKey();
			Console.Clear();
		}
		//#3. Exit game
		public void Exit()
		{
			if (option != "3")
			{
				Console.Write("\n ¿Quiere salir del programa? S/N: ");
				string confirmation = Console.ReadLine().ToUpper();
				if (confirmation != "N") start = false;
				else Console.Clear();
			}
			else if (option == "3") start = false;
		}
	}	
	internal static class Game
	{
		//#0. Apariencia general
		public static void ScreenSet()
		{
			Console.Title = "BUCK DICE GAME";
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.Clear();
		}
		//#0. Líneas divisorias
		public static void SetLines(string style, int number) 		{ 			for (int i = 0; i < number; i++) 			{ 				for (int j = 0; j < Console.WindowWidth; j++) 				{ 					Console.SetCursorPosition(j, Console.CursorTop); 					Console.Write(style); 				} 				Console.SetCursorPosition(0, Console.CursorTop + 1); 			} 		}
	}
	internal static class Dice
	{
		public static int[] dice = new int[3]; //dado
		public static Random random = new Random();
		public static BuckGame player = new BuckGame();
		public static void Roll(int nTimes)
		{
			for (int i = 0; i < nTimes; i++)
			{
				player.dice[i] = random.Next(1, 7);
			}
		}
		public static int Number(int inf, int sup)
		{ 
			return player.pIndex = random.Next(inf, sup);
		}
	} 
}
