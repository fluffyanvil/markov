using System.Collections.Generic;

namespace core.Interfaces
{
	public interface ITextCutter
	{
		IEnumerable<string> Cut(string input);
	}
}