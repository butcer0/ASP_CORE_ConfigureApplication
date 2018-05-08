using ConfiguringApps.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ConfiguringApps.Controllers
{
    public class HomeController : Controller
    {
        private UptimeService uptime;

        public HomeController(UptimeService up) => uptime = up;

        public ViewResult Index() => View(new Dictionary<string, string>
        {
            ["Message"] = "This is the Index action",
            ["Uptime"] = $"{uptime.Uptime}ms"
        });
    }
}
