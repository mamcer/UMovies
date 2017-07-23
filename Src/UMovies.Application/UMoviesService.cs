using System;
using System.Linq;
using CrossCutting.Core.Logging;
using UMovies.Core;
using UMovies.Data;

namespace UMovies.Application
{
    public class UMoviesService : BaseService, IUMoviesService
    {
        private readonly IMovieRepository _movieRepository;

        public UMoviesService(IMovieRepository movieRepository, ILogService logService) : base(logService)
        {
            _movieRepository = movieRepository;
        }

        public Movie GetByName(string name)
        {
            try
            {
                return _movieRepository.List(m => m.Name == name).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogException(ex);
                return null;
            }
        }
    }
}