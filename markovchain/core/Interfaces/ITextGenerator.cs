using System.Xml.Schema;

namespace core.Interfaces
{
	public interface ITextGenerator
	{
		string Generate(int maxLength);
	}
}