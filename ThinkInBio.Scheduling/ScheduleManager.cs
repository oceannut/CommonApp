using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.Scheduling
{

    public class ScheduleManager
    {

        internal Dictionary<string, Scheduler> Map { get; set; }

        public IEnumerable<Scheduler> SchedulerList
        {
            get
            {
                List<Scheduler> list = new List<Scheduler>(Map.Values);
                list.Sort(new Comparison<Scheduler>(
                    (e1, e2) =>
                    {
                        return e1.Name.CompareTo(e2.Name);
                    }));
                return list;
            }
        }

        internal ScheduleManager()
        {
            Map = new Dictionary<string, Scheduler>();
        }

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
            if (!Map.ContainsKey(scheduler.Name))
            {
                Map.Add(scheduler.Name, scheduler);
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
            if (Map.ContainsKey(name))
            {
                Map.Remove(name);
            }
        }

        public void Init()
        {
            foreach (Scheduler item in Map.Values)
            {
                item.Start();
            }
        }

        public void Destroy()
        {
            foreach (Scheduler item in Map.Values)
            {
                item.Stop();
            }
        }

    }

}
