//using Microsoft.AspNetCore.Http;
//using System.Threading.Tasks;

//public class JwtMiddleware
//{
//    private readonly RequestDelegate _next;

//    public JwtMiddleware(RequestDelegate next)
//    {
//        _next = next;
//    }

//    public async Task Invoke(HttpContext context)
//    {
//        if(context.Request.Headers.TryGetValue("Authorization",out var authorizationHeader))
//        {
//            var token = ExtractTocken(authorizationHeader);

//            //if (jwtService.ValidateToken(token))
//            //{
//               // await _next(context);
//               // return;
//           // }
//        }

//        context.Response.StatusCode = 401;
//        await context.Response.WriteAsync("Unauthorized");

//    }


//    private string ExtractTocken(string authorizationHeader)
//    {
//        if (authorizationHeader.StartsWith("Bearer "))
//        {
//            return authorizationHeader.Substring("Bearer".Length);
//        }

       
//        return string.Empty;
//    }

//}

//public static class JwtAuthorizationMiddlewareExtensions
//{
//    public static IApplicationBuilder UseJwtAuthorizationMiddleware(this IApplicationBuilder builder)
//    {
//        return builder.UseMiddleware<JwtMiddleware>();
//    }
//}