using System.Collections.Generic;
using UMovies.Core;

namespace UMovies.Data
{
    public interface IMovieRepository : IRepository<Movie, int>
    {
        IEnumerable<Movie> GetAll();

        int GetCount();
    }
}