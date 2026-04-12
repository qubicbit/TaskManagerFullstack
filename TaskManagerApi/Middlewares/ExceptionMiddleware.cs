namespace TaskManagerApi.Middlewares
{//global exception middleware Middleware för att fånga upp och hantera alla ohanterade (oväntade fel) undantag i applikationen
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    status = 500,
                    error = "Internal Server Error",
                    message = ex.Message
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
