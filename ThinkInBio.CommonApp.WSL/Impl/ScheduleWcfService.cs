using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.ServiceModel.Web;

using ThinkInBio.Common.Exceptions;
using ThinkInBio.Common.ExceptionHandling;
using ThinkInBio.Scheduling;

namespace ThinkInBio.CommonApp.WSL.Impl
{

    public class ScheduleWcfService : IScheduleWcfService
    {

        internal ScheduleManager ScheduleManager { get; set; }
        internal IExceptionHandler ExceptionHandler { get; set; }

        public SchedulerSummary[] GetSchedulerList()
        {
            try
            {
                List<SchedulerSummary> list = new List<SchedulerSummary>();
                IEnumerable<Scheduler> schedulerList = ScheduleManager.SchedulerList;
                foreach (Scheduler scheduler in schedulerList)
                {
                    SchedulerSummary summary = new SchedulerSummary();
                    summary.Name = scheduler.Name;
                    summary.Description = scheduler.Description;
                    summary.State = scheduler.State;
                    summary.LastStartTime = scheduler.LastStartTime.HasValue? scheduler.LastStartTime.Value.ToString(): "";
                    summary.LastStopTime = scheduler.LastStopTime.HasValue ? scheduler.LastStopTime.Value.ToString() : "";
                    list.Add(summary);
                }
                return list.ToArray();
            }
            catch (BusinessLayerException ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

    }

}
