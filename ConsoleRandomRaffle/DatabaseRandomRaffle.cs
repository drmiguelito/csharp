using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Globals;

namespace ConsoleRandomRaffle
{
	internal class DatabaseRandomRaffle 
	{
		//Objetos
		Random random = new Random();

		//AtributosL variables
		private string nameInput, taskInput;
		public int rowsNumber;

		//Atributos: listas
		List<string> tasksList = new List<string>();
		List<string> namesList = new List<string>();

		//Propiedades
		public string NameInput
		{
			get => nameInput;
			set => nameInput = value;
		}
		public string TaskInput
		{
			get => taskInput;
			set => taskInput = value;
		}
		public void ConnectDB(string query, string operation)
		{
            string connectionString = string.Format(
				"Server=Server1;" +
				"Database=Raffle;" +
				"User=root;" +
				"Password=root;"
				);
			SqlConnection connection = new SqlConnection(connectionString);
			SqlCommand cmd = new SqlCommand(query, connection); //<--QUERY

			try
			{
                connection.Open();
				if (operation == "show names") ReadNames(cmd, true);
				else if (operation == "show tasks") ReadTasks(cmd, true);
				else if (operation == "add name") AddName(cmd);
				else if (operation == "add task") AddTask(cmd);
				else if (operation == "edit or delete name") EditOrDeleteNames(cmd, connection);
				else if (operation == "edit or delete task") EditOrDeleteTasks(cmd, connection);
				else if (operation == "load names") ReadNames(cmd, false);
				else if (operation == "load tasks") ReadTasks(cmd, false);
				else Console.WriteLine(" Opción no encontrada");
            }
			catch (Exception ex)
			{
				Console.WriteLine(" Error: " + ex.Message);
				Console.ReadKey();
			}
			finally
			{
				connection.Close();
			}
		}
		public void ReadNames(SqlCommand cmd, bool showOnScreen)
		{
			SqlDataReader reader = cmd.ExecuteReader();
			int counter = 0;
			namesList.Clear();
			while (reader.Read())
			{
				namesList.Add($"{reader["name"]}");
				if(showOnScreen) 
					Console.WriteLine("#" + (counter + 1) + ". " + namesList[counter]);
				counter++;
			}
			if (!reader.HasRows) Console.WriteLine(" (i): Aún no hay nombres cargados");
			reader.Close();
			rowsNumber = counter; //Queda seteado la cantidad de filas
		}
		public void ReadTasks(SqlCommand cmd, bool showOnScreen)
		{
			SqlDataReader reader = cmd.ExecuteReader();
			int counter = 0;
			tasksList.Clear();
			while (reader.Read())
			{
				tasksList.Add($"{reader["task"]}");
				if(showOnScreen)
					Console.WriteLine("#" + (counter + 1) + ". " + tasksList[counter]);
				counter++;
			}
			if (!reader.HasRows) Console.WriteLine(" (i): Aún no hay tareas cargadas");
			reader.Close();
			rowsNumber = counter; //Queda seteado la cantidad de filas
		}
		public void AddName(SqlCommand cmd)
		{
			Console.Clear();
			Console.WriteLine(" ---> Ingrese nombre <--- | Salir: escriba exit");
			Console.WriteLine();

			int counter = 0;
			string plural = "";

			while (NameInput != "exit")
			{
				Console.Write($" Ingrese nombre {counter + rowsNumber}: ");
				NameInput = Console.ReadLine();
				if (NameInput == "exit") break;
				cmd.Parameters.AddWithValue("@NameInput", NameInput);
				cmd.ExecuteNonQuery();
				cmd.Parameters.Clear();
				counter++;
			}
			if (counter == 0) return;
			if (counter > 1) plural = "s";
			Console.WriteLine($" >> Nombre{plural} registrado{plural} con éxito <<");
			Console.ReadKey();
		}
		public void AddTask(SqlCommand cmd)
		{
			Console.Clear();
			Console.WriteLine(" ---> Ingrese tarea <--- | Salir: escriba exit");
			Console.WriteLine();

			int counter = 0;
			string plural = "";
			while (TaskInput != "exit")
			{
				Console.Write($" Ingrese tarea {counter + 1 + tasksList.Count}: ");
				TaskInput = Console.ReadLine();
				if (TaskInput == "exit") break;
				cmd.Parameters.AddWithValue("@TaskInput", TaskInput);
				cmd.ExecuteNonQuery();
				cmd.Parameters.Clear();
				counter++;
			}
			if (counter == 0) return;
			if (counter > 1) plural = "s";
			Console.WriteLine($" >> Tarea{plural} registrada{plural} con éxito <<");
			Console.ReadKey();
		}
		public void EditOrDeleteNames(SqlCommand cmd, SqlConnection connection)
		{
			Console.Clear();
			Console.WriteLine(" *** Random Ruffle ***");
            Console.WriteLine();
            Console.WriteLine(" >> Editar o eliminar nombres | Eliminar: escriba delete | Salir: escriba exit");
			Console.WriteLine();

			int id;
			string nameEdit;
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				id = Int32.Parse($"{reader["id"]}");
				NameInput = $"{reader["name"]}";
				Console.Write($" Nombre '{NameInput}': ");
				nameEdit = Console.ReadLine();
				if (nameEdit == "exit")
				{
					break;
				}
				else if (nameEdit == "delete")
				{
					reader.Close();
					string newQuery = "DELETE FROM names WHERE id = @id";
					cmd = new SqlCommand(newQuery, connection);
					cmd.Parameters.AddWithValue("@Id", id);
					cmd.ExecuteNonQuery();
					Console.WriteLine(" -->> Nombre eliminado con éxito <<--");
					newQuery = "SELECT * FROM names WHERE id > @Id";
					cmd = new SqlCommand(newQuery, connection);
					cmd.Parameters.AddWithValue("@Id", id);
					reader = cmd.ExecuteReader();
					Console.ReadKey();
				}
				else if (nameEdit != NameInput
					&& nameEdit != "delete"
					&& nameEdit != "")
				{
					reader.Close();
					NameInput = nameEdit;
					string newQuery = "UPDATE names SET name=@NameInput WHERE id = @id";
					cmd = new SqlCommand(newQuery, connection);
					cmd.Parameters.AddWithValue("@Id", id);
					cmd.Parameters.AddWithValue("@NameInput", NameInput);
					cmd.ExecuteNonQuery();
					Console.WriteLine(" -->> Nombre modificado con éxito <<--");
					newQuery = "SELECT * FROM names WHERE id > @Id";
					cmd = new SqlCommand(newQuery, connection);
					cmd.Parameters.AddWithValue("@Id", id);
					reader = cmd.ExecuteReader();
					Console.ReadKey();
				}
				cmd.Parameters.Clear();
			}
			reader.Close();
		}
		
