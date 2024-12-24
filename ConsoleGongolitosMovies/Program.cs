using System;

namespace ConsoleGongolitosMovies
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.Title = "Gongolitos Movies";
			Movie movie = new Movie();
			movie.Menu();
        }
	}
}
