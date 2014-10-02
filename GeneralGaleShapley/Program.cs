namespace GeneralGaleShapley
{
    using System;
	using System.IO;
	using System.Text;

    public class MainClass
	{
		public static void Main (string[] args)
		{
			int n = 1;
			int m = 1000;
			for (int i = 0; i < args.Length; i++)
			{
				switch (args [i])
				{
					case "-h":
					case "-c":
					case "-min":
					case "-max":
						throw new NotImplementedException ("Not ready, stay tuned.");
				}
			}

			if (args.Length > 0)
			{
				n = int.Parse (args[0]);
			}
			if (args.Length > 1)
			{
				m = int.Parse (args[1]);
			}

			string filename = string.Format ("results{0}-{1}.txt", n, m);
			string cwd = Directory.GetCurrentDirectory();
			string path = Path.Combine (cwd, filename);
			Console.WriteLine (path);
			using (StreamWriter sw = new StreamWriter (path, false, Encoding.UTF8))
			{
				for (int i = n; i <= m; i++)
			    {
					Console.WriteLine ("Parsing cases of size {0}", i);
					int repetitions = 1000;
					for (int j = 1; j <= repetitions; j++)
			        {
	                    GaleShapleyCase gs = new GaleShapleyCase(i, StatesInizialitation.Unrequested);
	                    ClassicGaleShapley cgs = new ClassicGaleShapley(gs);
	                    cgs.Solve();

						var result = string.Format("{0} {1} {2}", i, cgs.Comparisons, cgs.Elpased.Ticks);
						sw.WriteLine(result);

						Console.SetCursorPosition (0, Console.CursorTop);
						Console.Write ("{0:0.##}%        ", 100*((decimal)j/repetitions));
					}
					Console.WriteLine();
			    }
			}
		}
	}
}