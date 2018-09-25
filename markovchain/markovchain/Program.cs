using System;
using System.Text;
using System.Threading;
using core.Helpers;
using core.Implementations;
using core.Interfaces;

namespace markovchain
{
	class Program
	{
		private const string Root = "..\\..\\..\\";
		private static string _samplesFolder = $"{Root}samples";
		private static readonly string _models = $"{Root}models";

		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.UTF8;
			ITextLoader tl = new TextLoader();
			var s = tl.Load(_samplesFolder);
			var rs = RegexHelper.Clean(s);
			var db = new DictogramBuilder(rs);

			

			var m = db.LoadJson(_models).Result;
			var tg = new TextGenerator(m);
			for (var i = 0; i < 1000; ++i)
			{
				var stes = tg.Generate(100000);
				Console.WriteLine(stes);
				Thread.Sleep(100);

			}
			Console.ReadLine();
		}
	}
}