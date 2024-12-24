using System;
using System.Data.SqlClient;
using Globals;

namespace ConsoleGongolitosMovies
{
	internal class MoviesDatabase : Movie
	{
		//Atributos
		private string connectionString = string.Format(
				"Server=server1;" +
				"Database=movies;" +
				"User=root;" +
				"Password=root;"
				);
		//---Métodos---
		//--Conexión a base de datos
		public void ConnectDB(string query, string operation)
		{
			GlobalVariablesAndConstants.showError = true; //Setear mostrar error
			SqlConnection connection = new SqlConnection(connectionString);
			try
			{
				connection.Open();
				SqlCommand cmd = new SqlCommand(query, connection); //<--QUERY
				if (operation == "read") ReadList(cmd);
				else if (operation == "create") Create(cmd);
				else if (operation == "update") UpdateById(connection, cmd);
				else if (operation == "delete") Delete(connection, cmd);
				else SearchBy(cmd, operation);
			}
			catch (Exception ex)
			{
				if(GlobalVariablesAndConstants.showError) Console.WriteLine(" Error: " + ex.Message);
			}
			finally
			{
				connection.Close();
				if (operation != "read" && operation != "create"
					&& operation != "update" && operation != "delete")
				{
					Console.WriteLine();
					Console.Write(" Presione una tecla para continuar... ");
					Console.ReadKey();
				}
				else 
				{
                    Console.Write(" Presione enter para continuar... ");
					string enter;
					do
					{
						enter = Console.ReadLine();
                    } while (enter != string.Empty);
                }  
			}
		}
		//---Submétodos de ConnectDB(): Create(), Delete(), Read(), SearchBy()
		public void Create(SqlCommand cmd)
		{
			Form(); //heredado de Movies
			cmd.Parameters.AddWithValue("@Title", Title);
			cmd.Parameters.AddWithValue("@Year", Year);
			cmd.Parameters.AddWithValue("@Rate", Rate);
			cmd.Parameters.AddWithValue("@Country", Country);
			cmd.Parameters.AddWithValue("@Genre", Genre);
			cmd.Parameters.AddWithValue("@Type", Type);
			cmd.Parameters.AddWithValue("@Feature", Feature);
			cmd.Parameters.AddWithValue("@Director", Director);
			cmd.Parameters.AddWithValue("@Synopsis", Synopsis);
			cmd.Parameters.AddWithValue("@Observations", Observations);
			cmd.ExecuteNonQuery();
			Console.WriteLine(" ->>> Registro creado exitosamente <<<-");
			Console.WriteLine();
		}
		public void Delete(SqlConnection connection, SqlCommand cmd)
		{
			Console.Write(" Ingrese id: ");
			Id = Int32.Parse(Console.ReadLine());
			cmd.Parameters.AddWithValue("@Id", Id);
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				Console.WriteLine(
							$" Título a eliminar: {reader["title"]}" +
							$" ({reader["premier_year"]}," +
							$" {reader["country"]})" +
							$" - {reader["genre"]}" +
							$" [#{reader["id"]}]"
							);
			}
			reader.Close();
			Console.Write(" ¿Seguro de eliminar este título? S/N: ");
			string answer = Console.ReadLine().ToUpper();
			if (answer != "S")
			{
				Console.WriteLine(" Eliminación de registro cancelada");
			}
			else
			{
				string newQuery = "DELETE FROM Titles WHERE id = @Id";
				cmd = new SqlCommand(newQuery, connection);
				cmd.Parameters.AddWithValue("@Id", Id);
				cmd.ExecuteNonQuery();
				Console.WriteLine(" -->> Registro eliminado con éxito <<--");
			}
		}
		public void UpdateById(SqlConnection connection, SqlCommand cmd)
		{
			Console.Write(" Ingrese id: ");
			Id = Int32.Parse(Console.ReadLine());

			cmd.Parameters.AddWithValue("@Id", Id);
			Console.WriteLine();
			Console.WriteLine($" -->> Edición para id  {Id} <<--");
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				Title = $"{reader["title"]}";
				Year = Int32.Parse($"{reader["premier_year"]}");
				StringYear = Year.ToString();
				Country = $"{reader["country"]}";
				Rate = Double.Parse($"{reader["rate"]}");
				StringRate = Rate.ToString();
				Genre = $"{reader["genre"]}";
				Type = $"{reader["typeOf"]}";
				Feature = $"{reader["feature"]}";
				Director = $"{reader["director"]}";
				Synopsis = $"{reader["synopsis"]}";
				Observations = $"{reader["observations"]}";
			}
			reader.Close();

