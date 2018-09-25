using System.Collections.Generic;
using System.Linq;
using core.Interfaces;

namespace core.Implementations
{
	public class TextCutter : ITextCutter
	{
		public IEnumerable<string> Cut(string input)
		{
			return input.Split('.').Where(s => !string.IsNullOrEmpty(s)).Where(s => s.Split(' ').Length > 1).Select(s => s + ".").ToArray();
		}
	}
}