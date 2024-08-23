using System;

/*
 Práctica 2021 - #6
 Monoclase - multimétodo void - monolibrería
 */

namespace ConsoleRandomDiapper
{
	internal class Program
	{
		public string menu = "Menú principal: " +
			"\n1. Cambiar el pañal" +
			"\n2. Otra opción" +
			"\n3. Salir";
		public string option, sortType, stringNumber, result;
		bool isNumeric;
		public bool raffle = true;
		public int num;
		public string[] stringArray;

		static void Main(string[] args)
		{
			Program diapper  = new Program();
			Console.Title = "RANDOM DIAPPER";
			Console.WriteLine("*** Random Diapper & Dish Washing Raffle ***");

			while (diapper.raffle)
			{
				//Menu y derivación a métodos
                diapper.Menu();

                //Final
                Console.Write("\n¿Desea salir del programa? S/N: ");
                diapper.option = Console.ReadLine().ToUpper();
				if(diapper.option != "N")
				{
                    Console.WriteLine("\n¡Nos vemos la próxima!");
					diapper.raffle = false;
                }
				else
				{
					Console.Clear();
				}
			}
		}
		public void Menu()
		{
            Console.WriteLine(menu);
			Console.Write("Seleccione: ");
			option = Console.ReadLine();

            //Validación
            while (option != "1" && option != "2" 
				&& option != "3" && option != "4")
			{
                Console.WriteLine("Por favor, ingrese una opción válida:");
                Console.WriteLine(menu);
                Console.Write("Seleccione: ");
                option = Console.ReadLine();
            }
			//Opciones
			if (option == "1")
			{
				DiapperRaffle();
			}
			else if (option == "2")
			{
				OtherRaffle();
			}
			else if (option == "3")
			{
				Console.WriteLine("¡Nos vemos la próxima!");
				raffle = false;
			}
		}
		public void DiapperRaffle()
		{
			Console.WriteLine("\n---> PAÑAL <---");
			sortType = "cambiar el pañal";
			NamesArray();
			SortName();
		}
		public void OtherRaffle()
		{
			Console.WriteLine("\n---> OTRO ITEM <---");
			Console.Write("Escriba acción / asunto a ser sorteado (ej. lavar los platos): ");
			sortType = Console.ReadLine().ToLower();
			Console.WriteLine("Sorteo de {0}", sortType);
			NamesArray();
			SortName();
        }
		public void NamesArray()
		{
			//Cantidad
			Console.Write("Ingrese NUMERO de participantes: ");
			stringNumber = Console.ReadLine();
			NumberValidation();
			stringArray = new string[num];

			//Nombres
			Console.WriteLine("Son {0} participantes. Ingrese nombres: ", num);			
			for(int i = 0; i < stringArray.Length; i++)
			{
                Console.Write("#{0}: ", i + 1);
                stringArray[i] = Console.ReadLine();
				while (stringArray[i] == "")
				{
                    Console.WriteLine("Campo vacío. Ingrese nombre: ");
					stringArray[i] = Console.ReadLine();
				}
			}
		}
		public void SortName()
		{
			Random randomName = new Random();
			num = randomName.Next(0, stringArray.Length);
			result = stringArray[num].ToUpper();
            Console.WriteLine("Salio sorteado/a para {0}: {1}", sortType, result);
        }
		public void NumberValidation()
		{
			isNumeric = Int32.TryParse(stringNumber, out num);
			while (!isNumeric)
			{
				Console.Write("ERROR. Ingrese solamente números enteros: ");
				stringNumber = Console.ReadLine();
				isNumeric = Int32.TryParse(stringNumber, out num);
			}
			num = Int32.Parse(stringNumber);
		}
	}
}