/*
 * Namespace Globals:
 * Posee y trabaja con variables y constantes globales.
 * En C# no se puede declarar variables globales de forma directa
 * Una forma de hacerlo es por medio de un namespace destinado a tal fins
 */

using System;

namespace Globals
{
	public static class GlobalVariablesAndConstants
	{
		public static bool showError;

		public static void HandleMethodByInput(string keyWord)
		{
			if (keyWord == "exit")
			{
				showError = false;
				throw new Exception();
			}
		}
	}
}
