namespace GeneralGaleShapley
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class ClassicGaleShapley
    {
        public ClassicGaleShapley(GaleShapleyCase gs)
        {
            Gs = gs;
            Comparisons = 0;
            Solved = false;

            MenNextRequests = new int[Gs.Men];
            WomenCurrentMan = new int[Gs.Women];
            for (int i = 0; i < Gs.Women; i++)
            {
                WomenCurrentMan[i] = int.MaxValue;
            }

            int[] menList = Gs.MenPreferencesReverseIndex.Select(e => e.Key).ToArray();
            MenQueue = new Queue<int>(menList);
        }

        public bool Solved { get; private set; }

        public int Comparisons { get; private set; }

        public TimeSpan Elpased { get; private set; }

        public GaleShapleyCase Gs { get; set; }

        public void Solve()
        {
            if (Solved)
            {
                return;
            }

    
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (MenQueue.Count > 0)
            {
                Comparisons++;
                
                int man = MenQueue.Dequeue();
                int request = MenNextRequests[man];
                MenNextRequests[man]++;

                int woman = Gs.MenPreferencesMatrix[man][request];
                int manPrefIndex = Gs.WomenPreferencesReverseIndex[woman][man];
                if (WomenCurrentMan[woman] == int.MaxValue)
                {
                    WomenCurrentMan[woman] = man;
                    Gs.UpdateState(man, woman, States.Matched);
                    continue;
                }

                int previousMan = WomenCurrentMan[woman];
                int previousManPrefIndex = Gs.WomenPreferencesReverseIndex[woman][previousMan];
                if (manPrefIndex < previousManPrefIndex)
                {
                    WomenCurrentMan[woman] = man;
                    Gs.UpdateState(man, woman, States.Matched);
                    Gs.UpdateState(previousMan, woman, States.Rejected);
                    if (MenNextRequests[previousMan] < Gs.Women)
                    {
                        MenQueue.Enqueue(previousMan);
                    }
                }
                else
                {
                    Gs.UpdateState(man, woman, States.Rejected);
                    if (MenNextRequests[man] < Gs.Women)
                    {
                        MenQueue.Enqueue(man);
                    }
                }
            }

            sw.Stop();
            Elpased = sw.Elapsed;
            Solved = true;
        }

        public Queue<int> MenQueue { get; private set; }

        public int[] WomenCurrentMan { get; private set; }

        public int[] MenNextRequests { get; private set; }
    }
}