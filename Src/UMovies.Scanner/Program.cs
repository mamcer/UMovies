using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UMovies.Data;

namespace UMovies.Scanner
{
    class Program
    {
        static void Main(string[] args)
        {
            var entities = new UMoviesEntities();

            var rootFolder = ConfigurationManager.AppSettings["RootFolder"];
            var mediaFileExtensions = ConfigurationManager.AppSettings["MediaFileExtensions"];
            var pictureFileExtensions = ConfigurationManager.AppSettings["PictureFileExtensions"];

            ConsoleLog($"Starting scan on {rootFolder}");
            var folderPaths = Directory.GetDirectories(rootFolder);
            ConsoleLog($"Found {folderPaths.Length} folders");
            int movieCount = 0;

            foreach (var path in folderPaths)
            {
                var folderName = Path.GetFileName(path);
                var match = Regex.Match(folderName, "(\\d+)-([\\w\\s]+)", RegexOptions.IgnoreCase);
                var year = match.Groups[1].Value;
                var name = match.Groups[2].Value;
                var movieFileName = Directory.GetFiles(path, mediaFileExtensions).Select(Path.GetFileName).ToList();
                var thumbnailFileName = Path.GetFileName(Directory.GetFiles(path, pictureFileExtensions).FirstOrDefault());
                string sinopsis = string.Empty;
                var sinopsisPath = Path.Combine(path, "sinopsis.txt");
                if (File.Exists(sinopsisPath))
                {
                    sinopsis = File.ReadAllText(sinopsisPath, Encoding.Default);
                }

                ConsoleLog($"Scanning folder: {name}");

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
                    var existingMovie = entities.Movies.FirstOrDefault(m => m.Name == name);
                    if (existingMovie == null)
                    {
                        entities.Movies.Add(movie);
                    }
                }
            }

            ConsoleLog($"Total {movieCount} movies found");
            ConsoleLog("Saving changes");
            entities.SaveChanges();
            ConsoleLog("Save completed");
        }

        private static void ConsoleLog(string msg)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy.dd.MM-hh:mm:ss") + " - " + msg);
        }
    }
}