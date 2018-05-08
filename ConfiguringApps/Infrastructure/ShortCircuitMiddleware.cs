using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfiguringApps.Infrastructure
{
    public class ShortCircuitMiddleware
    {
        private RequestDelegate nextDelegate;

        public ShortCircuitMiddleware(RequestDelegate next) => nextDelegate = next;

        public async Task Invoke(HttpContext httpContext)
        {
            //Erik - 5/8/2018 User-Agent is header that identifies the browser being used.
            //Erik - 5/8/2018 This states, if using a browser containing 'edge' short-circuit and return 403 forbidden
            if (httpContext.Request.Headers["User-Agent"].Any(h => h.ToLower().Contains("safari")))
            {
                httpContext.Response.StatusCode = 403;
            }
            else
            {
                await nextDelegate.Invoke(httpContext);
            }
        }
    }
}
