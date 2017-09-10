using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatOrderingService
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomMiddleware> _logger;
        private readonly EnvironmentInfo _options;

        public CustomMiddleware(RequestDelegate next, ILogger<CustomMiddleware> logger, IOptions<EnvironmentInfo> options)
        {
            _next = next;
            _logger = logger;
            _options = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            //
            _logger.LogInformation(_options.WelcomeMessage);
            await this._next(context);
            _logger.LogInformation("End");

            //
        }

    }
}
