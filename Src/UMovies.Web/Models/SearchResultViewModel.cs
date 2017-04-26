using System.Collections.Generic;

namespace UMovies.Web.Models
{
    public class SearchResultViewModel
    {
        public string SearchText { get; set; }

        public int ResultCount { get; set; }

        public IEnumerable<MovieViewModel> Movies { get; set; }
    }
}