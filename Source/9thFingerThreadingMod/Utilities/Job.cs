using Harmony;
using System;
using System.Collections.Generic;

namespace _9thFingerThreadingMod.Utilities
{
    public class Job
    {
        public delegate object Del();
        private Del del { get; set; }
        public Object result { get; set; }
        public bool isDone { get; private set; }
        public Exception exception = null;

        public Job(Del dele)
        {
            del = dele;
            result = null;
            isDone = false;
        }

        public void doJob()
        {
            try
            {
                result = del();
            }
            catch (Exception e)
            {
                exception = e;
            }
            finally
            {
                isDone = true;
            }
        }
    }
}
