using System;

namespace ConsoleSimpleGeometryCalculator
{
	internal class Program
	{
		static void Main(string[] args)
		{
			bool calculator = true;
			string shape = "";
			double b, h, r, area, perimeter;
			Console.Title = "BASIC GEOMETRY CALCULATOR";
			Console.WriteLine("***Calculador de área y perimetro***");
			while (calculator)
			{
                Console.Write("Seleccione: cuadrado (c), triángulo (t), rectángulo (r), círculo (ci): ");
				shape = Console.ReadLine().ToUpper();
				while(shape != "C" && shape != "T" && shape != "R" && shape != "CI")
				{
                    Console.WriteLine("<ERROR>. Por favor, ingrese inicial" +
						" (señalada entre paréntesis) para seleccionar tipo de figura:");
					shape = Console.ReadLine().ToUpper();
				}
				//Cuadrado
				if (shape == "C")
				{
					Console.Write("--> CUADRADO <-- \nIngrese lado: ");
					try
					{
						b = Double.Parse(Console.ReadLine());
					}
					catch (Exception ex)
					{
						Console.WriteLine("Por favor, escriba sólamente valores numéricos: ");
						b = Double.Parse(Console.ReadLine());
					}
					area = Math.Round(Math.Pow(b, 2), 2);
					perimeter = Math.Round(b * 4, 2);
					Console.WriteLine("Resultado: Área = {0}. unidades Perímetro = {1} unidades^2\n", area, perimeter);
				}
				//Rectángulo
				else if (shape == "R")
				{
					Console.Write("--> RECTANGULO <-- \nIngrese base: ");
					try
					{
						b = Double.Parse(Console.ReadLine());
					}
					catch (Exception ex)
					{
						Console.WriteLine("Por favor, escriba sólamente valores numéricos: ");
						b = Double.Parse(Console.ReadLine());
					}
					Console.Write("Ingrese altura: ");
					try
					{
						h = Double.Parse(Console.ReadLine());
					}
					catch (Exception ex)
					{
						Console.WriteLine("Por favor, escriba sólamente valores numéricos: ");
						h = Double.Parse(Console.ReadLine());
					}
					area = Math.Round(b * h, 2);
					perimeter = Math.Round((b * 2) + (h * 2));
					Console.WriteLine("Resultado: Área = {0} unidades. Perímetro = {1} unidades^2\n", area, perimeter);
				}
				//Circulo
				else if (shape == "CI")
				{
					Console.Write("--> CIRCULO <-- \n" +
						"Ingrese radio: ");
					try
					{
						r = Double.Parse(Console.ReadLine());
					}
					catch (Exception ex)
					{
						Console.WriteLine("Por favor, escriba sólamente valores numéricos: ");
						r = Double.Parse(Console.ReadLine());
					}
					area = Math.Round(Math.PI * Math.Pow(r, 2), 2);
					perimeter = Math.Round(2 * r * Math.PI, 2);
					Console.WriteLine("Resultado: Área = {0} unidades. Perímetro = {1} unidades^2\n", area, perimeter);
				}
				//Triángulo
				else if (shape == "T")
				{
					Console.WriteLine("--> TRIANGULO <--");
					double[] sides = new double[3];
					for (int i = 0; i < sides.Length; i++)
					{
						try
						{
							Console.Write("Ingrese lado " + (i + 1) + ": ");
							sides[i] = Double.Parse(Console.ReadLine());
						}
						catch (Exception ex)
						{
							Console.WriteLine("Por favor, escriba sólamente valores numéricos: ");
							Console.Write("Ingrese lado " + (i + 1) + ": ");
							sides[i] = Double.Parse(Console.ReadLine());
						}
					}
					double s = (sides[0] + sides[1] + sides[2]) / 2; // semiperimeter
					area = Math.Round(Math.Sqrt(s * ((s - sides[0]) * (s - sides[1]) * (s - sides[2]))), 2);
					perimeter = Math.Round(sides[0] + sides[1] + sides[2], 2);
					Console.WriteLine("Resultado: Área = {0} unidades. Perímetro = {1} unidades^2\n", area, perimeter);
				}
                Console.WriteLine("\n¿Desea realizar un nuevo cálculo? S/N");
				string answer = Console.ReadLine().ToUpper();
				if (answer != "S") calculator = false;
            }
			Console.WriteLine("¡Nos vemos pronto!");
		}
	}
}
