using System.Linq;
using System.Web.Mvc;
using UMovies.Data;
using UMovies.Web.Models;

namespace UMovies.Web.Controllers
{
    public class SearchController : Controller
    {
        public ActionResult Index()
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
        public ActionResult Index(SearchViewModel searchViewModel)
        {
            var entities = new UMoviesEntities();
            var mediaCount = entities.Movies.Count();
            searchViewModel.MediaCount = mediaCount;
            searchViewModel.Movies =
                entities.Movies
                    .Where(m => m.Name.ToLower().StartsWith(searchViewModel.SearchText.ToLower()))
                    .Select(m => new MovieViewModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        MovieFolder = m.MovieFolder,
                        MovieFiles = m.MovieFiles.Select(v => v.FileName).ToList()
                    })
                    .ToList();
            searchViewModel.ResultCount = searchViewModel.Movies.Count();

            return View(searchViewModel);
        }

    }
}