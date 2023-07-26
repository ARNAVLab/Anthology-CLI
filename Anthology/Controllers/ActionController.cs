using Anthology.Models;
using Microsoft.AspNetCore.Mvc;

namespace Anthology.Controllers
{
    /// <summary>
    /// Manages action display.
    /// </summary>
    public class ActionController : Controller
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
        /// Serializes all actions and returns string.
        /// </summary>
        /// <returns>String representation of all serialized actions.</returns>
        public string Print()
        {
            string test = World.ReadWrite.SerializeAllActions();
            return test;
        }
    }
}
