using System.Collections.Generic;
using UMovies.Core;

namespace UMovies.Application
{
    public interface IMovieService
    {
        Movie GetByName(string name);

        Movie GetById(int id);

        Movie CreateMovie(Movie movie);

        Movie UpdateMovie(Movie movie);

        IEnumerable<Movie> GetAll();

        int GetMovieCount();

        IEnumerable<Movie> Search(string query);
    }
}