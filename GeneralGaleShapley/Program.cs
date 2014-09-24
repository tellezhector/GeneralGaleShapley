namespace GeneralGaleShapley
{
    using System;

    public class MainClass
	{
		public static void Main (string[] args)
		{
		    for (int i = 1; i <= 1000; i++)
		    {
		        for (int j = 0; j < 1000; j++)
		        {
                    GaleShapleyCase gs = new GaleShapleyCase(i, StatesInizialitation.Unrequested);
                    ClassicGaleShapley cgs = new ClassicGaleShapley(gs);
                    cgs.Solve();

                    Console.WriteLine("{0} {1} {2}", i, cgs.Comparisons, cgs.Elpased.Ticks);
		        }    
		    }
		}
	}
}