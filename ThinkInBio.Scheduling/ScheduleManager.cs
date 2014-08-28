using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.Scheduling
{

    public class ScheduleManager
    {

        internal Dictionary<string, ScheduleScheme> Map { get; set; }

        public IEnumerable<ScheduleScheme> SchemeList
        {
            get
            {
                List<ScheduleScheme> list = new List<ScheduleScheme>(Map.Values);
                list.Sort(new Comparison<ScheduleScheme>(
                    (e1, e2) =>
                    {
                        return e1.Name.CompareTo(e2.Name);
                    }));
                return list;
            }
        }

        internal ScheduleManager()
        {
            Map = new Dictionary<string, ScheduleScheme>();
        }

        public void Add(ScheduleScheme scheme)
        {
            if (scheme == null)
            {
                throw new ArgumentNullException();
            }
            if (string.IsNullOrWhiteSpace(scheme.Name))
            {
                scheme.Name = new Guid().ToString();
            }
            if (!Map.ContainsKey(scheme.Name))
            {
                Map.Add(scheme.Name, scheme);
            }
        }

        public ScheduleScheme Get(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException();
            }
            if (Map.ContainsKey(name))
            {
                return Map[name];
            }
            else
            {
                return null;
            }
        }

        public void Remove(ScheduleScheme scheduler)
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
            foreach (ScheduleScheme item in Map.Values)
            {
                if (item.IsAutoStart)
                {
                    item.Start();
                }
            }
        }

        public void Destroy()
        {
            foreach (ScheduleScheme item in Map.Values)
            {
                item.Stop();
            }
        }

    }

}
