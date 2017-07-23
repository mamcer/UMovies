using UMovies.Core;

namespace UMovies.Application
{
    public interface IMovieService
    {
        Movie GetByName(string name);

        Movie CreateMovie(Movie movie);

        Movie UpdateMovie(Movie movie);
    }
}