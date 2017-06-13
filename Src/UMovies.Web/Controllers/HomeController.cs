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
                .Select(m => new MovieViewModel
                {
                    Id = m.Id,
                    Year = m.Year,
                    Name = m.Name,
                    MovieFolder = m.MovieFolder,
                    MovieFile = m.MovieFile
                }).OrderBy(m => m.Name);

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
                .Select(m => new MovieViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    MovieFolder = m.MovieFolder,
                    MovieFile = m.MovieFile
                })
                .ToList();
            searchViewModel.ResultCount = searchViewModel.Movies.Count();

            return View(searchViewModel);
        }

        public ActionResult Player()
        {
            return View();
        }

        public ActionResult Show(int id)
        {
            var entities = new UMoviesEntities();
            var movie = entities.Movies.Where(m => m.Id == id).Select(m => new MovieViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Sinopsis = m.Sinopsis,
                Year = m.Year,
                MovieFile = m.MovieFile,
                MovieFolder = m.MovieFolder,
                ThumbnailFile = m.ThumbnailFile
            }).FirstOrDefault();

            return View(movie);
        }
    }
}