using System;

namespace ConsoleUnitConverter
{
	class Program
	{
		//Práctica 2021
		//Monolibrería - monoclase - multimétodo static void
		static void Main(string[] args)
		{
			//masa, velocidad, distancia, temperatura
			Console.Title = "UNIT CONVERTER";
			bool unitConverter = true;
            Console.WriteLine("***Conversor de unidades***\n");

			//Bucle
			while(unitConverter == true) 
			{
				Console.WriteLine("*Seleccione tipo de conversión:");
				Console.Write("Masa (m), distancia (d), temperatura (t): ");
				string conv = Console.ReadLine().ToUpper();

				//Validación de tipo de conversión
				while (conv != "M" && conv != "D" && conv != "T")
				{
					Console.WriteLine("<Error!!!>: Por favor, tipee inicial de tipo de unidad  deseada: ");
					Console.Write("*Masa (m), distancia (d), temperatura (t): ");
					conv = Console.ReadLine().ToUpper();
				}
				if (conv == "M") MassUnitConverter(); //MASA
				else if (conv == "D") DistanceUnitConverter(); //DISTANCIA
				else if (conv == "T") TemperatureUnitConverter(); //TEMPERATURA
				
				Console.WriteLine("¿Desea realizar otro pasaje? S/N");
				string turnOff = Console.ReadLine().ToUpper();
				if(turnOff != "S")
				{
                    Console.WriteLine("¡Gracias, nos vemos en la próxima!");
					unitConverter = false;
                } 
            }
		}
		public static void MassUnitConverter()
		{
			bool convM = true;
			while (convM == true)
			{
				//Unidad de origen
				Console.WriteLine("\n--> MASA <--");
				Console.WriteLine("*Unidad de ORIGEN:");
				Console.Write("Kg (k), gramos (g), onzas (o), stones (s): ");
				string unitM1 = Console.ReadLine().ToUpper();

				//Validación de unidad de origen
				while (unitM1 != "K" && unitM1 != "G" && unitM1 != "O" && unitM1 != "S")
				{
					Console.WriteLine("<Error>: Unidad no válida. Por favor, escriba correctamente inicial de unidad de origen: ");
					Console.Write("Kg (k), gramos (g), onzas (o), stones (s): ");
					unitM1 = Console.ReadLine().ToUpper();
				}

				//Unidad de destino
				Console.WriteLine("*Unidad de DESTINO: ");
				Console.Write("Kg (k), gramos (g), onzas (o), stones (s): ");
				string unitM2 = Console.ReadLine().ToUpper();

				//Validación de unidad de destino
				while (unitM2 != "K" && unitM2 != "G" && unitM2 != "O" && unitM2 != "S"
					|| unitM1 == unitM2)
				{
					string alert = "";
					if (unitM1 == unitM2)
					{
						alert = ": utilizaste misma unidad de origen que de destino.";
					}
					Console.WriteLine(
						"Error (unidad no válida)" + alert + ". Por favor, escriba correctamente inicial de unidad de DESTINO: ");
					Console.Write("Kg (k), gramos (g), onzas (o), stones (s)");
					unitM2 = Console.ReadLine().ToUpper();
				}

				//Ingreso de valor numérico
				Console.Write("*Ingrese valor numérico: ");
				double num = Double.Parse(Console.ReadLine());
				double result = 0;

				//Equivalencia de tipeo a unidad
				if (unitM1 == "K") unitM1 = "kg";
				if (unitM1 == "G") unitM1 = "g";
				if (unitM1 == "O") unitM1 = "oz";
				if (unitM1 == "S") unitM1 = "st";
				if (unitM2 == "K") unitM2 = "kg";
				if (unitM2 == "G") unitM2 = "g";
				if (unitM2 == "O") unitM2 = "oz";
				if (unitM2 == "S") unitM2 = "st";

				// Kilogramo a...
				if (unitM1 == "kg")
				{
					if (unitM2 == "g") result = num * 1000;
					else if (unitM2 == "oz") result = num * 35.97;
					else if (unitM2 == "st") result = num * 0.1574;
				}
				else if (unitM1 == "g")
				{
					if (unitM2 == "kg") result = num / 1000;
					else if (unitM2 == "oz") result = num * 0.035274;
					else if (unitM2 == "st") result = num * 0.00015747;
				}
				else if (unitM1 == "oz")
				{
					if (unitM2 == "kg") result = num * 0.028349;
					else if (unitM2 == "g") result = num * 28.349;
					else if (unitM2 == "st") result = num * 0.004464;
				}
				else if (unitM1 == "st")
				{
					if (unitM2 == "kg") result = num * 6.35;
					else if (unitM2 == "g") result = num * 6350.29;
					else if (unitM2 == "oz") result = num * 224;
				}
				Console.WriteLine("\n--> Resultado: " + num + " " + unitM1 +
					" a " + unitM2 + " = " + Math.Round(result, 2) + " " + unitM2 + "\n");

				//Final
				Console.WriteLine("¿Desea realizar un nuevo pasaje de MASA? S/N");
				string opcion = Console.ReadLine().ToUpper();
				if (opcion != "S")
				{
					convM = false;
				}
			}
		}
		public static void DistanceUnitConverter()
		{
			bool convD = true;
			while (convD)
			{
				//Unidad de origen
				Console.WriteLine("\n--> DISTANCIA <--");
				Console.WriteLine("*Unidad de ORIGEN:");
				Console.Write("Kilómetros (k), metros (m), millas (mi), yardas (y), furlogs (f): ");
				string unitD1 = Console.ReadLine().ToUpper();

				//Validación de unidad de origen
				while (unitD1 != "K" && unitD1 != "M" && unitD1 != "MI" && unitD1 != "Y" && unitD1 != "F")
				{
					Console.WriteLine("<Error>: Unidad no válida. Por favor, escriba correctamente inicial de unidad de origen: ");
					Console.Write("Kilómetros (k), metros (m), millas (mi), yardas (y), furlogs (f): ");
					unitD1 = Console.ReadLine().ToUpper();
				}
				//Unidad de destino
				Console.WriteLine("*Unidad de DESTINO: ");
				Console.Write("Kilómetros(k), metros(m), millas(mi), yardas(y), furlogs(f): ");
				string unitD2 = Console.ReadLine().ToUpper();

				//Validación de unidad de destino
				while (unitD2 != "K" && unitD2 != "M" && unitD2 != "MI" && unitD2 != "Y" && unitD2 != "F"
					|| unitD1 == unitD2)
				{
					string alert = "";
					if (unitD1 == unitD2)
					{
						alert = ": utilizaste misma unidad de origen que de destino.";
					}
					Console.WriteLine(
						"Error (unidad no válida)" + alert + ". Por favor, escriba correctamente inicial de unidad de DESTINO: ");
					Console.Write("Km (k), metros (m), millas (mi), yardas (y), furlogs (f): ");
					unitD2 = Console.ReadLine().ToUpper();
				}
				//Ingreso de valor numérico
				Console.Write("*Ingrese valor numérico: ");
				double num = Double.Parse(Console.ReadLine());
				double result = 0;

				//Equivalencia de tipeo a unidad
				if (unitD1 == "K") unitD1 = "km";
				if (unitD1 == "M") unitD1 = "m";
				if (unitD1 == "MI") unitD1 = "mi";
				if (unitD1 == "Y") unitD1 = "yd";
				if (unitD1 == "F") unitD1 = "f";
				if (unitD2 == "K") unitD2 = "km";
				if (unitD2 == "M") unitD2 = "m";
				if (unitD2 == "MI") unitD2 = "millas";
				if (unitD2 == "Y") unitD2 = "yd";
				if (unitD2 == "F") unitD2 = "f";

				if (unitD1 == "km") //KILOMETRO a...
				{
					if (unitD2 == "m") result = num * 1000; // metro
					else if (unitD2 == "mi") result = num * 0.62137119; //milla
					else if (unitD2 == "yd") result = num * 1093.6133; //yardas
					else if (unitD2 == "f") result = num * 0; //furlogs
				}
				else if (unitD1 == "m") //METRO a...
				{
					if (unitD2 == "km") result = num * 0.001;//kilómetro
					else if (unitD2 == "mi") result = num * 0.00062137;//milla
					else if (unitD2 == "yd") result = num * 1.0936133;//yarda
					else if (unitD2 == "f") result = num * 4.97096954; //furlogs
				}
				else if (unitD1 == "mi") //MILLA a...
				{
					if (unitD2 == "km") result = num * 1.609344; //kilómetro
					else if (unitD2 == "m") result = num * 1609.344; //metro
					else if (unitD2 == "yd") result = num * 1760; //yarda
					else if (unitD2 == "f") result = num * 8; //furlog
				}
				else if (unitD1 == "yd") //YARDA a...
				{
					if (unitD2 == "km") result = num * 0.0009144; //kilómetro
					else if (unitD2 == "m") result = num * 0.9144; //metro
					else if (unitD2 == "mi") result = num * 0.00056818; //milla
					else if (unitD2 == "f") result = num * 0.00454545; //furlog
				}
				else if (unitD1 == "f") //FURLOG a...
				{
					if (unitD2 == "km") result = num * 0.201168; //kilómetro
					else if (unitD2 == "m") result = num * 201.168; //metro
					else if (unitD2 == "mi") result = num * 0.125; //milla
					else if (unitD2 == "yd") result = num * 220; //furlog
				}

				Console.WriteLine("\n--> Resultado: " + num + " " + unitD1 +
					" a " + unitD2 + " = " + Math.Round(result, 2) + " " + unitD2 + "\n");

				Console.WriteLine("¿Desea realizar un nuevo pasaje de DISTANCIA? S/N");
				string opcion = Console.ReadLine().ToUpper();
				if (opcion != "S") convD = false;				
			}
		}
		public static void TemperatureUnitConverter()
		{
			bool convT = true;
			while (convT)
			{
				//Unidad de origen
				Console.WriteLine("\n--> TEMPERATURA <--");
				Console.WriteLine("*Unidad de ORIGEN:");
				Console.Write("Centígrados (c), Farenheid (f), Kelvin (K): ");
				string unitT1 = Console.ReadLine().ToUpper();

				//Validación de unidad de origen
				while (unitT1 != "C" && unitT1 != "F" && unitT1 != "K")
				{
					Console.WriteLine("<Error>: Unidad no válida. Por favor, escriba correctamente inicial de unidad de origen: ");
					Console.Write("Centígrados (c), Fahrenheit (f), Kelvin (K): ");
					unitT1 = Console.ReadLine().ToUpper();
				}
				//Unidad de destino
				Console.WriteLine("*Unidad de DESTINO: ");
				Console.Write("Centígrados (c), Farenheid (f), Kelvin (K): ");
				string unitT2 = Console.ReadLine().ToUpper();

				//Validación de unidad de destino
				while (unitT2 != "C" && unitT2 != "F" && unitT2 != "K"
					|| unitT1 == unitT2)
				{
					string alert = "";
					if (unitT1 == unitT2)
					{
						alert = ": utilizaste misma unidad de origen que de destino.";
					}
					Console.WriteLine(
						"Error (unidad no válida)" + alert + ". Por favor, escriba correctamente inicial de unidad de DESTINO: ");
					Console.Write("Centígrados (c), Fahrenheit (f), Kelvin (K): ");
					unitT2 = Console.ReadLine().ToUpper();
				}
				//Ingreso de valor numérico
				Console.Write("*Ingrese valor numérico: ");
				double num = Double.Parse(Console.ReadLine());
				double result = 0;

				//Equivalencia de tipeo a unidad
				if (unitT1 == "C") unitT1 = "°C";
				if (unitT1 == "F") unitT1 = "°F";
				if (unitT1 == "K") unitT1 = "Kelvin";
				if (unitT2 == "C") unitT2 = "°C";
				if (unitT2 == "F") unitT2 = "°F";
				if (unitT2 == "K") unitT2 = "Kelvin";
				
				//Conversión
				if (unitT1 == "°C") //CENTIGRADOS a...
				{
					if (unitT2 == "°F") result = num * 33.800; // Fahrenheit
					else if (unitT2 == "Kelvin") result = num + 273.15; //Kelvin
				}
				else if (unitT1 == "°F") //FAHRENHEIT a...
				{
					if (unitT2 == "°C") result = num - 273.15;//centigrados
					else if (unitT2 == "K") result = num * 0.00062137;//kelvin
				}
				else if (unitT1 == "K") //KELVIN a...
				{
					if (unitT2 == "°C") result = num - 273.15; //centigrados
					else if (unitT2 == "°F") result = ((num - 273.15) * 1.8) + 32; //fahrenheit
				}
				Console.WriteLine("\n--> Resultado: " + num + " " + unitT1 +
					" a " + unitT2 + " = " + Math.Round(result, 2) + " " + unitT2 + "\n");
				Console.WriteLine("¿Desea realizar un nuevo pasaje de TEMPERATURA? S/N");
				string opcion = Console.ReadLine().ToUpper();
				if (opcion != "S") convT = false;
			}
		}
	}
}
