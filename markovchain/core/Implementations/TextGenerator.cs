using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using core.Interfaces;

namespace core.Implementations
{
	public class TextGenerator : ITextGenerator
	{
		private IEnumerable<ChainItem> _items;

		public TextGenerator(IEnumerable<ChainItem> items)
		{
			_items = items;
		}
		public string Generate(int maxLength)
		{
			var wordArray = _items.Where(i => i.Dependencies != null).ToArray();
			var startWordIndex = new Random(Guid.NewGuid().GetHashCode()).Next(0, wordArray.Length);
			var startWord = wordArray[startWordIndex];
			var sb = new StringBuilder();
			sb.Append(startWord.Value);
			for (int i = 0; i < maxLength; i++)
			{
				if (startWord?.Dependencies.Count > 1)
				{
					var nextWordIndex = new Random(Guid.NewGuid().GetHashCode()).Next(0, startWord.Dependencies.Count); ;
					var nextWord = startWord.Dependencies?.ToArray()[nextWordIndex];
					sb.Append($" {nextWord.Value.Key}");
					startWord = wordArray.FirstOrDefault(w => w.Value.Equals(nextWord.Value.Key));
				}
				
			}
			return sb.ToString();
		}
	}
}