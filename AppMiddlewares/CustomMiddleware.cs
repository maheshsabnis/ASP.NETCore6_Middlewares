using Net6_Middlewares.Models;
namespace Net6_Middlewares.AppMiddlewares
{
    public class ErrorInfo
    {
        public int StatusCode { get; set; } = 0;
        public string ErrorMessage { get; set; } = string.Empty;
    }


    public class ErrorHandler
    {
        private readonly RequestDelegate requestDelegate;
         
        public ErrorHandler(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;
           
        }

        public async Task InvokeAsync(HttpContext context, LoggerDbContext loggerContext)
        {
            try
            {
                await requestDelegate(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;

                var errorInfo = new ErrorInfo()
                { 
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = ex.Message
                };

                var errorLog = new ErrorLogger()
                { 
                  ErrorDetails = errorInfo.ErrorMessage,
                  LogDate = DateTime.Now
                };
                await loggerContext.ErrorLogger.AddAsync(errorLog);
                await loggerContext.SaveChangesAsync();

                await context.Response.WriteAsJsonAsync<ErrorInfo>(errorInfo);
            }
        }
    }



    public static class MiddlewareRegistrationExtension
    {
        public static void UseAppException(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ErrorHandler>();
        }
    }
}
