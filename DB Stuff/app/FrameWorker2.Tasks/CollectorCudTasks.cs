using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieDatabase.Domain;
using MovieDatabase.Tasks.ViewModels;
using SharpLite.Domain.DataInterfaces;

namespace MovieDatabase.Tasks
{
    public class CollectorCudTasks : BaseEntityCudTasks<Collector, EditCollectorViewModel>
    {
        public CollectorCudTasks(IRepository<Collector> itemRepository)
            : base(itemRepository)
        {
        }

        protected override void TransferFormValuesTo(Collector toUpdate, Collector fromForm)
        {
            toUpdate.FirstName = fromForm.FirstName;
            toUpdate.LastName = fromForm.LastName;
        }
    }
}
