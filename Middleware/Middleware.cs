using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Library.Middleware;
//This class is used to handle unhandled exceptions.
public class Middleware
{
    //This is the next middleware.
    private readonly RequestDelegate next;
    //This is the logger instance for logging.
    private readonly ILogger<Middleware> logger;
    //This is the cinstructor for setting the logger and next middleware.
    public Middleware(RequestDelegate next, ILogger<Middleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }
    //This method is called every time there is a HTTP request.
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception occured");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new
            {
                Message = "An unexpected error occured"
            });
        }
    }
}