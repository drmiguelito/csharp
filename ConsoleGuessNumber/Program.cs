using System;

namespace ConsoleGuessNumber
{
	internal class Program
	{
		static void Main(string[] args)
		{
			bool game = true;

			while(game == true)
			{
				Console.WriteLine("~~~+++***CONSOLE GUESS NUMBER***+++~~~");
				Console.WriteLine("-----Adivina el número aleatorio entre 1 y 100----");
				Console.WriteLine("Por favor, ingrese un número entre 1 y 100...");
				//Objeto necesario para aleatorizar
				Random number = new Random();
				//Obtención de un número entre 0 y 100
				int guessNumber = number.Next(0, 100);
				//variable respuesta
				int answer;
				//Contador de intentos
				int i = 0;
				do
				{
					try
					{
						answer = Int32.Parse(Console.ReadLine());
						if( answer > 100 || answer < 1 ) 
						{
                            Console.WriteLine("Por favor, ingrese un número entre 1 y 100");
							answer = Int32.Parse(Console.ReadLine());
						}
                    }
					catch (FormatException error) //Formato erróneo
					{
						Console.WriteLine("Por favor, ingrese solamente números...");
						answer = Int32.Parse(Console.ReadLine());
					}
					catch (OverflowException error) //Número no contemplado en int
					{
						Console.WriteLine("Por favor, ingrese un número válido...");
						answer = Int32.Parse(Console.ReadLine());
					}
					if (answer < guessNumber)
					{
						Console.WriteLine("El número es mayor...");
					}
					else if (answer > guessNumber)
					{
						Console.WriteLine("El número es menor...");
					}
					i++;
					Console.WriteLine("(Intento " + i + ")");
				} while (answer != guessNumber);
				Console.WriteLine("¡Felicitaciones! El número a adivinar es el " + guessNumber + ".");
				Console.Write("Número de intentos: " + i);
				if(i == 1)
				{
                    Console.WriteLine(" ¡Acertaste de una! ¡Todo un récord!");
                } 
				else if(i < 6)
				{
					Console.WriteLine(" ¡Eso estuvo genial!");
				}
				else if(i > 5 && i < 11)
				{
					Console.WriteLine(" ¡Eso estuvo muy bien!");
				}
				else
				{
					Console.WriteLine(" Costó un poquito ¡La próxima será mejor!");
				}
                Console.WriteLine("¿Quiere volver a jugar? Escriba Y (yes) para continuar " +
					"y cualquier otra letra para salir del juego");
				string continueGame = Console.ReadLine().ToUpper();
				if(continueGame != "Y")
				{
					game = false;
                    			Console.WriteLine("¡Gracias por jugar! Hasta la próxima...");
                		}
            		}
		}
	}
}
