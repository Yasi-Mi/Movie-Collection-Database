using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieDatabase.Domain;

namespace MovieDatabase.Tasks.ViewModels
{
    public class EditMovieViewModel
    {
        public Movie Movie { get; set; }
        public IEnumerable<Director> AvailableDirectors { get; set; }
        public IEnumerable<Writer> AvailableWriters { get; set; }
        public IEnumerable<Genre> AvailableGenres { get; set; }
    }
}
