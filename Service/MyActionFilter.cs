using Contract;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ENTITYAPP.Service
{
    public class MyActionFilter : ActionFilterAttribute
    {
       
        private readonly IloggerManager _logger;

        public MyActionFilter(IloggerManager logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInfo($"Action Method{context.ActionDescriptor.DisplayName} executing at {DateTime.Now.ToShortDateString()}");

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInfo($"Action Method{context.ActionDescriptor.DisplayName} executing at {DateTime.Now.ToShortDateString()}");

        }
    }
}
