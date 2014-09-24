namespace GeneralGaleShapley
{
	public class MainClass
	{
		public static void Main (string[] args)
		{
			GaleShapleyCase gs = new GaleShapleyCase(5, 3, StatesInizialitation.Random);
			gs.Print();

            gs = new GaleShapleyCase(5, 3, StatesInizialitation.UnrequestedAndUnknown);
            gs.Print();

            gs = new GaleShapleyCase(5, 3, StatesInizialitation.Unrequested);
            gs.Print();

            gs.UpdateState(0, 0, States.Matched);
            gs.Print();
		}
	}
}