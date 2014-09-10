using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.ServiceModel.Web;

using ThinkInBio.Common.Exceptions;
using ThinkInBio.Common.ExceptionHandling;
using ThinkInBio.CommonApp.BLL;
using R = ThinkInBio.CommonApp.WSL.Properties.Resources;

namespace ThinkInBio.CommonApp.WSL.Impl
{
    public class TimeStampWcfService : ITimeStampWcfService
    {

        internal ITimeStampService TimeStampService { get; set; }
        internal IExceptionHandler ExceptionHandler { get; set; }

        public DateTime? GetTimeStamp()
        {
            try
            {
                return TimeStampService.NextTimeStamp();
            }
            catch (BusinessLayerException ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                throw new WebFaultException(HttpStatusCode.InternalServerError);
            }
        }

        public DateTime? GetLocalTimeStamp()
        {
            return DateTime.Now;
        }

    }
}
