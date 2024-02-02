using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ENTITYAPP.ApiDocumentation
{
    public static class SwaggerDocumentation
    {
        // Include 'SecurityScheme' to use JWT Authentication
        public static OpenApiSecurityScheme jwtSecurityScheme = new OpenApiSecurityScheme
        {
            BearerFormat = "JWT",
            Name = "JWT Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

            Reference = new OpenApiReference
            {
                Id = JwtBearerDefaults.AuthenticationScheme,
                Type = ReferenceType.SecurityScheme
            }
        };

    }

    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if ( context.MethodInfo.GetCustomAttributes(true).Any(x => x is AuthorizeAttribute) ||
                (context.MethodInfo.DeclaringType?.GetCustomAttributes(true).Any(x => x is AuthorizeAttribute) ?? false))
            {
                operation.Security = new List<OpenApiSecurityRequirement>
                    {
                        new OpenApiSecurityRequirement
                        {
                            { SwaggerDocumentation.jwtSecurityScheme, Array.Empty<string>() }
                        }
                    };
            }
        }
    }

}
