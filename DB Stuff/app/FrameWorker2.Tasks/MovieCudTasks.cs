using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieDatabase.Domain;
using MovieDatabase.Tasks.ViewModels;
using SharpLite.Domain.DataInterfaces;

namespace MovieDatabase.Tasks
{
    public class MovieCudTasks : BaseEntityCudTasks<Movie, EditMovieViewModel>
    {
        IRepository<Director> _directorRepository;
        IRepository<Writer> _writerRepository;
        IRepository<Genre> _genreRepository;
        public MovieCudTasks(IRepository<Movie> itemRepository, IRepository<Director> directorRepository, IRepository<Writer> writerRepository, IRepository<Genre> genreRepository)
            : base(itemRepository)
        {
            _directorRepository = directorRepository;
            _writerRepository = writerRepository;
            _genreRepository = genreRepository;
        }

        protected override void TransferFormValuesTo(Movie toUpdate, Movie fromForm)
        {
            toUpdate.Title = fromForm.Title;
            toUpdate.TagLine = fromForm.TagLine;
            toUpdate.Collectors.Clear();
            foreach (Collector x in fromForm.Collectors)
                toUpdate.Collectors.Add(x);

            toUpdate.Genres.Clear();
            foreach (Genre x in fromForm.Genres)
                toUpdate.Genres.Add(x);

            toUpdate.Directors.Clear();
            foreach (Director x in fromForm.Directors)
                toUpdate.Directors.Add(x);

            toUpdate.Writers.Clear();
            foreach (Writer x in fromForm.Writers)
                toUpdate.Writers.Add(x);
        }

        public override EditMovieViewModel CreateEditViewModel()
        {
            var viewModel = base.CreateEditViewModel();
            viewModel.AvailableDirectors = _directorRepository.GetAll();
            viewModel.AvailableWriters = _writerRepository.GetAll();
            viewModel.AvailableGenres = _genreRepository.GetAll();
            return viewModel;
        }
    }
}
