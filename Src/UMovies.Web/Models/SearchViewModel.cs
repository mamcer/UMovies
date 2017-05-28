using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UMovies.Web.Models
{
    public class SearchViewModel
    {
        [DataType(DataType.Text)]
        [MaxLength(255)]
        public string SearchText { get; set; }

        public int MediaCount { get; set; }

        public int ResultCount { get; set; }

        public IEnumerable<MovieViewModel> Movies { get; set; }
    }
}