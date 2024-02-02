using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace ENTITYAPP.Filters
{
    public class LogAttribute : ActionFilterAttribute
    {
        public LogAttribute()
        {

        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            Trace.WriteLine(string.Format("Action Method {0} executing at {1}", actionContext.ActionDescriptor.DisplayName, DateTime.Now.ToShortDateString()), "Web API Logs");
        }

        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            Trace.WriteLine(string.Format("Action Method {0} executed at {1}", actionExecutedContext.ActionDescriptor.DisplayName, DateTime.Now.ToShortDateString()), "Web API Logs");
        }
    }
}
