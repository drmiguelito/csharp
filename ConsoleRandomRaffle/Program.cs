using System;
using Globals;

namespace ConsoleRandomRaffle
{
	internal class Program
	{
		static void Main(string[] args)
		{
			GlobalVariablesAndConstants.ScreenSet(
				"Random Ruffle",
				ConsoleColor.Black,
				ConsoleColor.Yellow
				);
			RandomRaffle raffle = new RandomRaffle();
			raffle.Menu();
		}
	}
}
/* SQL Queries in SQL Server:
 * CREATE DATABASE Gongolitos; -- crear Base de datos
 * CREATE TABLE names (
	id int primary key identity (1,1) not null,
	name varchar(40) not null, 
	genre varchar (10), 
	age int,
	country varchar (50)
	); --crear tabla names--
* CREATE TABLE tasks (
    id int primary key identity (1,1) not null,
	task varchar(100) not null, 
	task_type varchar (40)
	); --crear tabla tasks--
*/

