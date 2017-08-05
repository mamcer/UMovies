using CrossCutting.Core.Logging;
using CrossCutting.MainModule.IOC;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UMovies.Application;
using UMovies.Core;

namespace UMovies.Scanner
{
    class Program
    {
        private static ILogService _logService;

        static void Main(string[] args)
        {
            try
            {
                var container = new IocUnityContainer();
                var movieService = container.Resolve<IMovieService>();
                _logService = container.Resolve<ILogService>();
                DateTime scanTime = DateTime.Now;

                var rootFolder = ConfigurationManager.AppSettings["RootFolder"];
                var mediaFileExtensions = ConfigurationManager.AppSettings["MediaFileExtensions"];
                var pictureFileExtensions = ConfigurationManager.AppSettings["PictureFileExtensions"];

                ConsoleLog($"starting scan on {rootFolder}");
                var folderPaths = Directory.GetDirectories(rootFolder);
                ConsoleLog($"{folderPaths.Length} folders found");
                int movieCount = 0;

                foreach (var path in folderPaths)
                {
                    var folderName = Path.GetFileName(path);
                    var match = Regex.Match(folderName, "(\\d+)-([\\w\\s]+)", RegexOptions.IgnoreCase);
                    var year = match.Groups[1].Value;
                    var name = match.Groups[2].Value;
                    var movieFileName = Directory.GetFiles(path, mediaFileExtensions).Select(Path.GetFileName).ToList();
                    var thumbnailFileName =
                        Path.GetFileName(Directory.GetFiles(path, pictureFileExtensions).FirstOrDefault());
                    string sinopsis = string.Empty;
                    var sinopsisPath = Path.Combine(path, "sinopsis.txt");
                    if (File.Exists(sinopsisPath))
                    {
                        sinopsis = File.ReadAllText(sinopsisPath, Encoding.Default);
                    }

                    if (movieFileName.Any())
                    {
                        var movie = new Movie
                        {
                            Year = Convert.ToInt32(year),
                            Name = name,
                            MovieFolder = folderName,
                            MovieFiles = movieFileName.Select(m => new MovieFile
                            {
                                FileName = m
                            }).ToList(),
                            ThumbnailFileName = thumbnailFileName,
                            Sinopsis = sinopsis
                        };
                        movieCount += 1;
                        var existingMovie = movieService.GetByName(name);
                        if (existingMovie == null)
                        {
                            movieService.CreateMovie(movie);
                            ConsoleLog($"scanning folder: {name} [create]");
                        }
                        else
                        {
                            movieService.UpdateMovie(movie);
                            ConsoleLog($"scanning folder: {name} [update]");
                        }
                    }
                    else
                    {
                        ConsoleLog($"no movie files found for folder: {name}");
                    }
                }

                ConsoleLog("scan finished");
                ConsoleLog($"{movieCount} movies found");
                ConsoleLog($"total time: {DateTime.Now.Subtract(scanTime).ToString("hh\\:mm\\:ss")}");
                _logService.Info($"{Environment.NewLine}=========================================================================================");
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        private static void ConsoleLog(string msg)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy.dd.MM-hh:mm:ss") + " - " + msg);
            _logService.Info(msg);
        }

        private static void LogException(Exception ex)
        {
            if (ex != null)
            {
                _logService.Error(ex.Message);
                _logService.Error(ex.StackTrace);
                if (ex.InnerException != null)
                {
                    _logService.Error(ex.InnerException.Message);
                    _logService.Error(ex.InnerException.StackTrace);
                }
            }
        }
    }
}