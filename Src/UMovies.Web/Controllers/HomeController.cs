using System.Linq;
using System.Web.Mvc;
using CrossCutting.Core.Logging;
using UMovies.Application;
using UMovies.Web.Models;

namespace UMovies.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogService _logService;
        private readonly IMovieService _movieService;

        public HomeController(ILogService logService, IMovieService movieService)
        {
            _logService = logService;
            _movieService = movieService;
        }

        public ActionResult Index()
        {
            var movies = _movieService.GetAll()
                .Select(m => new MovieViewModel
                {
                    Id = m.Id,
                    Year = m.Year,
                    Name = m.Name,
                    MovieFolder = m.MovieFolder,
                    MovieFiles = m.MovieFiles.Select(v => v.FileName).ToList()
                }).OrderBy(m => m.Name);

            return View(movies);
        }

        public ActionResult Show(int id)
        {
            var movie = _movieService.GetById(id);

            if (movie != null)
            {
                var movieViewModel = new MovieViewModel
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    Sinopsis = movie.Sinopsis,
                    Year = movie.Year,
                    MovieFiles = movie.MovieFiles.Select(v => v.FileName).ToList(),
                    MovieFolder = movie.MovieFolder,
                    ThumbnailFile = movie.ThumbnailFileName
                };

                return View(movieViewModel);
            }

            _logService.Warn($"Movie with if {id} not found.");
            return RedirectToAction("Index");
        }
    }
}