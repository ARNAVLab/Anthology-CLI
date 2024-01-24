using Anthology.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Anthology.SimulationManager;
using Anthology.SimulationManager.HistoryManager;

namespace Anthology.Controllers
{
    /// <summary>
    /// Home controller of simulation. 
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Logger responsible for logging generic information.
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Initializes logger to given logger.
        /// </summary>
        /// <param name="logger">The logger to use.</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Initializes the Simulation manager.
        /// </summary>
        /// <returns>View of Index.</returns>
        public IActionResult Index()
        {
            SimManager.Init("Data/Paths.json", typeof(AnthologyRS), typeof(LyraKS), typeof(MongoHM));
            return View();
        }

        /// <summary>
        /// Privacy page.
        /// </summary>
        /// <returns>View of privacy page.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Error page containing request id and trace info.
        /// </summary>
        /// <returns>View of error (request id and trace info).</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}