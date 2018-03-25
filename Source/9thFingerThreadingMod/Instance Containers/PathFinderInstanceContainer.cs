using Harmony;
using System;
using System.Collections.Generic;
using System.Threading;
using Verse;
using Verse.AI;

namespace _9thFingerThreadingMod
{
    class PathFinderInstanceContainer
    {
        private static PathFinderInstanceContainer instance = null;
        public static PathFinderInstanceContainer GetInstance()
        {
            if (instance == null)
                instance = new PathFinderInstanceContainer();
            return instance;
        }
        private PathFinderInstanceContainer() { }

        //Need a container that holds pathfinders indexed by map
        Dictionary<String, newPathFinder[]> finders = new Dictionary<String, newPathFinder[]>();
        Dictionary<String, int> numBusyFinders = new Dictionary<String, int>();
        Dictionary<String, bool[]> busyFinders = new Dictionary<String, bool[]>();

        private Dictionary<String, EventWaitHandle> findersBusy = new Dictionary<String, EventWaitHandle>();
        private EventWaitHandle addingFinders = new EventWaitHandle(true, EventResetMode.AutoReset);

        private static readonly object _syncObject = new object();

        public void refreshFinders()
        {
            finders = new Dictionary<String, newPathFinder[]>();
            numBusyFinders = new Dictionary<String, int>();
            busyFinders = new Dictionary<String, bool[]>();
            findersBusy = new Dictionary<String, EventWaitHandle>();
            addingFinders = new EventWaitHandle(true, EventResetMode.AutoReset);
        }

        public newPathFinder requestFinder(String index, ref int ticket, Map map)
        {
            if (!finders.ContainsKey(index))
            {
                addingFinders.WaitOne();
                if (!finders.ContainsKey(index))
                    InitializeNewMapFinders(index, map);
                addingFinders.Set();
            }
            findersBusy[index].WaitOne();
            numBusyFinders[index]++;
            if (numBusyFinders[index] >= ThreadingMod.NUM_THREADS_PER_MAP)
                findersBusy[index].Reset();

            return SelectFinder(index, ref ticket);
        }

        public void CheckInFinder(String index, int ticket)
        {
            if (ticket < 0 || ticket > ThreadingMod.NUM_THREADS_PER_MAP)
                throw new Exception("Invalid finder ticket");
            try
            {
                busyFinders[index][ticket] = false;
                numBusyFinders[index]--;
            }
            catch (Exception e)
            {
                Log.Message("Key not found in Pathfinder Container: " + index);
            }
            if (numBusyFinders[index] < ThreadingMod.NUM_THREADS_PER_MAP)
                findersBusy[index].Set();
            return;
        }

        private newPathFinder SelectFinder(String index, ref int ticket)
        {
            newPathFinder reachability = null;
            lock (_syncObject)
            {
                for (int i = 0; i < ThreadingMod.NUM_THREADS_PER_MAP; i++)
                {
                    if (busyFinders[index][i] == false)
                    {
                        busyFinders[index][i] = true;
                        reachability = finders[index][i];
                        ticket = i;
                        break;
                    }
                }
            }
            return reachability;
        }

        private void InitializeNewMapFinders(String index, Map map)
        {
            newPathFinder[] newFinders = new newPathFinder[ThreadingMod.NUM_THREADS_PER_MAP];

            for (int i = 0; i < ThreadingMod.NUM_THREADS_PER_MAP; i++)
            {
                newFinders[i] = new newPathFinder(map);
            }
            finders.Add(index, newFinders);
            numBusyFinders.Add(index, 0);
            busyFinders.Add(index, new bool[ThreadingMod.NUM_THREADS_PER_MAP]);
            findersBusy.Add(index, new EventWaitHandle(true, EventResetMode.ManualReset));
        }
    }
}