			string newQuery = "UPDATE Titles SET title=@Title, premier_year=@Year, rate=@Rate," +
				" country=@Country, genre=@Genre, typeOf=@Type, Feature=@Feature," +
				" director=@Director, synopsis=@Synopsis, observations=@Observations WHERE Id=@Id";
			cmd = new SqlCommand(newQuery, connection);

			//Formulario:
			Console.Write($" Título ({Title}): ");
			Title = ValueControl(Title, Console.ReadLine());
			Console.Write($" Año ({Year}): ");
			StringYear = ValueControl(StringYear, Console.ReadLine());
			Year = Int32.Parse(StringYear);
			Console.Write($" Puntuación ({Rate}): ");
			StringRate = ValueControl(StringRate, Console.ReadLine());
			Rate = Double.Parse(StringRate);
			Console.Write($" País ({Country}): ");
			Country = ValueControl(Country, Console.ReadLine());
			Console.Write($" Género ({Genre}): ");
			Genre = ValueControl(Genre, Console.ReadLine());
			Console.Write($" Tipo ({Type}): ");
			Type = ValueControl(Type, Console.ReadLine());
			Console.Write($" Feature ({Feature}): ");
			Feature = ValueControl(Feature, Console.ReadLine());
			Console.Write($" Director ({Director}): ");
			Director = ValueControl(Director, Console.ReadLine());
			Console.Write($" Sinopsis ({Synopsis}): ");
			Synopsis = ValueControl(Synopsis, Console.ReadLine());
			Console.Write($" Observaciones ({Observations}): ");
			Observations = ValueControl(Observations, Console.ReadLine());

