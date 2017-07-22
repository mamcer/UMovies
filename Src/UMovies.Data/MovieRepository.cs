using System.Data.Entity;

namespace UMovies.Data
{
    public class MovieRepository : EntityFrameworkRepository<Movie, int>, IMovieRepository
    {
        public MovieRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}