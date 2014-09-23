using System;

namespace GeneralGaleShapley
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var gs = new GaleShapleyCase (5, 3);
			gs.Print ();
		}
	}

}
