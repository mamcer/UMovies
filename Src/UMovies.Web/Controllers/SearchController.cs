using System.Linq;
using System.Web.Mvc;
using CrossCutting.Core.Logging;
using UMovies.Application;
using UMovies.Web.Models;

namespace UMovies.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogService _logService;
        private readonly IMovieService _movieService;

        public SearchController(ILogService logService, IMovieService movieService)
        {
            _logService = logService;
            _movieService = movieService;
        }

        public ActionResult Index()
        {
            var mediaCount = _movieService.GetMovieCount();
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
            var searchResults = _movieService.Search(searchViewModel.SearchText);

            searchViewModel.MediaCount = _movieService.GetMovieCount();
            searchViewModel.Movies = searchResults.Select(m => new MovieViewModel
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