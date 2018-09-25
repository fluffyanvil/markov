using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;

namespace core.Implementations
{
	public class TextLoader : Interfaces.ITextLoader
	{
		public string Load(string folder)
		{
			var sb = new StringBuilder();
			var files = new DirectoryInfo(folder).GetFiles().Select(fi => fi.FullName);
			foreach (var file in files)
			{
				using (var reader = new StreamReader(file, Encoding.GetEncoding(1251)))
				{
					sb.Append(reader.ReadToEnd());
				}
				sb.AppendLine();
			}
			return sb.ToString();
		}
	}
}