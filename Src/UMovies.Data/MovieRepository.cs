using System.Data.Entity;
using UMovies.Core;

namespace UMovies.Data
{
    public class MovieRepository : EntityFrameworkRepository<Movie, int>, IMovieRepository
    {
        public MovieRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}