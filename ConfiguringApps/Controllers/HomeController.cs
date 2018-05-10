using ConfiguringApps.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace ConfiguringApps.Controllers
{
    public class HomeController : Controller
    {
        private UptimeService uptime;
        private ILogger<HomeController> logger;

        public HomeController(UptimeService up, ILogger<HomeController> log)
        {
            uptime = up;
            logger = log;
        }

        
        #region Depricated - 5/10/2018 Introduced ILogger interface
        /*
        public HomeController(UptimeService up) => uptime = up;
        */
        #endregion


        public ViewResult Index(bool throwException = false)
        {
            logger.LogDebug($"Handled {Request.Path} at uptime {uptime.Uptime}");

            if(throwException)
            {
                throw new System.NullReferenceException();
            }
            return View(new Dictionary<string, string>
            {
                ["Message"] = "This is the Index action",
                ["Uptime"] = $"{uptime.Uptime}ms"
            });
        }

        #region Depricated - 5/10/2018 Introduce Exception Handling
        /*
         public ViewResult Index() => View(new Dictionary<string, string>
{
    ["Message"] = "This is the Index action",
    ["Uptime"] = $"{uptime.Uptime}ms"
});
        */
        #endregion

        public ViewResult Error() => View(nameof(Index),
            new Dictionary<string, string>
            {
                ["Message"] = "This is the Error action"
            });

    }
}
