using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanReadableCodeService
{
    public class HumanReadableCodeMiddleware
    {
        private readonly RequestDelegate _next;
        public readonly EnvironmentInfo _options;

        public HumanReadableCodeMiddleware(RequestDelegate next, IOptions<EnvironmentInfo> optionsAccessor)
        {
            _next = next;
            _options = optionsAccessor.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            //Code

            //Call the next delegate
            await this._next(context);

            //or Code
        }
    }
}
