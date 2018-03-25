using _9thFingerThreadingMod.Replacement_Objects;
using System;
using System.Collections.Generic;
using System.Threading;
using Verse;

namespace _9thFingerThreadingMod
{
    class ReachabilityInstanceContrainer
    {
        private static ReachabilityInstanceContrainer instance = null;
        public static ReachabilityInstanceContrainer GetInstance()
        {
            if (instance == null)
                instance = new ReachabilityInstanceContrainer();
            return instance;
        }
        private ReachabilityInstanceContrainer() { }

        private Dictionary<String, EventWaitHandle> reachersBusy = new Dictionary<String, EventWaitHandle>();
        private Dictionary<String, int> numBusyReachers = new Dictionary<String, int>();
        private Dictionary<String, bool[]> busyReachers = new Dictionary<String, bool[]>();
        private Dictionary<String, newReachabilityCache> reacherCaches = new Dictionary<String, newReachabilityCache>();

        private EventWaitHandle addingReachers = new EventWaitHandle(true, EventResetMode.AutoReset);
        private Dictionary<String, newReachability[]> reachers = new Dictionary<String, newReachability[]>();

        public void refreshReachers()
        {
            reachers = new Dictionary<String, newReachability[]>();
            addingReachers = new EventWaitHandle(true, EventResetMode.AutoReset);
            reacherCaches = new Dictionary<String, newReachabilityCache>();
            busyReachers = new Dictionary<String, bool[]>();
            numBusyReachers = new Dictionary<String, int>();
            reachersBusy = new Dictionary<String, EventWaitHandle>();
        }

        public newReachability requestReacher(String index, ref int ticket, Map map)
        {
            if (!reachers.ContainsKey(index))
            {
                addingReachers.WaitOne();
                if (!reachers.ContainsKey(index))
                    InitializeNewMapReachers(index, map);
                addingReachers.Set();
            }

            reachersBusy[index].WaitOne();
            numBusyReachers[index]++;
            if (numBusyReachers[index] >= ThreadingMod.NUM_THREADS_PER_MAP)
                reachersBusy[index].Reset();

            return SelectReacher(index, ref ticket);
        }

        public void CheckInReacher(String index, int ticket)
        {
            if (ticket < 0 || ticket > ThreadingMod.NUM_THREADS_PER_MAP)
                throw new Exception("Invalid reacher ticket");

            busyReachers[index][ticket] = false;
            numBusyReachers[index]--;
            if (numBusyReachers[index] < ThreadingMod.NUM_THREADS_PER_MAP)
                reachersBusy[index].Set();
            return;

        }

        private void InitializeNewMapReachers(String index, Map map)
        {
            reacherCaches.Add(index, new newReachabilityCache());
            newReachability[] reachersArray = new newReachability[ThreadingMod.NUM_THREADS_PER_MAP];

            for (int i = 0; i < ThreadingMod.NUM_THREADS_PER_MAP; i++)
            {
                reachersArray[i] = new newReachability(map, reacherCaches[index], (uint) i);
            }
            reachers.Add(index, reachersArray);
            numBusyReachers.Add(index, 0);
            reachersBusy.Add(index, new EventWaitHandle(true, EventResetMode.ManualReset));
            busyReachers.Add(index, new bool[ThreadingMod.NUM_THREADS_PER_MAP]);
        }

        private newReachability SelectReacher(String index, ref int ticket)
        {
            newReachability reachability = null;
            for (int i = 0; i < ThreadingMod.NUM_THREADS_PER_MAP; i++)
            {
                if (busyReachers[index][i] == false)
                {
                    busyReachers[index][i] = true;
                    reachability = reachers[index][i];
                    ticket = i;
                    break;
                }
            }
            return reachability;
        }
    }
}
