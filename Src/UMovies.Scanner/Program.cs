using System;
using System.Configuration;
using System.IO;
using System.Linq;
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
            ConsoleLog($"Starting scan on {rootFolder}");
            var folderPaths = Directory.GetDirectories(rootFolder);
            ConsoleLog($"Found {folderPaths.Length} folders");
            int movieCount = 0;
            foreach (var path in folderPaths)
            {
                var name = Path.GetFileName(path);
                var fileName = Path.GetFileName(Directory.GetFiles(path, mediaFileExtensions).FirstOrDefault());

                ConsoleLog($"Scanning folder: {name}");

                if (!string.IsNullOrEmpty(fileName))
                {
                    var movie = new Movie {Name = name, FilePath = Path.Combine(name, fileName)};
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