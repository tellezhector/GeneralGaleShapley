using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneralGaleShapley
{

	public class Permutations
	{
		public int Size { get; set; }

		private Random _random;

		private static Dictionary<int, int> _factorials = new Dictionary<int, int>();

		private Random Random
		{
			get
			{
				if (_random == null)
				{
					_random = new Random ();
				}

				return _random;
			}
		}

		public Permutations(int size)
		{
			Size = size;
		}

		public List<int> NumberedPermutation(int number){
			int[] permutation = new int[Size];
			List<int> available = new List<int> ();
			for (int i = 0; i < Size; i++)
			{
				available.Add (i);
			}

			for (int i = 0; i < Size; i++)
			{
				int index = (number % (Size - i));
				permutation [available[index]] = i;
				available.RemoveAt (index);
			}

			return permutation.ToList();
		}

		private static int Factorial(int n){
			if (n < 0)
			{
				throw new NotSupportedException ("Undefined factorial for negative numbers.");
			}

			if (n == 0)
			{
				return 1;
			}
	
			if (!_factorials.ContainsKey (n))
			{
				_factorials.Add (n, n*Factorial (n - 1));
			}

			return _factorials [n];
		}

		public List<int> RandomPermutation()
		{
			return NumberedPermutation (Random.Next(0, Factorial(Size)));
		}
	}
}
