namespace Day4_Day3Refactoring.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //capture the start time of the request
            var startTime = DateTime.UtcNow;
            var endTime = DateTime.UtcNow;

            // log the http method and path of the incoming request
            Console.WriteLine($"Incoming request: {context.Request.Method} {context.Request.Path}");

            // call the next middleware in the pipeline
            await _next(context);

            //log how long it took to process the request

                
           var duration = endTime - startTime;
           Console.WriteLine($"Request processed in {duration.TotalMilliseconds} ms");
        }

    }
}
