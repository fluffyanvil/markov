using System;
using System.Text.RegularExpressions;

namespace core.Helpers
{
	public static class RegexHelper
	{
		private const string SrtPattern =
				@"^([0-9]+(\r\n)([0-9][0-9]:[0-5][0-9]:[0-5][0-9],[0-9][0-9][0-9]\s-->\s[0-9][0-9]:[0-5][0-9]:[0-5][0-9],[0-9][0-9][0-9]))";

		private const string SpacesPattern = @"(\s)+";

		public static string Clean(string input)
		{
			var result = Regex.Replace(input, SrtPattern, string.Empty, RegexOptions.Multiline).Replace("\r\n", "").Replace("<i>", "").Replace("</i>", "").Replace("[", " ").Replace("]", " ").Replace("(", " ").Replace(")", "");
			return Regex.Replace(result, SpacesPattern, " ");
		}
	}
}