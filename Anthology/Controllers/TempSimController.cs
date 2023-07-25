using Anthology.Models;
using Microsoft.AspNetCore.Mvc;

namespace Anthology.Controllers
{
    /// <summary>
    /// Temporary simulation controller for handling simulation execution.
    /// </summary>
    public class TempSimController : Controller
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
        /// Executes simulation based on given amount of steps and returns state info.
        /// </summary>
        /// <param name="id">Number of steps to execute.</param>
        /// <returns>String representation of simulation state.</returns>
        public string Step(int id)
        {
            ExecutionManager.RunSim(id);
            string state = "Time: " + World.Time + "\n\n" + World.ReadWrite.SerializeAllAgents();
            return state;
        }
    }
}
