using System;  namespace ConsoleDiceGameGoingToBoston { 	/*
	 * Práctica #9. Año 2021 
	 * Programa Monoclase (sólo clase Program) - monolibrería
	 * Juego de dados Going To Boston
	 * Jugadores: 3 (para esta versión)
	 */
	 internal class Program 
		{
		 //Atributos: variables
		 bool game = true;
		 string title, menu, option, message, lines;
		 int pIndex;
		 
		 //Atributos: arrays
		 string[] defaultPlayersNames =
		{
			"Carolina", "Cecilia", "Gimena","Valeria",
			"Charly", "Robert", "Brian", "Jason"
		};
		 string[] defaultPlayersNamesClone;
		 string[] playerName = new string[3];
		 //Nombre de los participantes
		 int[] playerPoints = new int[3]; //Valor de puntos de cada jugador
		 int[] partialPoints = new int[3]; //Puntos por donda de cada jugador
		 int[] dice = new int[3]; //Valor de cada dado
		 
		 //Atributos: objetos
		 Random random = new Random();
		 //#I-O
		 static void Main(string[] args)
		 {
			 Program bostonGame = new Program();
			 bostonGame.ScreenSet();
			 while (bostonGame.game)
			 {
				 Console.WriteLine(
					 bostonGame.title = "\n *** Going To Boston Dice Game ***\n");
				 Console.Write(
					 bostonGame.menu = " MENU:" +
					 "\n 1. Nueva partida" +
					 "\n 2. Reglas" +
					 "\n 3. Salir" +
					 "\n Seleccione: ");
				 bostonGame.MenuDerivation();
			 }
			 Console.WriteLine("\n ¡Nos vemos en la próxima!");
		 }
		 //#0. Screen set
		 public void ScreenSet()
		 {
			 Console.Title = "GOING TO BOSTON DICE GAME";
			 Console.BackgroundColor = ConsoleColor.DarkGreen;
			 Console.ForegroundColor = ConsoleColor.Gray;
			 Console.Clear();
		 }
		 //#0. Líneas divisorias
		 public string SetLines(string style, int number)
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
			 return style;
		 }
		 //#0. Menú
		 public void MenuDerivation()
		 {
			 option = Console.ReadLine();
			 //Validación
			 while (option != "1" && option != "2" && option != "3")
			 {
				 Console.WriteLine("\n ERROR. Por favor, ingrese opción correcta: ");
				 Console.Write(menu);
				 option = Console.ReadLine();
			 }
			 //Derivación
			 if (option == "1") GoingToBoston();
			 else if (option == "2") RulesOfGoingToBoston();
			 else if (option == "3") ExitGame();
		 }
		 //#1. Juego Going To Boston
		 public void GoingToBoston()
		 {
			 GoingToBostonNamesAssignation();
			 GoingToBostonWhoStarts();
			 GoingToBostonRounds();
			 EndGame();
			 ExitGame();
		 }
		 //#1.1. Asignación de nombres
		 public void GoingToBostonNamesAssignation()
		 {
			 //Array de string de jugadores
			 playerName = new string[3];
			 //Asignación de nombre de usuario
			 Console.WriteLine("\n a. --> JUGADORES <--");
			 Console.Write(" Introducí tu nombre o nickname: ");
			 playerName[0] = Console.ReadLine();
			 //Validación
			 while (playerName[0] == String.Empty)
			 {
				 Console.Write(" ERROR ¡No escribiste ningún nombre! Ingresa tu nombre o nickname: ");
				 playerName[0] = Console.ReadLine();
			 }
			 //Versión clonada para preservar el array original
			 defaultPlayersNamesClone = (string[])defaultPlayersNames.Clone();
			 //Sorteo de nombres:
			 int j;
			 //i = 1 porque i = 0 es reservado al nickname del usuario
			 for (int i = 1; i < playerName.Length; i++)
			 {
				 j = random.Next(0, defaultPlayersNamesClone.Length);
				 playerName[i] = defaultPlayersNamesClone[j];
				 while (playerName[i] == "")
				 {
					 j = random.Next(0, defaultPlayersNamesClone.Length);
					 playerName[i] = defaultPlayersNamesClone[j];
				 }
				 /*El namespace System no tiene listas, que permiten añadir o quitar elementos de forma dinámica
				 * La consigna en este ejercicio es una aplicación monolibrería.
				 * Una forma de evitar repetición con dicha condición es borrar elementos del array ya utilizados
				 * y cotejarlos a cadenas vacías para reiniciar el sorteo, hasta que salgan nombres utilizables. 
				 */
				 playerName[i] = defaultPlayersNamesClone[j];
				 defaultPlayersNamesClone[j] = "";
			 }
			 Console.WriteLine(" Jugadores: {0} (tú) - {1} - {2}",
					   playerName[0], playerName[1], playerName[2]);
		 }
		 //#1.2. Quien saca el valor más grande de los dados
		 public void GoingToBostonWhoStarts()
		 {
			 int whoStarts1st, whoStarts2nd, whoStartsLast;
			 string name1, name2, name3;
			 Console.WriteLine("\n b. --> APERTURA <-- " +
					   "\n Quien obtenga mayor número iniciará la partida:");
			 GoingToBostonWhoStartsDiceRoll();
			 //Asignaciones de orden de turnos
			 whoStarts1st = GoingToBostonHigherDiceValue(
				 partialPoints[0],
				 partialPoints[1],
				 partialPoints[2]
			 );
			 whoStartsLast = GoingToBostonLowerDiceValue(
				 partialPoints[0],
				 partialPoints[1],
				 partialPoints[2]
			 );
			 whoStarts2nd = 3 - whoStarts1st - whoStartsLast;
			 name1 = playerName[whoStarts1st];
			 name2 = playerName[whoStarts2nd];
			 name3 = playerName[whoStartsLast];
			 playerName[0] = name1;
			 playerName[1] = name2;
			 playerName[2] = name3;
			 Console.ReadKey();
			 Console.WriteLine();
			 SetLines("*", 1);
			 Console.WriteLine(" >> Inicia la partida: {0} ({1} pts). " +
					   "La ronda sigue con {2} y luego con {3}",
					   playerName[0], partialPoints[whoStarts1st],
					   playerName[1],
					   playerName[2]);
			 SetLines("*", 1);
			 Console.ReadKey();
		 }
		 //#1.2.1. Tirada de dados inicial
		 public void GoingToBostonWhoStartsDiceRoll()
		 {
			 for (int i = 0; i < playerName.Length; i++)
			 {
				 partialPoints[i] = 0;
				 for (int j = 0; j < dice.Length; j++)
				 {
					 dice[j] = random.Next(1, 7);
					 partialPoints[i] += dice[j];
				 }
				 Console.ReadKey();
				 Console.WriteLine(" {0}: [{1}] [{2}] [{3}]. Total: {4} pts.",
						   playerName[i], dice[0], dice[1], dice[2], partialPoints[i]);
			 }
		 }
		 //#1.2.2. Evaluación del valor más grande de los dados
		 public int GoingToBostonHigherDiceValue(int points1, int points2, int points3)
		 {
			 //Determinación del mayor valor
			 int itsHigher = Math.Max(points1, Math.Max(points2, points3));
			 //En caso de empate, se desempata
			 while (partialPoints[0] == partialPoints[1] && partialPoints[1] == partialPoints[2] ||
				itsHigher == partialPoints[0] && partialPoints[0] == partialPoints[1] ||
				itsHigher == partialPoints[1] && partialPoints[1] == partialPoints[2] ||
				itsHigher == partialPoints[2] && partialPoints[2] == partialPoints[0] )
			 {
				 Console.WriteLine("\n EMPATE. Tiraremos de nuevo: ");
				 Console.Clear();
				 Console.WriteLine("\n b. --> APERTURA: Desempate <-- ");
 				 GoingToBostonWhoStartsDiceRoll();
				 
			}
			 if (itsHigher == points1) pIndex = 0;
			 else if (itsHigher == points2) pIndex = 1;
			 else if (itsHigher == points3) pIndex = 2;
			 return pIndex;
		 }
		 //#1.2.3. Evaluación del valor más chico de los dados
		 public int GoingToBostonLowerDiceValue(int points1, int points2, int points3)
		 {
			 int itsLower = Math.Min(points1, Math.Min(points2, points3));
			 if (itsLower == points1) pIndex = 0;
			 else if (itsLower == points2) pIndex = 1;
			 else if (itsLower == points3) pIndex = 2;
			 return pIndex;
		 }
		 //#1.3. Rondas del juego
		 public void GoingToBostonRounds()
		 {
			 int round = 1;
			 Console.Clear();
			 Console.ReadKey();
			 while (playerPoints[0] < 100
				&& playerPoints[1] < 100
				&& playerPoints[2] < 100)
			 {
				 Console.Clear();
				 Console.Write(title);
				 Console.WriteLine("\n  c. --> RONDAS <--");
				 SetLines("-", 1);
				 Messages(round);
				 Console.Write(" RONDA {0}: {1}", round, message);
				 Console.WriteLine("~ {0}: {1} pts, {2}: {3} pts, {4}: {5} pts",
						   playerName[0], playerPoints[0],
						   playerName[1], playerPoints[1],
						   playerName[2], playerPoints[2]);
				 SetLines("-", 1);
				 for (int i = 0; i < playerName.Length; i++)
				 {
					 int keptDice;
					 //1era tirada (3 dados):
					 for (int j = 0; j < dice.Length; j++) dice[j] = random.Next(1, 7);
					 keptDice = Math.Max(dice[0], Math.Max(dice[1], dice[2]));
					 playerPoints[i] += keptDice;
					 Console.ReadKey();
					 Console.WriteLine(" {0}:\t [{1}] [{2}] [{3}]\t Retiene: {4}",
							   playerName[i], dice[0], dice[1], dice[2], keptDice);
					 //2da tirada (2 dados):
					 for (int k = 0; k < dice.Length - 1; k++) dice[k] = random.Next(1, 7);
					 keptDice = Math.Max(dice[0], dice[1]);
					 playerPoints[i] += keptDice;
					 Console.ReadKey();
					 Console.WriteLine(" {0}:\t [{1}] [{2}] \t Retiene: {3}",
							   playerName[i], dice[0], dice[1], keptDice, playerPoints[i]);
					 //3era tirada (1 dado):
					 dice[0] = random.Next(1, 7);
					 playerPoints[i] += dice[0];
					 Console.ReadKey();
					 Console.WriteLine(" {0}:\t [{1}] \t \t Puntos: {2} pts.",
							   playerName[i], dice[0], (dice[0] + dice[1] + dice[2]));
					 Console.ReadKey();
					 SetLines("-", 1);
				 }
				 Console.ReadKey();
				 if (playerPoints[0] < 100
				     && playerPoints[1] < 100
				     && playerPoints[2] < 100)
					 message = " SUBTOTAL: ";
				 else message = " TOTAL";
				 Console.WriteLine(message + " {0}: {1} pts | {2}: {3} pts | {4}: {5} pts",
						   playerName[0], playerPoints[0],
						   playerName[1], playerPoints[1],
						   playerName[2], playerPoints[2]);
				 SetLines("-", 1);
				 Console.ReadKey();
				 round++;
			 }
		 }
		 //#1.3.1. Mensaje
		 public int Messages(int round)
		 {
			 if (round == 1) MessagesContent(random.Next(1, 3));
			 else if (round == 2) MessagesContent(random.Next(3, 5));
			 else MessagesContent(random.Next(5, 7));
			 return round;
		 }
		 //#1.3.2. Contenido de mensaje
		 public int MessagesContent(int randomMessage)
		 {
			 message = "";
			 switch (randomMessage)
			 {
				 case 1:
					 message = " >>> ¿Están listos? ¡Que comience el juego! <<<";
					 break;
				 case 2:
					 message = " >>> ¡A rodar los dados! <<<";
					 break;
				 case 3:
					 if (partialPoints[0] - partialPoints[1] - partialPoints[2] < 5)
						 message = " ¡¡Esto está parejo!!";
					 else if (partialPoints[0] - partialPoints[1] - partialPoints[2] >= 5)
						 message = " Hay una pequeña tendencia...";
					 else message = " ... ";
					 break;
				 case 4:
				 case 5:
					 message = "";
					 break;
				 case 6:
					 if (partialPoints[0] - partialPoints[1] - partialPoints[2] > 20)
						 message = " ¡Clarísima tendencia!";
					 else message = playerName[0] + ", " + playerName[1] +
						 ", " + playerName[2] + " vienen en tenaz lucha";
					 break;
			 }
			 return randomMessage;
		 }
		 //#1.4. Final del juego
		 public void EndGame()
		 {
			int itsHigher = Math.Max(playerPoints[0], Math.Max(playerPoints[1], playerPoints[2]));
 			bool itsEqual;
 			if (itsEqual = Math.Equals(playerPoints[0],Math.Equals(playerPoints[1], playerPoints[2])))
				Console.WriteLine("Triple empate de {0}, {1}, y {2}",
									 playerName[0], playerName[1], playerName[2]);
 			else if (itsEqual = Math.Equals(playerPoints[0], playerPoints[1]) &&
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
				SetLines("*", 2);
				Console.WriteLine(" --> FINAL <--" +
					"\n  ¡¡¡HA GANADO {0}!!! <--", playerName[pIndex].ToUpper());
				SetLines("*", 2);
			}
		 }
		 //#2. Reglas
		 public void RulesOfGoingToBoston()
		 {
			 Console.Clear();
			 Console.WriteLine(title);
			 Console.WriteLine("\n --- Reglas del Going To Boston  ---" +
					   "\n NOMBRES ALTERNATIVOS: Boston (habla hispana) y Going To Town." +
					   "\n JUGADORES: 2 o más." +
					   "\n MATERIALES: 3 dados." +
					   "\n OBJETIVO: Gana quien alcanza primero los 100 puntos." +
					   "\n PARTIDA: " +
					   "\n 1. Preparación: Cada jugador tira un dado: el de mayor puntuación abre la partida." +
					   "\n 2. Desarrollo: Por TURNOS, cada jugador lanzará 3 VECES: " +
					   "\n 2.1. En la 1era tirada lanzará 3 dados y apartará el del mayor valor." +
					   "\n 2.2. En la 2da tirada lanzará 2 dados, y apartará el de mayor valor." +
					   "\n 2.3. En la 3era tirada lanzará el dado restante y lo apartará. " +
					   "\n La SUMA de esos 3 dados apartados serán los puntos ganados por el jugador en dicha ronda." +
					   "\n El jugador que totalice 100 puntos o más gana la partida."
					  );
			 Console.WriteLine("\n Presione cualquier tecla para volver al menú");
			 Console.ReadKey();
			 Console.Clear();
		 }
		 //#3. Exit game
		 public void ExitGame()
		 {
			 if (option != "3")
			 {
				 Console.WriteLine("\n ¿Quiere salir del programa? S/N");
				 string confirmation = Console.ReadLine().ToUpper();
				 if (confirmation != "N") game = false;
				 else Console.Clear();
			 }
			 else if (option == "3") game = false;
		 }
	 }
 }
