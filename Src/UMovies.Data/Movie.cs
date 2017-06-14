using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UMovies.Data
{
    [Table("Movie")]
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public int Year { get; set; }

        [Required]
        [MaxLength(255)]
        public string MovieFolder { get; set; }

        public virtual ICollection<MovieFile> MovieFiles { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(2500)]
        public string Sinopsis { get; set; }

        [MaxLength(255)]
        public string ThumbnailFileName { get; set; }
    }
}