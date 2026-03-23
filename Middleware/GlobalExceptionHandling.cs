namespace Day4_Day3Refactoring.Middleware
{
    public class GlobalExceptionHandling
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        public GlobalExceptionHandling(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

            }catch(Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                string message="";
                string deetails="";
                if (_env.IsDevelopment())
                {
                    message = "Development error";
                    deetails = ex.Message;
                }
                else
                {
                    message = "Internal server error";
                    deetails = "Please contact support";
                }
               Console.WriteLine($"Error: {message}: {deetails}");
                context.Response.WriteAsJsonAsync(new {error=message, details=deetails });

            }
        }
    }
}
