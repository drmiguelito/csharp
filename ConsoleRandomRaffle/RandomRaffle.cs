using System;

namespace ConsoleRandomRaffle
{
	internal class RandomRaffle
	{
		//Objetos
		DatabaseRandomRaffle db = new DatabaseRandomRaffle();

		//Atributos
		private string option;

		//Propiedades
		public string Option
		{
			get=> option;
			set 
			{
				option = value;
				while(option != "1" && option != "2" && option != "3")
				{
					Console.Write(" ERROR. Ingrese una opción válida: ");
                    option = Console.ReadLine();
				}
			} 
		}
		//---Métodos---
		public void Menu()
		{
			bool game = true;
			
			while (game)
			{
				Console.Clear();
				Console.WriteLine(" ***Random Ruffle***");
                Console.WriteLine();
                Console.WriteLine(" <Menú principal>");
				Console.WriteLine(" 1. Sortear participantes para una tarea");
				Console.WriteLine(" 2. Sortear participantes para distintas tareas");
				Console.WriteLine(" 3. Opciones y configuración");
				Console.WriteLine(" 4. Salir");
                Console.Write(" Seleccione: ");
                Option = Console.ReadLine();
				switch (Option)
				{
					case "1": db.RandomPlayer();
						break;
					case "2": db.RandomPlayerAndTask();
						break;
					case "3": Config();
						break;
					case "4":
                        Console.WriteLine(" ¿Seguro de salir del programa? S/N");
						string answer = Console.ReadLine().ToUpper();
						if (answer != "N") game = false;
                        break;
				}
			}
            Console.WriteLine(" ¡Nos vemos en la próxima!");
        }
		public void Config()
		{
			Console.Clear();
            Console.WriteLine(" *** Random Raffle ***");
            Console.WriteLine();
            Console.WriteLine(" <Configuración> ");
            Console.WriteLine(" 1. Gestionar nombres");
			Console.WriteLine(" 2. Gestionar motivos");
			Console.WriteLine(" 3. Volver al menú principal");
			string answer = Console.ReadLine();

			switch(answer)
			{
				case "1": HandleNames(); 
					break;
				case "2": HandleTasks(); 
					break;
				case "3": //do nothing
					break;
				default: 
					Console.Clear(); 
					Config(); 
					break;
			}
        }
		//-- Submétodos Config() --
		public void HandleNames()
		{
			Console.Clear();
			Console.WriteLine(" *** Random Raffle ***");
            Console.WriteLine();
            Console.WriteLine(" << Nombres cargados en sistema >>");
			db.ConnectDB("SELECT * FROM names", "show names");
            Console.WriteLine();
            Console.WriteLine(" <Agregar / Editar / Eliminar nombres>");
			Console.WriteLine(" 1. Agregar nombre");
			Console.WriteLine(" 2. Editar / eliminar nombre");
			Console.WriteLine(" 3. Volver al menú anterior");
			Console.Write(" Seleccione: ");
			string answer = Console.ReadLine();
			switch (answer)
			{
				case "1": //Agregar nombres
					db.ConnectDB("INSERT INTO names (name) VALUES (@NameInput);", "add name");
					break;
				case "2": //Editar o eliminar nombres
					db.ConnectDB("SELECT * FROM names;", "edit or delete name");
					break;
				case "3": //Salir
					Console.WriteLine(" Presione una tecla para continuar...");
					break;
				default:
					Console.WriteLine(" Por favor, seleccione una opción válida");
					HandleNames();
					break;
			}
		}
		public void HandleTasks()
		{
			Console.Clear();
			Console.WriteLine(" *** Random Raffle ***");
            Console.WriteLine();  
            Console.WriteLine(" << Tareas cargadas en sistema >>");
			db.ConnectDB("SELECT * FROM tasks", "show tasks");
			Console.WriteLine();
			Console.WriteLine(" >> Agregar / Editar / Eliminar tareas:");
			Console.WriteLine(" 1. Agregar tarea");
			Console.WriteLine(" 2. Editar / eliminar tarea");
			Console.WriteLine(" 3. Volver al menú anterior");
			Console.Write(" Seleccione: ");
			string answer = Console.ReadLine();
			switch (answer)
			{
				case "1": //Agrega tarea
					db.ConnectDB("INSERT INTO tasks (task) VALUES (@TaskInput);", "add task");
					break;
				case "2": //Edita o elimina tarea
					db.ConnectDB("SELECT * FROM tasks;", "edit or delete task");
					break;
				case "3": //Salir
					Console.WriteLine(" Presione una tecla para continuar...");
					break;
				default:
					Console.WriteLine(" Por favor, seleccione una opción válida");
					HandleTasks();
					break;
			}
		}
	}
}
