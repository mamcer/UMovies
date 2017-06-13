namespace UMovies.Web.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Year { get; set; }

        public string Sinopsis { get; set; }

        public string MovieFolder { get; set; }

        public string MovieFile { get; set; }

        public string ThumbnailFile { get; set; }
    }
}