			cmd.Parameters.AddWithValue("@Id", Id);
			cmd.Parameters.AddWithValue("@Title", Title);
			cmd.Parameters.AddWithValue("@Year", Year);
			cmd.Parameters.AddWithValue("@Rate", Rate);
			cmd.Parameters.AddWithValue("@Country", Country);
			cmd.Parameters.AddWithValue("@Genre", Genre);
			cmd.Parameters.AddWithValue("@Type", Type);
			cmd.Parameters.AddWithValue("@Feature", Feature);
			cmd.Parameters.AddWithValue("@Director", Director);
			cmd.Parameters.AddWithValue("@Synopsis", Synopsis);
			cmd.Parameters.AddWithValue("@Observations", Observations);
			cmd.ExecuteNonQuery();
			Console.WriteLine(" ->>> Cambios guardados exitosamente <<<-");
			Console.WriteLine();
		}
		public void ReadList (SqlCommand cmd)
		{
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					Console.WriteLine(
						$" [#{reader["id"]}]" +
						$" {reader["title"]}" +
						$" ({reader["premier_year"]}," +
						$" {reader["country"]}" +
						$" - {reader["genre"]})" +
						$". {reader["rate"]}pts" 
						);
				}
				reader.Close();
			Console.WriteLine();
		}
		public void SearchBy(SqlCommand cmd, string operation)
		{
			switch (operation)
			{
				case "search by id":
					SearchById(cmd);
					break;
				case "search by title":
					SearchByTitle(cmd);
					break;
				case "search by genre":
					SearchByGenre(cmd);
					break;
				case "search by type":
					SearchByType(cmd);
					break;
				case "search by feature":
					SearchByFeature(cmd);
					break;
				case "search by director":
					SearchByDirector(cmd);
					break;
				case "search by synopsis":
					SearchBySynopsis(cmd);
					break;
				case "search by observations":
					SearchByObservations(cmd);
					break;				
			}
		}
		//-- Submétodos SearchBy--
		public void SearchById(SqlCommand cmd)
		{
			Console.WriteLine();
			Console.WriteLine(" -->> Búsqueda por id <<--");
			Console.Write(" Ingrese id: ");
			Id = Int32.Parse(Console.ReadLine());
			cmd.Parameters.AddWithValue("@Id", Id);
			Console.WriteLine();
			Console.WriteLine(" ->>> Coincidencias de búsqueda <<<-");
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				Console.WriteLine(
					$" Título: {reader["title"]} " +
					$"({reader["premier_year"]}," +
					$" {reader["country"]})" +
					$" [id: #{reader["id"]}]\n" +
					$" Género: {reader["genre"]}\n" +
					$" Tipo: {reader["typeOf"]}\n" +
					$" Feature: {reader["feature"]} \n" +
					$" Director: {reader["director"]}\n" +
					$" Sinopsis: {reader["synopsis"]}\n" +
					$" Observaciones: {reader["observations"]}\n" +
					$" Puntuación: {reader["rate"]} de 5 puntos\n"
					);
			}
			reader.Close();
		}
		public void SearchByTitle(SqlCommand cmd)
		{
			Console.WriteLine();
			Console.WriteLine(" -->> Búsqueda por título <<--");
			Console.Write(" Ingrese título de película o serie: ");
			Title = Console.ReadLine();
			cmd.Parameters.AddWithValue("@Title", "%" + Title + "%");
			Console.WriteLine();
			Console.WriteLine(" ->>> Coincidencias de búsqueda <<<-");
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				Console.WriteLine(
					$" {reader["title"]}" +
					$" ({reader["premier_year"]}," +
					$" {reader["country"]}" +
					$" - {reader["genre"]})" +
					$". {reader["rate"]}pts" +
					$" [#id: {reader["id"]}]"
					);
			}
			if(!reader.HasRows) Console.WriteLine(" Ups... no hay resultados para la búsqueda");
            reader.Close();
		}
		public void SearchByGenre(SqlCommand cmd)
		{
			Console.WriteLine();
			Console.WriteLine(" -->> Búsqueda por género <<--");
			Console.Write(" Ingrese género: ");
			Genre = Console.ReadLine();
			cmd.Parameters.AddWithValue("@Genre", "%" + Genre + "%");
			Console.WriteLine();
			Console.WriteLine(" ->>> Coincidencias de búsqueda <<<-");
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				Console.WriteLine(
					$" *{reader["genre"]} - " +
					$" {reader["title"]}" +
					$" ({reader["premier_year"]}," +
					$" {reader["country"]}" +
					$" - {reader["typeOf"]})" +
					$". {reader["rate"]}pts" +
					$" [#id: {reader["id"]}]"
					);
			}
			if (!reader.HasRows) Console.WriteLine(" Ups... no hay resultados para la búsqueda");
			reader.Close();
		}
		public void SearchByType(SqlCommand cmd)
		{
			Console.WriteLine();
			Console.WriteLine(" -->> Búsqueda por tipo <<--");
			Console.Write(" Ingrese tipo: ");
			Type = Console.ReadLine();
			cmd.Parameters.AddWithValue("@Type", "%" + Type + "%");
			Console.WriteLine();
			Console.WriteLine(" ->>> Coincidencias de búsqueda <<<-");
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				Console.WriteLine(
					$" Tipo: {reader["typeOf"]} - " +
					$" {reader["title"]}" +
					$" ({reader["premier_year"]}," +
					$" {reader["country"]}" +
					$" - {reader["genre"]})" +
					$". {reader["rate"]}pts" +
					$" [#id: {reader["id"]}]"
					);
			}
			if (!reader.HasRows) Console.WriteLine(" Ups... no hay resultados para la búsqueda");
			reader.Close();
		}
		public void SearchByFeature(SqlCommand cmd)
		{
			Console.WriteLine();
			Console.WriteLine(" -->> Búsqueda por actores principales <<--");
			Console.Write(" Ingrese nombre del actor / actríz de la película o serie: ");
			Feature = Console.ReadLine();
			cmd.Parameters.AddWithValue("@Feature", "%" + Feature + "%");
			Console.WriteLine();
			Console.WriteLine(" ->>> Coincidencias de búsqueda <<<-");
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				Console.WriteLine(
					$" {reader["feature"]} (rol protagónico) - " +
					$" {reader["title"]}" +
					$" ({reader["premier_year"]}," +
					$" {reader["country"]}" +
					$" - {reader["genre"]})" +
					$". {reader["rate"]}pts" +
					$" [#id: {reader["id"]}]"
					);
			}
			if (!reader.HasRows) Console.WriteLine(" Ups... no hay resultados para la búsqueda");
			reader.Close();
		}
		public void SearchByDirector(SqlCommand cmd)
		{
			Console.WriteLine();
			Console.WriteLine(" -->> Búsqueda por director <<--");
			Console.Write(" Ingrese nombre del director de la película o serie: ");
			Director = Console.ReadLine();
			cmd.Parameters.AddWithValue("@Director", "%" + Director + "%");
			Console.WriteLine();
			Console.WriteLine(" ->>> Coincidencias de búsqueda <<<-");
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				Console.WriteLine(
					$" {reader["director"]} - " +
					$" {reader["title"]}" +
					$" ({reader["premier_year"]}," +
					$" {reader["country"]}" +
					$" - {reader["genre"]})" +
					$". {reader["rate"]}pts" +
					$" [#id: {reader["id"]}]"
					);
			}
			if (!reader.HasRows) Console.WriteLine(" Ups... no hay resultados para la búsqueda");
			reader.Close();
		}
		public void SearchBySynopsis(SqlCommand cmd)
		{
			Console.WriteLine();
			Console.WriteLine(" -->> Búsqueda por sinopsis <<--");
			Console.Write(" Ingrese palabra clave de sinopsis: ");
			Synopsis = Console.ReadLine();
			cmd.Parameters.AddWithValue("@Synopsis", "%" + Synopsis + "%");
			Console.WriteLine();
			Console.WriteLine(" ->>> Coincidencias de búsqueda <<<-");
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				Console.WriteLine(
					$" [#id: {reader["id"]}]" +
					$" {reader["title"]}" +
					$" ({reader["premier_year"]}," +
					$" {reader["country"]}" +
					$" - {reader["genre"]})" +
					$" [Sinopsis: {reader["synopsis"]}]"
					);
			}
			if (!reader.HasRows) Console.WriteLine(" Ups... no hay resultados para la búsqueda");
			reader.Close();
		}
		public void SearchByObservations(SqlCommand cmd)
		{
			Console.WriteLine();
			Console.WriteLine(" -->> Búsqueda por observaciones <<--");
			Console.Write(" Ingrese palabra clave de observación: ");
			Observations = Console.ReadLine();
			cmd.Parameters.AddWithValue("@Observations", "%" + Observations + "%");
			Console.WriteLine();
			Console.WriteLine(" ->>> Coincidencias de búsqueda <<<-");
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				Console.WriteLine(
					$" [#id: {reader["id"]}]" +
					$" {reader["title"]}" +
					$" ({reader["premier_year"]}," +
					$" {reader["country"]}" +
					$" - {reader["genre"]})" +
					$" [Sinopsis: {reader["synopsis"]}]"
					);
			}
			if (!reader.HasRows) Console.WriteLine(" Ups... no hay resultados para la búsqueda");
			reader.Close();
		}
	}
}
