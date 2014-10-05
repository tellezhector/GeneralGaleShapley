namespace Simulator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Permutations
	{
		public int Size { get; set; }

		private static Random _random;

		private static Random Random
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

		public List<int> RandomPermutation()
		{
            int[] permutation = new int[Size];
            List<int> available = new List<int>();
            for (int i = 0; i < Size; i++)
            {
                available.Add(i);
            }
            
            for (int i = 0; i < Size; i++)
            {
                int random = Random.Next(0, Size - i);
                permutation[i] = available[random];
                available.RemoveAt(random);
            }

		    return permutation.ToList();
		}
	}
}
