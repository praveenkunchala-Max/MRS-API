using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Augadh.SecurityMonitoring.Common.Entities;

namespace Augadh.SecurityMonitoring.API.Handlers
{
    public class ObjectResponseHandler
    {
        public static IActionResult GetObjectResponse(object resultData, Error error = null)
        {
            return new ObjectActionResult<object>(BuildObjectResponse<object>(resultData, error));
        }

        public static IActionResult GetObjectResponse<T>(ObjectResponse<T> objResponse)
        {
            ObjectResponse<T> obectResponse2 = new ObjectResponse<T>();
            obectResponse2.Response = objResponse.Response ?? (objResponse.Error == null ? "Success" : "Error");
             obectResponse2.ResponseCode = objResponse.ResponseCode ?? (objResponse.Error == null ? "200" : "404");
            obectResponse2.ResponseMessage = objResponse.ResponseMessage ?? (objResponse.Error == null ? "Success" : "Error");
            obectResponse2.ResponseData = objResponse.ResponseData;
            obectResponse2.Error = objResponse.Error;
            return new ObjectActionResult<T>(new ObjectResponse<T>()
            {
                Response = objResponse.Response ?? (objResponse.Error == null ? "Success" : "Error"),
                ResponseCode = objResponse.ResponseCode ?? (objResponse.Error == null ? "200" : "404"),
                ResponseMessage = objResponse.ResponseMessage ?? (objResponse.Error == null ? "Success" : "Error"),
                ResponseData = objResponse.ResponseData,
                ResponseValue = objResponse.ResponseValue,
                Error = objResponse.Error
            });
        }

        private static ObjectResponse<T> BuildObjectResponse<T>(T resultData, Error error)
        {
            if (error != null)
            {
                return new ObjectResponse<T>() { Error = error, ResponseMessage = "Error", ResponseCode = error.ErrorCode };
            }
            else
            {
                return new ObjectResponse<T>() { Response = "Success", ResponseData = resultData, ResponseCode = "200", ResponseMessage = "Success" };
            }
        }
    }
    public class ObjectActionResult<T> : IActionResult
    {
        private readonly ObjectResponse<T> _response;

        public ObjectActionResult(ObjectResponse<T> response)
        {
            _response = response;
        }


        public async Task ExecuteResultAsync(ActionContext context)
        {
            string status = "";
            var objectResult = new ObjectResult(_response)
            {
                StatusCode = _response.Error != null ? StatusCodes.Status500InternalServerError : StatusCodes.Status200OK
                
            };
            var st = objectResult;
            await objectResult.ExecuteResultAsync(context);
        }
    }
}
