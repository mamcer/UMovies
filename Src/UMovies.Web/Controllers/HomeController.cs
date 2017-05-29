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
            var movies = entities.Movies
                .Select(m => new MovieViewModel { Name = m.Name, FilePath = m.MovieFilePath.Replace("\\", "\\\\") });

            return View(movies);
        }

        public ActionResult Search()
        {
            var entities = new UMoviesEntities();
            var mediaCount = entities.Movies.Count();
            var searchViewModel = new SearchViewModel
            {
                MediaCount = mediaCount,
                ResultCount = 0
            };

            return View(searchViewModel);
        }

        [HttpPost]
        public ActionResult Search(SearchViewModel searchViewModel)
        {
            var entities = new UMoviesEntities();
            var mediaCount = entities.Movies.Count();
            searchViewModel.MediaCount = mediaCount;
            searchViewModel.Movies =
                entities.Movies
                .Where(m => m.Name.ToLower().StartsWith(searchViewModel.SearchText.ToLower()))
                .Select(m => new MovieViewModel{ Name =  m.Name, FilePath = m.MovieFilePath })
                .ToList();
            searchViewModel.ResultCount = searchViewModel.Movies.Count();

            return View(searchViewModel);
        }

        public ActionResult Player()
        {
            return View();
        }
    }
}