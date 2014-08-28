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

        public ScheduleSchemeTO[] GetSchemeList()
        {
            try
            {
                List<ScheduleSchemeTO> list = new List<ScheduleSchemeTO>();
                IEnumerable<ScheduleScheme> schemeList = ScheduleManager.SchemeList;
                foreach (ScheduleScheme scheme in schemeList)
                {
                    list.Add(BuildScheduleSchemeTO(scheme));
                }
                return list.ToArray();
            }
            catch (BusinessLayerException ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public ScheduleSchemeTO ChangeSchemeState(string name, string state)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new WebFaultException<string>("name", HttpStatusCode.BadRequest);
            }
            bool stateBool = false;
            try
            {
                stateBool = Convert.ToBoolean(state);
            }
            catch
            {
                throw new WebFaultException<string>("state", HttpStatusCode.BadRequest);
            }
            ScheduleScheme scheme = ScheduleManager.Get(name);
            if (scheme == null)
            {
                throw new WebFaultException(HttpStatusCode.NotFound);
            }
            try
            {
                if (stateBool)
                {
                    scheme.Start();
                }
                else
                {
                    scheme.Stop();
                }
                return BuildScheduleSchemeTO(scheme);
            }
            catch (BusinessLayerException ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        private ScheduleSchemeTO BuildScheduleSchemeTO(ScheduleScheme scheme)
        {
            ScheduleSchemeTO to = new ScheduleSchemeTO();
            to.Name = scheme.Name;
            to.Description = scheme.Description;
            to.State = scheme.State;
            to.LastStartTime = scheme.LastStartTime.HasValue ? scheme.LastStartTime.Value.ToString() : "";
            to.LastStopTime = scheme.LastStopTime.HasValue ? scheme.LastStopTime.Value.ToString() : "";

            ISchedule schedule = scheme.Schedule;
            to.DelayedSeconds = schedule.DelayedSeconds;
            to.RepeatSeconds = schedule.RepeatSeconds;
            to.RepeatCount = schedule.RepeatCount;
            to.Expression = schedule.Expression;

            return to;
        }

    }

}
