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
        ScheduleSchemeTO[] GetSchemeList();

        [OperationContract]
        [WebInvoke(Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "/schedule/{name}/{state}/",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json)]
        ScheduleSchemeTO ChangeSchemeState(string name, string state);

    }

    public class ScheduleSchemeTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ScheduleState State { get; set; }
        public string LastStartTime { get; set; }
        public string LastStopTime { get; set; }

        public int DelayedSeconds { get; set; }
        public int RepeatSeconds { get; set; }
        public int RepeatCount { get; set; }
        public string Expression { get; set; }
    }

}
