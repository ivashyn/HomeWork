using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatOrderingService.Filters
{
    public class MyExceptionFilter : ExceptionFilterAttribute
    {
        private ILogger<string> _logger;

        public MyExceptionFilter(ILogger<string> logger)
        {
            _logger = logger;
        }



        public override void OnException(ExceptionContext context)
        {
            _logger.LogInformation("FARAFA => " + context.Exception.ToString());
            context.Result = new BadRequestResult();
            context.ExceptionHandled = true;
        }
    }
}
