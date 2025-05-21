using Microsoft.Extensions.Hosting;
using Serilog;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DocumentManagementAPI.ExceptionHandling
{
    public class ExceptionHandlingMiddleWare(RequestDelegate next , ILogger<ExceptionHandlingMiddleWare> logger, IHostEnvironment env)
    {
        //requestDelegate : the next part of the request pipeline.
        //ILogger: lets us log errors.
        //IHostEnvironment env: tells us if we’re in development or production.

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context); // Continue to controller
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unhanded exception occurred");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = ex switch
                {
                    // I can add more than one Exception handler here
                    NotFoundException => StatusCodes.Status404NotFound,
                    BadRequestException => StatusCodes.Status400BadRequest,
                    UnauthorizedException => StatusCodes.Status401Unauthorized,
                    _ => StatusCodes.Status500InternalServerError //here send 500 if there another status
                };
                //this below will be as response json 
                var respons = new
                {
                    statuscode= context.Response.StatusCode,
                    error= env.IsDevelopment() ? ex.Message : "An error occurred",
                    type = ex.GetType().Name
                };
                await context.Response.WriteAsJsonAsync(respons);
            }
        }
    }
}
