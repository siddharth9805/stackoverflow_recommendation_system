using Microsoft.AspNetCore.Mvc;
using stackoverflow_recommendation_system.Models;
using stackoverflow_recommendation_system.Services;
using System.Diagnostics;

namespace stackoverflow_recommendation_system.Controllers
{
    public class HomeController : Controller
    {
        private readonly SearchResultService _searchResultService;

        public HomeController(SearchResultService searchResultService)
        {
            this._searchResultService = searchResultService;
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

        public async Task<IActionResult> Search(string searchKey, int pageNumber)
        {
            var users = await this._searchResultService.GetSearchResults(searchKey, pageNumber);
            return Ok(users);
        }
    }
}
