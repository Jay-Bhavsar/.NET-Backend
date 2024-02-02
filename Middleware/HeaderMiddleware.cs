namespace ENTITYAPP.Middleware
{
    public class HeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public HeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string customHeaderValue = context.Request.Headers["X-Custom-Header"];

            if(customHeaderValue == "MySecretValue")
            {
                context.Response.Headers.Add("X-Additional-Header", "Hello from Middleware!");

            }

            await _next(context);
        }


    }
}
