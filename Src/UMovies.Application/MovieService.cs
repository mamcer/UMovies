﻿using System;
using System.Linq;
using CrossCutting.Core.Logging;
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
                return _movieRepository.List(m => m.Name == name).FirstOrDefault();
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
                var existingMovie = _movieRepository.List(m => m.Name == movie.Name).FirstOrDefault();
                if (existingMovie != null)
                {
                    existingMovie.Name = movie.Name;
                    existingMovie.Sinopsis = movie.Sinopsis;
                    existingMovie.MovieFolder = movie.MovieFolder;
                    existingMovie.ThumbnailFileName = movie.ThumbnailFileName;
                    existingMovie.Year = movie.Year;
                    existingMovie.MovieFiles.Clear();
                    foreach (var movieFile in movie.MovieFiles)
                    {
                        existingMovie.MovieFiles.Add(movieFile);
                    }
                }

                _movieRepository.Update(existingMovie);
                _unitOfWork.SaveChanges();

                return movie;
            }
            catch (Exception ex)
            {
                LogException(ex);
                return null;
            }
        }
    }
}