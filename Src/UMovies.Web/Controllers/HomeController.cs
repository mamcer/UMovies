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
                    MovieFiles = m.MovieFiles.Select(v => v.FileName).ToList()
                }).OrderBy(m => m.Name);

            return View(movies);
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
                MovieFiles = m.MovieFiles.Select(v => v.FileName).ToList(),
                MovieFolder = m.MovieFolder,
                ThumbnailFile = m.ThumbnailFileName
            }).FirstOrDefault();

            return View(movie);
        }
    }
}