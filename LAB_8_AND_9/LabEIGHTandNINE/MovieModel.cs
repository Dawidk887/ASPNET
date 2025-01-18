using System;
using System.ComponentModel.DataAnnotations;

namespace LabEIGHTandNINE
{
    public class MovieModel
    {
        [Required]
        public string Title { get; set; }

        public string? Overview { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; } // Zmienione z byte[] na DateTime

        public long? Budget { get; set; }

        public long? Revenue { get; set; }

        public long? Runtime { get; set; }

        public string? MovieStatus { get; set; }

        public string? Tagline { get; set; }

        public double? VoteAverage { get; set; }

        public long? VoteCount { get; set; }
    }
}
