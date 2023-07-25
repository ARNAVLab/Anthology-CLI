using Microsoft.AspNetCore.Mvc;
using Anthology.Models;
using System.Text.Json;

namespace Anthology.Controllers
{
    /// <summary>
    /// Manages agent display.
    /// </summary>
    public class AgentController : Controller
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
        /// Serializes all agents and returns string.
        /// </summary>
        /// <returns>String representation of all serialized agents.</returns>
        public string Print()
        {
            string test = World.ReadWrite.SerializeAllAgents();
            return test;
        }
    }
}
