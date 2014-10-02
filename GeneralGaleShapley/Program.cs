namespace GeneralGaleShapley
{
    using System;
	using System.IO;
	using System.Text;

    public class MainClass
	{
		public static void Main (string[] args)
		{
			int min = 1;
			int max = 100;
			for (int i = 0; i < args.Length; i++)
			{
				try
				{
					switch (args [i])
					{
						case "-h":
							DisplayHelp ();
							return;
						case "-c":
							throw new NotImplementedException ("Not ready, stay tuned.");
						case "-min":
							i++;
							min = int.Parse (args[i]);
							break;
						case "-max":
							i++;
							max = int.Parse (args[i]);
							break;
					}
				}
				catch
				{ 
					DisplayHelp ();
					throw;
				}
			}

			string filename = string.Format ("results{0}-{1}.txt", min, max);
			string cwd = Directory.GetCurrentDirectory();
			string path = Path.Combine (cwd, filename);
			Console.WriteLine (path);
			using (StreamWriter sw = new StreamWriter (path, false, Encoding.UTF8))
			{
				for (int i = min; i <= max; i++)
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

		static void DisplayHelp ()
		{
			Console.WriteLine ("-h               Show this help.");
			Console.WriteLine ("-c <casename>    Algorithm version to be used. Options are:");
			Console.WriteLine ("                 Classic, UnknownResentful, UnknownForgivers.");
			Console.WriteLine ("                 Default Classic.");
			Console.WriteLine ("-min <n>         Min size to be simulated. Default 1.");
			Console.WriteLine ("-max <n>         Max size to be simulated. Default 100.");
		}
	}
}