		public void EditOrDeleteTasks(SqlCommand cmd, SqlConnection connection)
		{
			Console.Clear();
			Console.WriteLine(" >> Editar o eliminar tareas | Eliminar: escriba delete | Salir: escriba exit");
			Console.WriteLine();
			int id;
			string taskEdit;
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				id = Int32.Parse($"{reader["id"]}");
				TaskInput = $"{reader["task"]}";
				Console.Write($" Tarea '{TaskInput}': ");
				taskEdit = Console.ReadLine();
				if (taskEdit == "exit") break;
				
				else if (taskEdit == "delete")
				{
					reader.Close();
					string newQuery = "DELETE FROM tasks WHERE id = @id";
					cmd = new SqlCommand(newQuery, connection);
					cmd.Parameters.AddWithValue("@Id", id);
					cmd.ExecuteNonQuery();
					Console.WriteLine(" -->> Tarea eliminada con éxito <<--");
					newQuery = "SELECT * FROM tasks WHERE id > @Id";
					cmd = new SqlCommand(newQuery, connection);
					cmd.Parameters.AddWithValue("@Id", id);
					reader = cmd.ExecuteReader();
					Console.ReadKey();
				}
				else if (taskEdit != TaskInput
					&& taskEdit != "delete"
					&& taskEdit != "")
				{
					reader.Close();
					TaskInput = taskEdit;
					string newQuery = "UPDATE tasks SET task=@TaskInput WHERE id = @id";
					cmd = new SqlCommand(newQuery, connection);
					cmd.Parameters.AddWithValue("@Id", id);
					cmd.Parameters.AddWithValue("@TaskInput", TaskInput);
					cmd.ExecuteNonQuery();
					Console.WriteLine(" -->> Tarea modificada con éxito <<--");
					newQuery = "SELECT * FROM tasks WHERE id > @Id";
					cmd = new SqlCommand(newQuery, connection);
					cmd.Parameters.AddWithValue("@Id", id);
					reader = cmd.ExecuteReader();
					Console.ReadKey();
				}
				cmd.Parameters.Clear();
			}
			reader.Close();
		}
		public void RandomPlayer()
		{
			int counter = 0;
			namesList.Clear();
			ConnectDB("SELECT * FROM names;", "load names");
			tasksList.Clear();
			ConnectDB("SELECT * FROM tasks;", "load tasks");

			Console.Clear();
			Console.WriteLine(" *** Random Shuffle ***");
			Console.WriteLine();
			Console.WriteLine($" >> Sorteo # {++GlobalVariablesAndConstants.round} >> ");
            //Tareas
            Console.WriteLine(" >> Tareas disponibles: ");
			foreach (string task in tasksList)
			{
				Console.WriteLine($" #{++counter}. " + task);
			}
            //Nombres
            Console.Write(" >> Nombres disponibles: ");
			string phrase;
			counter = 0;
			foreach(string names in namesList)
			{
				if ((counter + 1) == namesList.Count) phrase = ".";
				else phrase = ", ";
				Console.Write(names + phrase);
				counter++;
			}
            Console.Write("\n >> Seleccione tarea: ");
			int id = GlobalVariablesAndConstants.ValidateInteger(Console.ReadLine()) - 1;
			Console.WriteLine(" >>> Usted seleccionó: [" + tasksList[id] + "]");
            Console.WriteLine();
            Console.ReadKey();
			int randomNumber = random.Next(0, namesList.Count);
			Console.WriteLine(" ... |>|>|>| ... Sorteando persona... |>|>|>| ... ");
			Console.WriteLine();
			Console.ReadKey();
            Console.WriteLine($" >> La persona sorteada para " +
				$"'{tasksList[id]}' es : *** {namesList[randomNumber]} ***");
            Console.Write(" ¿Desea repetir el sorteo? S/N: ");
			string answer = Console.ReadLine().ToUpper();
			if (answer == "S") RandomPlayer();
			else GlobalVariablesAndConstants.round = 0;
        }
		public void RandomPlayerAndTask()
		{
			int counter = 0;
			namesList.Clear();
			ConnectDB("SELECT * FROM names;", "load names");
			tasksList.Clear();
			ConnectDB("SELECT * FROM tasks;", "load tasks");

			Console.Clear();
			Console.WriteLine(" *** Random Shuffle ***");
			Console.WriteLine();
			Console.WriteLine($" >> Sorteo # {++GlobalVariablesAndConstants.round} >> ");
			//Tareas
			Console.WriteLine(" >> Tareas disponibles: ");
			foreach (string task in tasksList)
			{
				Console.WriteLine($" #{++counter}. " + task);
			}
			//Nombres
			Console.Write(" >> Nombres disponibles: ");
			string phrase;
			counter = 0;
			foreach (string names in namesList)
			{
				if ((counter + 1) == namesList.Count) phrase = ".";
				else phrase = ", ";
				Console.Write(names + phrase);
				counter++;
			}
			Console.ReadKey();
			int randomNumber1 = random.Next(0, tasksList.Count);
			Console.WriteLine();
			Console.WriteLine();
			Console.Write(" ... |>|>|>| ... Sorteando tarea ... |>|>|>| ... ");
			Console.ReadLine();
			Console.WriteLine($" La tarea sorteada es: [{tasksList[randomNumber1]}]");
			Console.WriteLine();
			int randomNumber2 = random.Next(0, namesList.Count);
			Console.ReadKey();
			Console.Write(" ... |>|>|>| ... Sorteando persona ... |>|>|>| ... ");
			Console.WriteLine();
			Console.ReadKey();
			Console.WriteLine($" La persona sorteada es: ***  {namesList[randomNumber1]} ***");
			Console.WriteLine();
			Console.WriteLine($" >> {namesList[randomNumber2]} salió sorteado/a para '{tasksList[randomNumber1]}'");
			Console.Write(" ¿Desea repetir el sorteo? S/N: ");
			string answer = Console.ReadLine().ToUpper();
			if (answer == "S") RandomPlayerAndTask();
			else GlobalVariablesAndConstants.round = 0;
		}
	}
}