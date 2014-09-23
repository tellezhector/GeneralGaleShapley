using System;
using System.Linq;

namespace GeneralGaleShapley
{
	public enum States
	{
		Unknown = -1,
		Unrequested = 0,
		Matched = 1,
		Rejected = 2
	}

	public class GaleShapleyCase
	{
		public GaleShapleyCase(int n) : this(n, n){}

		public int Men { get; private set; }

		public int Women { get; private set; }

		public GaleShapleyCase (int men, int women)
		{
			if (men <= 0 || women <= 0)
			{
				throw new NotSupportedException ("Only positive integers allowed.");
			}
			Men = men;
			Women = women;
		}

		int[][] _menMatrix;

		int[][] _womenMatrix;

		States[][] _menStatesMatrix;

		States[][] _womenStatesMatrix;

		public int[][] MenMatrix 
		{
			get
			{
				if(_menMatrix == null)
				{
					_menMatrix = InitializePreferencesMatrix (Men, Women);
				}
		
				return _menMatrix; 
			}
		}

		public States[][] MenStatesMatrix
		{
			get
			{
				if (_menStatesMatrix == null)
				{
					_menStatesMatrix = InitializeStatesMatrix (Men, Women);
				}

				return _menStatesMatrix;
			}
		}

		public int[][] WomenMatrix 
		{
			get
			{
				if(_womenMatrix == null)
				{
					_womenMatrix = InitializePreferencesMatrix (Women, Men);
				}

				return _womenMatrix; 
			}
		}

		public States[][] WomenStatesMatrix
		{
			get
			{ 
				if (_womenStatesMatrix == null)
				{
					_womenStatesMatrix = InitializeStatesMatrix (Women, Men);
				}

				return _womenStatesMatrix;
			}
		}

		States[][] InitializeStatesMatrix (int persons, int preferencesSize)
		{
			var matrix = new States[persons][];
			var preflength = matrix [0].Length;
			for(int i = 0; i < persons; i++)
			{

				for (int j = 0; j < preflength; i++)
				{
					matrix [i] [j] = States.Unrequested;
				}
			}

			return matrix; 		
		}

		private int[][] InitializePreferencesMatrix(int persons, int preferencesSize){
			var matrix = new int[persons][];
			Permutations permutations = new Permutations(preferencesSize);
			for(int i = 0; i < persons; i++)
			{
				matrix [i] = permutations.RandomPermutation().ToArray();
			}

			return matrix; 
		}

		public void Print()
		{
			Console.WriteLine ("Men's matrix:");
			PrintMatrix (MenMatrix);
			Console.WriteLine ("Women's matrix:");
			PrintMatrix (WomenMatrix);
		}

		private void PrintMatrix (int[][] matrix)
		{
			int persons = matrix.Length;
			int preferencesLength = matrix [0].Length;
			int pad1 = persons.ToString().Length + 2;
			int pad2 = preferencesLength.ToString().Length + 2; 
			int lineLengt = pad1 + 1 + ((pad2 + 1) * preferencesLength);
	
			Console.WriteLine(new String('-', lineLengt));
			for (int i = 0; i < persons; i++)
			{
				string lineName = i.ToString().PadRight(pad1);
				string preferences = string.Join(",", matrix[i].Select(s => s.ToString().PadLeft(pad2)));
				string line = string.Format("{0}|{1}", lineName, preferences);
				Console.WriteLine (line);
			}
			Console.WriteLine(new String('-', lineLengt));
		}
	} 
}