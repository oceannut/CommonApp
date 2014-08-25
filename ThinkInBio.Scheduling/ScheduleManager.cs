using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.Scheduling
{

    public class ScheduleManager
    {

        private static ScheduleManager sole = new ScheduleManager();
        private Dictionary<string, Scheduler> map = new Dictionary<string, Scheduler>();

        public static ScheduleManager SoleInstance
        {
            get { return sole; }
        }

        public IEnumerable<Scheduler> SchedulerList
        {
            get
            {
                List<Scheduler> list = new List<Scheduler>(map.Values);
                list.Sort(new Comparison<Scheduler>(
                    (e1, e2) =>
                    {
                        return e1.Name.CompareTo(e2.Name);
                    }));
                return list;
            }
        }

        private ScheduleManager() { }

        public void Add(Scheduler scheduler)
        {
            if (scheduler == null)
            {
                throw new ArgumentNullException();
            }
            if (string.IsNullOrWhiteSpace(scheduler.Name))
            {
                scheduler.Name = new Guid().ToString();
            }
            if (!map.ContainsKey(scheduler.Name))
            {
                map.Add(scheduler.Name, scheduler);
            }
        }

        public void Remove(Scheduler scheduler)
        {
            if (scheduler == null || string.IsNullOrWhiteSpace(scheduler.Name))
            {
                throw new ArgumentNullException();
            }
            Remove(scheduler.Name);
        }

        public void Remove(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException();
            }
            if (map.ContainsKey(name))
            {
                map.Remove(name);
            }
        }

    }

}
