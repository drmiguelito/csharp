using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleCardGameWar
{
	internal class Card
	{
		//Atributos: variables
		private string name1, name2;
		private int value1, value2;
		private string option, cardA, cardB;
		public bool game = true;
		public int i, j;

		//Atributos: arrays
		private string[] defaultPlayersNames =
		{
			"Carolina", "Marina", "Cecilia","Jessica",
			"Charly", "Robert", "Jack", "Jason"
		};
		private string[] playerName = new string[2];
		private string[] cardDeck =
		{
			"cA","cK","cQ", "cJ", "c10", "c9",
			"hA", "hK","hQ", "hJ", "h10", "h9",
			"dA", "dK","dQ", "dJ", "d10", "d9",
			"eA","eK","eQ", "eJ", "e10", "e9"
		};
		public List<string> upCards = new List<string>();
		
		//Atributos: listas
		public List<string> playerCards1, playerCards2;

		//Atributos: objetos
		Random random = new Random();

		//Propiedades:
		public string Name1 { get; set; }
		public string Name2 { get; set; }
		public string[] PlayersNames
		{
			get =>PlayersNames;
			set { PlayersNames = value; }
		}
		public string[] DefaultPlayersNames
		{
			get => defaultPlayersNames;
			set { defaultPlayersNames = value; }
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
		public string[] CardDeck
		{
			get => cardDeck;
			set { cardDeck = value; }
		}
		public int Value1 { get; set; }
		public int Value2 { get; set; }
		public string CardPlayed { get; set; }
		public string CardA { get; set; }
		public string CardB { get; set; }

		//---Métodos---
		//#3. GamePlay
		public void WarGamePlay()
		{
			//Variables y constantes locales:
			const int TRICKSTOSHOW = 3;
			const int ROUNDSLIMIT = 72;
			game = true;
			i = 0;
			j = 0;
			int trickCounter = 1;

			Console.Clear();
			//Asignación de nombre de usuario
			Console.WriteLine();
			Console.WriteLine(" *** War Card Game ***");
			Console.Write(" Introducí tu nombre o nickname: ");
			PlayerName = Console.ReadLine();
			Name1 = PlayerName;
			Name2 = Game.NameAssignation(DefaultPlayersNames);
			Console.WriteLine($" Tu oponente será: {Name2}");
			Console.WriteLine();
			Game.SetLines("*", 1);
			Console.WriteLine($" Partida: {Name1} (tú) vs. {Name2}");
			Game.SetLines("*", 1);
			Console.ReadKey();

			//Mezcla y repartir las cartas:
			DealingCards(ShuffleCardDeck(CardDeck));

			//Juego
			while (game)
			{
				Console.Clear();
                Console.WriteLine();
                Console.WriteLine(" *** War Cards Game ***");
				for (int n = 0; n < TRICKSTOSHOW; n++)
				{
					if (trickCounter > ROUNDSLIMIT
						|| playerCards1.Count <= 0
						|| playerCards2.Count <= 0)
					{
						game = false;
						break;
					}
					Game.SetLines("-", 1);
					Console.WriteLine(" Baza " + trickCounter);
					Game.SetLines("-", 1);

					//Baza
					TrickWinner();
					Console.WriteLine(" {0}: {1} cartas - {2}: {3} cartas ",
						Name1, playerCards1.Count, Name2, playerCards2.Count);
					Console.ReadKey();
					trickCounter++;
				}
			}
			EndGame();
			Game.Exit(false);
		}
		//#3.1. Mezclar mazo de cartas (en array)
		public string[] ShuffleCardDeck(string[] CardDeck) =>
			CardDeck.OrderBy(x => random.Next()).ToArray();

		//#3.2. Repartir cartas (en listas)
		public void DealingCards(string[] mixedCardeck)
		{
			playerCards1 = mixedCardeck.Take(mixedCardeck.Length / 2).ToList();
			playerCards2 = mixedCardeck.Skip(mixedCardeck.Length / 2).ToList();
		}
		 //#3.4. Establecer ganador de la baza
		public void TrickWinner()
		{
			if (playerCards1.Count == 0 || playerCards2.Count == 0)
			{
                Console.WriteLine(" EMPATE pero no alcanzan las cartas");
                game = false;
				return;
			}
			//Baza en sí
			CardA = playerCards1[i];
			CardB = playerCards2[j];
			Value1 = Game.CardValue(CardA);
			Value2 = Game.CardValue(CardB);

			//Levantada de cartas de baza
			TrickLiftCards();

			if (Value1 > Value2) //Si gana jugador A
			{
				playerCards1.AddRange(upCards);
				CardA = CardA + "] ~ es mayor";
				CardB = CardB + "]";
			}
			else if (Value1 < Value2) //Si gana jugador B
			{
				playerCards2.AddRange(upCards);
				CardA = CardA + "]";
				CardB = CardB + "] ~ es mayor";
			}
			else if (Value1 == Value2) //Si es empate
			{
				TieBreaker();
				if (Value1 > Value2)
				{
					CardA = CardA + "~ es mayor";
					playerCards1.AddRange(upCards);
				}
				else if (Value1 < Value2)
				{
					CardB = CardB + "~ es mayor";
					playerCards2.AddRange(upCards);
				}
				else
				{
					while (Value1 == Value2)
					{
						TieBreaker();
						if (playerCards1.Count == 0 || playerCards2.Count == 0)
						{
							Console.WriteLine(" Un jugador se ha quedado sin cartas... ");
							if(playerCards1.Count > playerCards2.Count)
							{
								Console.WriteLine(" Ha ganado el empate P1 por falta de cartas de P2");
								playerCards1.AddRange(upCards);
								cardA = cardA + "--V";
								cardB = cardB + "--X";
							}
							else
							{
								Console.WriteLine(" Ha ganado el empate P2 por falta de cartas de P1");
								playerCards2.AddRange(upCards);
								cardA = cardA + "--X";
								cardB = cardB + "--V";
							}
							game = false;
							break;
						}
					}
					if(Value1 > Value2) playerCards1.AddRange(upCards);
					else playerCards2.AddRange(upCards);
				}
            }
			Console.WriteLine(" P1: [" + CardA);
			Console.WriteLine(" P2: [" + CardB);
			upCards.Clear();//limpia la carga
		}
		public void TieBreaker()
		{
			if (playerCards1.Count == 0 || playerCards2.Count == 0)
			{
				game = false;
				return;
			}
			//Carta del medio
			CardA = CardA + "] (empate)" + " [" + playerCards1[i] + "]";
			CardB = CardB + "] (empate)" + " [" + playerCards2[j] + "]";

			//Carga
			if (playerCards1.Count == 0 || playerCards2.Count == 0)
			{
				game = false;
				return;
			}
			TrickLiftCards();

			//Carta de decisión
			if (playerCards1.Count == 0 || playerCards2.Count == 0)
			{
				Console.WriteLine(" Un jugador se ha quedado sin cartas... ");
				game = false;
				return;
			}

			CardA = CardA + " --> " + " [" + playerCards1[i] + "]";
			CardB = CardB + " --> " + " [" + playerCards2[j] + "]";

			Value1 = Game.CardValue(playerCards1[i]);
			Value2 = Game.CardValue(playerCards2[j]);

			TrickLiftCards();
		}
		//Carga de cartas de baza
		public void TrickLiftCards()
		{
			upCards.Add(playerCards1[i]);//5
			upCards.Add(playerCards2[j]);//6
			playerCards1.RemoveAt(i);
			playerCards2.RemoveAt(j);
		}
		public void EndGame()
		{
			string endTitle = " ***--> FINAL <--***";
			string winnersName ="";
			string endMessage = "";
			string totalCards = "";

			if (playerCards1.Count != playerCards2.Count)
			{
				if (playerCards1.Count > playerCards2.Count)
				{
					winnersName = Name1;
					totalCards = playerCards1.Count.ToString();
				}
				else
				{
					winnersName = Name2;
					totalCards = playerCards2.Count.ToString();
				}
				endMessage = " ***** ¡¡¡HA GANADO " + winnersName.ToUpper() + "!!!***** ";
				totalCards = "Total de cartas: " + totalCards;
			}
			else //si playerCards1 == playerCards2 ...
			{
				endMessage = " |||| EMPATE entre " + Name1 + " y " + Name2 + "|||| ";
				totalCards = "";
			}
			//Pantalla final
			Console.Clear();
			Console.WriteLine();
			Console.SetCursorPosition((Console.WindowWidth - endMessage.Length) / 2,
				Console.CursorTop);
			Console.WriteLine();
			Game.SetLines("*", 2);

			//Alineamiento
			Console.SetCursorPosition((Console.WindowWidth - endTitle.Length) / 2, Console.CursorTop);
			Console.WriteLine(endTitle);
			Console.SetCursorPosition((Console.WindowWidth - endMessage.Length) / 2, Console.CursorTop);
			Console.WriteLine(endMessage);
			Console.SetCursorPosition((Console.WindowWidth - totalCards.Length) / 2, Console.CursorTop);
			Console.WriteLine(totalCards);
			Game.SetLines("*", 2);
		}
	}
}