using UMovies.Core;

namespace UMovies.Application
{
    public interface IUMoviesService
    {
        Movie GetByName(string name);
    }
}
