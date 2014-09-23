using System;

namespace GeneralGaleShapley
{
	public class GaleShapleyCase
	{
		public GaleShapleyCase(int n) : this(n, n){}

		public int Men { get; private set; }

		public int Women { get; private set; }

		public GaleShapleyCase (int men, int women)
		{
			Men = men;
			Women = women;
		}

		int[][] _menMatrix;

		int[][] _womenMatrix;

		public int[][] MenMatrix 
		{
			get
			{
				if(_menMatrix != null)
				{
					return _menMatrix;
				}

				_menMatrix = new int[Men][];
			
				Permutations permutations = new Permutations(Women);
				for(int i = 0; i < Men; i++)
				{
					_menMatrix [i] = permutations.RandomPermutation().ToArray();
				}

				return _menMatrix; 
			}
		}

		public int[][] WomenMatrix 
		{
			get
			{
				if(_womenMatrix != null)
				{
					return _womenMatrix;
				}

				_womenMatrix = new int[Women][];

				Permutations permutations = new Permutations(Men);
				for(int i = 0; i < Men; i++)
				{
					_womenMatrix [i] = permutations.RandomPermutation().ToArray();
				}

				return _womenMatrix; 
			}
		}
	}
}

