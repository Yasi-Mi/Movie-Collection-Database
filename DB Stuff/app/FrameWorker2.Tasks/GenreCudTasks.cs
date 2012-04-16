using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieDatabase.Domain;
using MovieDatabase.Tasks.ViewModels;
using SharpLite.Domain.DataInterfaces;

namespace MovieDatabase.Tasks
{
    public class GenreCudTasks : BaseEntityCudTasks<Genre, EditGenreViewModel>
    {
        public GenreCudTasks(IRepository<Genre> itemRepository)
            : base(itemRepository)
        {
        }

        protected override void TransferFormValuesTo(Genre toUpdate, Genre fromForm)
        {
            toUpdate.Name = fromForm.Name;
        }
    }
}
