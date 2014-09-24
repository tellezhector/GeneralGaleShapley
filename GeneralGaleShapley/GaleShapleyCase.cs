using System;

namespace GeneralGaleShapley
{
    using System.Collections.Generic;

    public class GaleShapleyCase
	{
        private static Random _random;

        public GaleShapleyCase(int n, StatesInizialitation statesInizialitation) : this(n, n, statesInizialitation){}

		public int Men { get; private set; }

		public int Women { get; private set; }

		public GaleShapleyCase (int men, int women, StatesInizialitation statesInizialitation)
		{
			if (men <= 0 || women <= 0)
			{
				throw new NotSupportedException ("Only positive integers allowed.");
			}

			Men = men;
			Women = women;
		    StatesInizialitation = statesInizialitation;
            
            MenPreferencesMatrix = InitializePreferencesMatrix(Men, Women);
            WomenPreferencesMatrix = InitializePreferencesMatrix(Women, Men);
            
            MenPreferencesReverseIndex = InitializePreferencesReverseIndex(MenPreferencesMatrix);
            WomenPreferencesReverseIndex = InitializePreferencesReverseIndex(WomenPreferencesMatrix);

            MenStatesMatrix = InitializeMenStates();
		    WomenStatesMatrix = InitializeWomenStates();
		}

        private StatesInizialitation StatesInizialitation { get; set; }

        public int[][] MenPreferencesMatrix { get; private set; }

        public States[][] MenStatesMatrix { get; private set; }

        public int[][] WomenPreferencesMatrix { get; private set; }

        public States[][] WomenStatesMatrix { get; private set; }

        public void UpdateState(int man, int woman, States state)
        {
            int w = MenPreferencesReverseIndex[man][woman];
            MenStatesMatrix[man][w] = state;
            int m = WomenPreferencesReverseIndex[woman][man];
            WomenStatesMatrix[woman][m] = state;
        }

        //reverse index;
        public Dictionary<int, Dictionary<int, int>> MenPreferencesReverseIndex { get; private set; }

        //reverse index;
        public Dictionary<int, Dictionary<int, int>> WomenPreferencesReverseIndex { get; private set; }

        private States[][] InitializeWomenStates()
        {
            var states = new States[Women][];
            for (int w = 0; w < Women; w++)
            {
                states[w] = new States[Men];
                for (int j = 0; j < Men; j++)
                {
                    int m = WomenPreferencesMatrix[w][j];
                    States state = MenStatesMatrix[m][MenPreferencesReverseIndex[m][w]];
                    states[w][j] = state;
                }
            }

            return states;
        }

        private static Random Random
        {
            get
            {
                if (_random == null)
                {
                    _random = new Random();
                }

                return _random;
            }
        }

        private States[][] InitializeMenStates()
		{
			States[][] matrix = new States[Men][];
			for(int i = 0; i < Men; i++)
			{
				matrix[i] = new States[Women];
				for (int j = 0; j < Women; j++)
				{
				    States state = States.Unrequested;
				    switch (StatesInizialitation)
				    {
				        case StatesInizialitation.Random:
                            state = (States)Random.Next(-1, 3);
				        break;

                        case StatesInizialitation.Unrequested:
                            state = States.Unrequested;
				        break;

                        case StatesInizialitation.UnrequestedAndUnknown:
                        state = (States)Random.Next(-1, 1);
				        break;
				    }

				    matrix[i][j] = state;
				}
			}

			return matrix; 		
		}

		public void Print()
		{
			Console.WriteLine ("Men's matrix:");
			PrintMatrix (MenPreferencesMatrix, MenStatesMatrix);
			Console.WriteLine ("Women's matrix:");
			PrintMatrix (WomenPreferencesMatrix, WomenStatesMatrix);
		    PrintColorMeanings();
		}

        private static void PrintColorMeanings()
        {
            SetMatchedStateColors();
            Console.WriteLine("Matched");
            SetUnrequestedStateColors();
            Console.WriteLine("Unrequested");
            SetRejectedStateColors();
            Console.WriteLine("Rejected");
            SetUnknownStateColors();
            Console.WriteLine("Unknown");
            Console.ResetColor();
        }

        private static Dictionary<int, Dictionary<int, int>> InitializePreferencesReverseIndex(int[][] matrix)
        {
            var dictionary = new Dictionary<int, Dictionary<int, int>>();
            var persons = matrix.Length;
            var prefLength = matrix[0].Length;
            for (int i = 0; i < persons; i++)
            {
                dictionary.Add(i, new Dictionary<int, int>());
                for (int j = 0; j < prefLength; j++)
                {
                    dictionary[i].Add(matrix[i][j], j);
                }
            }

            return dictionary;
        }

        private static int[][] InitializePreferencesMatrix(int persons, int preferencesSize)
        {
            var matrix = new int[persons][];
            Permutations permutations = new Permutations(preferencesSize);
            for (int i = 0; i < persons; i++)
            {
                matrix[i] = permutations.RandomPermutation().ToArray();
            }

            return matrix;
        }

		private static void PrintMatrix (int[][] matrix, States[][] states)
		{
			int persons = matrix.Length;
			int preferencesLength = matrix [0].Length;
			int pad1 = persons.ToString().Length + 2;
			int pad2 = preferencesLength.ToString().Length + 2; 
			int lineLengt = pad1 + 1 + ((pad2 + 1) * preferencesLength);
	
			Console.WriteLine(new String('-', lineLengt));
			for (int i = 0; i < persons; i++)
			{
				Console.Write ("{0}|", i.ToString().PadRight (pad1));
				for (int j = 0; j < preferencesLength; j++)
				{
					var s = matrix [i] [j].ToString().PadLeft (pad2);
					switch (states [i] [j])
					{
						case States.Matched:
							SetMatchedStateColors();
					        break;
						
						case States.Unrequested:
                            SetUnrequestedStateColors();
					        break;
						
						case States.Rejected:
							SetRejectedStateColors();
					        break;

						case States.Unknown:
							SetUnknownStateColors();
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

        private static void SetUnknownStateColors()
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void SetRejectedStateColors()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void SetUnrequestedStateColors()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void SetMatchedStateColors()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
        }
	} 
}