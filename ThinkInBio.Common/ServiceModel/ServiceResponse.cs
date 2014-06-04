using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkInBio.Common.ServiceModel
{


    public class ServiceResponse
    {

        public ServiceResponseCode Code { get; set; }

        public string Message { get; set; }

        public static ServiceResponse BuildNormal()
        {
            ServiceResponse response = new ServiceResponse();
            response.Code = ServiceResponseCode.Normal;
            return response;
        }

        public static ServiceResponse Build(ServiceResponseCode code, string message)
        {
            ServiceResponse response = new ServiceResponse();
            response.Code = code;
            response.Message = message;
            return response;
        }

    }

    public class ServiceResponse<T>
    {

        public ServiceResponseCode Code { get; set; }

        public string Message { get; set; }

        public T Result { get; set; }

        public static ServiceResponse<T> BuildResult(T result)
        {
            ServiceResponse<T> response = new ServiceResponse<T>();
            response.Code = ServiceResponseCode.Normal;
            response.Result = result;
            return response;
        }

        public static ServiceResponse<T> Build(ServiceResponseCode code, string message)
        {
            ServiceResponse<T> response = new ServiceResponse<T>();
            response.Code = code;
            response.Message = message;
            return response;
        }

    }
}
