using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Accessible.Models;
using Accessible.Core.DTOs;
using Accessible.Core.Repositories;
using System.Linq;

namespace Accessible.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var repo = new CoreRepository();
            var model = new MapViewModel
            {
                Locations = repo.Get(new Rectangle(-34.5, -33, 151, 152))
            };

            return View(model);
        }

        public IActionResult AddLocation()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddLocation(Location location)
        {
            var repo = new CoreRepository();
            repo.Add(location);
            return Index();
        }

        [HttpPost]
        public ActionResult<Location[]> GetLocations([FromBody] Rectangle rectangle)
        {
            return new CoreRepository().Get(rectangle).ToArray();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
