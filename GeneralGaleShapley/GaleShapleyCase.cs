using System;
using System.Linq;

namespace GeneralGaleShapley
{

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
			States[][] matrix = new States[persons][];
			var random = new Random ();
			for(int i = 0; i < persons; i++)
			{
				matrix[i] = new States[preferencesSize];
				for (int j = 0; j < preferencesSize; j++)
				{
					matrix [i] [j] = (States)random.Next(-1, 3);
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
			PrintMatrix (MenMatrix, MenStatesMatrix);
			Console.WriteLine ("Women's matrix:");
			PrintMatrix (WomenMatrix, WomenStatesMatrix);
		}

		private void PrintMatrix (int[][] matrix, States[][] states)
		{
			int persons = matrix.Length;
			int preferencesLength = matrix [0].Length;
			int pad1 = persons.ToString().Length + 2;
			int pad2 = preferencesLength.ToString().Length + 2; 
			int lineLengt = pad1 + 1 + ((pad2 + 1) * preferencesLength);
	
			Console.WriteLine(new String('-', lineLengt));
			for (int i = 0; i < persons; i++)
			{
				Console.Write (string.Format ("{0}|", i.ToString ().PadRight (pad1)));
				for (int j = 0; j < preferencesLength; j++)
				{
					var s = matrix [i] [j].ToString ().PadLeft (pad2);
					switch (states [i] [j])
					{
						case States.Matched:
							Console.BackgroundColor = ConsoleColor.Green;
							Console.ForegroundColor	= ConsoleColor.White;
							break;
						
						case States.Unrequested:
							Console.ResetColor ();
							break;
						
						case States.Rejected:
							Console.BackgroundColor = ConsoleColor.Red;
							Console.ForegroundColor = ConsoleColor.White;
							break;

						case States.Unknown:
							Console.BackgroundColor = ConsoleColor.Black;
							Console.ForegroundColor = ConsoleColor.White;
							break;
					}

					Console.Write (s);
					Console.ResetColor();
					if (j == preferencesLength - 1)
					{
						Console.Write ("|");
					} 
					else
					{
						Console.Write (",");
					}
				}
				Console.WriteLine ();
			}
			Console.WriteLine(new String('-', lineLengt));
		}
	} 
}