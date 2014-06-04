using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.Common.ServiceModel
{


    public class ServiceResponse
    {

        public ServiceResponseCode Code { get; private set; }

        public string Message { get; private set; }

        protected ServiceResponse() { }

        protected ServiceResponse(ServiceResponseCode code)
        {
            this.Code = code;
        }

        protected ServiceResponse(ServiceResponseCode code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public static ServiceResponse BuildNormal()
        {
            return new ServiceResponse(ServiceResponseCode.Normal);
        }

        public static ServiceResponse Build(ServiceResponseCode code, string message)
        {
            return new ServiceResponse(code, message);
        }

    }

    public class ServiceResponse<T>
    {

        public ServiceResponseCode Code { get; private set; }

        public string Message { get; private set; }

        public T Result { get; private set; }

        protected ServiceResponse() { }

        protected ServiceResponse(ServiceResponseCode code, T result)
        {
            this.Code = code;
            this.Result = result;
        }

        protected ServiceResponse(ServiceResponseCode code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public static ServiceResponse<T> BuildResult(T result)
        {
            return new ServiceResponse<T>(ServiceResponseCode.Normal, result);
        }

        public static ServiceResponse<T> Build(ServiceResponseCode code, string message)
        {
            return new ServiceResponse<T>(code, message);
        }

    }
}
