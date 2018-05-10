using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfiguringApps.Infrastructure
{
    public class ErrorMiddleware
    {
        private RequestDelegate nextDelegate;

        public ErrorMiddleware(RequestDelegate next)
        {
            nextDelegate = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await nextDelegate.Invoke(httpContext);

            if (httpContext.Response.StatusCode == 403
                && httpContext.Request.Headers["User-Agent"].Any())
            {
                //await httpContext.Response.WriteAsync("Chrome not supported", Encoding.UTF8);
                string curBrowser = httpContext.Request.Headers["User-Agent"].ToString();
                await httpContext.Response.WriteAsync($"{curBrowser} not supported", Encoding.UTF8);
            }
            else if(httpContext.Response.StatusCode == 404)
            {
                await httpContext.Response.WriteAsync("No content middleware response", Encoding.UTF8);
            }
        }

    }
}
