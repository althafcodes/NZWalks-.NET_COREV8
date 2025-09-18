using System.Net;

namespace NZWalks.MiddleWare
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> logger1;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger1, RequestDelegate next)
        {
            this.logger1 = logger1;
            this.next = next;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString();
                //log exception
                logger1.LogError(ex, $" {errorId} :  {ex.Message}"); ;

                //return custom error resp
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong !"
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
