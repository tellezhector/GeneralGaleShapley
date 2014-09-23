using System;

namespace GeneralGaleShapley
{
	class MainClass
	{
		public static void Main (string[] args)
		{

			var permutations = new Permutations (3);
			for (int i = 0; i < 30; i++)
			{
				Console.WriteLine ("{0} {1}", i, string.Join(",", permutations.RandomPermutation ()));
			}
		}
	}

}
