using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UMovies.Core;

namespace UMovies.Data
{
    public class MovieRepository : EntityFrameworkRepository<Movie, int>, IMovieRepository
    {
        public MovieRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<Movie> GetAll()
        {
            return _dbContext.Set<Movie>()
                .ToList()
                .AsEnumerable();
        }

        public override Movie GetById(int id)
        {
            return _dbContext.Set<Movie>()
                .Include(m => m.MovieFiles)
                .FirstOrDefault(m => m.Id == id);
        }

        public int GetCount()
        {
            return _dbContext.Set<Movie>().Count();
        }
    }
}