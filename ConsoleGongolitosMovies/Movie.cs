using System;
using Globals; //namespace propio del programa para variables y constantes globales

namespace ConsoleGongolitosMovies
{
	internal class Movie 
	{
		//Atributos
		private string option;
		private int id, year;
		private double rate;
		private string stringYear, stringRate;
		private string title, country, genre, type, feature, director, synopsis, observations;
		//Propiedades
		public string Option 
		{ 
			get => option;
			set
			{
				option = value;
				while (option != "1" && option != "2" 
					&& option != "3" && option != "4"
					&& option != "5" && option != "6")
				{
					if (option == string.Empty) option = Console.ReadLine();
					Console.Write(" ERROR. Por favor, ingrese opción correcta: ");
					option = Console.ReadLine();
				}
			}
		}
		public int Id 
		{
			get => id; 
			set => id = value; 
		}
		public string Title
		{
			get => title;
			set => title = ValueControl(value);
		}
		public int Year 
		{
			get => year;
			set => year = value;
		}
		public string StringYear
		{
			get => stringYear;
			set => stringYear = NumberValidation(value, Year);
		}
		public double Rate 
		{ 
			get => rate;
			set => rate = value;
		}
		public string StringRate
		{
			get => stringRate;
			set => stringRate = NumberValidation(value, Rate);
		}
		public string Country 
		{ 
			get => country;
			set => country = ValueControl(value);
		}
		public string Genre 
		{
			get => genre;
			set => genre = ValueControl(value);
		}
		public string Type 
		{
			get => type;
			set => type = ValueControl(value);
		}
		public string Feature 
		{ 
			get => feature;
			set => feature = ValueControl(value);
		}
		public string Director 
		{ 
			get => director; 
			set => director = ValueControl(value);
		}
		public string Synopsis 
		{  
			get => synopsis;
			set => synopsis = ValueControl(value);
			
		}
		public string Observations
		{
			get => observations;
			set => observations = ValueControl(value);	
		}
		public void Menu()
		{
			bool programa = true;
			while (programa)
			{
				Console.Clear();
				Console.WriteLine(" *** Gongolitos Movies ***");
				Console.WriteLine(" 1. Listar títulos");
				Console.WriteLine(" 2. Buscar película / serie");
				Console.WriteLine(" 3. Agregar película / serie");
				Console.WriteLine(" 4. Editar película / serie");
				Console.WriteLine(" 5. Borrar película / serie");
				Console.WriteLine(" 6. Salir");
				Console.Write(" Seleccione: ");
				Option = Console.ReadLine();
				Console.WriteLine();

				switch (Option)
				{
					case "1":
						ListTitles();
						break;
					case "2":
						SearchTitle();
						break;
					case "3":
						CreateTitle();
						break;
					case "4":
						EditTitle();
						break;
					case "5":
						DeleteTitle();
						break;
					case "6":
						Console.WriteLine(" ¿Desea salir del programa? S/N");
						string salir = Console.ReadLine().ToUpper();
						if (salir != "N") programa = false;
						break;
                }
			}
			Console.WriteLine("¡Hasta la próxima!");
		}
		//---Sub métodos de Menu()---
		public void ListTitles()
		{
			MoviesDatabase database = new MoviesDatabase();
			string answer, queryChoice = "";
			Console.Clear();
			Console.WriteLine(" *** Gongolitos Movies ***");
			Console.WriteLine(" ---> Listar títulos <---");
			Console.WriteLine(" 1. Por Id");
			Console.WriteLine(" 2. Por año");
			Console.WriteLine(" 3. Por nombre");
			Console.WriteLine(" 4. Por género");
			Console.WriteLine(" 5. Por puntuación");
			Console.WriteLine(" Z. Volver al menú anterior <-|");
			Console.Write(" Seleccione: ");
			answer = Console.ReadLine();
			if (answer != "1" && answer != "2" && answer != "3" && answer != "4" && answer != "5")
				return;
			if (answer == "1") queryChoice = "id";
			else if (answer == "2") queryChoice = "premier_year";
			else if (answer == "3") queryChoice = "title";
			else if (answer == "4") queryChoice = "genre";
			else if (answer == "5") queryChoice = "rate";
			Console.Clear();
			Console.WriteLine($" -->> Listado de títulos ordenados por {queryChoice} <<--");
			database.ConnectDB($"SELECT * FROM Titles ORDER BY {queryChoice}", "read");
		}
		public void SearchTitle()
		{
			MoviesDatabase database = new MoviesDatabase();
			string answer, criteria, operation;
			Console.Clear();
			Console.WriteLine(" *** Gongolitos Movies ***");
			Console.WriteLine(" ---> Buscar títulos <---");
			Console.WriteLine(" 1. Por id");
			Console.WriteLine(" 2. Por título");
			Console.WriteLine(" 3. Por género");
			Console.WriteLine(" 4. Por tipo");
			Console.WriteLine(" 5. Por actor");
            Console.WriteLine(" 6. Por director");
			Console.WriteLine(" 7. Por sinopsis (palabra clave)");
			Console.WriteLine(" 8. Por observación (palabra clave)");
			Console.WriteLine(" Z. Volver al menú anterior <-|");
			Console.Write(" Seleccione: ");
			answer = Console.ReadLine();
			if (answer == "1")
			{
				criteria = "id=@Id";
				operation = "search by id";
			}
			else if (answer == "2")
			{
				criteria = "title LIKE @Title";
				operation = "search by title";
			}	
			else if (answer == "3")
			{
				criteria = "genre LIKE @Genre";
				operation = "search by genre";
			}
			else if (answer == "4")
			{
				criteria = "typeOf LIKE @Type";
				operation = "search by type";
			}	
			else if (answer == "5")
			{
				criteria = "feature LIKE @Feature";
				operation = "search by feature";
			}
			else if (answer == "6")
			{
				criteria = "director LIKE @Director";
				operation = "search by director";
			}
			else if (answer == "7")
			{
				criteria = "synopsis LIKE @Synopsis";
				operation = "search by synopsis";
			}
			else if (answer == "8")
			{
				criteria = "observations LIKE @Observations";
				operation = "search by observations";
			}
			else return;
			
			database.ConnectDB($"SELECT * FROM Titles WHERE {criteria}", operation);
		}
		public void CreateTitle()
		{
			MoviesDatabase database = new MoviesDatabase();
			Console.WriteLine();
			Console.WriteLine(" ---> Agregar título <--- | Salir: escriba exit");
			database.ConnectDB("INSERT INTO Titles (title, premier_year, rate, country, genre," +
				" typeOf, feature, director, synopsis," +
				" observations) VALUES (@Title, @Year, @Rate, @Country," +
				" @Genre, @Type, @Feature, @Director, @Synopsis, @Observations)",
				"create");
		}
		public void EditTitle()
		{
			MoviesDatabase database = new MoviesDatabase();
			Console.WriteLine(" ---> Editar título <--- | Salir: escriba exit");
            database.ConnectDB("SELECT * FROM Titles WHERE id=@Id", "update");
		}
		public void DeleteTitle()
		{
			MoviesDatabase database = new MoviesDatabase();
			Console.WriteLine(" ---> Eliminar título <---");
			database.ConnectDB("SELECT * FROM Titles WHERE id=@Id", "delete");
		}
		public void Form()
		{
			//Formulario:
			Console.Write(" Título: ");
			Title = Console.ReadLine();
			Console.Write(" Año: ");
			StringYear = Console.ReadLine();
			Year = Int32.Parse(StringYear);
			Console.Write(" Puntuación: ");
			StringRate = Console.ReadLine();
			Rate = Double.Parse(StringRate);
			Console.Write(" País: ");
			Country = Console.ReadLine();
			Console.Write(" Género: ");
			Genre = Console.ReadLine();
			Console.Write(" Tipo: ");
			Type = Console.ReadLine();
			Console.Write(" Feature: ");
			Feature = Console.ReadLine();
			Console.Write(" Director: ");
			Director = Console.ReadLine();
			Console.Write(" Sinopsis: ");
			Synopsis = Console.ReadLine();
			Console.Write(" Observaciones (ambientación y época - temática - comentarios - visto con): ");
			Observations = Console.ReadLine();
		}
		//---Validaciones---
		//--ValueControl()- 2 sobrecargas: ValueControl(string), ValueControl(string, string) 
		public string ValueControl(string value)
		{
			string stringValue = value;
			while (stringValue == string.Empty)
			{
				Console.Write(" Campo vacío. Por favor, ingrese un valor: ");
				stringValue = Console.ReadLine();
            }
			GlobalVariablesAndConstants.HandleMethodByInput(value);
			return stringValue;
		}
		public string ValueControl(string value, string readLine)
		{
			string stringInput = readLine;
			GlobalVariablesAndConstants.HandleMethodByInput(readLine);
			if (readLine == string.Empty) stringInput = value;
			return stringInput; 
		}
		/* 
		 * NumberValidation(): 2 sobrecargas: 
		 * NumberValidation(string, int), NumberValidation(string, double)
		 */
		public string NumberValidation (string stringDouble, double previousValue)
		{
			GlobalVariablesAndConstants.HandleMethodByInput(stringDouble);
			if( previousValue.ToString() == string.Empty)
			{
				stringDouble = previousValue.ToString();
            }
			else
			{
				bool isNumeric = Double.TryParse(stringDouble, out rate);
				if (!isNumeric)
				{
					Console.Write(" - ERROR. Por favor, solamente ingrese valores numéricos: ");
					stringDouble = Console.ReadLine();
					isNumeric = Double.TryParse(stringDouble, out rate);
				}
				//Estandarización del punto a coma
				string dot = ".";
				bool containsDot = stringDouble.Contains(dot);
				if (containsDot) stringDouble = stringDouble.Replace(".", ",");
			}
			return stringDouble;
		}
		public string NumberValidation(string stringInt, int previousValue)
		{
			GlobalVariablesAndConstants.HandleMethodByInput(stringInt);
			string stringIntValidated = stringInt;
			if (stringInt == string.Empty)
			{
				if (previousValue.ToString() != "0")
					stringIntValidated = previousValue.ToString();
				else
				{
					while ( stringIntValidated == string.Empty)
					{
						Console.Write(" Campo vacío. Ingrese un valor númérico: ");
						stringIntValidated = Console.ReadLine();
					}
					bool isNumeric = Int32.TryParse(stringIntValidated, out year);
					while (!isNumeric)
					{
						Console.Write(" - ERROR. Por favor, solamente ingrese valores numéricos: ");
						stringIntValidated = Console.ReadLine();
					}
				}				
			}
			return stringIntValidated;
		}
	}
}