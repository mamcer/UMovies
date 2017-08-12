using CrossCutting.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using UMovies.Core;
using UMovies.Data;

namespace UMovies.Application
{
    public class MovieService : BaseService, IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IUnitOfWork unitOfWork, IMovieRepository movieRepository, ILogService logService) : base(unitOfWork, logService)
        {
            _movieRepository = movieRepository;
        }

        public Movie GetByName(string name)
        {
            try
            {
                return _movieRepository
                            .List(m => m.Name == name)
                            .FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogException(ex);
                return null;
            }
        }

        public Movie CreateMovie(Movie movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException(nameof(movie));
            }

            try
            {
                _movieRepository.Create(movie);
                _unitOfWork.SaveChanges();

                return movie;
            }
            catch (Exception ex)
            {
                LogException(ex);
                return null;
            }
        }

        public Movie UpdateMovie(Movie movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException(nameof(movie));
            }

            try
            {
                var existingMovie = _movieRepository
                                        .List(m => m.Name == movie.Name)
                                        .FirstOrDefault();
                _movieRepository.Delete(existingMovie);
                _movieRepository.Create(movie);
                _unitOfWork.SaveChanges();

                return movie;
            }
            catch (Exception ex)
            {
                LogException(ex);
                return null;
            }
        }

        public IEnumerable<Movie> GetAll()
        {
            try
            {
                var allMovies = _movieRepository.GetAll();
                return allMovies;
            }
            catch (Exception ex)
            {
                LogException(ex);
                return null;
            }
        }

        public Movie GetById(int id)
        {
            try
            {
                return _movieRepository.GetById(id);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return null;
            }
        }

        public int GetMovieCount()
        {
            try
            {
                return _movieRepository.GetCount();
            }
            catch (Exception ex)
            {
                LogException(ex);
                return 0;
            }
        }

        public IEnumerable<Movie> Search(string query)
        {
            try
            {
                return _movieRepository
                    .GetAll()
                    .Where(m => m.Name.ToLower()
                    .StartsWith(query.ToLower()));
            }
            catch (Exception ex)
            {
                LogException(ex);
                return null;
            }
        }
    }
}