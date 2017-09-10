using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;

namespace MatOrderingService.Services.Swagger
{
    public class SwaggerAuthorizationHeaderParameter : IOperationFilter
    {
        private readonly string _authSchemaName;

        public SwaggerAuthorizationHeaderParameter(string authSchemaName)
        {
            _authSchemaName = authSchemaName;
        }

        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter
            {
                In = "header",
                Name = "Authorization",
                Description = "Auth token.",
                Required = true,
                Type = "string",
                Default = $"{_authSchemaName} prodkey"
            });
        }
    }
}
