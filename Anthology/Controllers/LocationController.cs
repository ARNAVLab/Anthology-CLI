using Anthology.Models;
using Microsoft.AspNetCore.Mvc;

namespace Anthology.Controllers
{
    /// <summary>
    /// Manages location display.
    /// </summary>
    public class LocationController : Controller
    {
        /// <summary>
        /// Default view of controller.
        /// </summary>
        /// <returns>Default view of controller.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Serializes all locations and returns a string.
        /// </summary>
        /// <returns>String representation of all serialized locations.</returns>
        public string Print()
        {
            string test = World.ReadWrite.SerializeAllLocations();
            return test;
        }
    }
}
