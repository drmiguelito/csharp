using System;
namespace ConsoleCardGameWar
{
	/*
	 */
	internal class Program
	{
		static void Main(string[] args)
		{
			Game.ScreenSet(
				"WAR CARD GAME", 
				ConsoleColor.DarkRed, 
				ConsoleColor.Gray
				);
			while (Game.start)
			{
				Game.Menu();
			}
			Console.WriteLine(" Nos vemos en la proxima!");
		}
	}
}
