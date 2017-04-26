using System.Linq;
using System.Web.Mvc;
using UMovies.Data;
using UMovies.Web.Models;

namespace UMovies.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var entities = new UMoviesEntities();
            var mediaCount = entities.Movies.Count();
            var searchModel = new IndexViewModel { MediaCount = mediaCount };
            return View(searchModel);
        }

        [HttpPost]
        public ActionResult Index(IndexViewModel indexViewModel)
        {
            return RedirectToAction("Search", new { searchText = indexViewModel.SearchText});
        }

        public ActionResult Search(string searchText)
        {
            var searchResults = new SearchResultViewModel();
            var entities = new UMoviesEntities();
            searchResults.Movies =
                entities.Movies.Where(m => m.Name.ToLower().StartsWith(searchText.ToLower())).Select(m => new MovieViewModel { Name = m.Name, FilePath = m.FilePath }).ToList();
            searchResults.ResultCount = searchResults.Movies.Count();
            searchResults.SearchText = searchText;

            return View(searchResults);
        }

        [HttpPost]
        public ActionResult Search(SearchResultViewModel searchResults)
        {
            var entities = new UMoviesEntities();
            searchResults.Movies =
                entities.Movies.Where(m => m.Name.ToLower().StartsWith(searchResults.SearchText.ToLower())).Select(m => new MovieViewModel{ Name =  m.Name, FilePath = m.FilePath }).ToList();
            searchResults.ResultCount = searchResults.Movies.Count();
            searchResults.SearchText = searchResults.SearchText;

            return View(searchResults);
        }

        public ActionResult All()
        {
            var entities = new UMoviesEntities();
            var movies = entities.Movies.Select(m => new MovieViewModel { Name = m.Name, FilePath = m.FilePath.Replace("\\", "\\\\") });
            return View(movies);
        }

        public ActionResult Player()
        {
            return View();
        }
    }
}