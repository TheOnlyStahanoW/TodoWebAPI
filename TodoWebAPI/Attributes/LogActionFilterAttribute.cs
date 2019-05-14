using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace TodoWebAPI.Attributes
{ 
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        private readonly bool logRequestURL;
        private readonly bool logRequestMethod;
        private readonly bool logRequestParams;
        private readonly bool logRequestHeaders;
        private readonly bool logRequestBody;
        private readonly bool logResponseStatusCode;
        private readonly bool logResponseTime;
        private DateTime startDateTime;

        public LogActionFilterAttribute(bool logRequestURL, bool logRequestMethod, bool logRequestParams, bool logRequestHeaders, bool logRequestBody, bool logResponseStatusCode, bool logResponseTime)
        {
            this.logRequestURL = logRequestURL;
            this.logRequestMethod = logRequestMethod;
            this.logRequestParams = logRequestParams;
            this.logRequestHeaders = logRequestHeaders;
            this.logRequestBody = logRequestBody;
            this.logResponseStatusCode = logResponseStatusCode;
            this.logResponseTime = logResponseTime;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                filterContext.HttpContext.Request.Query.Append(new KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues>("startDateTime", new Microsoft.Extensions.Primitives.StringValues(DateTime.Now.ToString())));

                if (logRequestURL)
                {
                    Log("OnActionExecuting - Request URL", filterContext.RouteData, filterContext.HttpContext.Request.Path.ToString());
                }

                if (logRequestMethod)
                {
                    Log("OnActionExecuting - Request Method", filterContext.RouteData, filterContext.HttpContext.Request.Method);
                }

                if (logRequestParams)
                {
                    var queryString = string.Join("&", filterContext.HttpContext.Request.Query.ToDictionary(x => x.Key, x => string.Join("," , x.Value)).Select(x => string.Format("{0} = {1}", x.Key, x.Value)));
                    Log("OnActionExecuting - Request Params", filterContext.RouteData, queryString);
                }

                if (logRequestHeaders)
                {
                    var queryString = string.Join("&", filterContext.HttpContext.Request.Headers.ToDictionary(x => x.Key, x => string.Join(",", x.Value)).Select(x => string.Format("{0} = {1}", x.Key, x.Value)));
                    Log("OnActionExecuting - Request Headers", filterContext.RouteData, queryString);
                }

                if (logRequestBody)
                {
                    using (var memoryStreamForCopy = new MemoryStream())
                    {
                        filterContext.HttpContext.Request.Body.CopyTo(memoryStreamForCopy);
                        using (StreamReader streamReader = new StreamReader(memoryStreamForCopy))
                        {
                            Log("OnActionExecuting - Request Body", filterContext.RouteData, streamReader.ReadToEnd());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log("OnActionExecuting - Exception occured", null, ex.Message);
            }
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            try
            {
                if (logResponseStatusCode)
                {
                    Log("OnResultExecuted - Response StatusCode", filterContext.RouteData, filterContext.HttpContext.Response.StatusCode.ToString());
                }

                if (logResponseTime)
                {
                    DateTime startDateTime = DateTime.Parse(filterContext.HttpContext.Request.Query.FirstOrDefault(x => x.Key == "startDateTime").Value.FirstOrDefault());
                    Log("OnResultExecuted - Response Time", filterContext.RouteData, (DateTime.Now - startDateTime).TotalMilliseconds.ToString() + "ms");
                }
            }
            catch (Exception ex)
            {
                Log("OnResultExecuted - Exception occured", null, ex.Message);
            }
        }

        private void Log(string methodName, RouteData routeData, string logText)
        {
            string message;

            if (routeData == null)
            {
                var controllerName = routeData.Values["controller"];
                var actionName = routeData.Values["action"];
                message = String.Format("{0} controller:{1} action:{2} logText:{3}", methodName, controllerName, actionName, logText);
            }
            else
            {
                message = String.Format("{0} logText:{1}", methodName, logText);
            }
            
            Debug.WriteLine(message, "Action Filter Log");
        }

    }
}
