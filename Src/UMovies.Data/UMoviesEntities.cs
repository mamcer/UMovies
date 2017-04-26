using System.Data.Entity;

namespace UMovies.Data
{
    public class UMoviesEntities : DbContext
    {
        public UMoviesEntities() : base("name=UMovies")
        {
        }

        public DbSet<Movie> Movies { get; set; }
    }
}