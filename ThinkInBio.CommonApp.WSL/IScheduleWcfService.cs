using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;

using ThinkInBio.Scheduling;

namespace ThinkInBio.CommonApp.WSL
{

    [ServiceContract]
    public interface IScheduleWcfService
    {

        [OperationContract]
        [WebGet(UriTemplate = "/schedule/",
            RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        SchedulerSummary[] GetSchedulerList();

    }

    public class SchedulerSummary
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SchedulerState State { get; set; }
        public string LastStartTime { get; set; }
        public string LastStopTime { get; set; }
    }

}
