using System.Collections.Generic;
using System.Threading.Tasks;

namespace core.Interfaces
{
	public interface IDictogramBuilder
	{
		IEnumerable<ChainItem> Build();

		Task SaveJson(string folder);

		Task<IEnumerable<ChainItem>> LoadJson(string filename);
	}
}