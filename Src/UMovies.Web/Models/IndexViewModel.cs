using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UMovies.Web.Models
{
    public class IndexViewModel
    {
        [DataType(DataType.Text)]
        [MaxLength(255)]
        public string SearchText { get; set; }

        public int MediaCount { get; set; }
    }
}