using Microsoft.AspNetCore.Mvc;
using stackoverflow_recommendation_system.Models;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data.SqlClient;

namespace stackoverflow_recommendation_system.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger,IConfiguration config)
        {
            this.configuration = config;
            _logger = logger;
        }
        
        [HttpPost]
        public ActionResult SubmitForm(search_prompt search)
        {
            // You can now access the form data through the `person` object.
            // For example:
            ViewBag.Message = $"Search: {search.searchQuery}";
            return View("Index");
        }
        
        [HttpGet]
        public ActionResult FetchData(int pageNumber)
        {
            string? connectionString = configuration.GetConnectionString("DefaultConnection");
            if (connectionString == null)
            {
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            }

            int pageSize = 10; // Set the number of records per page
            using (var db = new SqlConnection(connectionString))
            {
                var offset = pageSize * (pageNumber - 1);
                var sql = @"SELECT * FROM Badges ORDER BY Id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";
                var data = db.Query<dynamic>(sql, new { Offset = offset, PageSize = pageSize }).ToList();
                return Json(data);
            }
        }
        
        
        public IActionResult Index()
        {   
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
